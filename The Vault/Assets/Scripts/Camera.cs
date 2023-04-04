using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    //holds the player
    [SerializeField]
    private Transform player;
    //holds the camera's positions
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
    //holds the camera-based input actions
    [SerializeField]
    private InputAction zoom;
    [SerializeField]
    private InputAction rotate;

    //activates upon start
    private void Awake()
    {
        //accesses the action asset that controls Kaitlyn
        controls = new PlayerControls();
        //sets the initial position of the camera relative to Kaitlyn
        transform.position = player.transform.position + new Vector3(0f, 3f, -3f);
    }

    //enables the functions of the input actions
    private void OnEnable()
    {
        controls.Kaitlyn.Zoom.started += DoZoom;
        //controls.Kaitlyn.Rotate.started += DoRotate;
        rotate = controls.Kaitlyn.Rotate;
        controls.Kaitlyn.Enable();
    }

    //disables the functions of the input actions
    private void OnDisable()
    {
        controls.Kaitlyn.Zoom.started -= DoZoom;
        //controls.Kaitlyn.Rotate.started -= DoRotate;
        controls.Kaitlyn.Disable();
    }

    //holds the camera's movement
    void Update()
    {
        //holds the sines and cosines of the camera's azimuth and altitude
        float sx = Mathf.Sin(xPos), sy = Mathf.Sin(yPos), cx = Mathf.Cos(xPos), cy = Mathf.Cos(yPos);
        //sets the position and rotation of the camera
        transform.SetPositionAndRotation(new Vector3(sy, sx, cx * cy) * zPos + player.transform.position, Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up));
        float inputValueAzimuth = rotate.ReadValue<Vector2>().x * sensitivity * 17.45f * Time.deltaTime;
        float inputValueAltitude = rotate.ReadValue<Vector2>().y * sensitivity * 17.45f * Time.deltaTime;
        yPos += inputValueAzimuth;
        xPos += inputValueAltitude;
    }

    //rotates the camera
    /*private void DoRotate(InputAction.CallbackContext obj)
    {
        float inputValueAzimuth = obj.ReadValue<Vector2>().x * sensitivity * 17.45f;
        float inputValueAltitude = obj.ReadValue<Vector2>().y * sensitivity * 17.45f;
        yPos += inputValueAzimuth;
        xPos += inputValueAltitude;
    }*/

    //zooms the camera
    private void DoZoom (InputAction.CallbackContext obj)
    {
        float inputValue = obj.ReadValue<float>() * sensitivity;
        zPos += inputValue;
        zPos = Mathf.Clamp(zPos, minZ, maxZ);
    }
}
