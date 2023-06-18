using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    //holds the rigidbody
    public Rigidbody rb;
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
    int speedHash = Animator.StringToHash("Speed");
    //stores the bool for if Kaitlyn is digging
    public bool IsDigging;
    //accesses the EventsSO scriptable object
    [SerializeField]
    private EventsSO eventsSO;
    //holds Kaitlyn's density
    public float density;
    //holds the aquatic script
    [SerializeField]
    private Aquatic aquatic;
    //holds the force of a current
    public Vector3 currentForce;
    //contains the numbers of Kaitlyn's held abilities
    public float AbilityNumber;
    //contains the highest ability that Kaitlyn can have
    [SerializeField]
    private int NumCap = 4;
    //holds the projectile for Kaitlyn's water gun
    public GameObject waterGun;
    //holds the projectile for Kaitlyn's lightningbolt
    public GameObject lightningBolt;
    //holds the projectile for Kaitlyn's stretch arm
    public GameObject stretchArm;
    //holds the projectile for Kaitlyn's frost breath
    public GameObject frostBreath;
    //holds the projectile for Kaitlyn's flamethrower
    public GameObject flamethrower;

    //gets Kaitlyn's rigidbody, collider, animator, and controls, while setting her stats
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
        capsule = GetComponent<CapsuleCollider>();
        kaitlyn.maxHP = 100 + kaitlyn.HealthPickup * 100;
        strength = 5 + kaitlyn.StrengthPickup;
        kaitlyn.HP = kaitlyn.maxHP;
        JumpForce = 7 + kaitlyn.HighJump;
        movementForce = 1 + kaitlyn.Sprint;
        WalkSpeed = 2 + 2 * kaitlyn.Sprint;
        IsPaused = false;
        graphicRaycaster = HUD.GetComponent<GraphicRaycaster>();
        click_data = new PointerEventData(EventSystem.current);
        click_results = new List<RaycastResult>();
        animator = GetComponent<Animator>();
        IsDigging = false;
        kaitlyn.floatHP = kaitlyn.HP;
        AbilityNumber = 0;
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
        controls.Kaitlyn.HighJump.started += DoHighJump;
        controls.Kaitlyn.Change.started += DoChange;
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
        controls.Kaitlyn.HighJump.started -= DoHighJump;
        controls.Kaitlyn.Change.started -= DoChange;
        controls.Menu.Pause.started -= DoPause;
        controls.Menu.Click.started -= DoClick;
        controls.Kaitlyn.Disable();
        controls.Menu.Disable();
    }

    //called once a frame
    private void FixedUpdate()
    {
        animator.SetFloat(speedHash, rb.velocity.magnitude / WalkSpeed);
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
        rb.AddForce(currentForce, ForceMode.Force);
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
        //makes Kaitlyn swim upwards if she's in water
        else if (aquatic.isSubmerged == true)
        {
            forceDirection += Vector3.up * JumpForce * 0.5f * (kaitlyn.WaterRespiration + 1);
            animator.SetTrigger("JumpTrigger");
        }
    }

    //checks if Kaitlyn is standing on solid ground
    private bool IsGrounded()
    {
        //contains the ground check raycast's origin and direction
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
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
        //makes Kaitlyn crouch on dry land
        if(aquatic.isSubmerged == false)
        {
            capsule.height = (float)(1.5f - (.75 * kaitlyn.SqueezeThrough));
            WalkSpeed = 1f;
            movementForce = 1f;
            animator.SetBool("CrouchBool", true);
        }
        //makes Kaitlyn dive in water
        else
        {
            forceDirection += Vector3.down * JumpForce * 0.5f * (kaitlyn.WaterRespiration + 1);
        }
        //lets Kaitlyn dig, only if she has the Dig power-up
        if(kaitlyn.Dig >= 1)
        {
            IsDigging = true;
        }
    }

    //lets Kaitlyn stand up straight after crouching or sprinting, resetting her height, top speed, and acceleration
    private void DoStand(InputAction.CallbackContext obj)
    {
        //holds Kaitlyn's terrestrial speed
        if(aquatic.isSubmerged == false)
        {
            capsule.height = 3f;
            WalkSpeed = 2f + 2 * kaitlyn.Sprint;
            movementForce = 1f + kaitlyn.Sprint;
        }
        //holds Kaitlyn's aquatic speed
        else
        {
            capsule.height = 3f;
            WalkSpeed = 2f + kaitlyn.Sprint + kaitlyn.WaterRespiration;
            movementForce = (1f + kaitlyn.Sprint) * .5f + kaitlyn.WaterRespiration;
        }
        //animator.SetTrigger("IdleTrigger");
        animator.SetBool("CrouchBool", false);
    }

    //allows Kaitlyn to run, increasing her top speed and acceleration
    private void DoSprint(InputAction.CallbackContext obj)
    {
        //holds Kaitlyn's terrestrial speed
        if(aquatic.isSubmerged == false)
        {
            WalkSpeed = 10f + 10 * kaitlyn.Sprint;
            movementForce = (float)(2.5f + 2.5 * kaitlyn.Sprint);
        }
        //holds Kaitlyn's aquatic speed
        else
        {
            WalkSpeed = 5f + 5 * kaitlyn.Sprint + kaitlyn.WaterRespiration;
            movementForce = (float)(1f + kaitlyn.Sprint + kaitlyn.WaterRespiration);
        }
        //animator.SetTrigger("RunTrigger");
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
            else
            {
                Shoot();
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

    //deletes saved data, and sets everything to zero
    public void Delete()
    {
        kaitlyn.Spawnpoint = null;
        transform.position = worldSpawn.position;
        kaitlyn.HealthPickup = 0;
        kaitlyn.StrengthPickup = 0;
        kaitlyn.Sprint = 0;
        kaitlyn.HighJump = 0;
        kaitlyn.WaterGun = 0;
        kaitlyn.StretchArm = 0;
        kaitlyn.Flamethrower = 0;
        kaitlyn.FrostBreath = 0;
        kaitlyn.Lightningbolt = 0;
        kaitlyn.WaterRespiration = 0;
        kaitlyn.HeatResistance = 0;
        kaitlyn.ColdResistance = 0;
        kaitlyn.ElectricityResistance = 0;
        kaitlyn.ThermalVision = 0;
        kaitlyn.HammerStrike = 0;
        kaitlyn.Invisible = 0;
        kaitlyn.TimeDilation = 0;
        kaitlyn.SqueezeThrough = 0;
        kaitlyn.Dig = 0;
        kaitlyn.Flight = 0;
        kaitlyn.Regeneration = 0;
        eventsSO.ZeroBool = false;
    }

    //allows Kaitlyn to take damage
    public void TakeDamage(int damage)
    {
        //checks if Kaitlyn has taken damage
        if(damage > 0)
        {
            kaitlyn.HP -= damage;
            HUDScript.hurtSprite();
            blood.Play();
            StartCoroutine(StopBleeding());
            kaitlyn.floatHP = kaitlyn.HP;
        }
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
        transform.position = kaitlyn.Spawnpoint.position;
        kaitlyn.HP = kaitlyn.maxHP;
        kaitlyn.floatHP = kaitlyn.HP;
    }

    //runs the function for high jumping
    private void DoHighJump(InputAction.CallbackContext obj)
    {
        //only lets Kaitlyn high jump if she's in contact with the ground, and has collected at least one High Jump power-up
        if (IsGrounded() && kaitlyn.HighJump >= 1)
        {
            forceDirection += Vector3.up * JumpForce * 2;
            animator.SetTrigger("JumpTrigger");
        }
    }

    //allows Kaitlyn to be pushed by currents
    public void Current(Vector3 CurrentForce)
    {
        currentForce = CurrentForce;
    }

    //allows Kaitlyn to shoot
    private void Shoot()
    {
        //allows Kaitlyn to fire the Water Gun, provided that she has collected it
        if(AbilityNumber == 0)
        {
            if(kaitlyn.WaterGun > 0)
            {
                //stores the speed of the water gun
                float speed = 10f;
                //fires the water right ahead of Kaitlyn
                Ray ray = new Ray(transform.position, transform.forward);
                Instantiate(waterGun, transform.position + Vector3.up, Quaternion.LookRotation(ray.direction));
                waterGun.transform.position += waterGun.transform.forward * speed * Time.deltaTime;
            }
        }
        //allows Kaitlyn to fire the Lightning Bolt, provided that she has collected it
        else if(AbilityNumber == 1)
        {
            if(kaitlyn.Lightningbolt > 0)
            {
                //stores the speed of the lightning bolt
                float speed = 10f;
                //fires the electricity right ahead of Kaitlyn
                Ray ray = new Ray(transform.position, transform.forward);
                Instantiate(lightningBolt, transform.position + Vector3.up, Quaternion.LookRotation(ray.direction));
                lightningBolt.transform.position += lightningBolt.transform.forward * speed * Time.deltaTime;
            }
        }
        //allows Kaitlyn to fire the Stretch Arm, provided that she has collected it
        else if(AbilityNumber == 2)
        {
            if(kaitlyn.StretchArm > 0)
            {
                //stores the speed of the stretch arm
                float speed = 10f;
                //extends Kaitlyn's arm right ahead of her
                Ray ray = new Ray(transform.position, transform.forward);
                Instantiate(stretchArm, transform.position + Vector3.up, Quaternion.LookRotation(ray.direction));
                stretchArm.transform.position += stretchArm.transform.forward * speed * Time.deltaTime;
            }
        }
        //allows Kaitlyn to fire the Frost Breath, provided that she has collected it
        else if(AbilityNumber == 3)
        {
            if(kaitlyn.FrostBreath > 0)
            {
                //stores the speed of the frost breath
                float speed = 10f;
                //fires the ultracold gas right ahead of Kaitlyn
                Ray ray = new Ray(transform.position, transform.forward);
                Instantiate(frostBreath, transform.position + Vector3.up, Quaternion.LookRotation(ray.direction));
                frostBreath.transform.position += frostBreath.transform.forward * speed * Time.deltaTime;
            }
        }
        //allows Kaitlyn to fire the Flamethrower, provided that she has collected it
        else
        {
            if(kaitlyn.Flamethrower > 0)
            {
                //stores the speed of the flamethrower
                float speed = 10f;
                //fires the well, fire right ahead of Kaitlyn
                Ray ray = new Ray(transform.position, transform.forward);
                Instantiate(flamethrower, transform.position + Vector3.up, Quaternion.LookRotation(ray.direction));
                flamethrower.transform.position += flamethrower.transform.forward * speed * Time.deltaTime;
            }
        }
    }

    //runs the function for Kaitlyn changing her abilities
    private void DoChange(InputAction.CallbackContext obj)
    {
        float inputValue = controls.Kaitlyn.Change.ReadValue<float>();
        AbilityNumber += inputValue;
        if(AbilityNumber > NumCap)
        {
            AbilityNumber = 0;
        }
        else if(AbilityNumber < 0)
        {
            AbilityNumber = NumCap;
        }
    }
}
