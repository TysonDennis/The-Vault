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
    //holds the reference to the mesh renderer
    [SerializeField]
    private MeshRenderer renderer;

    //gets references to the mesh renderer
    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        coals = GetComponent<MeshRenderer>().materials[1];
    }

    //sets the damage zone to inactive if it gets wet or cold, sets it to active if it is exposed to fire
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("IceEffect"))
        {
            damagezone.SetActive(false);
            GetComponent<MeshRenderer>().materials[1] = coals;
        }
        else if (other.gameObject.CompareTag("WaterEffect"))
        {
            damagezone.SetActive(false);
            GetComponent<MeshRenderer>().materials[1] = coals;
        }
        else if (other.gameObject.CompareTag("FireEffect"))
        {
            damagezone.SetActive(true);
            GetComponent<MeshRenderer>().materials[2] = coals;
        }
    }
}
