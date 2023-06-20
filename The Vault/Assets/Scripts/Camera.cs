using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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
    //holds Kaitlyn's scriptable object
    [SerializeField]
    private KaitlynSO kaitlyn;
    //holds the bool for if thermal vision is on or off
    [SerializeField]
    private bool thermal;
    //holds the cameras
    [SerializeField]
    private Camera visibleSpectrum;
    [SerializeField]
    private Camera infraredSpectrum;
    //holds the post processing volumes
    /*[SerializeField]
    private GameObject environment;
    [SerializeField]
    private GameObject hot;*/

    //activates upon start
    private void Awake()
    {
        //accesses the action asset that controls Kaitlyn
        controls = new PlayerControls();
        //sets the initial position of the camera relative to Kaitlyn
        transform.position = player.transform.position + new Vector3(0f, 3f, -3f);
        //sets certain things to inactive and active
        visibleSpectrum.enabled = true;
        infraredSpectrum.enabled = false;
        /*environment.SetActive(false);
        hot.SetActive(false);*/
    }

    //enables the functions of the input actions
    private void OnEnable()
    {
        controls.Kaitlyn.Zoom.started += DoZoom;
        controls.Kaitlyn.ThermalVision.started += DoThermalVision;
        rotate = controls.Kaitlyn.Rotate;
        controls.Kaitlyn.Enable();
    }

    //disables the functions of the input actions
    private void OnDisable()
    {
        controls.Kaitlyn.Zoom.started -= DoZoom;
        controls.Kaitlyn.ThermalVision.started -= DoThermalVision;
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
        //shows the view of what camera is selected
        if(thermal == true)
        {
            visibleSpectrum.enabled = false;
            infraredSpectrum.enabled = true;
            /*environment.SetActive(true);
            hot.SetActive(true);*/
        }
        else
        {
            visibleSpectrum.enabled = true;
            infraredSpectrum.enabled = false;
            /*environment.SetActive(false);
            hot.SetActive(false);*/
        }
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

    //lets Kaitlyn alternate between thermal and normal vision, if she has the ability
    private void DoThermalVision(InputAction.CallbackContext obj)
    {
        if (kaitlyn.ThermalVision > 0)
        {
            if(thermal == false)
            {
                thermal = true;
            }
            else
            {
                thermal = false;
            }
        }
    }
}
