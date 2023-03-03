using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float[] circleRadiuses;
    public float[] farmRadiuses;
    public GameObject[] circlesCharacterArray;
    public int[] numberOfGridsInCircle; 
    public int[] numberOfGridsInFarm; 
   
    public GameObject gridPrefab;
    public GameObject CircleParentsParent;
    public GameObject farmParentsParent;
    public List<GameObject> circleParentsList;
    public List<GameObject> farmParentsList;
    public int currentCircle;
    void Start()
    {
        DesignLevel();
    }

    public void DesignLevel()
    {
        int tempNumberOfPeople = GameDataManager.Instance.numberOfPeople;
        int circleCount = 0;

        while (tempNumberOfPeople != 0)
        {
            Debug.Log(tempNumberOfPeople);
            int howManyPeopleToAdd = 0;
            if (numberOfGridsInCircle[circleCount] > tempNumberOfPeople)
            {
                howManyPeopleToAdd = tempNumberOfPeople;
                tempNumberOfPeople = 0;
            }
            else
            {
                tempNumberOfPeople -= numberOfGridsInCircle[circleCount];
                howManyPeopleToAdd = numberOfGridsInCircle[circleCount];
            }
            GameObject currentCircle = CreateCircleGameData(numberOfGridsInCircle[circleCount], circleRadiuses[circleCount]);
            GameObject currentFarm = CreateFarmGameData(numberOfGridsInFarm[circleCount], farmRadiuses[circleCount]);
            circleParentsList.Add(currentCircle);
            farmParentsList.Add(currentFarm);
            for (int i = 0; i < howManyPeopleToAdd;i++)
            {
                Instantiate(circlesCharacterArray[circleCount], currentCircle.transform.GetChild(i).transform);
            }
            circleCount++;
            
        }
        currentCircle = circleCount - 1;

    }

  /*  public void OnClickCreateCircle(int numberOfObjects,float radius )
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(dancerPrefab, pos, rot, gameObject.transform);
        }
    }*/

    public GameObject CreateCircleGameData(int numberOfObjects,float radius)
    {
        GameObject temp = new GameObject();
        temp.name = "Circle";
        GameObject parentCircle = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity, CircleParentsParent.transform);
        parentCircle.AddComponent<CircleManager>();
        parentCircle.AddComponent<RotateCircle>();
        parentCircle.GetComponent<RotateCircle>().planetSpeed = 20;
        
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(gridPrefab, pos, rot, parentCircle.transform);
        }
        return parentCircle;
    }
    public GameObject CreateFarmGameData(int numberOfObjects, float radius)
    {
        GameObject temp = new GameObject();
        temp.name = "Farm";
        GameObject parentCircle = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity, farmParentsParent.transform);
        farmParentsParent.AddComponent<CircleManager>();
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            Instantiate(gridPrefab, pos, rot, parentCircle.transform);
        }
        return parentCircle;
    }
}
