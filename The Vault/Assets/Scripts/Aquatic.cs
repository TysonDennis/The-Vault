using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquatic : MonoBehaviour
{
    //checks if Kaitlyn's head is underwater
    public bool isSubmerged;
    //holds Kaitlyn's buoyancy
    [SerializeField]
    private Vector3 buoyancy;
    //holds the player script
    public Player player;
    //holds Kaitlyn's max oxygen
    public float maxO2;
    //holds Kaitlyn's oxygen
    public float oxygen;
    //holds the drowning damage
    [SerializeField]
    private int damage;
    //holds the delay to taking damage
    [SerializeField]
    private float delay;
    //gets the scriptable object for Kaitlyn
    [SerializeField]
    private KaitlynSO kaitlyn;

    //sets the isSubmerged to false, and sets the oxygen to max
    private void Awake()
    {
        isSubmerged = false;
        oxygen = maxO2;
    }

    //allows Kaitlyn to float in water
    public void FloatInWater(float waterDensity)
    {
        buoyancy = 9.81f * Vector3.up * waterDensity / player.density;
    }

    //applies force to Kaitlyn's main rigidbody
    private void FixedUpdate()
    {
        if(isSubmerged == true)
        {
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            playerRB.AddForce(buoyancy, ForceMode.Force);
            //depletes Kaitlyn's oxygen as long as she is underwater, and doesn't have Water Respiration
            if(kaitlyn.WaterRespiration == 0)
            {
                oxygen -= Time.deltaTime;
            }
            //stops Kaitlyn's oxygen from going beneath 0, and damages her if her oxygen is below 0
            if(oxygen < 0)
            {
                oxygen = 0;
                player.transform.gameObject.SendMessage("TakeDamage", damage);
                player.GetComponent<Player>().IsInvisible = false;
                StartCoroutine(DamageDelay());
            }
        }
        //replenishes Kaitlyn's oxygen while her head isn't submerged
        else
        {
            oxygen += Time.deltaTime;
            //stops Kaitlyn's oxygen from overflowing
            if(oxygen > maxO2)
            {
                oxygen = maxO2;
            }
        }
    }

    //delays Kaitlyn taking damage
    IEnumerator DamageDelay()
    {
        isSubmerged = false;
        yield return new WaitForSeconds(delay);
        isSubmerged = true;
    }
}
