using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    //accesses the SunSO
    [SerializeField]
    private SunSO sunSO;

    //makes the directional light universal and sets the rotation
    private void Awake()
    { 
        DontDestroyOnLoad(this.gameObject);
        var r = this.transform.rotation;
        r.x = sunSO.rotation;
        this.transform.rotation = r;
    }

    private void FixedUpdate()
    {
        //makes the directional light rotate, creating a day/night cycle
        transform.Rotate(Time.fixedDeltaTime * 0.03f, 0, 0);
        //sends the rotation value to the scriptable object
        sunSO.rotation = this.transform.rotation.x;
    }
}
