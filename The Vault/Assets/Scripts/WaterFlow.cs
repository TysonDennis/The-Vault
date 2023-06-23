using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
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
    //gets the steam particles
    [SerializeField]
    private ParticleSystem flow;
    //holds the ice
    [SerializeField]
    private GameObject ice;
    //holds the ice's dimensions
    [SerializeField]
    private float iceX;
    [SerializeField]
    private float iceY;
    [SerializeField]
    private float iceZ;
    //holds the starting torque
    [SerializeField]
    private Vector3 InitialTorque;
    //holds the bool for if the water is frozen
    [SerializeField]
    private bool IsFrozen;
    //holds the audio source
    [SerializeField]
    private AudioSource audio;

    //gets the rigidbody of objects put in water, and sets the force of the current
    private void Awake()
    {
        aquatic = Object.FindObjectOfType<Aquatic>();
        player = Object.FindObjectOfType<Player>();
        IsFrozen = false;
        flow.Play();
    }

    //applies force of buoyancy when the object enters the water
    private void OnTriggerEnter(Collider other)
    {
        //releases steam if exposed to fire
        if (other.gameObject.CompareTag("FireEffect"))
        {
            IsFrozen = false;
        }
        //freezes if exposed to ice
        else if (other.gameObject.CompareTag("IceEffect"))
        {
            GameObject newobject = Instantiate(ice, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            newobject.transform.localScale = new Vector3(iceX, iceY, iceZ);
            IsFrozen = true;
        }
        //applies buoyancy
        if (other.TryGetComponent<Grabbable>(out Grabbable grabbable))
        {
            //applise buoyancy
            grabbable.isSubmerged = true;
            other.transform.gameObject.SendMessage("FloatInWater", waterDensity);
            //applies the force of the current
            //other.transform.gameObject.SendMessage("Current", CurrentForce);
        }
        else if (other.TryGetComponent<Aquatic>(out Aquatic aquatic))
        {
            aquatic.isSubmerged = true;
            other.transform.gameObject.SendMessage("FloatInWater", waterDensity);
        }
    }

    //stops water wheels and currents if frozen
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Grabbable>(out Grabbable grabbable))
        {
            //applies the force of the current
            other.transform.gameObject.SendMessage("Current", CurrentForce);
        }
        //applies current force to the player
        else if (other.TryGetComponent<Player>(out Player player))
        {
            other.transform.gameObject.SendMessage("Current", CurrentForce);
        }
        //applies current force to water wheel
        else if (other.TryGetComponent<WaterWheel>(out WaterWheel waterWheel))
        {
            other.transform.gameObject.SendMessage("Current", CurrentTorque);
        }
    }

    //removes force of buoyancy and water current when the object exits the water
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Grabbable>(out Grabbable grabbable))
        {
            grabbable.isSubmerged = false;
            grabbable.currentForce = Vector3.zero;
        }
        else if (other.TryGetComponent<Aquatic>(out Aquatic aquatic))
        {
            aquatic.isSubmerged = false;
        }
        else if (other.TryGetComponent<Player>(out Player player))
        {
            player.currentForce = Vector3.zero;
        }
        else if (other.TryGetComponent<WaterWheel>(out WaterWheel waterWheel))
        {
            waterWheel.currentTorque = Vector3.zero;
        }
    }

    //holds what to do if the water is frozen
    private void FixedUpdate()
    {
        if (IsFrozen == false)
        {
            CurrentForce = xForce * Vector3.right + yForce * Vector3.up + zForce * Vector3.forward;
            CurrentTorque = InitialTorque;
            flow.Play();
            audio.Play();
        }
        else
        {
            CurrentForce = Vector3.zero;
            CurrentTorque = Vector3.zero;
            flow.Stop();
            audio.Stop();
        }
    }
}
