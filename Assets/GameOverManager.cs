using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public GameObject gameOverUI;

    public GameOverManager instance;

    public string nom_scene;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }
        instance = this;
    }

    public void RetryButton()
    {
        //Recommencer le niveau 
        SceneManager.LoadScene(nom_scene);
        gameOverUI.SetActive(false);
    }

    public void MainMenuButton()
    {
        //Retour au menu principal

        SceneManager.LoadScene("MenuPrincipal");

        gameOverUI.SetActive(false);
    }

    public void QuitButton()
    {
        //Fermer le jeu
        Application.Quit();
    }

}
