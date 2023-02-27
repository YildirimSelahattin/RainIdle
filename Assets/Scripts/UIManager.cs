using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private float startingTime;
    public float baseLinePower = 2f;
    public float maxSpeed = 40;
    public float minGap = 75;
    public bool speedUpButton = false;
    public bool speedDownButton = false;
    
    private void Update()
    {
        if(speedUpButton)
        {
            startingTime = Time.time;
        }
        
        if(speedUpButton)
        {
            if(CircularMove.Instance.dancerSpeed < maxSpeed)
            {
                CircularMove.Instance.dancerSpeed += (Time.time - startingTime) * baseLinePower;
            }
            
            if(CircularMove.Instance.Gap > minGap)
            {
                CircularMove.Instance.dancerSpeed -= (Time.time - startingTime) * baseLinePower;
            }
        }
        
        if(speedDownButton)
        {
            if(CircularMove.Instance.dancerSpeed > 20)
            {
                CircularMove.Instance.dancerSpeed -= (Time.time - startingTime) * baseLinePower;
            }
            
            if(CircularMove.Instance.Gap > 150)
            {
                CircularMove.Instance.dancerSpeed += (Time.time - startingTime) * baseLinePower;
            }
        }
    }

    public void OnSpeedUp()
    {
        Debug.Log("asdasdasdasdasdasdas");
        speedUpButton = true;
    }
    
    public void OnSpeedDown()
    {
        speedDownButton = true;
    }
}
