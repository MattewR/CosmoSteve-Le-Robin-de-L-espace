﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementDeScene : MonoBehaviour
{
    public void Changer_niveau(string nom_scene)
    {
        SceneManager.LoadScene(nom_scene);
    }
}
