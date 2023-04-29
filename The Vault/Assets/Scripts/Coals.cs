using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coals : MonoBehaviour
{
    //holds the reference to the damage zone
    [SerializeField]
    private GameObject damagezone;
    //holds the reference to the coals
    [SerializeField]
    private Material coals;
    //holds the reference to the hot coals
    [SerializeField]
    private Material hotCoals;
    //holds an array for the materials
    [SerializeField]
    private ArrayList materialArray;

    //sets the damage zone to inactive if it gets wet or cold, sets it to active if it is exposed to fire
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("IceEffect"))
        {
            damagezone.SetActive(false);
            GetComponent<MeshRenderer>().material.[1] = coals;
        }
        else if (other.gameObject.CompareTag("WaterEffect"))
        {
            damagezone.SetActive(false);
            GetComponent<MeshRenderer>().material.[1] = coals;
        }
        else if (other.gameObject.CompareTag("FireEffect"))
        {
            damagezone.SetActive(true);
            GetComponent<MeshRenderer>().material.[1] = hotCoals;
        }
    }
}
