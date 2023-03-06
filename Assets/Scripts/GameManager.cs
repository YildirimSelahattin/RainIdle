using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update
    public float[] circleRadiuses;
    public float[] farmRadiuses;
    public List<GameObject[]> circleCharacterArray;
    public List<GameObject[]> circleCutterArray;
    public GameObject[] circles0CharacterArray;
    public GameObject[] circles1CharacterArray;
    public GameObject[] circles2CharacterArray;
    public GameObject[] circles0CutterCharacterArray;
    public GameObject[] circles1CutterCharacterArray;
    public GameObject[] circles2CutterCharacterArray;
    public GameObject[] farmCropArray;
    public int[] numberOfGridsInCircle;
    public int[] numberOfGridsInFarm;
    public GameObject[] totemParts;
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
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        circleCutterArray.Add(circles0CutterCharacterArray);
        circleCutterArray.Add(circles1CutterCharacterArray);
        circleCutterArray.Add(circles1CutterCharacterArray);

        circleCharacterArray.Add(circles0CharacterArray);
        circleCharacterArray.Add(circles1CharacterArray);
        circleCharacterArray.Add(circles2CharacterArray);
        DesignLevel();
    }

    public void DesignLevel()
    {
        int tempNumberOfPeople = GameDataManager.Instance.numberOfPeople;
        int circleCount = 0;
        int counter = 0;

        for(int i = 0; i<GameDataManager.Instance.totemUpgradeButtonLevel; i++)
        {
            totemParts[i].SetActive(true);
        }

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
            //add cutter farmer

            Instantiate(circleCutterArray[circleCount][GameDataManager.Instance.totemUpgradeButtonLevel], currentCircle.transform.GetChild(0).transform);
            for (counter = 1; counter < howManyPeopleToAdd+1; counter++)
            {
                
                GameObject tempFarmer = Instantiate(circleCharacterArray[circleCount][GameDataManager.Instance.totemUpgradeButtonLevel], currentCircle.transform.GetChild(counter).transform);
                farmerList.Add(tempFarmer);
                GameObject tempCrop = Instantiate(farmCropArray[circleCount], currentFarm.transform.GetChild(counter-1).transform);

            }

            if (tempNumberOfPeople != 0)
            {
                circleCount++;
            }

        }
        currentCircle = circleCount;
        indexToAddNext = counter;

        if (indexToAddNext == numberOfGridsInCircle[currentCircle])
        {
            UIManager.Instance.addCircleButton.interactable = true;
            UIManager.Instance.addPeopleButton.interactable = false;

        }
        else
        {
            UIManager.Instance.addCircleButton.interactable = false;
            UIManager.Instance.addPeopleButton.interactable = true;
        }
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
        if (currentCircle % 2 == 0)
        {
            parentCircle.GetComponent<RotateCircle>().planetSpeed = -10 * Mathf.Pow(1.1f, GameDataManager.Instance.speedButtonLevel);
        }
        else
        {
            parentCircle.GetComponent<RotateCircle>().planetSpeed = 10;
        }

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
            GameObject tempFarmer = Instantiate(circleCharacterArray[currentCircle][GameDataManager.Instance.totemUpgradeButtonLevel], circleParentsList[currentCircle].GetComponent<CircleManager>().listOfGrids[indexToAddNext].transform);
            farmerList.Add(tempFarmer);
            GameObject tempCrop = Instantiate(farmCropArray[currentCircle], farmParentsList[currentCircle].transform.GetChild(indexToAddNext).transform);
            //farm instantiate
            indexToAddNext++;
            GameDataManager.Instance.numberOfPeople++;

            foreach (GameObject farmer in farmerList)
            {
                farmer.SetActive(false);
                farmer.SetActive(true);
            }
            if (numberOfGridsInCircle[currentCircle] == indexToAddNext)
            {
                if (currentCircle == 2)
                {
                    UIManager.Instance.addPeopleButton.interactable = false;
                    //open add circle button
                    UIManager.Instance.addCircleButton.interactable = false;
                }
                //close add people button
                UIManager.Instance.addPeopleButton.interactable = false;
                //open add circle button
                UIManager.Instance.addCircleButton.interactable = true;
            }
        }
    }
    public void OnClickAddCircle()
    {
        currentCircle++;
        indexToAddNext = 0;
        GameObject currentCircleObject = CreateCircleGameData(numberOfGridsInCircle[currentCircle], circleRadiuses[currentCircle]);
        circleParentsList.Add(currentCircleObject);
        Instantiate(circleCutterArray[currentCircle][GameDataManager.Instance.totemUpgradeButtonLevel], currentCircleObject.transform.GetChild(0).transform);
        GameObject currentFarm = CreateFarmGameData(numberOfGridsInFarm[currentCircle], farmRadiuses[currentCircle]);
        farmParentsList.Add(currentFarm);
        //close add people button
        UIManager.Instance.addPeopleButton.interactable = true;
        //open add circle button
        UIManager.Instance.addCircleButton.interactable = false;
    }

    public void IncreaseAllFarmerLevels()
    {
        farmerList = new List<GameObject>();
        GameDataManager.Instance.totemUpgradeButtonLevel++;
        totemParts[GameDataManager.Instance.totemUpgradeButtonLevel - 1].SetActive(true);
        for (int circleCounter = 0; circleCounter < circleParentsList.Count; circleCounter++)
        {//for every parent
            GameObject gridObject = circleParentsList[circleCounter].transform.GetChild(0).gameObject;
            Destroy(gridObject.transform.GetChild(0).gameObject);
            Instantiate(circleCutterArray[circleCounter][GameDataManager.Instance.totemUpgradeButtonLevel],gridObject.transform);
            for (int gridCounter = 1; gridCounter < circleParentsList[circleCounter].transform.childCount; gridCounter++)
            {
                gridObject = circleParentsList[circleCounter].transform.GetChild(gridCounter).gameObject;
                Destroy(gridObject.transform.GetChild(0).gameObject);
                GameObject temp = Instantiate(circleCharacterArray[circleCounter][GameDataManager.Instance.totemUpgradeButtonLevel], gridObject.transform);
                farmerList.Add(temp);
            }
        }
    }
}
