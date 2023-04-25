using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPanel : MonoBehaviour
{
    //holds the reference to the damage zone
    [SerializeField]
    private GameObject damagezone;

    //sets the damage zone to inactive if it gets wet
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WaterEffect"))
        {
            Destroy(damagezone);
        }
    }
}
