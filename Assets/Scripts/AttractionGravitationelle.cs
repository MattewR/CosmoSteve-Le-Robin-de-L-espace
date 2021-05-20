using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cette classe d'appliquer une force gravitationnelle sur le personnage
public class AttractionGravitationelle : MonoBehaviour
{
    //Variables
    public Transform astre;
    private float constanteGravitationnelle = 6.6742f * Mathf.Pow(10, -11);
    public float distance2Rigidbody;
    public float distanceX;
    public float distanceY;
    public float attraction;
    public Rigidbody2D Steve;
    public float mG;
    public float angle;
    public Vector2 forceAttraction;
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
        angle = Mathf.Atan2(distanceY, distanceX);
        distanceX = Mathf.Cos(angle);
        distanceY = Mathf.Sin(angle);
        forceAttraction = new Vector2(distanceX, distanceY) * attraction;
        Steve.AddForce(forceAttraction);
    }

    private float CalculDistance2()
    {
        distanceX = astre.position.x - this.transform.position.x;
        distanceY = astre.position.y - this.transform.position.y;

        return (Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceY, 2));
    }
}
