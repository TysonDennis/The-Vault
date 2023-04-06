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

    //calls the coroutine that holds the explosion's lifespan
    private void Awake()
    {
        StartCoroutine(Duration());
        onExplode.Invoke();
    }

    //holds the function for damaging
    private void OnTriggerEnter(Collider other)
    {
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
    }

    //makes the explosion disappear after some time
    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(transform.gameObject);
    }
}
