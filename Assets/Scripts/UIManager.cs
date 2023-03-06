using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Button upgradeTotemButton;
    public TextMeshProUGUI upgradeTotemButtonPrice;
    public Button addPeopleButton;
    public TextMeshProUGUI addPeopleButtonPrice;
    public Button incomeButton;
    public TextMeshProUGUI incomeButtonPrice;
    public Button speedButton;
    public TextMeshProUGUI speedButtonPrice;
    public Button addCircleButton;
    public TextMeshProUGUI addCircleButtonPrice;
    public Button rainButton;
    public TextMeshProUGUI totalMoneyText;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        totalMoneyText.text = GameDataManager.Instance.TotalMoney.ToString();
        upgradeTotemButtonPrice.text = GameDataManager.Instance.totemUpgradeButtonMoney.ToString();
        addPeopleButtonPrice.text = GameDataManager.Instance.addFarmerButtonMoney.ToString();
        incomeButtonPrice.text = GameDataManager.Instance.incomeButtonMoney.ToString();
        speedButtonPrice.text = GameDataManager.Instance.speedButtonButtonMoney.ToString();
        addCircleButtonPrice.text = GameDataManager.Instance.addCircleButtonMoney.ToString();
    }

    public void OnSpeedUpgradeButton()
    {
        Debug.Log("Speed Money: " + GameDataManager.Instance.speedButtonButtonMoney);
        Debug.Log("Total Money: " + GameDataManager.Instance.TotalMoney);
        if (GameDataManager.Instance.speedButtonButtonMoney < GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.UpgradeSpeedMoney();
            
            foreach (GameObject circle in GameManager.Instance.circleParentsList)
            {
                circle.GetComponent<RotateCircle>().planetSpeed *= 1.1f;
            }
        }
    }

    public void OnTotemUpgradeButton()
    {
        if (GameDataManager.Instance.totemUpgradeButtonMoney < GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.UpgradeTotemMoney();
            
            GameManager.Instance.IncreaseAllFarmerLevels();
        }
    }

    public void OnIncomeButton()
    {
        if (GameDataManager.Instance.incomeButtonMoney < GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.UpgradeIncomeMoney();
        }
    }

    public void OnRainButton()
    {
        
    }
}
