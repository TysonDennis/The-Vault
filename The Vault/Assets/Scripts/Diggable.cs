using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diggable : MonoBehaviour
{
    //holds the player script
    [SerializeField]
    private Player player;
    //holds the particle system
    [SerializeField]
    private ParticleSystem particle;

    //gets the player
    private void Awake()
    {
        //player = gameObject.GetComponent<Player>();
    }

    //holds the event for if the player collides with the diggable dirt
    private void OnCollisionEnter(Collision collision)
    {
        //checks if the IsDigging bool is true, and that Kaitlyn has collided with the dirt
        if(player.IsDigging && collision.gameObject.tag == "Player")
        {
            StartCoroutine(Dug());
        }
    }

    //runs the function for if the player is digging when they touch the diggable
    private IEnumerator Dug()
    {
        particle.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
