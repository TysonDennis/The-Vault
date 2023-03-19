using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    //holds the player
    [SerializeField]
    Transform player;
    //holds the camera's positions relative to Kaitlyn
    [SerializeField]
    float xPos;
    [SerializeField]
    float yPos;
    //z for zoom
    [SerializeField]
    float zPos;
    //holds the minimum and maximum zoom levels
    [SerializeField]
    float minZ;
    [SerializeField]
    float maxZ;
    //holds the sensitivity
    [SerializeField]
    float sensitivity;
    //holds the player's controls
    [SerializeField]
    private PlayerControls controls;
    [SerializeField]
    private InputAction zoom;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        zoom = controls.Kaitlyn.Zoom;
        controls.Kaitlyn.Enable();
    }

    private void OnDisable()
    {
        controls.Kaitlyn.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(xPos, yPos, zPos);;
        //zPos = Mathf.Clamp(zPos, minZ, maxZ);
    }
}
