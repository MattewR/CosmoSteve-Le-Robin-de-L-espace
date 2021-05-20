using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NouvellePartie : MonoBehaviour
{
    public Sauvegarde sauvegarde;
    public ChangementDeScene changementDeScene;
    private Vector2 position;
    public void Nouvelle_Partie()
    {
        position = new Vector2(-1f, 0.3f);
        sauvegarde.Ecrire(position, "Niveau1", 0);
        sauvegarde.Lire();
        changementDeScene.Changer_niveau(sauvegarde.GetNiveau());
    }
}
