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
    private float constanteG = 6.6738f * Mathf.Pow(10, -11);
    private float rayonPlanete;


    // Start calcule la grandeur de la force ravitationnelle à appliquer à l'objet
    void Start()
    {
        InitialisationDesValeurs();
        accelerationGravitationnelle = (constanteG * massePlanete) / Mathf.Pow(rayonPlanete, 2);
        forceGravitationnelle = new Vector2(0, accelerationGravitationnelle * -1)*rb.mass;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Attraction();
    }


    //Cette méthode applique une force gravitationnelle vers la planète
    void Attraction()
    {
        rb.AddForce(forceGravitationnelle);
    }

    //Cette méthode donne la valeur de l'accélération gravitationnelle
    public float getAccelerationGravitationnelle()
    {
        return accelerationGravitationnelle;
    }

    //Cette méthode initialise les informations propre à la planète
    private void InitialisationDesValeurs()
    {
        informationsNiveau.miseAJour();
        massePlanete = informationsNiveau.getMassePlanete();
        rayonPlanete = informationsNiveau.getRayonPlanete();
    }
}
