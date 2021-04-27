using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activateur : MonoBehaviour
{
    public GameObject porte;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
        porte.SetActive(false);
    }
}
