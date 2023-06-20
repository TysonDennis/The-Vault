using UnityEngine;
using UnityEngine.Events;

public class Handle : MonoBehaviour
{
    //gets Kaitlyn's hold space
    [SerializeField]
    private Transform holdSpace;
    //holds Kaitlyn's transforms
    [SerializeField]
    private GameObject player;
    //stores the bool that checks if the handle has been released
    [SerializeField]
    private bool released;
    //gets the position Kaitlyn gets in if she grabs it
    [SerializeField]
    private Transform playerHold;
    //gets the speed
    [SerializeField]
    private float speed;
    //holds the events
    [SerializeField]
    private UnityEvent onGrab, onRelease;

    //gets the handle's rigidbody and the player
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        released = false;
    }

    //activates if the handle is grabbed
    public void Grab(Transform holdSpace)
    {
        this.holdSpace = holdSpace;
        holdSpace.position = this.transform.position;
        player.GetComponent<Rigidbody>().useGravity = false;
        onGrab.Invoke();
    }

    //activates if the handle is released
    public void Release()
    {
        this.holdSpace = null;
        released = true;
        player.GetComponent<Rigidbody>().useGravity = true;
        onRelease.Invoke();
    }

    //updates Kaitlyn's movement if she's stuck to the handle
    private void FixedUpdate()
    {
        if (holdSpace != null)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, playerHold.transform.position, speed * Time.deltaTime);
        }
    }
}
