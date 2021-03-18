using UnityEngine;

public class DeplacementJoueur : MonoBehaviour
{
    public float vitesseMouvement;
    public float forceDeSaut;

    public float cFriction;
    public float fNormale;
    public float mass;

    private bool isJumping = false;
    private bool auSol = true;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rb;
    private Vector3 velocite = Vector3.zero;
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

   
        //fNormale = rb.GetMass
        if ((Input.GetAxis("Horizontal") > 0)) fNormale = 5;
        if ((Input.GetAxis("Horizontal") < 0)) fNormale = -5;
        if (auSol == false && fNormale > 0) fNormale = 2;
        if (auSol == false && fNormale < 0) fNormale = -2;
        if (cFriction == 5) DeplacerJoueur(mouvementHorizontal + fNormale);
        else DeplacerJoueur(mouvementHorizontal);

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
