using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int circleIndexTransform = -3;
    private float startingTime;
    public float baseLinePower = 2f;
    public float maxSpeed = 40;
    public float minGap = 75;
    public GameObject DancerParrentPrefab;

    public void OnAddNewCircle()
    {
        Instantiate(DancerParrentPrefab, new Vector3(0, 1, circleIndexTransform), Quaternion.identity);
        circleIndexTransform -= 1;
    }
    public void OnAddPeople()
    {
       
    }
}
