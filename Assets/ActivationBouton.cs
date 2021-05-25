using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActivationBouton : MonoBehaviour
{
    public Sauvegarde sauvegarde;
    public Button bouton;
    public int niveauADebloquer;

    // Start is called before the first frame update
    public void Start()
    {
        try
        {
            if (niveauADebloquer > sauvegarde.GetNumeroNiveau())
            {
                bouton.interactable = false;
            }
        }
        catch
        {
            bouton.interactable = false;
        }
    }
}
