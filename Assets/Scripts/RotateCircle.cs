using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    public float planetSpeed = 2f;
    private void Start()
    {
        //LoopRotate();
    }
    void FixedUpdate()
    {

        transform.Rotate(Vector3.up * planetSpeed * Time.deltaTime);

    }

    public void LoopRotate()
    {
        //transform.DOLocalRotate(new Vector3(0, 360, 0), 15*Time.deltaTime, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(()=>LoopRotate());
    }
}
