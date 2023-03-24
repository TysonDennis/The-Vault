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
    //holds Kaitlyn's jump force
    [SerializeField]
    float JumpForce;
    //holds the player's capsule collider
    [SerializeField]
    private CapsuleCollider capsule;
    //holds the player's attack strength
    public int strength;

    //gets Kaitlyn's rigidbody, collider, and controls
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
        capsule = GetComponent<CapsuleCollider>();
    }

    //enables Kaitlyn's moveset
    private void OnEnable()
    {
        controls.Kaitlyn.Jump.started += DoJump;
        controls.Kaitlyn.Crouch.started += DoCrouch;
        controls.Kaitlyn.Crouch.canceled += DoStand;
        controls.Kaitlyn.Sprint.started += DoSprint;
        controls.Kaitlyn.Sprint.canceled += DoStand;
        controls.Kaitlyn.Attack.started += DoAttack;
        move = controls.Kaitlyn.Move;
        controls.Kaitlyn.Enable();
    }

    //disables Kaitlyn's moveset
    private void OnDisable()
    {
        controls.Kaitlyn.Jump.started -= DoJump;
        controls.Kaitlyn.Crouch.started -= DoCrouch;
        controls.Kaitlyn.Crouch.canceled -= DoStand;
        controls.Kaitlyn.Sprint.started -= DoSprint;
        controls.Kaitlyn.Sprint.canceled -= DoStand;
        controls.Kaitlyn.Attack.started -= DoAttack;
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

    //makes Kaitlyn jump, if she is touching the ground
    private void DoJump(InputAction.CallbackContext obj)
    {
        //only lets Kaitlyn jump if she's in contact with the ground
        if (IsGrounded())
        {
            forceDirection += Vector3.up * JumpForce;
        }
    }

    //checks if Kaitlyn is standing on solid ground
    private bool IsGrounded()
    {
        //contains the ground check raycast's origin and direction
        Ray ray = new Ray(this.transform.position + Vector3.up, Vector3.down);
        //sets to true if the raycast hits something
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            return true;
        }
        //sets to false if it doesn't hit
        else
        {
            return false;
        }
    }

    //lets Kaitlyn crouch, reducing her height and top speed
    private void DoCrouch(InputAction.CallbackContext obj)
    {
        capsule.height = 1.5f;
        WalkSpeed = 1f;
        movementForce = 1f;
    }

    //lets Kaitlyn stand up straight after crouching or sprinting, resetting her height, top speed, and acceleration
    private void DoStand(InputAction.CallbackContext obj)
    {
        capsule.height = 3f;
        WalkSpeed = 2f;
        movementForce = 1f;
    }

    //allows Kaitlyn to run, increasing her top speed and acceleration
    private void DoSprint(InputAction.CallbackContext obj)
    {
        WalkSpeed = 10f;
        movementForce = 2.5f;
    }

    //lets Kaitlyn attack
    private void DoAttack(InputAction.CallbackContext obj)
    {
        //sets the origin at the player's position and the direction at in front of Kaitlyn
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        //checks if there's something 1.665 m in front of Kaitlyn
        if(Physics.Raycast(ray, out RaycastHit hit, 1.665f))
        {
            //checks if that thing is tagged as damageable, and if so, communicates to the damageable script
            if(hit.transform.gameObject.tag == "Damageable")
            {
                hit.transform.gameObject.SendMessage("TakeDamage", strength);
            }
        }
    }
}
