using UnityEngine;

public class normalFloor : MonoBehaviour
{
    public DeplacementJoueur deplacementJoueur;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        deplacementJoueur.cFriction = 0.08f;
    }
}
