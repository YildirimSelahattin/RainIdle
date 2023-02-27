using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private float startingTime;
    
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)
        {
            startingTime = Time.time;
        }
        if(Input.GetKey(KeyCode.Mouse0)
        {
            if(currentPower < maxPower)
            {
                currentPower += (Time.time - startingTime) * baseLinePower;
            }
        }
    }
}
