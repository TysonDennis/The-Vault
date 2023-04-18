using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    //holds the prefab that is spawned in
    public GameObject prefab;
    //holds the spawn condition
    public bool spawnCondition;

    //instantiates the prefab when the game is started, and if conditions are met
    public void Awake()
    {
        if(spawnCondition == true)
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
