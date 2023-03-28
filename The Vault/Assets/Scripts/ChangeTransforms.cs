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
    //holds the lerp speed
    [SerializeField]
    private float LerpSpeed;

    //goes from starting position to finishing position
    public void GoToFinish()
    {
        Vector3 newPosition = Vector3.Lerp(sPos.position, fPos.position, Time.deltaTime * LerpSpeed);
    }
}
