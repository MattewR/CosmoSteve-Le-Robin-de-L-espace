using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAction : MonoBehaviour
{
    public bool pause = false;
    GameObject canvasMenu;
    // Start is called before the first frame update
    void Awake()
    {
        canvasMenu = GameObject.Find("mainCanvas");
        canvasMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            Debug.Log(canvasMenu.activeSelf);
        }
        catch
        {

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                pause = false;

            }
            else
            {
                pause = true;
            }
        }
        if (pause == true)
        {
            activateMenu();
        }
        else
        {
            deactivateMenu();
        }
    }

    public void pauseToUnpause()
    {
        pause = !pause;
    }

    public void activateMenu()
    {
        canvasMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void deactivateMenu()
    {
        canvasMenu.SetActive(false);
        Time.timeScale = 1;
    }

}
