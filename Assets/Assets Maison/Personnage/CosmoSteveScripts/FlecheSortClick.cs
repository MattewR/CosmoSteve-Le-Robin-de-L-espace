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
    private double frameCounter = 0;
    private float timeSpent = 10f;
    public float fastBallC = 1f;
    private int nombreFoisSin = 0;
    private bool isGravityFunc = false;

    GameObject Steve;
    private GameObject fleche;
    private SuivreSourisArc scriptArc;
    private Transform transformFleche;

    private Vector3 positionFleche;
    private Vector3 oldPos = Vector3.zero;

    private List<GameObject> balles = new List<GameObject>();
    
    private Gravite scriptGrav;
    

    private Rigidbody2D bodySteve;
    public Image cadreUI;
    public Text nombreFoisSinText;


    private bool toBeCalledSin = true;

    public void Collision(GameObject collision)
    {
        
        SwitchFunction();
        cadreUI.gameObject.SetActive(true);
        nombreFoisSinText.gameObject.SetActive(true);
        nombreFoisSin = 5;
        nombreFoisSinText.text = "x " + nombreFoisSin.ToString();
        GameObject.Destroy(collision.gameObject);
        toBeCalledSin = true;


    }


    public void SwitchFunction()

    {

        if (isGravityFunc)
        {
            functionsAttach = new Func<float, float[], Vector3>[] { 
                //fonction f(temps)
                /*
                 * Variables 0 : Amplitude
                 * Variable 1 : Velocité
                 * Variable 2 : offset x
                 * Variable 3 : offset en y
                 * Variable 4 : Angle de départ (degrés)
                 * Variable 5 : Droite ou Gauche, 0 ou 1
                 */
                (temps, variables) => {
                    float x = (variables[1] * temps);
                    float y = (variables[0] * Mathf.Sin(x));
                    float angleRad;
                    Debug.Log(variables[4]);
                    angleRad = (variables[4] * Mathf.Deg2Rad);
                    if(variables[5] != 0){
                        x = x * Mathf.Cos(angleRad)  - y * Mathf.Sin(angleRad);
                        y = x * Mathf.Sin(angleRad)  + y * Mathf.Cos(angleRad);
                    }
                    else
                    {
                        x = x * Mathf.Cos(angleRad)  + y * Mathf.Sin(angleRad);
                        y = -1 * x * Mathf.Sin(angleRad)  + y * Mathf.Cos(angleRad);
                    }
                    x += variables[2];
                    y += variables[3];

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
                 * Variable 5 : Droite ou Gauche
                 */
                (temps, variables) => {
                    float dx = variables[1] * temps;
                    float dxSecond = variables[1] * (temps + 1);
                    float dy = (variables[0] * variables[1] * Mathf.Cos(dx)) * dx;
                    float dySecond = (variables[0] * variables[1] * Mathf.Cos(dx)) * (dxSecond);
                    float angleRad = (variables[4] * Mathf.Deg2Rad);
                    if(variables[5] != 0){
                        dx = dx * Mathf.Cos(angleRad)  - dy * Mathf.Sin(angleRad);
                        dy = dx * Mathf.Sin(angleRad)  + dy * Mathf.Cos(angleRad);

                        dxSecond = dxSecond * Mathf.Cos(angleRad)  - dySecond * Mathf.Sin(angleRad);
                        dySecond = dxSecond * Mathf.Sin(angleRad)  + dySecond * Mathf.Cos(angleRad);

                    }
                    else
                    {
                        dx = dx * Mathf.Cos(angleRad)  + dy * Mathf.Sin(angleRad);
                        dy = -1 * dx * Mathf.Sin(angleRad)  + dy * Mathf.Cos(angleRad);

                        dxSecond = dxSecond * Mathf.Cos(angleRad)  + dySecond * Mathf.Sin(angleRad);
                        dySecond = -1 * dxSecond * Mathf.Sin(angleRad)  + dySecond * Mathf.Cos(angleRad);
                    }

                    dy = Mathf.Atan2((dySecond-dy),(dxSecond-dx));
                    dy = dy * Mathf.Rad2Deg;
                    float angle = variables[2] * Mathf.Rad2Deg;
                    if (angle > 90 || angle < -90){
                        return new Vector3(0,0,dy);
                    }

                    return new Vector3(0,0,dy);
                }


            };
            isGravityFunc = false;
        }
        else
        {
            isGravityFunc = true;
            functionsAttach = new Func<float, float[], Vector3>[] {
                //fonction f(temps)
                /*
                 * variables 0 = offset x
                 * variables 1 = offset y
                 * variables 2 = angle EN RADIANS
                 * variables 3 = gravity
                 * variables 4 = starting velocity
                 * variables 5 = drag_coefficient
                 * variables 6 = Constante Balle
                 * variables 7 = bonus de l'inertie en x
                 * variables 8 = bonus de l'inertie en y
                 */
                (temps, variables) => {
                    //En x
                    temps = temps * variables[6];
                    float x = (variables[4] * temps * Mathf.Cos(variables[2])) + variables[0] + variables[7] * temps;
                    float y = (variables[4] * temps * Mathf.Sin(variables[2]) + variables[1]) + (0.5f * -1 * variables[3] * Mathf.Pow(temps, 2)) + variables[8]*temps;
                    return new Vector3(x, y, 0);
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
                 * variables 6 = Constante Balle
                 * variables 7 = bonus de l'inertie en x
                 * variables 8 = bonus de l'inertie en y
                 */
                (temps, variables) => {

                    float dy = (variables[4] * Mathf.Sin(variables[2])) - (variables[3] * temps) + variables[8];
                    float nouv_pente = dy * temps;
                    dy = Mathf.Atan2(nouv_pente, temps);
                    dy = dy * Mathf.Rad2Deg;
                    float angle = variables[2] * Mathf.Rad2Deg;
                    if (angle > 90 || angle < -90)
                    {
                        return new Vector3(0, 180, dy / variables[5]);
                    }

                    return new Vector3(0, 0, dy / variables[5]);
                }


            };
        }

    }


    private void variableVectorSet()
    {
        float final_velocity = (starting_velocity * ((-1 * (Mathf.Exp(-0.5f * hold_time))) + 1));
        float bonusX = calcVelBoost().x;
        float bonusY = calcVelBoost().y;

        float boolToFloat = 0;
        if (scriptArc.isRight)
        {
            boolToFloat = 1;
        }

        if (isGravityFunc)
        {
            variablesWFunction = new float[]
            {
                transformFleche.position.x, // + Mathf.Cos(angle * Mathf.Deg2Rad) * 0.85f,
                transformFleche.position.y, // + Mathf.Sin(angle * Mathf.Deg2Rad) * 0.85f,
                Mathf.Deg2Rad * scriptArc.getAngle(),
                gravity,
                final_velocity,
                6f,
                1,
                bonusX,
                bonusY

            };
        }
        else
        {
            variablesWFunction = new float[]
            {
                1.8f,
                final_velocity/2.5f,
                transformFleche.position.x, // + Mathf.Cos(angle * Mathf.Deg2Rad) * 0.85f,
                transformFleche.position.y, // + Mathf.Sin(angle * Mathf.Deg2Rad) * 0.85f,
                scriptArc.getAngle(),
                boolToFloat


            };
        }

    }



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
        scriptGrav = Steve.GetComponent­<Gravite>();
        bodySteve = gameObject.GetComponent<Rigidbody2D>();
        cadreUI = GameObject.FindGameObjectWithTag("paintingUI").GetComponent<Image>();
        nombreFoisSinText = GameObject.FindGameObjectWithTag("paintingUIText").GetComponent<Text>();


    }

    /*private Vector3 getTransformVector()
    {
        return Steve.transform.position;
    }*/

    private Vector2 calcVelBoost()
    {
        //Debug.Log("Euler bitch : " + transform.eulerAngles.y.ToString());
        /*if ((bodySteve.velocity.x < 0 && transform.eulerAngles.y == 0f) || (transform.eulerAngles.y == 180f && bodySteve.velocity.x > 0))
        {
            return -bodySteve.velocity.magnitude;
        }
        else
        {
            return bodySteve.velocity.magnitude;
        }
        */
        return bodySteve.velocity;

    }
    public void ToggleScript()
    {
        timeSpent = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (nombreFoisSin == 0 && toBeCalledSin)
        {
            toBeCalledSin = false;
            cadreUI.gameObject.SetActive(false);
            nombreFoisSinText.gameObject.SetActive(false);
            SwitchFunction();

        }

        //Debug.Log("Boost Vector :" + calcVelBoost().ToString());


        if (frameCounter == 0)
        {
            gravity = scriptGrav.accelerationGravitationnelle;

        }

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
            //Debug.Log(hold_time.ToString());
            //Debug.Log("func e^x: " + (starting_velocity * ((-1 * (Mathf.Exp(-0.5f * hold_time))) + 1)).ToString());
            if (frameCounter % 20 == 1)
            {

                variableVectorSet();

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


            variableVectorSet();


            if (isGravityFunc == false)
            {
                nombreFoisSin -= 1;
                nombreFoisSinText.text = "x " + nombreFoisSin.ToString();
            }


            PhysicsMaterial2D materielle = new PhysicsMaterial2D
            {

            };

            fleche = new GameObject();
            fleche.layer = 8;
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
            flecheAccesTrans.localScale = new Vector3(0.285f, 0.286f, 0) * Scale;
            //Attache le script FlecheAttache
            fleche.AddComponent<FlecheAttache>();
            //Va chercher le script
            FlecheAttache scriptAtt = fleche.GetComponent<FlecheAttache>();
            //Changer les variables public
            scriptAtt.variablesImport = variablesWFunction;
            scriptAtt.fonctionUtil = functionsAttach;

            fleche.AddComponent<Rigidbody2D>();
            Rigidbody2D body = fleche.GetComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Kinematic;
            body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
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

        //oldPos = getTransformVector();

    }
};
