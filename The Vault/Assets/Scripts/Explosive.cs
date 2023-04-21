using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosive : MonoBehaviour
{
    //holds the damage the explosion deals
    [SerializeField]
    private int damage;
    //holds the explosion's lifespan
    [SerializeField]
    private float lifespan;
    //holds Kaitlyn's scriptable object
    [SerializeField]
    KaitlynSO kaitlyn;
    //holds the base damage of the explosion
    [SerializeField]
    private int baseDamage;
    //holds the event for when the object explodes
    public UnityEvent onExplode;
    //holds the script for ice
    [SerializeField]
    private Ice ice;

    //calls the coroutine that holds the explosion's lifespan
    private void Awake()
    {
        StartCoroutine(Duration());
        onExplode.Invoke();
        //Ice ice = GameObject.GetComponent<Ice>();
    }

    //holds the function for if something's caught in the blast
    private void OnTriggerEnter(Collider other)
    {
        //holds the function for damaging Kaitlyn
        if(other.transform.gameObject.tag == "Player")
        {
            //applies damage based on Kaitlyn's heat resistance
            if(gameObject.tag == "FireEffect")
            {
                damage = baseDamage - 10 * kaitlyn.HeatResistance;
                other.transform.gameObject.SendMessage("TakeDamage", damage);
            }
            //applies damage based on Kaitlyn's cold resistance
            else if(gameObject.tag == "IceEffect")
            {
                damage = baseDamage - 10 * kaitlyn.ColdResistance;
                other.transform.gameObject.SendMessage("TakeDamage", damage);
            }
            //applies damage based on Kaitlyn's electricity resistance
            else if(gameObject.tag == "ElectricEffect")
            {
                damage = baseDamage - 10 * kaitlyn.ElectricityResistance;
                other.transform.gameObject.SendMessage("TakeDamage", damage);
            }
            //applies damage without Kaitlyn's resistances
            else
            {
                damage = baseDamage;
                other.transform.gameObject.SendMessage("TakeDamage", damage);
            }
        }

        //holds the function for interacting with ice
        if(other.transform.gameObject.tag == "DestroyedByFire" && gameObject.tag == "FireEffect")
        {
            //ice.Melting();
            ice.GetComponent<Ice>().Melting();
        }
    }

    //makes the explosion disappear after some time
    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(transform.gameObject);
    }
}
