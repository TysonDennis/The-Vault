using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //holds the rigidbody
    [SerializeField]
    Rigidbody rb;
    //holds Kaitlyn's max speed while walking
    [SerializeField]
    float WalkSpeed;
    //holds the direction Kaitlyn moves in
    private Vector3 forceDirection = Vector3.zero;
    //holds Kaitlyn's terminal velocity
    [SerializeField]
    float terminalVelocity;
    //holds the check for if Kaitlyn is standing on ground
    [SerializeField]
    bool isGrounded;
    //holds the player's controls
    [SerializeField]
    private PlayerControls controls;
    //holds the movement
    [SerializeField]
    private InputAction move;
    //holds the force Kaitlyn applies to herself while moving
    [SerializeField]
    private float movementForce;
    //holds the camera
    [SerializeField]
    private Camera playerCamera;

    //gets Kaitlyn's rigidbody and controls
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
    }

    //enables Kaitlyn's movement
    private void OnEnable()
    {
        move = controls.Kaitlyn.Move;
        controls.Kaitlyn.Enable();
    }

    //disables Kaitlyn's movement
    private void OnDisable()
    {
        controls.Kaitlyn.Disable();
    }

    private void FixedUpdate()
    {
        //holds Kaitlyn's movement
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;
        //applies force to Kaitlyn
        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;
        //lets Kaitlyn fall with gravity
        if(rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }
        //holds Kaitlyn's horizontal velocity
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        //stops Kaitlyn from moving faster than her max speed
        if(horizontalVelocity.sqrMagnitude > WalkSpeed * WalkSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * WalkSpeed + Vector3.up * rb.velocity.y;
        }
        //calls the LookAt() function
        LookAt();
    }

    //bases Kaitlyn's X-axis off the camera's
    private Vector3 GetCameraRight(Camera camera)
    {
        Vector3 right = camera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    //bases Kaitlyn's Z-axis off the camera's
    private Vector3 GetCameraForward(Camera camera)
    {
        Vector3 forward = camera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    //makes Kaitlyn turn in the direction she looks
    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;
        if(move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}
