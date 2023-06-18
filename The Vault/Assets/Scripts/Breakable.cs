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

    //sets health to max health upon spawning
    void OnEnable()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if health is zero
        if(health <= 0)
        {
            //despawns if health is zero
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    //takes damage if hit by Kaitlyn's attacks
    public void TakeDamage(int damage)
    {
        health -= damage;
        particle.Play();
        StartCoroutine(StopParticles());
    }

    //stops the particle system
    private IEnumerator StopParticles()
    {
        yield return new WaitForSeconds(0.1f);
        particle.Stop();
    }
}
