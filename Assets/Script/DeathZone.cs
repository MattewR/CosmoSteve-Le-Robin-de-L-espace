using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    public InformationsNiveau niveau;
    public bool astre;
    public DeplacementAstre astreMassif;

    private void Start()
    {
        niveau.miseAJour();
        astre = niveau.getAstre();
    }

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = playerSpawn.position;

            if (astre == true)
            {
                astreMassif.resetPosition();
            }
        }
    }
}
