using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionGravitationelle : MonoBehaviour
{
    //Variables
    public Rigidbody2D astre;
    private float constanteGravitationnelle = 6.6742f * Mathf.Pow(10, -11);
    public float distance2Rigidbody;
    public float distanceX;
    public float distanceY;
    public float attraction;
    public Rigidbody2D Steve;
    public float mG;
    public float angle;
    public Vector2 forceAttraction;

    // Start is called before the first frame update
    void Start()
    {
        mG = 20000000000000 * constanteGravitationnelle * Steve.mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceX = this.transform.position.x - Steve.position.x;
        distanceY = this.transform.position.y - Steve.position.y;
        distance2Rigidbody = Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceY, 2);
        attraction = mG / distance2Rigidbody;
        angle = Mathf.Atan2(distanceY, distanceX);
        distanceX = Mathf.Cos(angle);
        distanceY = Mathf.Sin(angle);
        forceAttraction = new Vector2(distanceX, distanceY) * attraction;
        Steve.AddForce(forceAttraction);
    }
}
