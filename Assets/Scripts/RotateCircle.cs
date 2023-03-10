using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    public static RotateCircle Instance;
    public float planetSpeed = 2f;
    public float rainMultiplier = 1;
    public float tapSpeedMultiplier = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up * planetSpeed * Time.deltaTime * rainMultiplier * tapSpeedMultiplier);
    }
}
