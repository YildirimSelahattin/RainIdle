using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

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
    public float incomeMultiplier = 1f;
    public int[] cropPrices;
    
    //Money
    public long totalMoney = 0;
    public long TotalMoney
    {
        get { return totalMoney; }

        set
        {
            totalMoney = value;
            
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
        AddFarmerButtonMoney = (long)(Mathf.Pow(1.5f, addFarmerButtonLevel) * 100);
        
        UIManager.Instance.addPeopleButtonPrice.text = FormatNumbers.AbbreviateNumber(AddFarmerButtonMoney) + " $";//write button money
        UIManager.Instance.addPeopleButtonLevel.text = "LEVEL " + addFarmerButtonLevel.ToString();
        UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(TotalMoney);
        ControlButtons();
    }

    public void UpgradeSpeedMoney()
    {
        TotalMoney -= SpeedButtonButtonMoney;
        speedButtonLevel++;
        SpeedButtonButtonMoney = (long)(Mathf.Pow(1.65f, speedButtonLevel) * 28);

        UIManager.Instance.speedButtonPrice.text = FormatNumbers.AbbreviateNumber(SpeedButtonButtonMoney) + " $";//write button money
        UIManager.Instance.speedButtonLevel.text = "LEVEL " + speedButtonLevel.ToString();
        UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(TotalMoney);


        for (int i = 0; i < GameManager.Instance.circleParentsList.Count; i++)
        {
            if(i%2 == 0)
            {
                GameManager.Instance.circleParentsList[i].GetComponent<RotateCircle>().planetSpeed -= speedButtonLevel;
            }
            else
            {
                GameManager.Instance.circleParentsList[i].GetComponent<RotateCircle>().planetSpeed += speedButtonLevel;
            }
        }
        //ýnfo text
        UIManager.Instance.speedInfo.text = Mathf.Abs(GameManager.Instance.circleParentsList[0].GetComponent<RotateCircle>().planetSpeed).ToString();
        ControlButtons();
    }

    public void UpgradeAddCircleMoney()
    {
        TotalMoney -= AddCircleButtonMoney;
        addCircleButtonLevel++;
        
        AddCircleButtonMoney = (long)(Mathf.Pow(130, addCircleButtonLevel) * 15);
        
        UIManager.Instance.addCircleButtonPrice.text = FormatNumbers.AbbreviateNumber(AddCircleButtonMoney) + " $";//write button money
        UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(TotalMoney);


    }

    public void UpgradeIncomeMoney()
    {
        
        TotalMoney -= IncomeButtonMoney;//decrease total money 
        incomeButtonLevel++;//increase button level
        incomeMultiplier += (incomeButtonLevel + (incomeButtonLevel * 0.1f))/10; //increase income percentage
   
        for (int i = 0;i<cropPrices.Length; i++)
        {
            UIManager.Instance.cropMoneyInfoArray[i].text = (cropPrices[i]*incomeMultiplier).ToString() +" $";
        }
        IncomeButtonMoney = (long)(Mathf.Pow(1.6f, incomeButtonLevel) * 25);
        UIManager.Instance.incomeButtonPrice.text = FormatNumbers.AbbreviateNumber(IncomeButtonMoney) + " $";//write button money
        UIManager.Instance.totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(TotalMoney);//write total money
        UIManager.Instance.incomeInfo.text = (incomeMultiplier * 100).ToString();
        UIManager.Instance.incomeButtonLevel.text = "LEVEL " + incomeButtonLevel.ToString();
        ControlButtons();
    }

    public void UpgradeTotemMoney()
    {
        TotalMoney -= totemUpgradeButtonMoney;
        totemUpgradeButtonLevel++;
        
        TotemUpgradeButtonMoney = (long)(Mathf.Pow(25, totemUpgradeButtonLevel) * 3);
        
        UIManager.Instance.rainTime += 5;
        //increase Income 
        incomeMultiplier *= 2;//increase income percentage
       
        UIManager.Instance.incomeInfo.text = "%" + String.Format("{0:0.00}", (incomeMultiplier) * 100);
        for (int i = 0; i < cropPrices.Length; i++)
        {
            //UIManager.Instance.cropMoneyInfoArray[i].text = FormatNumbers.RoundNumberLikeText((long)cropPrices[i]*incomeMultiplier).ToString()+ " $";
        }

        for (int i = 0; i < GameManager.Instance.circleParentsList.Count; i++)
        {
            if (i % 2 == 0)
            {
                GameManager.Instance.circleParentsList[i].GetComponent<RotateCircle>().planetSpeed -= speedButtonLevel;
            }
            else
            {
                GameManager.Instance.circleParentsList[i].GetComponent<RotateCircle>().planetSpeed += speedButtonLevel;
            }
        }

        //ýnfo text
        UIManager.Instance.speedInfo.text = Mathf.Abs(GameManager.Instance.circleParentsList[0].GetComponent<RotateCircle>().planetSpeed).ToString();
        ControlButtons();

    }

    public void ControlButtons()
    {
        if (totemUpgradeButtonLevel != 6)
        {
            if (TotalMoney >= TotemUpgradeButtonMoney) //activate totem button
            {
                UIManager.Instance.upgradeTotemButton.interactable = true;
            }
            else
            {
                UIManager.Instance.upgradeTotemButton.interactable = false;
            }
        }
        if (GameManager.Instance.addFarmerShouldbeOpened)
        {
            Debug.Log("total" + TotalMoney);
            Debug.Log("cost" + AddFarmerButtonMoney);
            if (TotalMoney >= AddFarmerButtonMoney) //activate add farmer button
            {
                Debug.Log("ac" + TotalMoney);
            
                UIManager.Instance.addPeopleButton.interactable = true;
            }
            else
            {
                Debug.Log("kapa");
                UIManager.Instance.addPeopleButton.interactable = false;
            }
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

        if (GameManager.Instance.addCircleShouldbeOpened)
        {
            if (TotalMoney >= AddCircleButtonMoney) //activate add circle button
            {
                UIManager.Instance.addCircleButton.interactable = true;
            }
            else
            {
                UIManager.Instance.addCircleButton.interactable = false;
            }
        }
        
    }
}
