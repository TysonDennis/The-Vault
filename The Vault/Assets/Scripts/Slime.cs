using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    //holds the slime's components
    [SerializeField]
    private NavMeshAgent nva;
    //holds the slime's stats
    [SerializeField]
    private float DetectionRange;
    //holds the reference to the player
    public GameObject player;

    private void Awake()
    {
        nva = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        //makes the slime pursue the player
        if(Vector3.Distance(transform.position, player.transform.position) <= DetectionRange)
        {
            nva.destination = player.transform.position;
        }
    }

    //holds the interactions with the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
