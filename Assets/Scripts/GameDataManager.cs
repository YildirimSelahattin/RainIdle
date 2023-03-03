using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameDataManager Instance;
    
    public int numberOfPeople;
    public float[] farmerSpeed;
    public int speedButtonLevel;
    public int totemUpgradeButtonLevel;
    public int addFarmerButtonLevel;
    public int addCircleButtonLevel;
    public int incomeButtonLevel;
    public int incomeMultiplier;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void LoadData()
    {
        numberOfPeople = PlayerPrefs.GetInt("numberOfPeople",6);
    }
}
