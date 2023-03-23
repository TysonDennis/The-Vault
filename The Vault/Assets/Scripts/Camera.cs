using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    //holds the player
    [SerializeField]
    private Transform player;
    //holds the camera's positions relative to Kaitlyn
    [SerializeField]
    float xPos;
    [SerializeField]
    float yPos;
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
    [SerializeField]
    private InputAction rotate;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        zoom = controls.Kaitlyn.Zoom;
        controls.Kaitlyn.Rotate.started += DoRotate;
        controls.Kaitlyn.Enable();
    }

    private void OnDisable()
    {
        controls.Kaitlyn.Rotate.started -= DoRotate;
        controls.Kaitlyn.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        zPos = Vector3.Distance(player.transform.position, transform.position);
        zPos += zoom.ReadValue<float>() * sensitivity;
        zPos = Mathf.Clamp(zPos, minZ, maxZ);
        transform.position = player.transform.position + new Vector3(xPos, yPos, zPos);
        //transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
    }

    private void DoRotate(InputAction.CallbackContext obj)
    {
        float inputValue = obj.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, inputValue * sensitivity * 1000 + transform.rotation.eulerAngles.y, 0f);
        //transform.RotateAround(player.transform.position, Vector3.up, inputValue * sensitivity * 1000);
    }
}
