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
    public float TotalMoney
    {
        get { return totalMoney; }

        set
        {
            totalMoney = value;
            if (UIManager.Instance != null)
                ControlButtons();
        }
    }

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
        TotalMoney -= addFarmerButtonMoney;
        addFarmerButtonLevel++;
        addFarmerButtonMoney += addFarmerButtonMoney * incomeMultiplier;
        UIManager.Instance.addPeopleButtonPrice.text = addFarmerButtonMoney.ToString();
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
    }
    
    public void UpgradeSpeedMoney()
    {
        TotalMoney -= speedButtonButtonMoney;
        speedButtonLevel++;
        speedButtonButtonMoney += speedButtonButtonMoney * incomeMultiplier;
        UIManager.Instance.speedButtonPrice.text = speedButtonButtonMoney.ToString();
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
    }
    
    public void UpgradeAddCircleMoney()
    {
        TotalMoney -= addCircleButtonMoney;
        addCircleButtonLevel++;
        addCircleButtonMoney += addCircleButtonMoney * incomeMultiplier;
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
        UIManager.Instance.addCircleButtonPrice.text = addCircleButtonMoney.ToString();
    }
    
    public void UpgradeIncomeMoney()
    {
        CropManager.Instance.cropPrice += CropManager.Instance.cropPrice * 1.1f;
        TotalMoney -= incomeButtonMoney;
        incomeButtonLevel++;
        incomeButtonMoney += incomeButtonMoney * incomeMultiplier;
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
        UIManager.Instance.incomeButtonPrice.text = incomeButtonMoney.ToString();
    }
    
    public void UpgradeTotemMoney()
    {
        TotalMoney -= totemUpgradeButtonMoney;
        totemUpgradeButtonLevel++;
        totemUpgradeButtonMoney += totemUpgradeButtonMoney * incomeMultiplier;
        UIManager.Instance.rainTime += 5;
        UIManager.Instance.totalMoneyText.text = totalMoney.ToString();
        UIManager.Instance.upgradeTotemButtonPrice.text = totemUpgradeButtonMoney.ToString();
    }

    public void ControlButtons()
    {
        if (totalMoney >= totemUpgradeButtonMoney) //activate totem button
        {
            UIManager.Instance.upgradeTotemButton.interactable = true;
        }
        else
        {
            UIManager.Instance.upgradeTotemButton.interactable = false;
        }
        
        if (totalMoney >= addFarmerButtonMoney) //activate add farmer button
        {
            UIManager.Instance.addPeopleButton.interactable = true;
        }
        else
        {
            UIManager.Instance.addPeopleButton.interactable = false;
        }
        
        if (totalMoney >= incomeButtonMoney) //activate income button
        {
            UIManager.Instance.incomeButton.interactable = true;
        }
        else
        {
            UIManager.Instance.incomeButton.interactable = false;
        }
        
        if (totalMoney >= speedButtonButtonMoney) //activate speed button
        {
            UIManager.Instance.speedButton.interactable = true;
        }
        else
        {
            UIManager.Instance.speedButton.interactable = false;
        }
        
        /*
        if (totalMoney >= addCircleButtonMoney) //activate add circle button
        {
            UIManager.Instance.addCircleButton.interactable = true;
        }
        else
        {
            UIManager.Instance.addCircleButton.interactable = false;
        }
        */
    }
}
