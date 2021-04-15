﻿using UnityEngine;
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
    public Animator animation;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        angle = -collision.transform.rotation.z;

        if (collision.gameObject.CompareTag("snow_bar"))
        {
            cFriction = 0.03f;
        }
        else cFriction = 0.08f; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        auSol = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        float mouvementHorizontal = Input.GetAxis("Horizontal") * vitesseMouvement * Time.deltaTime;
        

        if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && auSol == true)
        // if (Input.GetKeyDown(KeyCode.W) && auSol == true) 
        {
            isJumping = true;
        }


        //Trouver la force de friction avec la plateforme
        mass = 7;
        gravitee = gravite.accelerationGravitationnelle;
        poidReel = mass * gravitee;
        fNormale = poidReel;
        fFriction = fNormale * cFriction;

       
       //Script pour la glace
        if (cFriction == 0.03f)
        {
            if ((Input.GetAxis("Horizontal") > 0) && ((System.Math.Abs(mouvementHorizontal) - System.Math.Abs(fFriction)) > 0))
            {
                DeplacerJoueur(mouvementHorizontal + fFriction);
                dir = 1;
            }
            else if ((Input.GetAxis("Horizontal") < 0) && ((System.Math.Abs(mouvementHorizontal) - System.Math.Abs(fFriction)) > 0))
            {
                DeplacerJoueur(mouvementHorizontal - fFriction);
                dir = -1;
            }
            else if (auSol == true && (Input.GetAxis("Horizontal") == 0))
            {
                if (angle < 0) DeplacerJoueur(-System.Math.Abs(fFriction * 3 * dir));
                else if (angle > 0) DeplacerJoueur(System.Math.Abs(fFriction * 3 * dir));
                else DeplacerJoueur(fFriction * 3 * dir);
            }
        }
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

    public bool getStatusSol()
    {
        return auSol;
    }

}
