using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InfoSteve : MonoBehaviour
{
    public float health;
    public int nombreDeVies;
    public Text boiteTexte;
    private Double time = 0;



    public void updateLifeCountDie()
    {
        if (time > 0.8d)
        {
            nombreDeVies -= 1;
            boiteTexte.text = "x " + nombreDeVies.ToString();
        }
        time = 0;
    }

    // Start is called before the first frame update
    void Awake()
    {
        nombreDeVies = 3;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
}
