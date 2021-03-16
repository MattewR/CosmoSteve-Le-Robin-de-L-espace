using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class FlecheAttache : MonoBehaviour
{
    public Func<float, float[], Vector3>[] fonctionUtil;
    public float[] variablesImport;
    private float frames = 0;
 
    // Start is called before the first frame update
    void Awake()
    {

    }

    void updateAngle(float t, float[] variables, Func<float,float[],Vector3> derive)
    {
        Vector3 rot = derive(t, variables);
        transform.eulerAngles = rot;
    }

    void updatePosition(float t, float[] variables, Func<float, float[], Vector3> fonction)
    {
        Vector3 nouvellePos = fonction(t, variables);
        transform.position = nouvellePos;

    }
    // Update is called once per frame
    void Update()
    {

        frames += Time.deltaTime;
        try
        {
            updatePosition(frames, variablesImport, fonctionUtil[0]);
            updateAngle(frames, variablesImport, fonctionUtil[1]);


        }

        catch (NullReferenceException)
        {
            Debug.Log("Waiting for function assignement");

        }
        

        
    }
}
