using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI totalMoneyText;
    public Button upgradeTotemButton;
    public TextMeshProUGUI upgradeTotemButtonPrice;
    public TextMeshProUGUI upgradeTotemButtonLevel;
    public Button addPeopleButton;
    public TextMeshProUGUI addPeopleButtonPrice;
    public TextMeshProUGUI addPeopleButtonLevel;
    public Button incomeButton;
    public TextMeshProUGUI incomeButtonPrice;
    public TextMeshProUGUI incomeButtonLevel;
    public Button speedButton;
    public TextMeshProUGUI speedButtonPrice;
    public TextMeshProUGUI speedButtonLevel;
    public Button addCircleButton;
    public TextMeshProUGUI addCircleButtonPrice;
    public Button rainButton;
    public GameObject rainParticles;
    public float rainTime = 10;
    public float timeLeft = 3;
    public bool isHold;
    public TextMeshProUGUI speedInfo;
    public TextMeshProUGUI incomeInfo;
    public TextMeshProUGUI[] cropMoneyInfoArray;
    

    
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(GameDataManager.Instance.TotalMoney) + " $";

        upgradeTotemButtonLevel.text = "LEVEL " + GameDataManager.Instance.totemUpgradeButtonLevel;
        upgradeTotemButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.TotemUpgradeButtonMoney) + " $";

        addPeopleButtonLevel.text = "LEVEL " + GameDataManager.Instance.addFarmerButtonLevel;
        addPeopleButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.AddFarmerButtonMoney) + " $";

        incomeButtonLevel.text = "LEVEL " + GameDataManager.Instance.incomeButtonLevel;
        incomeButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.IncomeButtonMoney) + " $";

        speedButtonLevel.text = "LEVEL " + GameDataManager.Instance.speedButtonLevel;
        speedButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.SpeedButtonButtonMoney) + " $";

        addCircleButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.AddCircleButtonMoney) + " $";
        speedInfo.text = (10 +(GameDataManager.Instance.speedButtonLevel) ).ToString();
        incomeInfo.text = (GameDataManager.Instance.incomeMultiplier * 100).ToString();
    }

    private void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        
        /*
        if(GameManager.Instance.circleParentsList[0].GetComponent<RotateCircle>().planetSpeed < (10 + (GameDataManager.Instance.speedButtonLevel * 1f)))
            GameManager.Instance.circleParentsList[0].GetComponent<RotateCircle>().planetSpeed -= timeLeft * 2;
        if(GameManager.Instance.circleParentsList[1].GetComponent<RotateCircle>().planetSpeed > -(10 + (GameDataManager.Instance.speedButtonLevel * 1f)))
            GameManager.Instance.circleParentsList[1].GetComponent<RotateCircle>().planetSpeed += timeLeft * 2;
        if(GameManager.Instance.circleParentsList[2].GetComponent<RotateCircle>().planetSpeed < (10 + (GameDataManager.Instance.speedButtonLevel * 1f)))
            GameManager.Instance.circleParentsList[2].GetComponent<RotateCircle>().planetSpeed -= timeLeft * 2;
            */
    }

    public void OnSpeedUpgradeButton()
    {
        if (GameDataManager.Instance.speedButtonButtonMoney < GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.UpgradeSpeedMoney();

      
        }
    }

    public void OnTotemUpgradeButton()
    {
        if (GameDataManager.Instance.TotemUpgradeButtonMoney <= GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.UpgradeTotemMoney();
            GameManager.Instance.IncreaseAllFarmerLevels();
        }
    }

    public void OnIncomeButton()
    {
        if (GameDataManager.Instance.IncomeButtonMoney <= GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.UpgradeIncomeMoney();
        }
    }

    public void OnAddFarmerButton()
    {
        if (GameDataManager.Instance.AddFarmerButtonMoney <= GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.UpgradeAddFarmerMoney();
            GameManager.Instance.AddFarmer();
        }
    }

    public void OnRainButton()
    {
        StartCoroutine(RainTimeCounter(rainTime));
    }

    IEnumerator RainTimeCounter(float time)
    {
        float tempSpeed = GameManager.Instance.circleParentsList[0].GetComponent<RotateCircle>().planetSpeed *= 1.1f;
        
        foreach (GameObject circle in GameManager.Instance.circleParentsList)
        {
            circle.GetComponent<RotateCircle>().planetSpeed *= 2.5f;
        }
        
        rainParticles.SetActive(true);
        rainButton.interactable = false;
        
        yield return new WaitForSeconds(time);
        rainButton.interactable = true;
        rainParticles.SetActive(false);
        
        foreach (GameObject circle in GameManager.Instance.circleParentsList)
        {
            //Formulden yeniden hesapalayip verilmesi lazim button level'ina gore
            GameManager.Instance.circleParentsList[0].GetComponent<RotateCircle>().planetSpeed = -(10 + (GameDataManager.Instance.speedButtonLevel * 1f));
            GameManager.Instance.circleParentsList[1].GetComponent<RotateCircle>().planetSpeed = (10 + (GameDataManager.Instance.speedButtonLevel * 1f));
            GameManager.Instance.circleParentsList[2].GetComponent<RotateCircle>().planetSpeed = -(10 + (GameDataManager.Instance.speedButtonLevel * 1f));
        }
    }

    
    public void tapIncreaseSpeed()
    {
        
    }

    IEnumerator DecreaseSpeed()
    {
        yield return new WaitForSeconds(timeLeft);
        
        
    }
    

}
