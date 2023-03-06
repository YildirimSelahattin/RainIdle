using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CropManager : MonoBehaviour
{
    public float growTime = 10f;
    public int currentCircle;
    Vector3 originalPos;
    void Start()
    {
        CropGrow();
    }

    public void CropGrow()
    {
        transform.DOLocalMove(new Vector3(0,2.5f, 0), 360/Mathf.Abs(GameManager.Instance.circleParentsList[currentCircle].GetComponent<RotateCircle>().planetSpeed));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cutter1")
        {
            transform.DOLocalMove(new Vector3(0,-10f, 0), 0.1f);
            CropGrow();
        }
    }
}