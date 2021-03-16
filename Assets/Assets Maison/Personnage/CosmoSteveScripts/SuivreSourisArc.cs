using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuivreSourisArc : MonoBehaviour
{

    private Transform transformDeLArc;
    private float angle;

   
    
    // Start is called before the first frame update
    void Awake()
    {
     transformDeLArc = transform.Find("Vise");
    }

    public float getAngle()
    {
        Debug.Log(angle);
        return angle;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*Debug.Log("x : " + mouse.x.ToString());
        Debug.Log("y : " + mouse.y.ToString());
        Debug.Log("z : " + mouse.z.ToString());*/
        Vector3 direction = (mouse - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Debug.Log("angle : " + angle.ToString());
        if (angle > 90 || angle < -90)
        {
            transformDeLArc.eulerAngles = new Vector3(0, 180, -(angle - 180));
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transformDeLArc.eulerAngles = new Vector3(0, 0, angle);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }


    }
}
