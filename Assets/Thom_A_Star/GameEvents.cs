using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public delegate void GraphComplete();
    public event GraphComplete pathGenTriggerReady;


    public void OnPathGenTrigger()
    {
        if(pathGenTriggerReady != null)
        {
            pathGenTriggerReady();
        }
    }

}
