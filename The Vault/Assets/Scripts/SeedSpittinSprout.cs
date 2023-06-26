using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpittinSprout : MonoBehaviour
{
    //holds the Seed-Spittin' Sprout's components
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform leafSensor;
    [SerializeField]
    private GameObject seed;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private AudioSource audio;
    //holds the reference to the player
    public GameObject player;
    [SerializeField]
    private Vector3 targetPosition;
    //holds the Seed-Spittin' Sprout's stats
    [SerializeField]
    private float detectionRange;
    [SerializeField]
    private bool inRange;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHP;

    private void Awake()
    {
        //gets the Seed-Spittin' Sprout's components
        animator = GetComponent<Animator>();
        particles = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        //gets the player
        player = GameObject.FindGameObjectWithTag("Player");
        //sets the bool for if the player is within range to false, and sets cooldown at 0
        inRange = false;
        cooldown = 0f;
        //sets the sprout's health at max
        health = maxHP;
    }

    private void FixedUpdate()
    {
        //makes the Seed-Spittin' Sprout target the player, if Kaitlyn is within range
        targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        if(Vector3.Distance(transform.position, player.transform.position) <= detectionRange && player.GetComponent<Player>().IsInvisible == false)
        {
            EnterRange();
            transform.LookAt(targetPosition);
            //inRange = true;
        }
        else
        {
            ExitRange();
        }
        //runs the function for when the player is in range
        if(inRange == true && cooldown == 0)
        {
            //uses a raycast to aim
            Ray ray = new Ray(transform.position + transform.up + transform.forward, transform.forward);
            if(Physics.Raycast(ray, out RaycastHit hit, 11f))
            {
                if(hit.transform.gameObject.tag == "Player")
                {
                    Shoot();
                }
            }
        }
        //makes the cooldown go down, stopping it from going below 0
        cooldown -= Time.fixedDeltaTime;
        if(cooldown < 0)
        {
            cooldown = 0;
        }
        //kills the sprout
        if(health <= 0)
        {
            StartCoroutine(Kill());
        }
    }

    //holds the event for the player entering range
    public void EnterRange()
    {
        inRange = true;
    }

    //holds the event for the player exiting range
    public void ExitRange()
    {
        inRange = false;
    }

    //holds the function for the sprout shooting
    private void Shoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        cooldown = .83f;
        animator.SetTrigger("ShootTrigger");
        audio.Play();
        Instantiate(seed, transform.position + Vector3.up * 2 + transform.forward, Quaternion.LookRotation(ray.direction));
    }

    //holds the function for taking damage
    public void TakeDamage(int damage)
    {
        animator.SetTrigger("DamageTrigger");
        particles.Play();
        health -= damage;
        StartCoroutine(StopParticles());
    }

    //stops the particles after a delay
    private IEnumerator StopParticles()
    {
        yield return new WaitForSeconds(1f);
        particles.Stop();
    }

    //holds the function for when the sprout is killed
    private IEnumerator Kill()
    {
        animator.SetTrigger("DeathTrigger");
        particles.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
