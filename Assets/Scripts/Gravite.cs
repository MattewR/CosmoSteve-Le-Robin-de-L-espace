using UnityEngine;
using System;

public class Gravite : MonoBehaviour
{
    //Variables 
    public Rigidbody2D rb;
    public Vector2 forceGravitationnelle;
    public InformationsNiveau informationsNiveau;

    public float accelerationGravitationnelle;
    public float massePlanete;
    private float constanteG = 6.6742f * Mathf.Pow(10, -11);
    private float rayonPlanete;
    public float distanceJoueur;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        informationsNiveau.miseAJour();
        massePlanete = informationsNiveau.getMassePlanete();
        rayonPlanete = informationsNiveau.getRayonPlanete();
        distanceJoueur = rb.position.y;
        distance = distanceJoueur + rayonPlanete;
        accelerationGravitationnelle = (constanteG * massePlanete) / Mathf.Pow(distance, 2);
        forceGravitationnelle = new Vector2(0, accelerationGravitationnelle * -1)*rb.mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attraction();
    }

    void Attraction()
    {
        rb.AddForce(forceGravitationnelle);
    }

    float getAccelerationGravitationnelle()
    {
        return accelerationGravitationnelle;
    }
}
