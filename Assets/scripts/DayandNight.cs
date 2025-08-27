using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayandNight : MonoBehaviour
{
    [Range(20f, 200f)]
    public float orbitSpeed;
    
    void Update()
    {
        SunRotate();
    }

    void SunRotate()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, orbitSpeed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
