using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMove : MonoBehaviour
{
    public float dancerSpeed = 5;
    public float bodySpeed = 5;
    public Transform totem;
    public int Gap = 10;

    // References
    public GameObject BodyPrefab;

    // Lists
    public List<GameObject> BodyParts = new List<GameObject>();

    // Update is called once per frame
    void Update() {

        // Move forward
        transform.RotateAround(totem.position, Vector3.up, dancerSpeed * Time.deltaTime);
        MoveListElements();

    }
    
    private void MoveListElements()
    {
        for(int i = 1; i < BodyParts.Count; i++)
        {
            Vector3 pos = BodyParts[i].transform.localPosition;
            pos.x = BodyParts[i - 1].transform.localPosition.x;
            //BodyParts[i].transform.DOLocalMoveX(pos.x, movementDelay);
        }
    }

    public void GrowSnake() {
        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }
}
