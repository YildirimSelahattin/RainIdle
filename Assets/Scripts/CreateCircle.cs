using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class CreateCircle : MonoBehaviour {
    // Instantiates prefabs in a circle formation
    public GameObject prefab;
    public int numberOfObjects = 4;
    public float radius = 5f;

    public void  OnClickCreateCircle() 
    {
        for (int i = 0; i < numberOfObjects; i++) {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(prefab, pos, rot, gameObject.transform);
        }
    }

    public GameObject CreateCircleGameData()
    {
        GameObject gameObject = new GameObject();
        GameObject parentCircle = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
        parentCircle.AddComponent<CircleManager>();
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(prefab, pos, rot, parentCircle.transform);
        }
        return parentCircle;
    }
}