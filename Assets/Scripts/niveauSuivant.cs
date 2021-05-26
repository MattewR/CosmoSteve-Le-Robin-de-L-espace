using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class niveauSuivant : MonoBehaviour
{
    public string nom_scene;
    public Sauvegarde fichierSauvegarde;
    public Vector2 position;
    public int ordre;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fichierSauvegarde.Verifiacteur(position, nom_scene, ordre);
            SceneManager.LoadScene(nom_scene);
        }
    }

}
