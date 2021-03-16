using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class FlecheSortClick : MonoBehaviour
{
    public Func<float, float[], Vector3>[] functionsAttach;
    public float[] variablesWFunction;
    //En radians
    private float angle;
    private float c;
    private float h;
    public float gravity = 9.81f;
    public float starting_velocity = 10f;
    public float Scale = 1;

    GameObject Steve;
    private GameObject fleche;
    private Vector3 positionFleche;
    private SuivreSourisArc scriptArc;
    private Transform transformFleche;
    // Start is called before the first frame update
    void Start()

    {
        
        Steve = this.gameObject;
        //Debug.Log(Steve);
        scriptArc = Steve.GetComponent<SuivreSourisArc>();
        transformFleche = transform.Find("Vise/flecheAttache");
        if (transformFleche == null)
        {
            Debug.Log("bruh");
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            
            //Peut get inventaire Steve
            //Exemple fonction de base
            //Appelé chaque frame (test)
            functionsAttach = new Func<float, float[], Vector3>[] { 
                //fonction f(temps)
                /*
                 * Variables 0 : Amplitude
                 * Variable 1 : Velocité
                 * Variable 2 : offset x
                 * Variable 3 : offset en y
                 * Variable 4 : Angle de départ (degrés)
                 */
                (temps, variables) => {
                    float x = (variables[1] * temps);
                    float y = (variables[0] * Mathf.Sin(x));
                    float angleRad = (variables[4] * Mathf.Deg2Rad);
                    x = x * Mathf.Cos(angleRad)  - y * Mathf.Sin(angleRad);
                    y = x * Mathf.Sin(angleRad)  + y * Mathf.Cos(angleRad);

                    return new Vector3(x,y,0);
                },
                //fonction df(temps)/dtemps
                //Retourne le vecteur z (angle)
                /*
                 * Variables 0 : Amplitude
                 * Variable 1 : Velocité
                 * Variable 2 : offset x
                 * Variable 3 : offset en y
                 * Variable 4 : Angle de départ (degrés)
                 */
                (temps, variables) => {
                    float dx = variables[1] * temps;
                    float dxSecond = variables[1] * (temps + 1);
                    float dy = (variables[0] * variables[1] * Mathf.Cos(dx)) * dx;
                    float dySecond = (variables[0] * variables[1] * Mathf.Cos(dx)) * (dxSecond);
                    float angleRad = (variables[4] * Mathf.Deg2Rad);
                    dx = dx * Mathf.Cos(angleRad)  - dy * Mathf.Sin(angleRad);
                    dy = dx * Mathf.Sin(angleRad)  + dy * Mathf.Cos(angleRad);
                    dxSecond = dxSecond * Mathf.Cos(angleRad)  - dySecond * Mathf.Sin(angleRad);
                    dySecond = dxSecond * Mathf.Sin(angleRad)  + dySecond * Mathf.Cos(angleRad);



                    dy = Mathf.Atan2((dySecond-dy),(dxSecond-dx));
                    dy = dy * Mathf.Rad2Deg;
                    float angle = variables[2] * Mathf.Rad2Deg;
                    if (angle > 90 || angle < -90){
                        return new Vector3(0,0,dy);
                    }

                    return new Vector3(0,0,dy);
                }


            };
            
            variablesWFunction = new float[]
            {
                3,
                starting_velocity,
                transformFleche.position.x, // + Mathf.Cos(angle * Mathf.Deg2Rad) * 0.85f,
                transformFleche.position.y, // + Mathf.Sin(angle * Mathf.Deg2Rad) * 0.85f,
                scriptArc.getAngle()


            };
            

            PhysicsMaterial2D materielle = new PhysicsMaterial2D
            {

            };

            fleche = new GameObject();
            fleche.AddComponent<BoxCollider2D>();
            BoxCollider2D fCollider = fleche.GetComponent<BoxCollider2D>();
            fCollider.size = new Vector2(2.8f, 0.548f);
            fCollider.offset = new Vector2(1.47f, 0.014f);
            fCollider.sharedMaterial = materielle;

            fleche.AddComponent<SpriteRenderer>();
            SpriteRenderer imageFleche = fleche.GetComponent<SpriteRenderer>();
            imageFleche.sprite = Resources.Load<Sprite>("Sprites/flechemod2");
            
            imageFleche.sortingLayerName = "Mains";

            Transform flecheAccesTrans = fleche.GetComponent<Transform>();
            flecheAccesTrans.localScale = new Vector3(0.344f, 0.362f, 0) * Scale; 

            fleche.AddComponent<FlecheAttache>();
            FlecheAttache scriptAtt= fleche.GetComponent<FlecheAttache>();
            scriptAtt.variablesImport = variablesWFunction;
            scriptAtt.fonctionUtil = functionsAttach;
            //Debug.Log(scriptAtt.variablesImport[0]);
            //Debug.Log(scriptAtt.fonctionUtil.Length);

        }



    }
};
