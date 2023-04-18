using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    //holds the force of buoyancy
    [SerializeField]
    private float buoyancy;
    //holds the rigidbody of whatever comes into contact with it
    [SerializeField]
    private Rigidbody rb;
    //holds the density of the water
    public float waterDensity;
    //gets the player script
    [SerializeField]
    private Player player;
    //gets the grabbable script
    [SerializeField]
    private Grabbable grabbable;

    //gets the rigidbody of objects put in water
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = gameObject.GetComponent<Player>();
        grabbable = Object.FindObjectOfType<Grabbable>();
    }

    //applies force when the object enters the water
    private void OnTriggerEnter(Collider other)
    {
        //applies buoyant force to the player
        if(other.transform.TryGetComponent(out player))
        {
            rb = player.GetComponent<Rigidbody>();
            buoyancy = (float) 9.81 * waterDensity / player.density;
            rb.AddForce(Vector3.up * buoyancy, ForceMode.Force);
        }
        //applies buoyant force to a grabbable object
        if (other.transform.TryGetComponent(out grabbable))
        {
            rb = grabbable.GetComponent<Rigidbody>();
            buoyancy = (float) 9.81 * waterDensity / grabbable.density;
            rb.AddForce(Vector3.up * buoyancy, ForceMode.Force);
        }
    }
}
