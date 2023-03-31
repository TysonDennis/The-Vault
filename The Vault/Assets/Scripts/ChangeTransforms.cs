using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTransforms : MonoBehaviour
{
    //holds the object's starting position
    [SerializeField]
    private Transform sPos;
    //holds the object's finishing position
    [SerializeField]
    private Transform fPos;
    //holds the object's move speed
    [SerializeField]
    private float speed;
    //holds the object's rotation speed
    [SerializeField]
    private float rotationSpeed;
    //holds the delays
    [SerializeField]
    private float StartDelay;
    [SerializeField]
    private float FinishDelay;
    //holds the bools for if they move
    [SerializeField]
    private bool StartMove;
    [SerializeField]
    private bool FinishMove;

    //sets the bools to false
    private void Awake()
    {
        StartMove = false;
        FinishMove = false;
    }

    //holds the movement
    private void FixedUpdate()
    {
        if(StartMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, fPos.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, fPos.rotation, rotationSpeed * Time.deltaTime);
            //checks if the position of the object and its destination are approximately equal
            if (Vector3.Distance(transform.position, fPos.position) < 0.001f && Quaternion.Angle(transform.rotation, fPos.rotation) < 0.001f)
            {
                transform.position = fPos.position;
                transform.rotation = fPos.rotation;
                StartMove = false;
            }
        }
        else if(FinishMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, sPos.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, sPos.rotation, rotationSpeed * Time.deltaTime);
            //checks if the position of the object and its destination are approximately equal
            if (Vector3.Distance(transform.position, sPos.position) < 0.001f && Quaternion.Angle(transform.rotation, sPos.rotation) < 0.001f)
            {
                transform.position = sPos.position;
                transform.rotation = sPos.rotation;
                FinishMove = false;
            }
        }
    }

    //goes from starting position to finishing position
    public void GoToFinish()
    {
        StartCoroutine(PointB());
    }

    //goes from finishing position to starting position
    public void GoToStart()
    {
        StartCoroutine(PointA());
    }

    //holds the contents and the delay
    public IEnumerator PointB()
    {
        yield return new WaitForSeconds(StartDelay);
        StartMove = true;
        FinishMove = false;
    }

    //holds the contents and the delay
    public IEnumerator PointA()
    {
        yield return new WaitForSeconds(FinishDelay);
        StartMove = false;
        FinishMove = true;
    }
}
