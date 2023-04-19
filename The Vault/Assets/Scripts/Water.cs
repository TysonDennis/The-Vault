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
    //holds the force of the water's current in the three cardinal directions
    [SerializeField]
    private float xForce;
    [SerializeField]
    private float yForce;
    [SerializeField]
    private float zForce;
    //holds the player script
    [SerializeField]
    private Player player;
    //holds the current force
    [SerializeField]
    private Vector3 CurrentForce;
    //holds water wheels
    [SerializeField]
    private WaterWheel waterWheel;
    //holds the current torque
    [SerializeField]
    private Vector3 CurrentTorque;

    //gets the rigidbody of objects put in water, and sets the force of the current
    private void Awake()
    {
        aquatic = Object.FindObjectOfType<Aquatic>();
        player = Object.FindObjectOfType<Player>();
        CurrentForce = xForce * Vector3.right + yForce * Vector3.up + zForce * Vector3.forward;
    }

    //applies force of buoyancy when the object enters the water
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Grabbable>(out Grabbable grabbable))
        {
            //applies the force of the current
            other.transform.gameObject.SendMessage("Current", CurrentForce);
            //applise buoyancy
            grabbable.isSubmerged = true;
            other.transform.gameObject.SendMessage("FloatInWater", waterDensity);
        }
        else if(other.TryGetComponent<Aquatic>(out Aquatic aquatic))
        {
            aquatic.isSubmerged = true;
            other.transform.gameObject.SendMessage("FloatInWater", waterDensity);
        }
        //applies current force to the player
        else if(other.TryGetComponent<Player>(out Player player))
        {
            other.transform.gameObject.SendMessage("Current", CurrentForce);
        }
        //applies current force to water wheel
        else if(other.TryGetComponent<WaterWheel>(out WaterWheel waterWheel))
        {
            other.transform.gameObject.SendMessage("Current", CurrentTorque);
        }
    }

    //removes force of buoyancy and water current when the object exits the water
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Grabbable>(out Grabbable grabbable))
        {;
            grabbable.isSubmerged = false;
            grabbable.currentForce = Vector3.zero;
        }
        else if(other.TryGetComponent<Aquatic>(out Aquatic aquatic))
        {
            aquatic.isSubmerged = false;
        }
        else if(other.TryGetComponent<Player>(out Player player))
        {
            player.currentForce = Vector3.zero;
        }
        else if(other.TryGetComponent<WaterWheel>(out WaterWheel waterWheel))
        {
            waterWheel.currentTorque = Vector3.zero;
        }
    }
}
