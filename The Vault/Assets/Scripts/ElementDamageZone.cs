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

    //starts with isIn set to false
    private void Awake()
    {
        isIn = false;
    }

    //deals continuous damage
    private void FixedUpdate()
    {
        if(isIn == true)
        {
            //applies damage based on Kaitlyn's heat resistance
            if (gameObject.tag == "FireEffect")
            {
                damage = baseDamage - 10 * kaitlyn.HeatResistance;
                if(damage < 0)
                {
                    damage = 0;
                }
                kaitlyn.floatHP -= damage * Time.fixedDeltaTime;
                kaitlyn.HP = Mathf.RoundToInt(kaitlyn.floatHP);
            }
            //applies damage based on Kaitlyn's cold resistance
            else if (gameObject.tag == "IceEffect")
            {
                damage = baseDamage - 10 * kaitlyn.ColdResistance;
                if (damage < 0)
                {
                    damage = 0;
                }
                kaitlyn.floatHP -= damage * Time.fixedDeltaTime;
                kaitlyn.HP = Mathf.RoundToInt(kaitlyn.floatHP);
            }
            //applies damage based on Kaitlyn's electricity resistance
            else if (gameObject.tag == "ElectricEffect")
            {
               damage = baseDamage - 10 * kaitlyn.ElectricityResistance;
               if (damage < 0)
               {
                   damage = 0;
               }
               kaitlyn.floatHP -= damage * Time.fixedDeltaTime;
               kaitlyn.HP = Mathf.RoundToInt(kaitlyn.floatHP);
            }
            //applies damage without Kaitlyn's resistances
            else
            {
               damage = baseDamage;
               kaitlyn.floatHP -= damage * Time.fixedDeltaTime;
               kaitlyn.HP = Mathf.RoundToInt(kaitlyn.floatHP);
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
            isIn = false;
        }
    }
}
