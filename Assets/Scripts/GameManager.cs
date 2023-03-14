using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update
    public float[] circleRadiuses;
    public float[] farmRadiuses;
    public List<GameObject[]> circleManArray = new List<GameObject[]>();
    public List<GameObject[]> circleWomanArray = new List<GameObject[]>();
    public List<GameObject[]> circleCutterArray = new List<GameObject[]>();
    public GameObject[] circles0ManArray;
    public GameObject[] circles0WomanArray;
    public GameObject[] circles1ManArray;
    public GameObject[] circles1WomanArray;
    public GameObject[] circles2ManArray;
    public GameObject[] circles2WomanArray;
    public GameObject[] circles0CutterCharacterArray;
    public GameObject[] circles1CutterCharacterArray;
    public GameObject[] circles2CutterCharacterArray;
    public GameObject[] farmCropArray;
    public int[] numberOfGridsInCircle;
    public int[] numberOfGridsInFarm;
    public GameObject[] totemParts;
    public GameObject gridPrefab;
    public GameObject[] farmGridPrefab;
    public GameObject CircleParentsParent;
    public GameObject farmParentsParent;
    public List<GameObject> circleParentsList;
    public List<GameObject> farmParentsList;
    public List<GameObject> farmerList;
    public int indexToAddNext;
    public float[] cameraSizeArray;
    public int currentCircle;
    public bool addFarmerShouldbeOpened;
    public bool addCircleShouldbeOpened;

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
        circleCutterArray.Add(circles2CutterCharacterArray);

        circleManArray.Add(circles0ManArray);
        circleManArray.Add(circles1ManArray);
        circleManArray.Add(circles2ManArray);

        circleWomanArray.Add(circles0WomanArray);
        circleWomanArray.Add(circles1WomanArray);
        circleWomanArray.Add(circles2WomanArray);
        DesignLevel();
        
        GameDataManager.Instance.ControlButtons();
    }

    public void DesignLevel()
    {
        int tempNumberOfPeople = GameDataManager.Instance.numberOfPeople;
        int circleCount = 0;
        int counter = 0;
        indexToAddNext = 1;
        if (GameDataManager.Instance.totemUpgradeButtonLevel == 6)
        {
            UIManager.Instance.upgradeTotemButton.interactable = false;
        }
        for (int i = 1; i<=GameDataManager.Instance.totemUpgradeButtonLevel; i++)
        {
            totemParts[i].SetActive(true);
        }
        if(tempNumberOfPeople == 1)
        {
            GameObject currentCircle = CreateCircleGameData(numberOfGridsInCircle[0], circleRadiuses[0], circleCount);
            GameObject currentFarm = CreateFarmGameData(numberOfGridsInFarm[0], farmRadiuses[0],circleCount);
            circleParentsList.Add(currentCircle);
            farmParentsList.Add(currentFarm);
            
            GameObject tempFarmer = Instantiate(circleManArray[circleCount][GameDataManager.Instance.totemUpgradeButtonLevel], circleParentsList[circleCount].GetComponent<CircleManager>().listOfGrids[indexToAddNext].transform);
            farmerList.Add(tempFarmer);
            //delete base and Instantiate base and farm part
            Destroy(farmParentsList[circleCount].transform.GetChild(indexToAddNext - 1).transform.GetChild(0).gameObject);
            GameObject tempCrop = Instantiate(farmCropArray[circleCount], farmParentsList[circleCount].transform.GetChild(indexToAddNext - 1).transform);
            //farm instantiate
            indexToAddNext++;

            //add cutter farmer
            Instantiate(circleCutterArray[0][GameDataManager.Instance.totemUpgradeButtonLevel], currentCircle.transform.GetChild(0).transform);
            indexToAddNext = 2;
        }
        else
        {
            while (tempNumberOfPeople != 0)
            {
                int howManyPeopleToAdd = 0;
                if (numberOfGridsInCircle[circleCount]-1 > tempNumberOfPeople)
                {
                    howManyPeopleToAdd = tempNumberOfPeople+1;
                    tempNumberOfPeople = 0;
                }
                else
                {
                    tempNumberOfPeople -= numberOfGridsInCircle[circleCount]-1;
                    howManyPeopleToAdd = numberOfGridsInCircle[circleCount];
                }
                GameObject currentCircle = CreateCircleGameData(numberOfGridsInCircle[circleCount], circleRadiuses[circleCount], circleCount);
                GameObject currentFarm = CreateFarmGameData(numberOfGridsInFarm[circleCount], farmRadiuses[circleCount],circleCount);
                circleParentsList.Add(currentCircle);
                farmParentsList.Add(currentFarm);

                //add cutter farmer
                Debug.Log("Farmer Circle Count: " + circleCount);
                Instantiate(circleCutterArray[circleCount][1], currentCircle.transform.GetChild(0).transform);
                Debug.Log("people" + howManyPeopleToAdd);
                for (counter = 1; counter < howManyPeopleToAdd; counter++)
                {
                    GameObject characterToAdd = null;
                    if(counter % 2 == 0)
                    {
                        characterToAdd = circleManArray[circleCount][GameDataManager.Instance.totemUpgradeButtonLevel];
                    }
                    else
                    {
                        characterToAdd = circleWomanArray[circleCount][GameDataManager.Instance.totemUpgradeButtonLevel];
                    }
                    GameObject tempFarmer = Instantiate(characterToAdd, currentCircle.transform.GetChild(counter).transform);
                    farmerList.Add(tempFarmer);
                    Destroy(currentFarm.transform.GetChild(counter - 1).transform.GetChild(0).gameObject);
                    GameObject tempCrop = Instantiate(farmCropArray[circleCount], currentFarm.transform.GetChild(counter-1).transform);
                }

                if (tempNumberOfPeople != 0)
                {
                    circleCount++;
                }
            }
            currentCircle = circleCount;

            indexToAddNext = counter;
        }

        Camera.main.orthographicSize = cameraSizeArray[currentCircle];
        if (indexToAddNext == numberOfGridsInCircle[currentCircle])
        {           
            UIManager.Instance.addPeopleButton.interactable = false;
            addFarmerShouldbeOpened = false;
            addCircleShouldbeOpened = true;
        }
        else
        {
            UIManager.Instance.addCircleButton.interactable = false;
            addFarmerShouldbeOpened = true;
            addCircleShouldbeOpened = false;
        }
        AddTotemAndSpeedLevelEffects();
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
    public void AddTotemAndSpeedLevelEffects()
    {
        //speedlevel;
        for (int i = 0; i < circleParentsList.Count; i++)
        {
               circleParentsList[i].GetComponent<RotateCircle>().planetSpeed *= (Mathf.Pow(1.1f, GameDataManager.Instance.totemUpgradeButtonLevel - 1));
        }

        //totem level effects
        UIManager.Instance.rainTime += 5 * (GameDataManager.Instance.totemUpgradeButtonLevel - 1);
        GameDataManager.Instance.incomeMultiplier *= Mathf.Pow(1.2f, GameDataManager.Instance.totemUpgradeButtonLevel - 1);
        UIManager.Instance.WriteInfos();
    }
    public GameObject CreateCircleGameData(int numberOfObjects, float radius, int curentCircle)
    {
        GameObject temp = new GameObject();
        temp.name = "Circle";
        GameObject parentCircle = Instantiate(temp, new Vector3(0, 0, 0), Quaternion.identity, CircleParentsParent.transform);
        parentCircle.AddComponent<CircleManager>();
        parentCircle.AddComponent<RotateCircle>();

        Debug.Log("CurrentCircle: " + curentCircle);

        if (curentCircle % 2 == 0)
        {
            parentCircle.GetComponent<RotateCircle>().planetSpeed = -(10 + (GameDataManager.Instance.speedButtonLevel * 1f));
        }
        else
        {
            Debug.Log("fsadfsdfsdfsd");
            parentCircle.GetComponent<RotateCircle>().planetSpeed = (10 + (GameDataManager.Instance.speedButtonLevel * 1f));
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

    public GameObject CreateFarmGameData(int numberOfObjects, float radius,int currentCircle)
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
            GameObject temp2 = Instantiate(gridPrefab, pos, rot, parentCircle.transform);
            Instantiate(farmGridPrefab[currentCircle], temp2.transform);
        }
        return parentCircle;
    }
    public void AddFarmer()
    {
        if (numberOfGridsInCircle[currentCircle] > indexToAddNext)
        {
            GameObject characterToAdd = null;
            if(indexToAddNext % 2 == 1)
            {
                Debug.Log("wom");
                characterToAdd = circleManArray[currentCircle][GameDataManager.Instance.totemUpgradeButtonLevel];
                    
            }
            else
            {
                Debug.Log("man");
                characterToAdd = circleWomanArray[currentCircle][GameDataManager.Instance.totemUpgradeButtonLevel];
            }
            GameObject tempFarmer = Instantiate(characterToAdd, circleParentsList[currentCircle].GetComponent<CircleManager>().listOfGrids[indexToAddNext].transform);
            farmerList.Add(tempFarmer);
            //delete base and Instantiate base and farm part
            Destroy(farmParentsList[currentCircle].transform.GetChild(indexToAddNext - 1).transform.GetChild(0).gameObject);
            GameObject tempCrop = Instantiate(farmCropArray[currentCircle], farmParentsList[currentCircle].transform.GetChild(indexToAddNext-1).transform);
            //farm instantiate
            indexToAddNext++;
            

            foreach (GameObject farmer in farmerList)
            {
                farmer.SetActive(false);
                farmer.SetActive(true);
            }
            if (numberOfGridsInCircle[currentCircle] == indexToAddNext)
            {
                if (currentCircle == 2)
                {
                    //close add people button
                    addFarmerShouldbeOpened = false;
                    UIManager.Instance.addPeopleButton.interactable = false;
                    //open add circle button
                    addCircleShouldbeOpened = false;
                    UIManager.Instance.addCircleButton.interactable = false;
                }
                //close add people button
                addFarmerShouldbeOpened = false;
                UIManager.Instance.addPeopleButton.interactable = false;
                //open add circle button
                addCircleShouldbeOpened = true;
                UIManager.Instance.addCircleButton.interactable = true;
            }
        }
    }
    public void OnClickAddCircle()
    {
        GameDataManager.Instance.UpgradeAddCircleMoney();
        currentCircle++;
        Camera.main.orthographicSize = cameraSizeArray[currentCircle];
        indexToAddNext = 1;
        GameObject currentCircleObject = CreateCircleGameData(numberOfGridsInCircle[currentCircle], circleRadiuses[currentCircle], currentCircle);
        circleParentsList.Add(currentCircleObject);
        Instantiate(circleCutterArray[currentCircle][GameDataManager.Instance.totemUpgradeButtonLevel], currentCircleObject.transform.GetChild(0).transform);
        GameObject currentFarm = CreateFarmGameData(numberOfGridsInFarm[currentCircle], farmRadiuses[currentCircle], currentCircle);
        farmParentsList.Add(currentFarm);
        //close add people button
        addFarmerShouldbeOpened = true;
        UIManager.Instance.addPeopleButton.interactable = true;
        //open add circle button
        addCircleShouldbeOpened = false;
        UIManager.Instance.addCircleButton.interactable = false;
        AddFarmer();
    }

    public void IncreaseAllFarmerLevels()
    {
        farmerList = new List<GameObject>();
        totemParts[GameDataManager.Instance.totemUpgradeButtonLevel ].SetActive(true);
        for (int circleCounter = 0; circleCounter < circleParentsList.Count; circleCounter++)
        {//for every parent
            GameObject gridObject = circleParentsList[circleCounter].transform.GetChild(0).gameObject;
            Destroy(gridObject.transform.GetChild(0).gameObject);
            Instantiate(circleCutterArray[circleCounter][GameDataManager.Instance.totemUpgradeButtonLevel],gridObject.transform);
            for (int gridCounter = 1; gridCounter < circleParentsList[circleCounter].transform.childCount; gridCounter++)
            {
                gridObject = circleParentsList[circleCounter].transform.GetChild(gridCounter).gameObject;
                if(gridObject.transform.childCount !=0)
                {
                    GameObject characterToAdd = null;
                    if (gridCounter % 2 == 0)
                    {
                        characterToAdd = circleManArray[circleCounter][GameDataManager.Instance.totemUpgradeButtonLevel];
                    }
                    else
                    {
                        characterToAdd = circleWomanArray[circleCounter][GameDataManager.Instance.totemUpgradeButtonLevel];
                    }
                    Destroy(gridObject.transform.GetChild(0).gameObject);
                    GameObject temp = Instantiate(characterToAdd, gridObject.transform);
                    farmerList.Add(temp);
                }
            }
        }

        if(GameDataManager.Instance.totemUpgradeButtonLevel == 6)
        {
            UIManager.Instance.upgradeTotemButton.interactable = false;
        }
    }
}
