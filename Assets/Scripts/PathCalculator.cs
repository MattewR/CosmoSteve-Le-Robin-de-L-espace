using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathCalculator : MonoBehaviour
{
    public bool TEESST = false;


    public GameObject objet_cible;
    public int y_cible;
    public int x_cible;
    public int y_depart;
    public int x_depart;

    //les coordonés réels de l'origine 0,0 du graph
    private float x_referentiel;
    private float y_referentiel;

    Coordinate cible;
    Coordinate depart;

    public bool genStart = false;
    public bool genFinish = false;

    public bool cible_atteinte = false;

    public bool TESST = false;

    public Dictionary<Coordinate, bool> graph; //false si la tile est vide, true si il s'y trouve un obstacle
    private Dictionary<Coordinate, int> cheminInverse; // stock les valeurs dT[s1] pour au finale tracer le chemin

    private Dictionary<int, Vector2Int> path; //liste (à indexe): suite de toutes les direction à prendre
    public int indexe_path = 1;

    
    int largeur;
    int hauteur;


    //********************************************

    public Vector2 targetPosition;
    private Vector2Int targetIntegerPosition;
    public float moveSpeed;


    //******************************************** pour wait entre deux déplacement
    float chrono = 0;

    private void Awake()
    {
        //ajuste la position cible
        targetIntegerPosition = new Vector2Int(Mathf.RoundToInt(objet_cible.transform.position.x), Mathf.RoundToInt(objet_cible.transform.position.y));
        targetPosition = new Vector2(targetIntegerPosition.x, targetIntegerPosition.y);
        objet_cible.transform.position = (Vector2)targetPosition;


        //ajuste la position ennemie
        targetIntegerPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        targetPosition = new Vector2(targetIntegerPosition.x, targetIntegerPosition.y);
        transform.position = (Vector2)targetPosition;


    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.pathGenTriggerReady += StarterPathGenerator;
    }

    // Update is called once per frame
    void Update()
    {

        var moving = (Vector2)transform.position != targetPosition;

        chrono += Time.deltaTime;

        //attente de 0.1 seconde
        if (chrono > 0.1)
        {
            chrono = 0;

            if (genFinish && !cible_atteinte && indexe_path <= path.Count && !moving)
            {

                targetPosition = new Vector2(transform.position.x, transform.position.y);

                targetPosition += path[path.Count - indexe_path];

                MoveToTargetPosition();

                indexe_path++;

                if (transform.position == objet_cible.transform.position)
                {
                    cible_atteinte = true;
                }

                //wait
                //StartCoroutine(wait(100f));
            }

        }

    }

    private void StarterPathGenerator()
    {
        GameObject scanner = GameObject.Find("GraphMaker");

        graph = scanner.GetComponent<Scanning>().Graph;

        largeur = scanner.GetComponent<Scanning>().Largeur;
        hauteur = scanner.GetComponent<Scanning>().Hauteur;

        x_referentiel = scanner.GetComponent<Scanning>().Origine_x;
        y_referentiel = scanner.GetComponent<Scanning>().Origine_y;

        //ajustement du référentiel ************************************************************************la Grid doit tout de même être décaler de 0.5 en x et en y
        x_depart = (int)(transform.position.x - x_referentiel);
        y_depart = (int)(transform.position.y - y_referentiel);

        

        y_cible = (int)(objet_cible.transform.position.y - y_referentiel);
        x_cible = (int)(objet_cible.transform.position.x - x_referentiel);

        bool cible_dans_graph = y_cible >= 0 && y_cible < hauteur && x_cible >= 0 && x_cible < largeur;

        if (cible_dans_graph && !genFinish)
        {
            TEESST = true;

            PathGeneration();

            genFinish = true;

            //targetPosition += path[path.Count - 1]; //on va prendre le premier déplacement à faire qui est au dernier indexe de path

            //transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            //targetPosition = new Vector2(targetIntegerPosition.x, targetIntegerPosition.y);
            //targetPosition += path[path.Count - 1];

            ////move to
            //transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


        }

       
    }

    private void PathGeneration()
    {
        //Dictionary<int, Vector2Int> path = new Dictionary<int, Vector2Int>(); //liste (à indexe): suite de toutes les direction à prendre


        cible = new Coordinate(x_cible, y_cible);
        depart = new Coordinate(x_depart, y_depart);


        //graph (contient les obstacles)

        int infini = (largeur * hauteur) ^ 2;

        Dictionary<int, Coordinate> Q_listeDeSommet = new Dictionary<int, Coordinate>();
        Dictionary<Coordinate, int> dT = new Dictionary<Coordinate, int>(); //Coordinate S1, int ordre_de_vérification
        Dictionary<Coordinate, int> dH = new Dictionary<Coordinate, int>(); //Coordinate S1, int fonction_de_cout (et valeur infini2 pour les obstacles)
        cheminInverse = new Dictionary<Coordinate, int>();


        //populer la liste Q de tout les sommets du graph

        for (int i = 0; i < largeur * hauteur; i++)
        {
            Q_listeDeSommet[i] = new Coordinate();
        }


        int k_indexe = 0;
        for (int l = 0; l < largeur; l++)
        {
            for (int h = 0; h < hauteur; h++)
            {
                Coordinate coordinate = new Coordinate(l, h);

                Q_listeDeSommet[k_indexe] = coordinate;

                if (graph[coordinate]) //obstacle
                {
                    dH[coordinate] = infini * 2;
                }

                dT[coordinate] = infini;

                if (!dH.ContainsKey(coordinate)) // si il n'y a pas déjà un obstacle
                {
                    int delta_x = (x_depart - x_cible) * (x_depart - x_cible);
                    int delta_y = (y_depart - y_depart) * (y_depart - y_depart);

                    int fonction_h = (int)(Mathf.Sqrt(delta_x + delta_y));
                    dH[coordinate] = fonction_h;

                }

                k_indexe++;

            }
        }

        dT[depart] = 0; //l'ordre de la case de départ est 0

        bool fin = false;


        int tour = 0;

        while (Q_listeDeSommet.Count > 0 && !fin)
        {

            tour++;

            int valeurMin = infini * 4;
            int indexeMinQ = 0; //WARNING

            Coordinate s1 = new Coordinate(); // (0,0)         

            foreach (KeyValuePair<int, Coordinate> sommet in Q_listeDeSommet)
            {
                try
                {
                    if ((dT[sommet.Value] + dH[sommet.Value]) < valeurMin)
                    {
                        valeurMin = dT[sommet.Value] + dH[sommet.Value];
                        indexeMinQ = sommet.Key;

                        s1.Clone(sommet.Value);

                    }
                }
                catch (KeyNotFoundException)
                {
                    //*********************************************************************************************************************************************************************
                    TEESST = true;
                    //la position de i à été supprimé
                }
            }


            //la cible est-elle atteinte?
            if (cible.Equals(s1))
            {
                fin = true;
            }



            //on retire s1 de Q
            bool indexeRemoved = false;
            indexeRemoved = Q_listeDeSommet.Remove(indexeMinQ);




            //on ajoute s1 au graph cheminInverse
            //contruit le graph qui servira à remonter l'ordre T en fesait ainsi le chemin inverse.
             cheminInverse[s1] = dT[s1];





            //check et assigner l'ordre T les quatres directions haut, droit, bas, gauche

            Coordinate haut = new Coordinate(s1.X, s1.Y + 1);
            Coordinate droit = new Coordinate(s1.X + 1, s1.Y);
            Coordinate bas = new Coordinate(s1.X, s1.Y - 1);
            Coordinate gauche = new Coordinate(s1.X - 1, s1.Y);

            //haut
            try
            {
                if (dT[haut] > dT[s1] + 1 && dH[haut] < infini * 2)
                {
                    dT[haut] = dT[s1] + 1;
                }
            }
            catch (KeyNotFoundException)
            {
                //s1 est en haut du graph
            }

            //droite
            try
            {
                if (dT[droit] > dT[s1] + 1 && dH[droit] < infini * 2)
                {
                    dT[droit] = dT[s1] + 1;
                }
            }
            catch (KeyNotFoundException)
            {
                //s1 est à droite du graph
            }

            //bas
            try
            {
                if (dT[bas] > dT[s1] + 1 && dH[bas] < infini * 2)
                {
                    dT[bas] = dT[s1] + 1;
                }
            }
            catch (KeyNotFoundException)
            {
                //s1 est en bas du graph
            }

            //gauche
            try
            {
                if (dT[gauche] > dT[s1] + 1 && dH[gauche] < infini * 2)
                {
                    dT[gauche] = dT[s1] + 1;
                }
            }
            catch (KeyNotFoundException)
            {
                //s1 est à gauche du graph
            }

            //fin = true;

        }


        //faire le chemin inverse
        Coordinate positionChemin = new Coordinate();
        positionChemin.Clone(cible); //on commence par la fin, par la cible

        int indexPath = 0;

        path = new Dictionary<int, Vector2Int>();

        //on remonte le chemin et on arrête à la position de depart
        while (!positionChemin.Equals(depart))
        {

            Vector2Int direction = new Vector2Int();
            int ordreMin = infini;

            //check et assigner l'ordre T les quatres directions haut, droit, bas, gauche

            Coordinate haut = new Coordinate(positionChemin.X, positionChemin.Y + 1);
            Coordinate droit = new Coordinate(positionChemin.X + 1, positionChemin.Y);
            Coordinate bas = new Coordinate(positionChemin.X, positionChemin.Y - 1);
            Coordinate gauche = new Coordinate(positionChemin.X - 1, positionChemin.Y);

            //haut
            try
            {
                if (ordreMin > cheminInverse[haut])
                {
                    ordreMin = cheminInverse[haut];
                    direction = Vector2Int.down; //chemin inverse

                }
            }
            catch (KeyNotFoundException)
            {
                //la position en haut n'a pas d'ordre
            }

            //droite
            try
            {
                if (ordreMin > cheminInverse[droit])
                {
                    ordreMin = cheminInverse[droit];
                    direction = Vector2Int.left; //chemin inverse
                    //direction = Vector2Int.right;
                }
            }
            catch (KeyNotFoundException)
            {
                //la position à droite n'a pas d'ordre
            }

            //bas
            try
            {
                if (ordreMin > cheminInverse[bas])
                {
                    ordreMin = cheminInverse[bas];
                    direction = Vector2Int.up; //chemin inverse
                    //direction = Vector2Int.down;
                }
            }
            catch (KeyNotFoundException)
            {
                //la position en bas n'a pas d'ordre
            }

            //gauche
            try
            {
                if (ordreMin > cheminInverse[gauche])
                {
                    ordreMin = cheminInverse[gauche];
                    direction = Vector2Int.right; //chemin inverse
                    //direction = Vector2Int.left;
                }
            }
            catch (KeyNotFoundException)
            {
                //la position à gauche n'a pas d'ordre
            }

            if (direction == Vector2Int.down)
            {
                //la position actu a monté
                positionChemin.Y++;
            }
            else if (direction == Vector2Int.left)
            {
                //la position actu est allé vers la droite
                positionChemin.X++;
            }
            else if (direction == Vector2Int.up)
            {
                //la position actu a dessendu
                positionChemin.Y--;
            }
            else if (direction == Vector2Int.right)
            {
                //la position actu est allé vers la gauche
                positionChemin.X--;
            }

            path[indexPath] = direction;
            //
            indexPath++;

        }

    }

    private bool graphGetBoolValue(Coordinate coord)
    {
        bool valueNotFound=true;
        bool value = false;

        foreach(KeyValuePair<Coordinate, bool> noeud in graph)
        {
            if(noeud.Key.Equals(coord))
            {
                valueNotFound = false;
                value = noeud.Value;
            }
        }

        if(valueNotFound)
        {
            Coordinate zeroZero = new Coordinate(0, 0);

            bool crasher = graph[zeroZero];
        }

        return value;

    }

    private void MoveToTargetPosition()
    {
        Vector2 current = new Vector2(transform.position.x, transform.position.y);
        //Vector2 target = new Vector2(tar) 


        //transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        //transform.position = Vector2.MoveTowards(current, targetPosition, moveSpeed * Time.deltaTime);
        //transform.position = Vector2.MoveTowards()
        transform.position = (Vector2)targetPosition;

    }

    IEnumerator wait(float seconde)
    {
        yield return new WaitForSeconds(seconde);
    }

    private void afficherDictionary(Dictionary<Coordinate,int> graph)
    {
        foreach (KeyValuePair<Coordinate, int> noeud in graph)
        {
            Console.WriteLine(noeud);
        }
    }

}
