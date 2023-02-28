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
    
    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(CircularMove.Instance.dancerSpeed < maxSpeed)
            {
                CircularMove.Instance.dancerSpeed += baseLinePower*Time.deltaTime;
            }
            
            
            if(CircularMove.Instance.Gap > minGap)
            {
                CircularMove.Instance.Gap -= (int)(baseLinePower*Time.deltaTime);
            }
            
        }
    }

    public void OnAddNewCircle()
    {
        Instantiate(DancerParrentPrefab, new Vector3(0, 1, circleIndexTransform), Quaternion.identity);
        circleIndexTransform -= 1;
    }
}
