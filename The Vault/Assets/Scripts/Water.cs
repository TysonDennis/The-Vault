using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    //holds the rigidbody of whatever comes into contact with it
    [SerializeField]
    private Rigidbody rb;
    //holds the density of the water
    public float waterDensity;
    //gets the player script
    [SerializeField]
    private Aquatic aquatic;
    //gets the grabbable script
    [SerializeField]
    private Grabbable grabbable;

    //gets the rigidbody of objects put in water
    private void Awake()
    {
        /*rb = gameObject.GetComponent<Rigidbody>();
        player = gameObject.GetComponent<Player>();
        grabbable = Object.FindObjectOfType<Grabbable>();*/
    }

    //applies force of buoyancy when the object enters the water
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Grabbable>(out Grabbable grabbable))
        {
            //Grabbable submergeCheck = other.GetComponent<Grabbable>();
            grabbable.isSubmerged = true;
            other.transform.gameObject.SendMessage("FloatInWater", waterDensity);
        }
        else if(other.TryGetComponent<Aquatic>(out Aquatic aquatic))
        {
            //Aquatic playerHead = other.GetComponent<Aquatic>();
            aquatic.isSubmerged = true;
            other.transform.gameObject.SendMessage("FloatInWater", waterDensity);
        }
    }

    //removes force of buoyancy when the object exits the water
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Grabbable>(out Grabbable grabbable))
        {
            //Grabbable submergeCheck = other.GetComponent<Grabbable>();
            grabbable.isSubmerged = false;
        }
        else if(other.TryGetComponent<Aquatic>(out Aquatic aquatic))
        {
            //Aquatic playerHead = other.GetComponent<Aquatic>();
            aquatic.isSubmerged = false;
        }
        
    }
}
