using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterWheel : MonoBehaviour
{
    //holds the events for when the water wheel turns
    [SerializeField]
    private UnityEvent onStop, onGo;
    //holds the threshold of how fast the water wheel has to rotate
    [SerializeField]
    private float threshold;
    //holds the rigidbody of the wheel
    [SerializeField]
    private Rigidbody rb;
    //holds the force applied by a current
    public Vector3 currentTorque;

    //gets the rigidbody of the water wheel
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentTorque = Vector3.zero;
    }

    //checks how fast it's rotating, invoking the respective events of the wheel's rotational speed
    private void FixedUpdate()
    {
        //rb.AddTorque(currentForce, ForceMode.Force);
        transform.Rotate(currentTorque * Time.fixedDeltaTime);
        if(rb.angularVelocity.magnitude < threshold)
        {
            onStop.Invoke();
        }
        else
        {
            onGo.Invoke();
        }
    }

    //allows the object to be pushed by currents
    public void Current(Vector3 CurrentTorque)
    {
        //rb.AddForce(CurrentForce, ForceMode.Force);
        currentTorque = CurrentTorque;
    }
}
