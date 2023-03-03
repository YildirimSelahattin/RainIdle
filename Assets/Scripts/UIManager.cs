using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        foreach (GameObject circle in GameManager.Instance.circleParentsList)
        {
            GameDataManager.Instance.speedButtonLevel++;
            circle.GetComponent<RotateCircle>().planetSpeed *= 1.1f;
        }
    }
}
