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
    public long totalMoney = 0;
    public long TotalMoney
    {
        get { return totalMoney; }

        set
        {
            totalMoney = value;
            if (UIManager.Instance != null)
                ControlButtons();
        }
    }

    public long addFarmerButtonMoney = 10;
    public long AddFarmerButtonMoney
    {
        get { return addFarmerButtonMoney; }
        set { addFarmerButtonMoney = FormatNumbers.RoundNumberLikeText(value); }
    }
    public long speedButtonButtonMoney = 10;
    public long SpeedButtonButtonMoney
    {
        get { return speedButtonButtonMoney; }
        set { speedButtonButtonMoney = FormatNumbers.RoundNumberLikeText(value); }
    }
    public long addCircleButtonMoney = 10;
    public long AddCircleButtonMoney
    {
        get { return addCircleButtonMoney; }
        set { addCircleButtonMoney = FormatNumbers.RoundNumberLikeText(value); }
    }

    public long incomeButtonMoney = 10;
    public long IncomeButtonMoney
    {
        get { return incomeButtonMoney; }
        set { incomeButtonMoney = FormatNumbers.RoundNumberLikeText(value); }
    }
    public long totemUpgradeButtonMoney = 10;
    public long TotemUpgradeButtonMoney
    {
        get { return totemUpgradeButtonMoney; }
        set { totemUpgradeButtonMoney = FormatNumbers.RoundNumberLikeText(value); }
    }
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
        TotalMoney -= AddFarmerButtonMoney;
        addFarmerButtonLevel++;
        AddFarmerButtonMoney = (long)(AddFarmerButtonMoney * 1.1f);
        UIManager.Instance.addPeopleButtonPrice.text = FormatNumbers.AbbreviateNumber(AddFarmerButtonMoney) + " $";//write button money
        UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(TotalMoney);
        UIManager.Instance.addPeopleButtonLevel.text = addFarmerButtonLevel.ToString();
    }
    
    public void UpgradeSpeedMoney()
    {
        TotalMoney -= SpeedButtonButtonMoney;
        speedButtonLevel++;
        SpeedButtonButtonMoney = (long)(speedButtonButtonMoney * 1.1f);
        UIManager.Instance.speedButtonPrice.text = FormatNumbers.AbbreviateNumber(SpeedButtonButtonMoney) + " $";//write button money
        UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(TotalMoney);
        UIManager.Instance.speedButtonLevel.text = speedButtonLevel.ToString();
    }
    
    public void UpgradeAddCircleMoney()
    {
        TotalMoney -= AddCircleButtonMoney;
        addCircleButtonLevel++;
        AddCircleButtonMoney = (long)(AddCircleButtonMoney * 1.1f);
        UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(TotalMoney);
        UIManager.Instance.addCircleButtonPrice.text = FormatNumbers.AbbreviateNumber(AddCircleButtonMoney) + " $";//write button money

    }
    
    public void UpgradeIncomeMoney()
    {
        incomeMultiplier *= 1.1f;//increase income percentage
        TotalMoney -= IncomeButtonMoney;//decrease total money 
        incomeButtonLevel++;//increase button level
        IncomeButtonMoney = (long)(IncomeButtonMoney * 1.1f);//increase button cost
        UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(TotalMoney);//write total money
        UIManager.Instance.incomeButtonPrice.text = FormatNumbers.AbbreviateNumber(IncomeButtonMoney) + " $";//write button money
    }
    
    public void UpgradeTotemMoney()
    {
        TotalMoney -= totemUpgradeButtonMoney;
        totemUpgradeButtonLevel++;
        TotemUpgradeButtonMoney = (long)(TotemUpgradeButtonMoney * 1.1f);
        UIManager.Instance.rainTime += 5;
        UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(TotalMoney);
        UIManager.Instance.upgradeTotemButtonPrice.text = FormatNumbers.AbbreviateNumber(TotemUpgradeButtonMoney) + " $";//write button money

    }

    public void ControlButtons()
    {
        if (TotalMoney >= TotemUpgradeButtonMoney) //activate totem button
        {
            UIManager.Instance.upgradeTotemButton.interactable = true;
        }
        else
        {
            UIManager.Instance.upgradeTotemButton.interactable = false;
        }
        
        if (TotalMoney >= AddFarmerButtonMoney) //activate add farmer button
        {
            UIManager.Instance.addPeopleButton.interactable = true;
        }
        else
        {
            UIManager.Instance.addPeopleButton.interactable = false;
        }
        
        if (TotalMoney >= IncomeButtonMoney) //activate income button
        {
            UIManager.Instance.incomeButton.interactable = true;
        }
        else
        {
            UIManager.Instance.incomeButton.interactable = false;
        }
        
        if (TotalMoney >= SpeedButtonButtonMoney) //activate speed button
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
