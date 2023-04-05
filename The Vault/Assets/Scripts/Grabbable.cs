using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour
{
    //holds the grabbable object's rigidbody
    [SerializeField]
    private Rigidbody rb;
    //gets Kaitlyn's hold space
    [SerializeField]
    private Transform holdSpace;
    //holds the forwards force of the thrown object
    [SerializeField]
    private float horizontalForce;
    //holds the upwards force of the thrown object
    [SerializeField]
    private float verticalForce;
    //holds the sideways force of the thrown object
    [SerializeField]
    private float sidewaysForce;
    //holds Kaitlyn's transforms
    [SerializeField]
    private GameObject player;
    //stores the events
    public UnityEvent onGrab, onCarry, onDrop, onThrow;

    //gets the grabbable object's rigidbody and the player
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //places the object within the hold space, once grabbed
    public void Grab(Transform holdSpace)
    {
        this.holdSpace = holdSpace;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        onGrab.Invoke();
    }

    //keeps the grabbed object within the hold space
    private void FixedUpdate()
    {
        if(holdSpace != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, holdSpace.position, Time.deltaTime * lerpSpeed);
            this.transform.rotation = holdSpace.rotation;
            rb.MovePosition(newPosition);
            onCarry.Invoke();
        }
    }

    //drops the grabbed object by pressing C
    public void Drop()
    {
        this.holdSpace = null;
        rb.useGravity = true;
        onDrop.Invoke();
    }

    //throws the grabbed object by pressing X
    public void Throw()
    {
        this.holdSpace = null;
        rb.useGravity = true;
        Vector3 forceDirection = player.transform.forward;
        Vector3 sidewaysDirection = player.transform.right;
        Vector3 ApplyForce = forceDirection * horizontalForce + transform.up * verticalForce + sidewaysDirection * sidewaysForce;
        rb.AddForce(ApplyForce, ForceMode.Impulse);
        onThrow.Invoke();
    }
}
