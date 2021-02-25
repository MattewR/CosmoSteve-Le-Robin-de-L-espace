using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float vitesseMouvement;

    public Rigidbody2D rb;
    private Vector3 velocite = Vector3.zero;
    // Update is called once per frame
    void FixedUpdate()
    {
        float mouvementHorizontal = Input.GetAxis("Horizontal") * vitesseMouvement * Time.deltaTime;

        DeplacerJoueur(mouvementHorizontal);
    }

    void DeplacerJoueur(float _mouvementHorizontal)
    {
        Vector3 velociteCible = new Vector2(_mouvementHorizontal, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velociteCible, ref velocite, .05f);
    }

}
