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
    private float angle;
    private float gravitee;
    private float fFriction;
    private Transform anglePlateforme;

    public void Awake()
    {
        anglePlateforme = GameObject.FindGameObjectWithTag("snow_bar_tilted").transform;
        angle = anglePlateforme.rotation.z;
        angle = -angle;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        auSol = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        float mouvementHorizontal = Input.GetAxis("Horizontal") * vitesseMouvement * Time.deltaTime;

        if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && auSol==true)
       // if (Input.GetKeyDown(KeyCode.W) && auSol == true) 
        {
            isJumping = true;
        }


        //Trouver la force de friction avec la plateforme
        mass = 7;       
        gravitee = gravite.champDeGravite;
        poidReel = mass * gravitee;
        fNormale = poidReel;
        fFriction = fNormale * cFriction;
        if (cFriction != 2)
        {

            if ((Input.GetAxis("Horizontal") > 0.1)) DeplacerJoueur(mouvementHorizontal - fFriction); ;
            if ((Input.GetAxis("Horizontal") < -0.1)) DeplacerJoueur(mouvementHorizontal + fFriction); ;
            if (auSol == false && (Input.GetAxis("Horizontal") > 0)) DeplacerJoueur(mouvementHorizontal - (fFriction)); ;
            if (auSol == false && (Input.GetAxis("Horizontal") < 0)) DeplacerJoueur(mouvementHorizontal + (fFriction)); ;
            if ((Input.GetAxis("Horizontal") == 0)) DeplacerJoueur(mouvementHorizontal); 
            Debug.Log("M" + mouvementHorizontal);
            Debug.Log("F" + fFriction);
        }
       

    }

    void DeplacerJoueur(float _mouvementHorizontal)
    {
        Vector3 velociteCible = new Vector2(_mouvementHorizontal, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velociteCible, ref velocite, .05f);

        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, forceDeSaut));
            isJumping = false;
        }
    }

}
