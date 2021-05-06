using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Sauvegarde sauvegarde;
    private bool verifReprise;

    private void Awake()
    {
        sauvegarde.Lire();
        verifReprise = sauvegarde.GetVerificationReprise();
        if (verifReprise == true)
        {
            transform.position =  new Vector3(sauvegarde.GetPositionX(), sauvegarde.GetPositionY(), 0);
            Debug.Log(sauvegarde.GetPositionX());
            Debug.Log(sauvegarde.GetPositionY());
            sauvegarde.SetVerificationReprise(false);
        }

        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}
