using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau : MonoBehaviour
{
    public float massePlanete;
    public float rayonPlanete;
    private bool astre = false;

    public void setMassePlanete(float _massePlanete)
    {
        massePlanete = _massePlanete;
    }

    public void setRayonPlanete(float _rayonPlanete)
    {
        rayonPlanete = _rayonPlanete;
    }

    public void setAstre(bool _astre)
    {
        astre = _astre;
    }

    public float getMassePlanete()
    {
        return massePlanete;
    }

    public float getRayonPlanete()
    {
        return rayonPlanete;
    }

    public bool getAstre()
    {
        return astre;
    }

    virtual public void miseAJour()
    {

    }

    virtual public void reinitialiser(Vector3 position)
    {

    }
}
