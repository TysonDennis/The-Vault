using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineOfSight : MonoBehaviour
{
    //gets the player
    [SerializeField]
    private GameObject player;
    //gets the event for if the player is spotted
    [SerializeField]
    private UnityEvent onSpotted;

    //gets the player
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //checks if the player enters the line of sight
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && player.GetComponent<Player>().IsInvisible == false)
        {
            onSpotted.Invoke();
        }
    }
}
