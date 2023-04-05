using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    //holds the damage the explosion deals
    [SerializeField]
    private int damage;
    //holds the explosion's lifespan
    [SerializeField]
    private float lifespan;

    //holds the function for damaging
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.tag == "Player")
        {
            other.transform.gameObject.SendMessage("TakeDamage", damage);
            other.transform.gameObject.SendMessage("Resist", gameObject.tag);
        }
    }
}
