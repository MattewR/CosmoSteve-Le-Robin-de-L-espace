using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Sauvegarde : MonoBehaviour
{
    private string emplacement = @"../CosmoSteve-le-Robin-de-L-espace/Assets/Resources/SauvegardePartie.txt";
  //  private string emplacement = @"C:/Users/emile/Test GitKraken/CosmoSteve-le-Robin-de-L-espace/Assets/Resources/SauvegardePartie.txt";
    //private StreamWriter fichier = new StreamWriter(@Application.dataPath + "/Resources/Sauvegarde.txt");

    public void ecrire()
    {
        //Debug.Log(Application.dataPath);

        //Lire un fichier
        List<string> lignes = new List<string>();
        lignes = File.ReadAllLines(emplacement).ToList();
        Debug.Log(lignes[0]);

        //Écrire
        List<string> test = new List<string>();
        test.Add("Allo");
        File.WriteAllLines(emplacement, test);
    }
}
