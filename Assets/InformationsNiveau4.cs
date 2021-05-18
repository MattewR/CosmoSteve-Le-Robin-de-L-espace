using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau4 : InformationsNiveau
{
    override public void miseAJour()
    {
        setMassePlanete(5f * 5.972f * Mathf.Pow(10, 24));
        setRayonPlanete(1.5f * 6371000);
    }

}