using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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
    public List<GameObject> farmerList;
    public int indexToAddNext;

    public int currentCircle;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        DesignLevel();
    }

    public void DesignLevel()
    {
        int tempNumberOfPeople = GameDataManager.Instance.numberOfPeople;
        int circleCount = 0;
        int counter = 0;
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
            for (counter = 0; counter < howManyPeopleToAdd; counter++)
            {
                GameObject tempFarmer = Instantiate(circlesCharacterArray[circleCount], currentCircle.transform.GetChild(counter).transform);
                farmerList.Add(tempFarmer);
            }

            if (tempNumberOfPeople != 0)
            {
                circleCount++;
            }

        }
        currentCircle = circleCount;
        indexToAddNext = counter;
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

    public GameObject CreateCircleGameData(int numberOfObjects, float radius)
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
            GameObject grid = Instantiate(gridPrefab, pos, rot, parentCircle.transform);
            parentCircle.GetComponent<CircleManager>().listOfGrids.Add(grid);
        }
        return parentCircle;
    }
    public GameObject CreateFarmGameData(int numberOfObjects, float radius)
    {
        GameObject temp = new GameObject();
        temp.name = "Farm";
        GameObject parentCircle = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity, farmParentsParent.transform);
        parentCircle.AddComponent<CircleManager>();
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
    public void AddFarmer()
    {
        if (numberOfGridsInCircle[currentCircle] > indexToAddNext)
        {
            GameObject tempFarmer = Instantiate(circlesCharacterArray[currentCircle], circleParentsList[currentCircle].GetComponent<CircleManager>().listOfGrids[indexToAddNext].transform);
            farmerList.Add(tempFarmer);
            //farm instantiate
            indexToAddNext++;
            GameDataManager.Instance.numberOfPeople++;
            
            foreach (GameObject farmer in farmerList)
            {
                farmer.SetActive(false);
                farmer.SetActive(true);
            }
        }
        else
        {
            //close add people button
            
            //open add circle button
        }

        
        
    }
    public void OnClickAddCircle()
    {
        currentCircle++;
        indexToAddNext = 0;
        GameObject currentCircleObject = CreateCircleGameData(numberOfGridsInCircle[currentCircle], circleRadiuses[currentCircle]);
        circleParentsList.Add(currentCircleObject);
        GameObject currentFarm = CreateFarmGameData(numberOfGridsInFarm[currentCircle], farmRadiuses[currentCircle]);
        farmParentsList.Add(currentFarm);
    }
}
