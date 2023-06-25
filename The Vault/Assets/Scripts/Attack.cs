using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //references to other scripts
    [SerializeField]
    private Player player;
    //holds the hitbox's stats
    [SerializeField]
    private int power;
    [SerializeField]
    private float speed;

    //connects to the player script
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    //the function for holding the damage
    public void Damage(int damage)
    {
        power = damage;
    }

    //the function for damaging the player if they enter the hitbox
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.SendMessage("TakeDamage", power);
        }
    }

    //the function for damaging the player if it's a solid collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.SendMessage("TakeDamage", power);
            Destroy(gameObject);
        }
    }

    //holds projectile movement
    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }
}
