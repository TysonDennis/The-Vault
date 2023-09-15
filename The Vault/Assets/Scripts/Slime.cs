using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    //holds the slime's components
    [SerializeField]
    private NavMeshAgent nva;
    private Animator animator;
    int speedHash = Animator.StringToHash("Speed");
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private ParticleSystem particles;
    //holds the slime's stats
    [SerializeField]
    private float DetectionRange;
    [SerializeField]
    private int damage;
    //holds the references
    public GameObject player;
    [SerializeField]
    private HUD hud;
    [SerializeField]
    private KaitlynSO kaitlyn;

    private void Awake()
    {
        //gets the slime's components
        nva = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
        //gets other objects in the scene
        player = GameObject.FindGameObjectWithTag("Player");
        hud = GameObject.FindObjectOfType<HUD>();
    }

    private void FixedUpdate()
    {
        animator.SetFloat(speedHash, nva.velocity.magnitude / nva.speed);
        //makes the slime pursue the player
        if (Vector3.Distance(transform.position, player.transform.position) <= DetectionRange)
        {
            nva.destination = player.transform.position;
        }
    }

    //holds what happens when Kaitlyn comes in contact with the slime
    private void OnCollisionStay(Collision collision)
    {
        //slows Kaitlyn upon touching the slime, and causes her to continuously take damage
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().IsSlowed = true;
            hud.hurtSprite();
            kaitlyn.floatHP -= damage * Time.fixedDeltaTime;
            kaitlyn.HP = Mathf.RoundToInt(kaitlyn.floatHP);
            player.GetComponent<Player>().IsInvisible = false;
        }
        //makes the slime die upon touching an elemental object
        else if (collision.gameObject.CompareTag("ElectricEffect"))
        {
            StartCoroutine(Kill());
        }
        else if (collision.gameObject.CompareTag("FireEffect"))
        {
            StartCoroutine(Kill());
        }
        else if (collision.gameObject.CompareTag("IceEffect"))
        {
            StartCoroutine(Kill());
        }
        else if (collision.gameObject.CompareTag("WaterEffect"))
        {
            StartCoroutine(Kill());
        }
    }

    //makes the slime die from an elemental effect
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ElectricEffect"))
        {
            StartCoroutine(Kill());
        }
        else if (other.gameObject.CompareTag("FireEffect"))
        {
            StartCoroutine(Kill());
        }
        else if (other.gameObject.CompareTag("IceEffect"))
        {
            StartCoroutine(Kill());
        }
        else if (other.gameObject.CompareTag("WaterEffect"))
        {
            StartCoroutine(Kill());
        }
    }

    //holds what happens when Kaitlyn stops contact with the slime
    private void OnCollisionExit(Collision collision)
    {
        //lets Kaitlyn return to her normal speed
        if (collision.gameObject.CompareTag("Player"))
        {
            audio.Play();
            player.GetComponent<Player>().IsSlowed = false;
        }
    }

    //holds the coroutine for the slime's death
    private IEnumerator Kill()
    {
        player.GetComponent<Player>().IsSlowed = false;
        particles.Play();
        audio.Play();
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
