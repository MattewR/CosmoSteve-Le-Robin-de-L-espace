using UnityEngine;
public class DeplacementJoueur : MonoBehaviour
{

    public Gravite gravite;

    public float vitesseMouvement;
    public float forceDeSaut;
    private bool isJumping = false;
    private bool auSol = true;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public Rigidbody2D rb;
    private Vector3 velocite = Vector3.zero;

    //Paramètres de la friction
    public float cFriction;
    private float fNormale;
    private float mass;
    private float poidReel;
    public float angle;
    private float gravitee;
    private float fFriction;
    private int dir;
    public Transform anglePlateforme;


    //Fonction qui détecte l'état de la plateforme en dessous du joueur
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //L'angle de la plateforme
        angle = -collision.transform.rotation.z;

        //Le coefficient de la plateforme
        if (collision.gameObject.CompareTag("snow_bar"))
        {
            cFriction = 0.03f;
        }else if (collision.gameObject.CompareTag("spider_web"))
        {
            cFriction = 0.67f;
        }
        else cFriction = 0.08f; 
    }


    public bool getStatusSol()
    {
        return auSol;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        auSol = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        float mouvementHorizontal = Input.GetAxis("Horizontal") * vitesseMouvement * Time.deltaTime + (poidReel * 0.08f);
         if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && auSol == true)
        // if (Input.GetKeyDown(KeyCode.W) && auSol == true) 
        {
            isJumping = true;
        }
        

        //Trouver la force de friction avec la plateforme
        mass = 7;
        gravitee = gravite.accelerationGravitationnelle;
        //Calcul de la force normale & la friction
        poidReel = mass * gravitee;
        fNormale = poidReel;
        fFriction = fNormale * cFriction;
        
        ////Script pour toile d'araignée
        if (cFriction == 0.67f)
        {
            if (auSol == true)
            {
                //Friction trop forte pour bouger au sol
                DeplacerJoueur(0);
            }
            else
            {
                //Il est ralenti en sautant
                if ((Input.GetAxis("Horizontal") > 0))
                {
                    DeplacerJoueur((fFriction / 20));
                }
                if ((Input.GetAxis("Horizontal") < 0))
                {
                    DeplacerJoueur(-(fFriction / 20));
                }
                
                //S'immobilise quand l'utilisateur lache la touche
                if ((Input.GetAxis("Horizontal") == 0)) DeplacerJoueur(0);
            }

        }

        //Script pour la glace
        else if (cFriction == 0.03)
        {
            //Quand Steve bouge a droite
            if ((Input.GetAxis("Horizontal") > 0) && ((System.Math.Abs(mouvementHorizontal) - System.Math.Abs(fFriction)) > 0))
            {
                Debug.Log("Droite");
                DeplacerJoueur(mouvementHorizontal - fFriction);
                dir = 1;
            }
            //Quand Steve bouge a gauche
            else if ((Input.GetAxis("Horizontal") < 0) && ((System.Math.Abs(mouvementHorizontal) - System.Math.Abs(fFriction)) > 0))
            {
                Debug.Log("Gauche");
                DeplacerJoueur(mouvementHorizontal + fFriction);
                dir = -1;
            }
            //Quand Steve est immobile
            else if (auSol == true && (Input.GetAxis("Horizontal") == 0))
            {
                if (angle < 0) DeplacerJoueur(-System.Math.Abs(fFriction * 3 * dir));
                else if (angle > 0) DeplacerJoueur(System.Math.Abs(fFriction * 3 * dir));
                else DeplacerJoueur(fFriction * 3 * dir);
            }
        }
        //si on est sur les plateformes normals on appelle la fonction sans variations
        else DeplacerJoueur(mouvementHorizontal - fFriction);



    }

    void DeplacerJoueur(float _mouvementHorizontal)
    {
        Vector3 velociteCible = new Vector2(_mouvementHorizontal, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velociteCible, ref velocite, .05f);

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, forceDeSaut));
            isJumping = false;
        }
    }

}
