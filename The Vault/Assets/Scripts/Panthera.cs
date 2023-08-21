using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Panthera : MonoBehaviour
{
    //holds Panthera's stats
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private float detectionRange;
    public int attackStrength;
    [SerializeField]
    private bool inRange;
    //holds Panthera's components
    [SerializeField]
    private NavMeshAgent nva;
    [SerializeField]
    private Transform attackHitbox;
    [SerializeField]
    private ParticleSystem particle;
    //holds the reference to the player
    public GameObject player;

    private void Awake()
    {
        //sets health to max upon activation
        health = maxHealth;
        //gets the components
        nva = GetComponent<NavMeshAgent>();
        particle = GetComponent<ParticleSystem>();
        //gets the player
        player = GameObject.FindGameObjectWithTag("Player");
        //sets Panthera to out of range
        inRange = false;
        attackHitbox.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        //activates the Kill function if HP falls below 0
        if(health <= 0)
        {
            StartCoroutine(Kill());
        }
        //makes the Panthera follow Kaitlyn if she's within detection range, and is visible
        if(Vector3.Distance(transform.position, player.transform.position) <= detectionRange && player.GetComponent<Player>().IsInvisible == false)
        {
            nva.destination = player.transform.position;
        }
        //makes the Panthera attack Kaitlyn when in range
        if(inRange == true)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 2.5f))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    StartCoroutine(ActivateAttack());
                }
            }
        }
    }

    //lets Panthera feel the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            nva.destination = player.transform.position;
            inRange = true;
        }
    }

    //holds the function for taking damage
    public void TakeDamage(int damage)
    {
        particle.Play();
        health -= damage;
        StartCoroutine(StopBleeding());
    }

    //kills Panthera
    private IEnumerator Kill()
    {
        particle.Play();
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    //holds the coroutine for the blood stopping
    private IEnumerator StopBleeding()
    {
        yield return new WaitForSeconds(1f);
        particle.Stop();
    }

    //attacks Kaitlyn
    private IEnumerator ActivateAttack()
    {
        attackHitbox.SendMessage("Damage", attackStrength);
        yield return new WaitForSeconds(.5f);
        attackHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        attackHitbox.gameObject.SetActive(false);
    }
}
