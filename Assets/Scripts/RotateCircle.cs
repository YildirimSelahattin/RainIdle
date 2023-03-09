using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    public float planetSpeed = 2f;

    void Update()
    {
        transform.Rotate(Vector3.up * planetSpeed * Time.deltaTime * UIManager.Instance.rainMultiplier * UIManager.Instance.tapSpeedMultiplier);
    }
}
