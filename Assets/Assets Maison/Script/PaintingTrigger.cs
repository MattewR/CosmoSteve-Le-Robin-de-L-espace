using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    bool hasBeenCalled = false;
    FlecheSortClick ScriptAction;
    void Start()
    {
        ScriptAction = GameObject.FindGameObjectWithTag("Player").GetComponent<FlecheSortClick>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Got here");
        if (collision.CompareTag("Player") && hasBeenCalled == false)
        {
            hasBeenCalled = true;
            ScriptAction.Collision(gameObject);

        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
