
using UnityEngine;

public class IcedFloor : MonoBehaviour
{
    public DeplacementJoueur deplacementJoueur;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        deplacementJoueur.cFriction = 0.03f;
    }
}
