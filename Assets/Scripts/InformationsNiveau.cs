using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau : MonoBehaviour
{
    public float massePlanete;
    public float rayonPlanete;

    public void setMassePlanete(float _massePlanete)
    {
        massePlanete = _massePlanete;
    }

    public void setRayonPlanete(float _rayonPlanete)
    {
        rayonPlanete = _rayonPlanete;
    }

    public float getMassePlanete()
    {
        return massePlanete;
    }

    public float getRayonPlanete()
    {
        return rayonPlanete;
    }

    virtual public void miseAJour()
    {

    }

    virtual public void reinitialiser(Vector3 position)
    {

    }
}
