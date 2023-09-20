using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    //holds the reference for the HUD script
    [SerializeField]
    private HUD hud;
    //holds the string text
    [SerializeField]
    private string text;
    //holds the cleared text
    [SerializeField]
    private string clearText;
    //holds the lifespan of the text
    [SerializeField]
    private float lifespan;
    //holds the max lifespan of the text
    [SerializeField]
    private float maxLifespan;
    //checks if the lifespan has been activated or not
    [SerializeField]
    private bool isActive;
    //checks if the text box is one-shot or not
    [SerializeField]
    private bool isOneShot;
    //holds the bool for if it causes Kaitlyn to display the hurt sprite
    [SerializeField]
    private bool isKaitlynHurt;
    //holds the audio of the text box
    [SerializeField]
    private AudioSource audio;

    private void Awake()
    {
        //gets the HUD
        hud = FindObjectOfType<HUD>();
        //gets the audio
        audio = GetComponent<AudioSource>();
        //sets isActive to false
        isActive = false;
        //sets lifespan to max
        lifespan = maxLifespan;
    }

    //displays the text and activates the lifespan when the player enters
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lifespan = maxLifespan;
            audio.Play();
            hud.transform.gameObject.SendMessage("Speak", text);
            isActive = true;
            //makes Kaitlyn's hurt sprite appear if she's hurt
            if(isKaitlynHurt == true)
            {
                hud.hurtSprite();
            }
        }
    }

    private void FixedUpdate()
    {
        //makes the lifespan tick down once activated
        if(isActive == true)
        {
            lifespan -= Time.fixedDeltaTime;
        }
        //makes the text disappear once the lifespan hits zero
        if(lifespan <= 0)
        {
            isActive = false;
            lifespan = 0;
            hud.transform.gameObject.SendMessage("Clear", clearText);
            //destroys the text box if it's one shot
            if(isOneShot == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
