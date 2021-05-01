using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    public InformationsNiveau niveau;
    public bool astre;
    public DeplacementAstre astreMassif;
    public static DeathZone instance;
    private Sauvegarde sauvegarde;
    public InfoSteve infoVie;


    private void Start()
    {
        try
        {
            niveau.miseAJour();
            astre = niveau.getAstre();
        }
        catch
        {
            Debug.Log("A fixe");
        }
    }

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;

        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Deathzone dans la scène");
            return;
        }
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            infoVie.updateLifeCountDie();
            collision.transform.position = playerSpawn.position;
            // sauvegarde.ecrire();
            try
            {
                niveau.reinitialiser(playerSpawn.position);
            }
            catch
            {
                Debug.Log("fix comme en haut");
            }
            //GameOverManager.instance.OnPlayerDeath();
        }
    }
}
