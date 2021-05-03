using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenStarter : MonoBehaviour
{
    public bool GenStart = false;


    void Start()
    {
        GameEvents.current.pathGenTriggerReady += GraphListener;
    }

    private void GraphListener()
    {
        GenStart = true;
    }
}
