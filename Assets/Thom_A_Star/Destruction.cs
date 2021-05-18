using UnityEngine;

public class Destruction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Ennemi"))
        {
            Destroy(transform.gameObject);
        }
    }
}
