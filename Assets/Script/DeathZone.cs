using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class DeathZone : MonoBehaviour
{
    public Transform playerSpawn;
    public InfoSteve infoVie;


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            infoVie.updateLifeCountDie();
            collision.transform.position = playerSpawn.position;
        }
    }
}
