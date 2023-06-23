using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    //stores the object's health
    [SerializeField]
    private int health;
    //stores the object's max health
    [SerializeField]
    private int maxHealth;
    //stores the particle system
    [SerializeField]
    private ParticleSystem particle;
    //stores the audio source, and its associated clip
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip clip;

    //sets health to max health upon spawning, and gets the components
    void OnEnable()
    {
        health = maxHealth;
        particle = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if health is zero
        if(health <= 0)
        {
            StartCoroutine(Destroy());
        }
    }

    //takes damage if hit by Kaitlyn's attacks
    public void TakeDamage(int damage)
    {
        health -= damage;
        particle.Play();
        StartCoroutine(StopParticles());
        //plays the sound of the rock breaking
        audio.PlayOneShot(clip, 1);
    }

    //stops the particle system
    private IEnumerator StopParticles()
    {
        yield return new WaitForSeconds(0.1f);
        particle.Stop();
    }

    //holds the function for the rock being destroyed
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        //despawns if health is zero
        Destroy(gameObject);
    }
}
