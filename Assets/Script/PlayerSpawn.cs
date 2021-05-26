using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Sauvegarde sauvegarde;
    private bool verifReprise;
    public InformationsNiveau informationsNiveau;

    private void Awake()
    {
        sauvegarde.Lire();
        verifReprise = sauvegarde.GetVerificationReprise();
        if (verifReprise == true)
        {
            transform.position =  new Vector2(sauvegarde.GetPositionX(), sauvegarde.GetPositionY());
            sauvegarde.SetVerificationReprise(false);
            informationsNiveau.reinitialiser(transform.position);
        }
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}
