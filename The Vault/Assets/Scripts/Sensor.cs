using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    //holds the reference to the player
    [SerializeField]
    private GameObject player;
    //holds the reference to the event called
    [SerializeField]
    private UnityEvent onDetect, onLeave;

    private void Awake()
    {
        //gets the reference to the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //sets inRange to true if the player touches the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onDetect.Invoke();
        }
    }

    //sets inRange to false if the player exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onLeave.Invoke();
        }
    }
}
