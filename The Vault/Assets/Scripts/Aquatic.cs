using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquatic : MonoBehaviour
{
    //checks if Kaitlyn's head is underwater
    public bool isSubmerged;
    //holds Kaitlyn's buoyancy
    [SerializeField]
    private Vector3 buoyancy;
    //holds the player script
    [SerializeField]
    private Player player;

    //sets the isSubmerged to false
    private void Awake()
    {
        isSubmerged = false;
    }

    //allows Kaitlyn to float in water
    public void FloatInWater(float waterDensity)
    {
        buoyancy = 9.81f * Vector3.up * waterDensity / player.density;
    }

    //applies force to Kaitlyn's main rigidbody
    private void FixedUpdate()
    {
        if(isSubmerged == true)
        {
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            playerRB.AddForce(buoyancy, ForceMode.Force);
        }
    }
}
