using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SuivreSourisArc : MonoBehaviour
{

    private Transform transformDeLArc;
    private float angle;
    private bool isRight = true;
   
    
    // Start is called before the first frame update
    void Awake()
    {
     transformDeLArc = transform.Find("Vise");
    }

    public float getAngle()
    {

        if (angle < 50 && angle > -25)

        {

            return angle;
        }
        else if (angle > 130 || (angle < 0 && angle < -155))
        {

            return angle;
        }
        else
        {
            //Debug.Log(angle);
            if (isRight == false)
            {
                if(Mathf.Abs(130f - angle) < Mathf.Abs(-155f - angle))
                {
                    return 130f;
                }
                else
                {
                    return -155f;
                }
            }
            else
            {
                if (Mathf.Abs(50f - angle) < Mathf.Abs(-25f - angle))
                {
                    return 50f;
                }
                else
                {
                    return -25f;
                }
            }
        }

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
        
        if (angle < 50 && angle > -25)

        {
            isRight = true;
            transformDeLArc.eulerAngles = new Vector3(0, 0, angle);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(angle > 130 || (angle < 0 && angle < -155)){
            isRight = false;
            transformDeLArc.eulerAngles = new Vector3(0, 180, -(angle - 180));
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }
}
