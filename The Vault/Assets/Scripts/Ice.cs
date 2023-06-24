using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    //holds the particle system for steam
    [SerializeField]
    private ParticleSystem particle;
    //holds the reference to the audio
    [SerializeField]
    private AudioSource audio;

    //gets the components
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
    }

    //allows the ice to interact with fire
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("FireEffect"))
        {
            StartCoroutine(Melt());
        }
    }

    //also allows ice to be melted by fire
    public void Melting()
    {
        StartCoroutine(Melt());
    }

    //holds the function for the ice melting
    private IEnumerator Melt()
    {
        audio.Play();
        particle.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
