using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    //holds the rigidbody of the projectile
    [SerializeField]
    private Rigidbody rb;
    //holds the speed of the projectile
    [SerializeField]
    private float speed;
    //holds the lifespan of the projectile
    [SerializeField]
    private float lifespan;
    //holds the amount of damage the projectile does
    [SerializeField]
    private int damage;
    //holds the reference to the player script
    public Player player;
    //holds the reference to Kaitlyn's scriptable object
    [SerializeField]
    private KaitlynSO kaitlyn;
    //communicates to the grabbable script
    public Grabbable grabbable;
    //gets the holdspace
    public Transform holdSpace;

    //assigns the rigidbody of the projectile, and the damage the projectile does
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = Object.FindObjectOfType<Player>();
        holdSpace = player.holdSpace;
        //assigns damage values based on the ability Kaitlyn has selected, her strength, and her ability level
        if(player.AbilityNumber == 0)
        {
            damage = 2 + kaitlyn.StrengthPickup + kaitlyn.WaterGun;
        }
        else if(player.AbilityNumber == 1)
        {
            damage = 2 + kaitlyn.StrengthPickup + kaitlyn.Lightningbolt;
        }
        else if(player.AbilityNumber == 2)
        {
            damage = 2 + kaitlyn.StrengthPickup + kaitlyn.StretchArm;
        }
        else if(player.AbilityNumber == 3)
        {
            damage = 2 + kaitlyn.StrengthPickup + kaitlyn.FrostBreath;
        }
        else
        {
            damage = 2 + kaitlyn.StrengthPickup + kaitlyn.Flamethrower;
        }
    }

    //Holds the movement and the lifespan of the projectile
    void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
        lifespan -= Time.fixedDeltaTime;
        //destroys the projectile if its lifespan hits 0
        if(lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Holds the projectile's interactions
    private void OnTriggerEnter(Collider other)
    {
        //makes the projectile damage objects and terminate
        if(other.tag == "Damageable")
        {
            other.transform.gameObject.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
        //lets the stretch arm grab grabbable objects
        if(player.AbilityNumber == 2 && other.transform.TryGetComponent(out grabbable))
        {
            other.GetComponent<Grabbable>();
            //player.grabbable.Grab(holdSpace: player.holdSpace);
            grabbable.Grab(holdSpace);
        }
    }
}
