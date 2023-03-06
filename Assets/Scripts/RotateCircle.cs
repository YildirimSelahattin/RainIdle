using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    public float planetSpeed = 2f;
    
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * planetSpeed * Time.deltaTime);
    }
}
