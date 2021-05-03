using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Sauvegarde : MonoBehaviour
{
    private string emplacement = @"Assets/Resources/SauvegardePartie.txt";
    private List<string> lignes = new List<string>();
    private List<string> test = new List<string>();

    //    private string emplacement = @"../CosmoSteve-le-Robin-de-L-espace/Assets/Resources/SauvegardePartie.txt";
    //  private string emplacement = @"C:/Users/emile/Test GitKraken/CosmoSteve-le-Robin-de-L-espace/Assets/Resources/SauvegardePartie.txt";


     public void Lire()
    {
        //Lire un fichier
        lignes = File.ReadAllLines(emplacement).ToList();
        Debug.Log(lignes[0]);
        Debug.Log(lignes[1]);
        Debug.Log(lignes[2]);
    }

    //Écrit les informations nécessaires pour la sauvegarde
    public void Ecrire(Vector2 position, string niveau)
    {
        //Debug.Log(Application.dataPath);

        //Écrire
        test.Add(niveau);
        test.Add(position.x.ToString());
        test.Add(position.y.ToString());
        File.WriteAllLines(emplacement, test);
        test.Clear();
    }
}
