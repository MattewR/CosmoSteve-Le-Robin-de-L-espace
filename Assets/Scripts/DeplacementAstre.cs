using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementAstre : MonoBehaviour
{
    new public Transform transform;
    public float vitesseDeplacement;
    public Rigidbody2D rb;
    private Vector3 velocite = Vector3.zero;
    private Vector3 debutDeParcours = new Vector3(-40f, 20f, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x >= 178f)
        {
            transform.position = debutDeParcours;
        }
        Vector3 velociteCible = new Vector2(vitesseDeplacement * Time.deltaTime, 0);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velociteCible, ref velocite, .05f);
    }

    public void resetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
