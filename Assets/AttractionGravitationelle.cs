using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionGravitationelle : MonoBehaviour
{
    //Variables
    public Rigidbody2D astre;
    private float constanteGravitationnelle = 6.6742f * Mathf.Pow(10, -11);
    public float distanceRigidbody;
    public Rigidbody2D Steve;
   // public Transform transform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distanceRigidbody = Mathf.Sqrt(Mathf.Pow(this.transform.position.x - Steve.position.x, 2) + Mathf.Pow(this.transform.position.y - Steve.position.y, 2));
    }
}
