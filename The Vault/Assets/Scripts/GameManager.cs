using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Stores gameplay events
    public UnityEvent onEventZero;
    //holds the EventsSO script
    [SerializeField]
    EventsSO eventsSO;

    //calls the events if the bools are set to true
    private void FixedUpdate()
    {
        if(eventsSO.ZeroBool == true)
        {
            onEventZero.Invoke();
        }
    }
}
