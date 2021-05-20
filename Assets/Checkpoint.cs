using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Animator animator;
    private Transform playerSpawn;
    public Sauvegarde sauvegarde;
    public string niveau;
    public int ordre;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
            sauvegarde.Verifiacteur(playerSpawn.position, niveau, ordre);
            animator.SetBool("IsChecked", true);
        }
    }
}
