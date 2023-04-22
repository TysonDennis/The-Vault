using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Generator : MonoBehaviour
{
    //holds the events for if the generator is charged or uncharged
    [SerializeField]
    private UnityEvent onDead, onAlive;
    //holds how long the generator holds a charge
    [SerializeField]
    private float lifetime;
    //holds the current charge of the generator
    [SerializeField]
    private float charge;
    //holds the display material
    [SerializeField]
    private Material display;

    //sets the generator to dead upon start
    private void Awake()
    {
        charge = 0;
        display = GetComponent<Renderer>().material;
        display.SetColor("_Color", Color.black);
    }

    //determines what event plays, depending on how much charge there is left
    private void FixedUpdate()
    {
        if(charge > 0)
        {
            onAlive.Invoke();
            //makes the charge run out over time
            charge -= Time.fixedDeltaTime;
            //sets the color to green when it has power
            display.SetColor("_Color", Color.green);
        }
        else
        {
            onDead.Invoke();
            //prevents the charge from falling below 0
            charge = 0;
            //sets the color to black when out of power
            display.SetColor("_Color", Color.black);
        }
    }

    //allows the generator to be powered by electricity
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ElectricEffect"))
        {
            charge = lifetime;
        }
    }
}
