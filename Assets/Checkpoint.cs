using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Animator animator;
    private Transform playerSpawn;
    public Sauvegarde sauvegarde;
    public string niveau;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
            sauvegarde.Ecrire(playerSpawn.position, niveau);
            animator.SetBool("IsChecked", true);
        }
    }
}
