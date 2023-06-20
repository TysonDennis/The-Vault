using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stake : MonoBehaviour
{
    //holds the initial altitude of the stake
    [SerializeField]
    private Transform initialPos;
    //holds the final altitude of the stake
    [SerializeField]
    private Transform finalPos;
    //holds the event for when the stake gets pounded down
    [SerializeField]
    private UnityEvent onPound;

    //sets the position at the starting position upon start
    private void Awake()
    {
        transform.position = initialPos.position;
    }

    //holds the script for when the stake gets pounded down
    public void PoundDown()
    {
        transform.position = finalPos.position;
        onPound.Invoke();
    }
}
