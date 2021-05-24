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
    private float vitesseSpawn = 9.0f;
    public float tempsDemarage = 1.0f;

    public int nbr_ennemi = 0;
    public bool fin = false;

    public ChangementDeScene sceneFinale;

    void Start()
    {
        GameEvents.current.pathGenTriggerReady += StartSpawn;
        chrono = vitesseSpawn - tempsDemarage;
    }

    void Update()
    {
        if(graphReady && !fin)
        {

            chrono += Time.deltaTime;

            //attente de X secondes
            if (chrono > vitesseSpawn)
            {
                chrono = 0;

                spawnEnnemi();

                nbr_ennemi++;

                if(nbr_ennemi >= 20)
                {
                    fin = true;
                }
                else if(nbr_ennemi >= 15)
                {
                    vitesseSpawn = 4.0f;
                }
                else if (nbr_ennemi >= 2 )
                {
                    vitesseSpawn = 7.0f;
                }

            }

        }
        if (fin == true)
        {
            //TIMER DE 30 SECONDES

            Time.timeScale = 0;
            sceneFinale.Changer_niveau("MenuFin");
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
