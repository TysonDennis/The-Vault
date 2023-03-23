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
    Vector3 offset;
    //z for zoom
    [SerializeField]
    float offsetDistance;
    //holds the minimum and maximum zoom levels
    [SerializeField]
    float minOffset;
    [SerializeField]
    float maxOffset;
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
        offset = transform.position - player.transform.position;
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
        transform.position = player.transform.position + new Vector3(xPos, yPos, zPos);
        zPos += zoom.ReadValue<float>() * sensitivity;
        zPos = Mathf.Clamp(zPos, minZ, maxZ);
        //transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
    }

    private void DoRotate(InputAction.CallbackContext obj)
    {
        //transform.RotateAround(player.transform.position, Vector3.up, obj.ReadValue<Vector2>().x * sensitivity * Time.deltaTime);
        //transform.RotateAround(player.transform.position, Vector3.up, obj.ReadValue<Vector2>().y * sensitivity * Time.deltaTime);
        float inputValue = obj.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, inputValue * sensitivity * 1000 + transform.rotation.eulerAngles.y, 0f);
    }
}
