using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau4 : InformationsNiveau
{
    override public void miseAJour()
    {
        setMassePlanete(1.4f * 5.972f * Mathf.Pow(10, 24));
        setRayonPlanete(1.17f * 6371000);
    }

}