using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightOrLeft : MonoBehaviour

{

    private float angle;
    private SuivreSourisArc scriptSuivreSouris;
    private DeplacementJoueur auSolScr;
    public Animator animationSteve;
    // Start is called before the first frame update
    void Start()
    {
        
        scriptSuivreSouris = gameObject.GetComponent<SuivreSourisArc>();
        auSolScr = gameObject.GetComponent<DeplacementJoueur>();


    }

    // Update is called once per frame
    void Update()
    {
        if (auSolScr.getStatusSol())
        {
            animationSteve.SetBool("Au sol", true);
        }
        else
        {
            animationSteve.SetBool("Au sol", false);
        }


        angle = scriptSuivreSouris.getAngle();
        Debug.Log(angle);
        if (angle < 50 && angle > -25)
        {
            if (Input.GetKey(KeyCode.D))
            {
                animationSteve.SetBool("D pressed", true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                animationSteve.SetBool("A pressed", true);
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                animationSteve.SetBool("D pressed", false);
                animationSteve.SetBool("A pressed", false);
            }
        }
        else if (angle > 130 || (angle < 0 && angle < -155))
        {
            if (Input.GetKey(KeyCode.A))
            {
                animationSteve.SetBool("D pressed", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                animationSteve.SetBool("A pressed", true);
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                animationSteve.SetBool("D pressed", false);
                animationSteve.SetBool("A pressed", false);
            }
        }


    }
}
