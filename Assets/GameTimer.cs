using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float time;
    // Start is called before the first frame update
    void Start()
    {
       time = 1;
      

    }
    void FixedUpdate()
    {
        Debug.Log(time);
        time = time + 1;
    }
    
}
