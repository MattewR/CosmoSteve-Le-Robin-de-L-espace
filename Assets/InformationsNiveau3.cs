using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau3 : InformationsNiveau
{
    override public void miseAJour()
    {
        setMassePlanete(1.172f * 5.972f * Mathf.Pow(10, 24));
        setRayonPlanete(1.07f * 6371000);
    }
}