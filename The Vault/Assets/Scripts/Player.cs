using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    CharacterController character;
    [SerializeField]
    float gravity;
    [SerializeField]
    float WalkSpeed;
    Vector3 velocity;
    Vector3 move;
    [SerializeField]
    float terminalVelocity;
    [SerializeField]
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        character.Move(move * WalkSpeed * Time.deltaTime);
        if (isGrounded == true)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y = -gravity * Time.deltaTime;
            character.Move(velocity * Time.deltaTime);
            if(velocity.y < terminalVelocity)
            {
                velocity.y = terminalVelocity;
            }
        }
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, -1);
        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
