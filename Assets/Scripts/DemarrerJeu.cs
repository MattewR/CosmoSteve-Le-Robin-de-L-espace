using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemarrerJeu : MonoBehaviour
{
    public void changer_niveau(string nom_scene)
    {
        SceneManager.LoadScene(nom_scene);
    }
}
