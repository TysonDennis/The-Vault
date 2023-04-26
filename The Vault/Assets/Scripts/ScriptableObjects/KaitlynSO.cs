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
    //stores an approximate decimal value to Kaitlyn's HP
    public float floatHP;
    //stores number of health power-ups Kaitlyn has collected
    public int HealthPickup;
    //stores number of strength power-ups Kaitlyn has collected
    public int StrengthPickup;
    //stores sprint power-ups
    public int Sprint;
    //stores high jump power-ups
    public int HighJump;
    //stores water gun power-ups
    public int WaterGun;
    //stores stretch arm power-ups
    public int StretchArm;
    //stores flamethrower power-ups
    public int Flamethrower;
    //stores frost breath power-ups
    public int FrostBreath;
    //stores lightningbolt power-ups
    public int Lightningbolt;
    //stores water respiration power-ups
    public int WaterRespiration;
    //stores heat resistance power-ups
    public int HeatResistance;
    //stores cold resistance power-ups
    public int ColdResistance;
    //stores electricity resistance power-ups
    public int ElectricityResistance;
    //stores thermal vision power-ups
    public int ThermalVision;
    //stores hammer strike power-ups
    public int HammerStrike;
    //stores invisible power-ups
    public int Invisible;
    //stores time dilation power-ups
    public int TimeDilation;
    //stores squeeze through power-ups
    public int SqueezeThrough;
    //stores dig power-ups
    public int Dig;
    //stores flight power-ups
    public int Flight;
    //stores regeneration power-ups
    public int Regeneration;

    //sets Kaitlyn's spawn position
    public void SetSpawnPosition(Transform position)
    {
        Spawnpoint = position;
        Debug.Log("Spawn point set!");
    }
}
