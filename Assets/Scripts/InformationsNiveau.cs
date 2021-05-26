using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationsNiveau : MonoBehaviour
{
    //Variables
    public float massePlanete;
    public float rayonPlanete;


    //Méthode virtuelle servant à attitrer les valeurs des planètes
    virtual public void miseAJour()
    {

    }

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

    //Méthode virtuelle servant à réinitialiser les composantes variables d'un niveau
    virtual public void reinitialiser(Vector3 position)
    {

    }
}
