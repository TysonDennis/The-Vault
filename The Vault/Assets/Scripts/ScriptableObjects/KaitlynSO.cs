using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KaitlynSO", menuName = "Game Data/KaitlynSO")]
public class KaitlynSO : ScriptableObject
{
    //stores Kaitlyn's spawn position
    public Transform Spawnpoint;
    //stores Kaitlyn's max HP
    public int maxHP;
    //stores Kaitlyn's HP
    public int HP;

    //sets Kaitlyn's spawn position
    public void SetSpawnPosition(Transform position)
    {
        Spawnpoint = position;
        Debug.Log("Spawn point set!");
    }
}
