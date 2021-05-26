using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class changeSonBar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject barDeSon;
    private Slider valSlider;
    private AudioSource source;
    void Awake()
    {
        valSlider = barDeSon.GetComponent<Slider>();
        source = gameObject.GetComponent<AudioSource>();
        source.volume = 0.5f;
    }

    public void changerSon()
    {
        source.volume = valSlider.value;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
