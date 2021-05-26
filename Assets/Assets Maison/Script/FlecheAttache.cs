using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity;
using UnityEngine;
using System;

public class FlecheAttache : MonoBehaviour
{
    //Fonctions à utiliser
    public Func<float, float[], Vector3>[] fonctionUtil;
    //Variables pour la fonctions
    public float[] variablesImport;
    //Nombres de frames qui se sont écoulées
    private float frames = 0;


    // Start is called before the first frame update
    void Awake()
    {

    }

    //Met à jour l'angle selon la fonction passé en variable
    //Prend le temps et les variables en paramètre
    void updateAngle(float t, float[] variables, Func<float, float[], Vector3> derive)
    {
        Vector3 rot = derive(t, variables);
        transform.eulerAngles = rot;
    }
    //Met à jour la position selon la fonction passé en variable
    //Prend le temps et les variables en paramètre
    void updatePosition(float t, float[] variables, Func<float, float[], Vector3> fonction)
    {
        Vector3 nouvellePos = fonction(t, variables);
        transform.position = nouvellePos;

    }


    //Une fois par frame
    void Update()
    {
        //Si le jeu est sur pause détruire la flèche
        if (Time.timeScale != 1)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
        frames += Time.deltaTime;
        try
        {
            //Essayons de mettre à jour la position de l'objet 
            updatePosition(frames, variablesImport, fonctionUtil[0]);
            //Essayons de mettre à jour la rotation de l'objet
            updateAngle(frames, variablesImport, fonctionUtil[1]);

        }

        catch (NullReferenceException)
        {
            Debug.LogWarning("Erreur lors de la mise à jour de la position");
        }

        

        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("TYOREIGRSIOYEFH:SIUAPHI");
        if (!col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}


