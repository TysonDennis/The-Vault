using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    //stores the events that happen if it's pressed and if it's released
    public UnityEvent onPressed, onRelease;

    //invokes the onPressed event if something touches it
    void OnCollisionEnter(Collision collision)
    {
        onPressed.Invoke();
    }

    //invokes the onRelease event if something stops touching it
    private void OnCollisionExit(Collision collision)
    {
        onRelease.Invoke();
    }
}
