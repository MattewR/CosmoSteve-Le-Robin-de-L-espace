using System.Collections.Generic;
using UnityEngine;



public class Scanning : MonoBehaviour
{
    public bool PROBLEME = false;
    public bool fin = false;
    private bool eventSended = false;

    public int xColl;
    public int yColl; 

    public int largeur;
    public int hauteur;

    public int Largeur
    {
        get
        {
            return largeur;
        }
    }

    public int Hauteur
    {
        get
        {
            return hauteur;
        }
    }

    private Dictionary<Coordinate, bool> graph;


    public Dictionary<Coordinate, bool> Graph 
    { 
        get 
        { 
            if(fin)
            {
                return graph;
            }
            else
            {
                Dictionary<Coordinate, bool> vide = new Dictionary<Coordinate, bool>();

                return vide;
            }
        } 
    }//false si la tile est vide, true si il s'y trouve un obstacle

    public float Origine_y { get => origine_y; set => origine_y = value; }
    public float Origine_x { get => origine_x; set => origine_x = value; }

    private float origine_y;
    private float origine_x;

    public bool dernierMouvEtaitVerti = false;

    public float moveSpeed;


    public bool isCollide;
    public Transform colliderCheckUpLeft;
    public Transform colliderCheckDownRight;

    public int nbr_case_scaned = 0;
    public int nbr_case_scannedMAX;

    //********************************************

    public Vector2 targetPosition;
    private Vector2Int targetIntegerPosition;

    private void Awake()
    {

        //ajuste la position du scanner (enligné sur les tiles) **********************************pour que les positions discrètes soit au centre des tiles, il faut ajuster la grid de .5 vers la gauche et .5 vers la droite
        targetIntegerPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        targetPosition = new Vector2(targetIntegerPosition.x, targetIntegerPosition.y);
        transform.position = (Vector2)targetPosition;

        graph = new Dictionary<Coordinate, bool>();

        origine_x = transform.position.x;
        origine_y = transform.position.y;


        //création du graph
        for(int l = 0; l < largeur; l++)
        {
            for (int h = 0; h < hauteur; h++)
            {
                Coordinate coordinate = new Coordinate(l, h);

                graph.Add(coordinate, false);
                nbr_case_scannedMAX++;

            }
        }

    }

    private void MoveToTargetPosition()
    {
        //transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        transform.position = (Vector2)targetPosition;

        //isCollide = Physics2D.OverlapArea(colliderCheckUpLeft.position, colliderCheckDownRight.position);

    }

    private void SetTargetPosition() //sert aussi à scanner la map
    {


        float p_actu_x = transform.position.x - origine_x;
        float p_actu_y = transform.position.y - origine_y;

        int coordX = (int)p_actu_x;
        int coordY = (int)p_actu_y;

        isCollide = Physics2D.OverlapArea(colliderCheckUpLeft.position, colliderCheckDownRight.position);

        //contruction du graph /////////////////////////////////////////////////////////////////////////////////////////////////
        if (Physics2D.OverlapArea(colliderCheckUpLeft.position, colliderCheckDownRight.position))
        {

            
            xColl = coordX;
            yColl = coordY;
            
            Coordinate coordinate_actu = new Coordinate(coordX, coordY);

            //graph[coordinate_actu] = true;

            try
            {
                graph[coordinate_actu] = true;
            }
            catch (KeyNotFoundException)
            {
                PROBLEME = true;
            }
        }

        nbr_case_scaned++;


        
        if (nbr_case_scaned == 1)//check position est à origine
        {
            targetPosition += Vector2Int.up;
            dernierMouvEtaitVerti = true;
        }
        else//si position n'est pas origine
        {
            
            if((coordY == 0 || coordY == (hauteur-1)) && dernierMouvEtaitVerti) //doit-il faire déplace ver la gauche (en bas du tab ou en haut du tab), si oui, le dernier mouv devient horizontal
            {
                targetPosition += Vector2Int.right;

                dernierMouvEtaitVerti = false;
            }
            else//soit déplace haut ou bas (x impair ou pair)
            {
               
                if(float_is_even(p_actu_x))
                {
                    targetPosition += Vector2Int.up;
                }
                else
                {
                    targetPosition += Vector2Int.down;
                }

                dernierMouvEtaitVerti = true;
            }
        }
    }


    private void Update()
    {

        var moving = (Vector2)transform.position != targetPosition;

        if(moving)
        {
            MoveToTargetPosition();
        }
        else if ((nbr_case_scaned+2) <= nbr_case_scannedMAX)
        {
            SetTargetPosition();
        }
        else if (!fin) //il faut maintenant scanner la dernière tile
        {
            float p_actu_x = transform.position.x - origine_x;
            float p_actu_y = transform.position.y - origine_y;

            int coordX = (int)p_actu_x;
            int coordY = (int)p_actu_y;

            //contruction du graph (à la dernière tile)
            if (isCollide)
            {
                

                Coordinate coordinate_actu = new Coordinate(coordX, coordY);

                graph[coordinate_actu] = true;

                try
                {
                    graph[coordinate_actu] = true;
                }
                catch (KeyNotFoundException)
                {
                    PROBLEME = true;
                }
            }

            nbr_case_scaned++;

            //doit arrêter le script
            //this.StopAllCoroutines;

            fin = true;
        }

        if(fin && !eventSended)
        {
            GameEvents.current.OnPathGenTrigger();
        }

    }

    private bool float_is_even(float f_nbr)
    {
        int nbr = (int)f_nbr;

        if(nbr % 2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

}
