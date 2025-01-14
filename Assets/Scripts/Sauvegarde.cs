using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Sauvegarde : MonoBehaviour
{
    private string emplacement = @"Assets/Resources/SauvegardePartie.txt";
    private List<string> lignes = new List<string>();
    private List<string> listeNiveauCheckpoint = new List<string>();
    private List<string> nouvellesLignes = new List<string>();
    List<string> temp = new List<string>();
    private int niveauActuel;
    private int niveauCheckpoint;
    private int ordreActuel;

    //  private string emplacement = @"../CosmoSteve-le-Robin-de-L-espace/Assets/Resources/SauvegardePartie.txt";
    //  private string emplacement = @"C:/Users/emile/Test GitKraken/CosmoSteve-le-Robin-de-L-espace/Assets/Resources/SauvegardePartie.txt";

    private void Start()
    {
        try
        {
            Lire();
        }
        catch
        {
            emplacement = @"Cosmo Steve le Robin de l'espace_Data/Resources/SauvegardePartie.txt";
            Lire();
        }
    }

    public void Lire()
    {
        lignes.Clear();
        //Lire un fichier
        try
        {
            lignes = File.ReadAllLines(emplacement).ToList();
        }
        catch
        {
            emplacement = @"Cosmo Steve le Robin de l'espace_Data/Resources/SauvegardePartie.txt";
            lignes = File.ReadAllLines(emplacement).ToList();
        }
    }

    //Écrit les informations nécessaires pour la sauvegarde
    public void Ecrire(Vector2 position, string niveau, int ordre)
    {
        //Écrire
        nouvellesLignes.Add(niveau);
        nouvellesLignes.Add(ordre.ToString());
        nouvellesLignes.Add(position.x.ToString());
        nouvellesLignes.Add(position.y.ToString());
        nouvellesLignes.Add("False");
        File.WriteAllLines(emplacement, nouvellesLignes);
        nouvellesLignes.Clear();
    }

    public void Verifiacteur(Vector2 position, string niveau, int ordre)
    {
        niveauActuel = GetNumeroNiveau();

        listeNiveauCheckpoint = niveau.Split('u').ToList();
        niveauCheckpoint = int.Parse(listeNiveauCheckpoint[1]);

        ordreActuel = int.Parse(lignes[1]);

        if (niveauCheckpoint > niveauActuel)
        {
            Ecrire(position, niveau, ordre);
        }
        else if((niveauCheckpoint == niveauActuel) && (ordre > ordreActuel))
        {
            Ecrire(position, niveau, ordre);
        }

        listeNiveauCheckpoint.Clear();
    }

    public string GetNiveau()
    {
        return lignes[0];
    }

    public int GetNumeroNiveau()
    {
        Lire();
        temp.Clear();
        temp = lignes[0].Split('u').ToList();
        return int.Parse(temp[1]);
    }

    public float GetPositionX()
    {
        return float.Parse(lignes[2]);
    }

    public float GetPositionY()
    {
        return float.Parse(lignes[3]);
    }

    public bool GetVerificationReprise()
    {
        Lire();
        return bool.Parse(lignes[4]);
    }

    public void SetVerificationReprise(bool reponse)
    {
        lignes.RemoveAt(4);
        lignes.Add(reponse.ToString());
        File.WriteAllLines(emplacement, lignes);
    }
}
