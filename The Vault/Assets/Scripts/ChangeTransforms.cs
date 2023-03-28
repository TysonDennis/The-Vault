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
    //holds the object's x-position rate
    [SerializeField]
    private float xRate;
    //holds the object's y-position rate
    [SerializeField]
    private float yRate;
    //holds the object's z-position rate
    [SerializeField]
    private float zRate;
    //holds the object's x-rotation rate
    [SerializeField]
    private float xRot;
    //holds the object's y-rotation rate
    [SerializeField]
    private float yRot;
    //holds the object's z-rotation rate
    [SerializeField]
    private float zRot;

    //sets the position and rotation to the start upon starting
    private void Awake()
    {
        this.transform.SetPositionAndRotation(sPos.position, sPos.rotation);
    }

    //goes from starting position to finishing position
    public void GoToFinish()
    {
        if(this.transform.position != fPos.position)
        {
            transform.Translate(xRate * Time.deltaTime, yRate * Time.deltaTime, zRate * Time.deltaTime);
        }
        if(this.transform.rotation != fPos.rotation)
        {
            transform.Rotate(xRot * Time.deltaTime, yRot * Time.deltaTime, zRot * Time.deltaTime);
        }
    }

    //goes from finishing position to starting position
    public void GoToStart()
    {
        if (this.transform.position != sPos.position)
        {
            transform.Translate(-xRate * Time.deltaTime, -yRate * Time.deltaTime, -zRate * Time.deltaTime);
        }
        if (this.transform.rotation != sPos.rotation)
        {
            transform.Rotate(-xRot * Time.deltaTime, -yRot * Time.deltaTime, -zRot * Time.deltaTime);
        }
    }
}
