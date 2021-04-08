using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAction : MonoBehaviour
{
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
            if(canvasMenu.activeSelf == false)
            {
                activateMenu();
            }
            else
            {
                deactivateMenu();
            }
        }

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
