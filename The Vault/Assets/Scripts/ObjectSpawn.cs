using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    //holds the prefab that is spawned in
    public GameObject prefab;

    //instantiates the prefab when the game is started
    void Awake()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
