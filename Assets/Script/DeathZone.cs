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
        niveau.miseAJour();
        astre = niveau.getAstre();
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


        if (collision.CompareTag("Player"))
        {
            infoVie.updateLifeCountDie();
            collision.transform.position = playerSpawn.position;
           // sauvegarde.ecrire();
            niveau.reinitialiser(playerSpawn.position);
            GameOverManager.instance.OnPlayerDeath();
        }
    }
}
