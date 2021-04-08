using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class SuivreSourisArc : MonoBehaviour
{

    private Transform transformDeLArc;
    private float angle;
    private bool isRight = true;
    private Camera cameraMaison;
    private Vector3 camOffset;
    
    // Start is called before the first frame update
    void Awake()
    {
        transformDeLArc = transform.Find("Vise");
        cameraMaison = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        CameraFollow scriptFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        camOffset = new Vector3(scriptFollow.posOffset.x, scriptFollow.posOffset.y, scriptFollow.posOffset.z);
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

        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f);
        //Debug.Log(mouse);
        Vector3 cameraMouse = cameraMaison.ScreenToWorldPoint(mouse);
        
        cameraMouse.z = 0f;
        //Debug.Log(cameraMouse);
        //Debug.Log("y : " + mouse.y.ToString());
        //Debug.Log("z : " + mouse.z.ToString());
        //Debug.Log(cameraMouse - (cameraMaison.transform.position + camOffset));
        Vector3 rotVect = cameraMouse - (cameraMaison.transform.position + camOffset);
        rotVect.z = 0;
        rotVect.y *= -1;
        rotVect.x *= -1;
        Vector3 direction = (rotVect).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Debug.Log("angle : " + angle.ToString());
        
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
