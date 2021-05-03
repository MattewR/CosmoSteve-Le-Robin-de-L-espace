using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau1 : InformationsNiveau
{
    public GameObject[] cible;
    public GameObject[] porte;
    override public void miseAJour()
    {
        setMassePlanete(5.972f * Mathf.Pow(10, 24));
        setRayonPlanete(6371000);
        setAstre(false);
    }

    public override void reinitialiser(Vector3 position)
    {
        for (int i = 0; i < cible.Length; i++)
        {
            cible[i].SetActive(true);
        }

        for (int i = 0; i < porte.Length; i++)
        {
            porte[i].SetActive(true);
        }
    }
}
