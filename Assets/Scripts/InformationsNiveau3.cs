using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau3 : InformationsNiveau
{
    public GameObject[] cible;
    public GameObject[] porte;
    public DeplacementAstre astreMassif;
    override public void miseAJour()
    {
        setMassePlanete(1.172f * 5.972f * Mathf.Pow(10, 24));
        setRayonPlanete(1.07f * 6371000);
        setAstre(true);
    }

    public override void reinitialiser(Vector3 position)
    {
        position = new Vector3(position.x - 6, 20, 0);
        astreMassif.resetPosition(position);

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