
using UnityEngine;

public class IcedTiltedFloor : MonoBehaviour
{
    public DeplacementJoueur deplacementJoueur;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        deplacementJoueur.cFriction = 0.02f;
    }
}
