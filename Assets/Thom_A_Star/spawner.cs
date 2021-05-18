using System.Collections;
using System.Collections.Generic;
using System.Runtime;

using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject ennemiPrefab;

    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;
    public GameObject position5;
    public GameObject position6;

    public bool graphReady = false;

    private float chrono = 0;
    public float vitesseSpawn = 2.0f;
    public float tempsDemarage = 1.0f;

    void Start()
    {
        GameEvents.current.pathGenTriggerReady += StartSpawn;
        chrono = vitesseSpawn - tempsDemarage;
    }

    void Update()
    {
        if(graphReady)
        {

            chrono += Time.deltaTime;

            //attente de X secondes
            if (chrono > vitesseSpawn)
            {
                chrono = 0;

                spawnEnnemi();

            }

        }
    }

    private void spawnEnnemi()
    {
        GameObject ennemi = Instantiate(ennemiPrefab) as GameObject;

        System.Random rndm = new System.Random();

        int position = rndm.Next(1, 7);

        switch (position)
        {
            case 1:
                ennemi.transform.position = position1.transform.position;
                break;

            case 2:
                ennemi.transform.position = position2.transform.position;
                break;

            case 3:
                ennemi.transform.position = position3.transform.position;
                break;

            case 4:
                ennemi.transform.position = position4.transform.position;
                break;

            case 5:
                ennemi.transform.position = position5.transform.position;
                break;

            case 6:
                ennemi.transform.position = position6.transform.position;
                break;
        }

    }

    private void StartSpawn()
    {
        graphReady = true;
    }
}
