using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cette classe d'appliquer une force gravitationnelle sur le personnage
public class AttractionGravitationelle : MonoBehaviour
{
    //Variables
    public Transform astre;
    private float constanteGravitationnelle = 6.6738f * Mathf.Pow(10, -11);
    public float distance2Rigidbody;
    public float distanceX;
    public float distanceY;
    public float attraction;
    public Rigidbody2D Steve;
    public float mG;
    public float angle;
    public Vector2 forceAttraction;
    private Vector2 vecteurUnitaire;
    private float masseAstre = 20000000000000;

    //Les valeurs constantes dans la formule gravitationnelle sont calculées au départ
    void Start()
    {
        mG = masseAstre * constanteGravitationnelle * Steve.mass;
    }

    //Cette méthode permet d'appliquer la force gravitationnelle au personnage
    void FixedUpdate()
    {
        distance2Rigidbody = CalculDistance2();
        attraction = mG / distance2Rigidbody;

        vecteurUnitaire = CalculVecteurUnitaire();
        forceAttraction = vecteurUnitaire * attraction;
        Steve.AddForce(forceAttraction);
    }

    //Cette méthode calcule la distance au carré qui sépare le centre de l'astre et le centre de l'objet
    private float CalculDistance2()
    {
        distanceX = astre.position.x - this.transform.position.x;
        distanceY = astre.position.y - this.transform.position.y;

        return (Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceY, 2));
    }

    //Cette méthode calcule le vecteur unitaire orienté vers le centre de l'astre 
    private Vector2 CalculVecteurUnitaire()
    {
        angle = Mathf.Atan2(distanceY, distanceX);
        distanceX = Mathf.Cos(angle);
        distanceY = Mathf.Sin(angle);

        return new Vector2(distanceX, distanceY);
    }
}
