using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CutterManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "crop1")
        {
            Debug.Log("hit ground");
            other.gameObject.SetActive(false);
        }
    }
}
