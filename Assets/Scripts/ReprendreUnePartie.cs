using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReprendreUnePartie : MonoBehaviour
{
    public Sauvegarde sauvegarde;
    public Button bouton;

    private void Start()
    {
        try
        {
            bool test = sauvegarde.GetVerificationReprise();
        }
        catch
        {
            bouton.interactable = false;
        }
    }
    public void Changer_niveau()
    {
            sauvegarde.SetVerificationReprise(true);
            SceneManager.LoadScene(sauvegarde.GetNiveau());
    }
}
