using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau1 : InformationsNiveau
{
    override public void miseAJour()
    {
        setMassePlanete(5.972f * Mathf.Pow(10, 24));
        setRayonPlanete(6371000);
    }
}
