using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameDataManager Instance;
    
    public int numberOfPeople;
    public float[] farmerSpeed;
    public int speedButtonLevel = 1;
    public int totemUpgradeButtonLevel= 1;
    public int addFarmerButtonLevel= 1;
    public int addCircleButtonLevel= 1;
    public int incomeButtonLevel= 1;

    public float incomeMultiplier = 1.1f;
    //Money
    public float totalMoney;

    public float addFarmerButtonMoney = 10;
    public float speedButtonButtonMoney = 10;
    public float addCircleButtonMoney = 10;
    public float incomeButtonMoney = 10;
    public float totemUpgradeButtonMoney = 10;
    
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

    public void UpgradeAddFarmerMoney()
    {
        UIManager.Instance.addPeopleButtonPrice.text = addFarmerButtonMoney.ToString();
        totalMoney -= addFarmerButtonMoney;
        addFarmerButtonLevel++;
        addFarmerButtonMoney += addFarmerButtonMoney * incomeMultiplier;
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
    }
    
    public void UpgradeSpeedMoney()
    {
        UIManager.Instance.speedButtonPrice.text = speedButtonButtonMoney.ToString();
        totalMoney -= speedButtonButtonMoney;
        speedButtonLevel++;
        speedButtonButtonMoney += speedButtonButtonMoney * incomeMultiplier;
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
    }
    
    public void UpgradeAddCircleMoney()
    {
        UIManager.Instance.addCircleButtonPrice.text = addCircleButtonMoney.ToString();
        totalMoney -= addCircleButtonMoney;
        addCircleButtonLevel++;
        addCircleButtonMoney += addCircleButtonMoney * incomeMultiplier;
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
    }
    
    public void UpgradeIncomeMoney()
    {
        UIManager.Instance.incomeButtonPrice.text = incomeButtonMoney.ToString();
        totalMoney -= incomeButtonMoney;
        incomeButtonLevel++;
        incomeButtonMoney += incomeButtonMoney * incomeMultiplier;
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
    }
    
    public void UpgradeTotemMoney()
    {
        UIManager.Instance.upgradeTotemButtonPrice.text = totemUpgradeButtonMoney.ToString();
        totalMoney -= totemUpgradeButtonMoney;
        totemUpgradeButtonLevel++;
        totemUpgradeButtonMoney += totemUpgradeButtonMoney * incomeMultiplier;
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
    }
}
