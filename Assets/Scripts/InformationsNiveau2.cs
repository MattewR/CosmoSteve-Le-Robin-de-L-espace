using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau2 : InformationsNiveau
{
    override public void miseAJour()
    {
        setMassePlanete(7.342f * Mathf.Pow(10, 22));
        setRayonPlanete(1737400);
    }
}
