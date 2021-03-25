using UnityEngine;

public class normalFloor : MonoBehaviour
{
    public DeplacementJoueur deplacementJoueur;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        deplacementJoueur.cFriction = 0.08f;
    }
}
