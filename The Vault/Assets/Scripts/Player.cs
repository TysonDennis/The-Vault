using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    //accesses the transform that Kaitlyn gives to the items she grabs
    public Transform holdSpace;
    //stores the rigidbody of the object picked up
    [SerializeField]
    Rigidbody _heldObject;
    //communicates to the grabbable script
    [SerializeField]
    private Grabbable grabbable;
    //stores the KaitlynSO
    [SerializeField]
    private KaitlynSO kaitlyn;
    //stores the bool for checking if the game's paused
    public bool IsPaused;
    //holds the HUD
    public GameObject HUD;
    //holds the UI raycaster
    [SerializeField]
    private GraphicRaycaster graphicRaycaster;
    //holds the pointer event data
    [SerializeField]
    private PointerEventData click_data;
    //holds the results of the click
    [SerializeField]
    List<RaycastResult> click_results;
    //holds the world spawn Kaitlyn starts at
    [SerializeField]
    private Transform worldSpawn;
    //communicates to the script for the HUD
    [SerializeField]
    private HUD HUDScript;
    //gets the particle system for Kaitlyn's blood
    [SerializeField]
    private ParticleSystem blood;
    //holds Kaitlyn's animator
    private Animator animator;

    //gets Kaitlyn's rigidbody, collider, animator, and controls, while setting her HP to max
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
        capsule = GetComponent<CapsuleCollider>();
        kaitlyn.HP = kaitlyn.maxHP;
        IsPaused = false;
        graphicRaycaster = HUD.GetComponent<GraphicRaycaster>();
        click_data = new PointerEventData(EventSystem.current);
        click_results = new List<RaycastResult>();
        animator = GetComponent<Animator>();
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
        controls.Kaitlyn.Grab.started += DoGrab;
        controls.Kaitlyn.Pause.started += DoPause;
        controls.Menu.Pause.started += DoPause;
        controls.Menu.Click.started += DoClick;
        move = controls.Kaitlyn.Move;
        controls.Kaitlyn.Enable();
        controls.Menu.Enable();
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
        controls.Kaitlyn.Grab.started -= DoGrab;
        controls.Kaitlyn.Pause.started -= DoPause;
        controls.Menu.Pause.started -= DoPause;
        controls.Menu.Click.started -= DoClick;
        controls.Kaitlyn.Disable();
        controls.Menu.Disable();
    }

    //called once a frame
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
        //calls the Kill function if Kaitlyn's HP is 0
        if(kaitlyn.HP <= 0)
        {
            Kill();
        }
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
            animator.SetTrigger("JumpTrigger");
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
        animator.SetBool("CrouchBool", true);
    }

    //lets Kaitlyn stand up straight after crouching or sprinting, resetting her height, top speed, and acceleration
    private void DoStand(InputAction.CallbackContext obj)
    {
        capsule.height = 3f;
        WalkSpeed = 2f;
        movementForce = 1f;
        animator.SetTrigger("IdleTrigger");
        animator.SetBool("CrouchBool", false);
    }

    //allows Kaitlyn to run, increasing her top speed and acceleration
    private void DoSprint(InputAction.CallbackContext obj)
    {
        WalkSpeed = 10f;
        movementForce = 2.5f;
        animator.SetTrigger("RunTrigger");
    }

    //lets Kaitlyn attack and throw
    private void DoAttack(InputAction.CallbackContext obj)
    {
        //checks if Kaitlyn's hands are empty
        if(grabbable == null) 
        { 
            //sets the origin at the player's position and the direction at in front of Kaitlyn
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            //checks if there's something 1.665 m in front of Kaitlyn
            if (Physics.Raycast(ray, out RaycastHit hit, 1.665f))
            {
                //holds the rigidbody of the grabbable object
                Rigidbody otherRB = hit.rigidbody;
                //checks if that thing is tagged as damageable, and if so, communicates to the damageable script
                if (hit.transform.gameObject.tag == "Damageable")
                {
                    hit.transform.gameObject.SendMessage("TakeDamage", strength);
                }
            }
        }
        //throws if Kaitlyn is holding something
        else
        {
            grabbable.Throw();
            grabbable = null;
        }
    }

    //lets Kaitlyn grab, carry, and drop objects
    private void DoGrab(InputAction.CallbackContext obj)
    {
        //checks if Kaitlyn's holding something
        if(grabbable == null) 
        { 
            //sets the origin at the player's position and the direction at in front of Kaitlyn
            Ray ray = new Ray(this.transform.position, this.transform.forward);
            //checks if there's something 1.665 m in front of Kaitlyn
            if (Physics.Raycast(ray, out RaycastHit hit, 1.665f))
            {
                //grabs if that object in front of Kaitlyn has a grabbable script
                if(hit.transform.TryGetComponent(out grabbable))
                {
                    grabbable.Grab(holdSpace);
                }
            }
        }
        //if Kaitlyn is holding something, she drops it
        else
        {
            grabbable.Drop();
            grabbable = null;
        }
    }

    //alternates between paused and unpaused
    private void DoPause(InputAction.CallbackContext obj)
    {
        if(IsPaused == false)
        {
            IsPaused = true;
            controls.Kaitlyn.Disable();
            controls.Menu.Enable();
        }
        else
        {
            IsPaused = false;
            controls.Kaitlyn.Enable();
            controls.Menu.Disable();
        }
    }

    //clicks on buttons on the pause menu UI
    private void DoClick(InputAction.CallbackContext obj)
    {
        click_data.position = Mouse.current.position.ReadValue();
        click_results.Clear();
        graphicRaycaster.Raycast(click_data, click_results);
        foreach(RaycastResult result in click_results)
        {
            GameObject ui_element = result.gameObject;
            Debug.Log(ui_element.name);
        }
    }

    //deletes saved data
    public void Delete()
    {
        kaitlyn.Spawnpoint = null;
        this.transform.position = worldSpawn.position;
    }

    //allows Kaitlyn to take damage
    public void TakeDamage(int damage)
    {
        kaitlyn.HP -= damage;
        HUDScript.hurtSprite();
        blood.Play();
        StartCoroutine(StopBleeding());
    }

    //stops the bleeding after a bit
    private IEnumerator StopBleeding()
    {
        yield return new WaitForSeconds(0.1f);
        blood.Stop();
    }

    //runs the function for if Kaitlyn is killed
    private void Kill()
    {
        HUDScript.hurtSprite();
        blood.Play();
        StartCoroutine(StopBleeding());
        this.transform.position = kaitlyn.Spawnpoint.position;
        kaitlyn.HP = kaitlyn.maxHP;
    }
}
