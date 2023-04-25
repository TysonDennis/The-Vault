using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDamageZone : MonoBehaviour
{
    //holds the damage that the area deals per second
    [SerializeField]
    private int damage;
    //holds the base damage the area deals
    [SerializeField]
    private int baseDamage;
    //checks if the player is in the damaging zone
    [SerializeField]
    private bool isIn;
    //holds the scriptable object for the player character
    [SerializeField]
    private KaitlynSO kaitlyn;
    //holds the player
    [SerializeField]
    private Player player;
    //holds the delay time
    [SerializeField]
    private float delay;
    //holds how long the player is safe from damage
    [SerializeField]
    private float safetyTime;
    //holds the max amount of time the player is safe from damage
    [SerializeField]
    private float maxSafetyTime;

    //starts with isIn set to false
    private void Awake()
    {
        isIn = false;
        safetyTime = maxSafetyTime;
    }

    //deals continuous damage
    private void FixedUpdate()
    {
        if(isIn == true)
        {
            //depletes Kaitlyn's safety time, as long as she is in
            if(safetyTime >= 0)
            {
                safetyTime -= Time.deltaTime;
                StopCoroutine(DamageDelay());
            }
            else
            {
                safetyTime = 0;
                //applies damage based on Kaitlyn's heat resistance
                if (gameObject.tag == "FireEffect")
                {
                    damage = baseDamage - 10 * kaitlyn.HeatResistance;
                    player.transform.gameObject.SendMessage("TakeDamage", damage);
                    StartCoroutine(DamageDelay());
                }
                //applies damage based on Kaitlyn's cold resistance
                else if (gameObject.tag == "IceEffect")
                {
                    damage = baseDamage - 10 * kaitlyn.ColdResistance;
                    player.transform.gameObject.SendMessage("TakeDamage", damage);
                    StartCoroutine(DamageDelay());
                }
                //applies damage based on Kaitlyn's electricity resistance
                else if (gameObject.tag == "ElectricEffect")
                {
                    damage = baseDamage - 10 * kaitlyn.ElectricityResistance;
                    player.transform.gameObject.SendMessage("TakeDamage", damage);
                    StartCoroutine(DamageDelay());
                }
                //applies damage without Kaitlyn's resistances
                else
                {
                    damage = baseDamage;
                    player.transform.gameObject.SendMessage("TakeDamage", damage);
                    StartCoroutine(DamageDelay());
                }
            }
        }
        else
        {
            safetyTime += Time.deltaTime;
            StopCoroutine(DamageDelay());
            damage = 0;
            if(safetyTime > maxSafetyTime)
            {
                safetyTime = maxSafetyTime;
            }
        }
    }

    //activates if the player is in the damaging area
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            isIn = true;
        }
    }

    //activates once the player leaves the damaging area
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            StopCoroutine(DamageDelay());
            isIn = false;
        }
    }

    //delays the damage taken
    IEnumerator DamageDelay()
    {
        isIn = false;
        yield return new WaitForSeconds(delay);
        //isIn = true;
    }
}
