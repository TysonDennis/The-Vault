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

    //sets the position and rotation to the start upon starting
    private void Awake()
    {
        //this.transform.SetPositionAndRotation(sPos.position, sPos.rotation);
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
        this.transform.position = Vector3.MoveTowards(this.transform.position, fPos.position, speed);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, fPos.rotation, rotationSpeed);
        //checks if the position of the object and its destination are approximately equal
        if(Vector3.Distance(this.transform.position, fPos.position) < 0.001f)
        {
            this.transform.position = fPos.position;
        }
    }

    //holds the contents and the delay
    public IEnumerator PointA()
    {
        yield return new WaitForSeconds(FinishDelay);
        this.transform.position = Vector3.MoveTowards(this.transform.position, sPos.position, speed);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, sPos.rotation, rotationSpeed);
        //checks if the position of the object and its destination are approximately equal
        if (Vector3.Distance(this.transform.position, sPos.position) < 0.001f)
        {
            this.transform.position = sPos.position;
        }
    }
}
