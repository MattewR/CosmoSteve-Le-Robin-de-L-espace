using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Cette classe permet à une cible de désactiver une porte lorsque la cible est touchée
public class Activateur : MonoBehaviour
{
    //Variable
    public GameObject porte;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.SetActive(false);
        porte.SetActive(false);
    }
}
