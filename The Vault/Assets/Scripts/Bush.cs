using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    //holds the particle system
    [SerializeField]
    private ParticleSystem flames;

    //holds the event for the bush setting on fire if exposed to fire
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FireEffect"))
        {
            StartCoroutine("Burn");
        }
    }

    //holds the coroutine for the bush burning away
    private IEnumerator Burn()
    {
        flames.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
