using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class FlecheSortClick : MonoBehaviour
{
    public Func<float, float[], Vector3>[] functionsAttach = { 
                //fonction f(temps)
                /*
                 * variables 0 = offset x
                 * variables 1 = offset y
                 * variables 2 = angle EN RADIANS
                 * variables 3 = gravity
                 * variables 4 = starting velocity
                 * variables 5 = drag_coefficient
                 * variables 6 = Constante Balle
                 */
                (temps, variables) => {
                    //En x
                    temps = temps * variables[6];
                    float x = (variables[4] * temps * Mathf.Cos(variables[2]))  + variables[0];
                    float y = (variables[4] * temps * Mathf.Sin(variables[2]) + variables[1]) + (0.5f * -1 * variables[3] * Mathf.Pow(temps,2));
                    return new Vector3(x,y,0);
                },
                //fonction df(temps)/dtemps
                //Retourne le vecteur z (angle)
                /*
                 * variables 0 = offset x
                 * variables 1 = offset y
                 * variables 2 = angle EN RADIANS
                 * variables 3 = gravity
                 * variables 4 = starting velocity
                 * variables 5 = drag_coefficient
                 */
                (temps, variables) => {

                    float dy = (variables[4] * Mathf.Sin(variables[2])) - (variables[3] * temps);
                    float nouv_pente = dy * temps;
                    dy = Mathf.Atan2(nouv_pente,temps);
                    dy = dy * Mathf.Rad2Deg;
                    float angle = variables[2] * Mathf.Rad2Deg;
                    if (angle > 90 || angle < -90){
                        return new Vector3(0,180,dy / variables[5]);
                    }

                    return new Vector3(0,0,dy / variables[5]);
                }


            };
    public float[] variablesWFunction;
    public float gravity = 9.81f;
    public float starting_velocity = 10f;
    public float Scale = 1;
    private float hold_time = 0;
    GameObject Steve;
    private GameObject fleche;
    private Vector3 positionFleche;
    private SuivreSourisArc scriptArc;
    private Transform transformFleche;
    private double frameCounter = 0;
    private List<GameObject> balles = new List<GameObject>();
    private float timeSpent = 10f;
    public float fastBallC = 1f;
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
    public void ToggleScript()
    {
        timeSpent = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter += 1;
        timeSpent += Time.deltaTime;

        if (Time.timeScale == 0)
        {
            hold_time = 0;
            try
            {
                foreach (var ball in balles)
                {
                    UnityEngine.Object.Destroy(ball.gameObject);
                }
            }
            catch
            {

            }
        }

        if (Input.GetMouseButton(0) && Time.timeScale == 1 && timeSpent > 0.5f)
        {
            hold_time += Time.deltaTime;
            Debug.Log(hold_time.ToString());
            Debug.Log("func e^x: " + (starting_velocity * ((-1 * (Mathf.Exp(-0.5f * hold_time))) + 1)).ToString());
            if (frameCounter % 20 == 1)
            {
                float final_velocity = (starting_velocity * ((-1 * (Mathf.Exp(-0.5f * hold_time))) + 1));
                variablesWFunction = new float[]
                {
                transformFleche.position.x, // + Mathf.Cos(angle * Mathf.Deg2Rad) * 0.85f,
                transformFleche.position.y, // + Mathf.Sin(angle * Mathf.Deg2Rad) * 0.85f,
                Mathf.Deg2Rad * scriptArc.getAngle(),
                gravity,
                final_velocity,
                6f,
                fastBallC

                };
                GameObject tracer = new GameObject();
                balles.Add(tracer);
                tracer.AddComponent<SpriteRenderer>();
                SpriteRenderer imageFleche = tracer.GetComponent<SpriteRenderer>();
                imageFleche.sprite = Resources.Load<Sprite>("Sprites/rectangleIndicateur");

                imageFleche.sortingLayerName = "Indicator";

                Transform tracerAccesTrans = tracer.GetComponent<Transform>();
                tracerAccesTrans.localScale = new Vector3(0.08796673f, 0.09430709f, 1.6f) * Scale;

                tracer.AddComponent<FlecheAttache>();
                FlecheAttache scriptAttRect = tracer.GetComponent<FlecheAttache>();
                scriptAttRect.variablesImport = variablesWFunction;
                scriptAttRect.fonctionUtil = functionsAttach;


                //Debug.Log(scriptAtt.variablesImport[0]);
                //Debug.Log(scriptAtt.fonctionUtil.Length);




            }
        }

        if (Input.GetMouseButtonUp(0) && Time.timeScale == 1 && timeSpent > 0.5f)
        {



            float final_velocity = (starting_velocity * ((-1 * (Mathf.Exp(-0.5f * hold_time))) + 1));
            variablesWFunction = new float[]
            {
                transformFleche.position.x, // + Mathf.Cos(angle * Mathf.Deg2Rad) * 0.85f,
                transformFleche.position.y, // + Mathf.Sin(angle * Mathf.Deg2Rad) * 0.85f,
                Mathf.Deg2Rad * scriptArc.getAngle(),
                gravity,
                final_velocity,
                6f,
                1

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
            
            FlecheAttache scriptAtt = fleche.GetComponent<FlecheAttache>();
            scriptAtt.variablesImport = variablesWFunction;
            scriptAtt.fonctionUtil = functionsAttach;

            fleche.AddComponent<Rigidbody2D>();
            Rigidbody2D body = fleche.GetComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Kinematic;
            body.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            body.useFullKinematicContacts = true;
            /*fleche.AddComponent<BoxCollider2D>();
            BoxCollider2D colliderFleche = fleche.GetComponent<BoxCollider2D>();
            colliderFleche.offset = new Vector2(1.4723f, 0.0141f);
            colliderFleche.size = new Vector2(2.699f, 0.54714f);*/
            //Debug.Log(scriptAtt.variablesImport[0]);
            //Debug.Log(scriptAtt.fonctionUtil.Length);
            hold_time = 0;
            try
            {
                foreach (var ball in balles)
                {
                    UnityEngine.Object.Destroy(ball.gameObject);
                }
            }
            finally
            {

            }
        }



    }
};
