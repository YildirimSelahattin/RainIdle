using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Button upgradeTotemButton;
    public Button addPeopleButton;
    public Button incomeButton;
    public Button speedButton;
    public Button addCircleButton;
    public Button rainButton;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void OnSpeedUpgradeButton()
    {
        if (GameDataManager.Instance.speedButtonButtonMoney > GameDataManager.Instance.totalMoney)
        {
            foreach (GameObject circle in GameManager.Instance.circleParentsList)
            {
                GameDataManager.Instance.speedButtonLevel++;
                circle.GetComponent<RotateCircle>().planetSpeed *= 1.1f;
            }
        }
    }

    public void OnTotemUpgradeButton()
    {
        if (GameDataManager.Instance.totemUpgradeButtonMoney > GameDataManager.Instance.totalMoney)
        {
            GameDataManager.Instance.totemUpgradeButtonLevel++;
            GameManager.Instance.totemParts[GameDataManager.Instance.totemUpgradeButtonLevel - 1].SetActive(true);
            GameManager.Instance.IncreaseAllFarmerLevels();
        }
    }

    public void OnIncomeButton()
    {
        if (GameDataManager.Instance.incomeButtonMoney > GameDataManager.Instance.totalMoney)
        {
        }
    }

    public void OnRainButton()
    {
        
    }
}
