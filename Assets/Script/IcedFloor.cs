
using UnityEngine;

public class IcedFloor : MonoBehaviour
{
    public DeplacementJoueur deplacementJoueur;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        deplacementJoueur.cFriction = 5;
    }
}
