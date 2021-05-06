using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReprendreUnePartie : MonoBehaviour
{
    public Sauvegarde sauvegarde;

    public void Changer_niveau()
    {
        sauvegarde.SetVerificationReprise(true);
        SceneManager.LoadScene(sauvegarde.GetNiveau());
    }
}
