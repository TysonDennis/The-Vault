using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    //stores Kaitlyn's associated scriptable object
    [SerializeField]
    KaitlynSO kaitlyn;
    //stores the spawnpoint's transforms
    [SerializeField]
    private Transform transform;

    //gets the spawnpoint's transforms upon awake
    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    //sets Kaitlyn's spawnpoint to the position of the spawnpoint
    private void OnTriggerEnter(Collider other)
    {
        //checks if Kaitlyn has entered the spawnpoint
        if(other.transform.gameObject.tag == "Player")
        {
            //sets Kaitlyn's spawnpoint to be the transforms of the spawnpoint
            kaitlyn.SetSpawnPosition(transform);
        }
    }
}
