using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flea : MonoBehaviour
{
    //holds the flea's components
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform attackHitbox;
    //holds the flea's stats
    [SerializeField]
    private float detectionRange;
    [SerializeField]
    private float speed;
    public int attackStrength;
    [SerializeField]
    private bool inRange;
    //holds the reference to the player
    public GameObject player;
    [SerializeField]
    private Vector3 targetPosition;

    private void Awake()
    {
        //gets the flea's components
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //gets the player, who is not in attack range
        player = GameObject.FindGameObjectWithTag("Player");
        //sets the bool for if the player is in range and if the attack hitbox is on to false
        inRange = false;
        attackHitbox.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //makes the flea follow the player, if Kaitlyn is within range
        if(Vector3.Distance(transform.position, player.transform.position) <= detectionRange && player.GetComponent<Player>().IsInvisible == false)
        {
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        //makes the flea jump
        if (IsGrounded() && inRange == false)
        {
            rb.velocity = new Vector3(0, 10, 0);
        }
        //makes the flea try to attack the player with a attack with raycast-based detection
        else if (inRange == true)
        {
            //grounds the flea in place
            rb.velocity = Vector3.zero;
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

    //checks if the flea is on solid ground
    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, 2.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //allows the flea to feel the player
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            transform.LookAt(targetPosition);
            inRange = true;
        }
    }

    //holds the event for when the player is out of the flea's range
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.LookAt(targetPosition);
            inRange = false;
        }
    }

    //holds the attack
    private IEnumerator ActivateAttack()
    {
        attackHitbox.SendMessage("Damage", attackStrength);
        yield return new WaitForSeconds(.5f);
        attackHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        attackHitbox.gameObject.SetActive(false);
    }
}
