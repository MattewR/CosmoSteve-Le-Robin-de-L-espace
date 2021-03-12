using UnityEngine;
using System;

public class Gravite : MonoBehaviour
{
    //Variables 
    public Rigidbody2D rb;
    public Vector2 accelerationGravitationnelle;

    public Transform verificationSolGauche;
    public Transform verificationSolDroit;

    public bool auSol;

    public float champDeGravite;
    public float massePlanete = 5.972f * Mathf.Pow(10, 24);
    private float constanteG = 6.6742f * Mathf.Pow(10, -11);
    private float rayonPlanete = 6371000;
    public float distanceJoueur;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        distanceJoueur = rb.position.y;
        distance = distanceJoueur + rayonPlanete;
        champDeGravite = (constanteG * massePlanete) / Mathf.Pow(distance, 2);
        accelerationGravitationnelle = new Vector2(0, champDeGravite * -1)*rb.mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //distanceJoueur = rb.position.y;
        //distance = distanceJoueur + rayonPlanete;
        auSol = Physics2D.OverlapArea(verificationSolGauche.position, verificationSolDroit.position);

        if (auSol == false)
        {
            Attraction();
        }
    }

    void Attraction()
    {
        rb.AddForce(accelerationGravitationnelle);
    }
}
