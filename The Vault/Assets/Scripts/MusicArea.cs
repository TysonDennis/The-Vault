using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicArea : MonoBehaviour
{
    //holds the reference to the player
    [SerializeField]
    private GameObject player;
    //holds the reference to the music's audio
    [SerializeField]
    private AudioSource audio;

    //gets the music
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
    }

    //plays the music when the player enters
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audio.Play();
        }
    }

    //stops the music when the player exits
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            audio.Stop();
        }
    }
}
