using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{

    int isSoundOn;
    int isMusicOn;
    int isVibrateOn;

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
    public float remainingSpeedTime = 5;
    public int tapIncreaseSpeedCounter = 0;
    public TextMeshProUGUI speedInfo;
    public TextMeshProUGUI incomeInfo;
    public TextMeshProUGUI PeopleInfo;
    public TextMeshProUGUI[] cropMoneyInfoArray;
    public GameObject increaseParticleGameObject;
    [SerializeField] GameObject soundOn;
    [SerializeField] GameObject soundOff;
    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;
    [SerializeField] GameObject vibrationOff;
    [SerializeField] GameObject vibrationOn;
    [SerializeField] GameObject optionBar;
    [SerializeField] GameObject gameMusic;


    public Image rainDropImage;
    public int fillTime;
    public float fillCounter;
    public bool shouldCount;
    public float rainMultiplier = 1;
    public float tapSpeedMultiplier = 1;
    public bool speedButtonUp = false;
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        totalMoneyText.text = FormatNumbers.AbbreviateNumberForTotalMoney(GameDataManager.Instance.TotalMoney) + " $";

        if(GameDataManager.Instance.totemUpgradeButtonLevel == 6)
        {
            upgradeTotemButton.interactable = false;
            upgradeTotemButtonLevel.text = "MAX";
            upgradeTotemButtonPrice.text = "";
        }
        upgradeTotemButtonLevel.text = "LEVEL " + GameDataManager.Instance.totemUpgradeButtonLevel;
        upgradeTotemButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.TotemUpgradeButtonMoney) + " $";
        if (GameDataManager.Instance.numberOfPeople == 36)
        {
            addPeopleButton.interactable = false;
            addPeopleButtonLevel.text = "MAX";
            addPeopleButtonPrice.text = "";
        }
        else
        {
            addPeopleButtonLevel.text = "LEVEL " + GameDataManager.Instance.addFarmerButtonLevel;
            addPeopleButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.AddFarmerButtonMoney) + " $";
        }
        

        incomeButtonLevel.text = "LEVEL " + GameDataManager.Instance.incomeButtonLevel;
        incomeButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.IncomeButtonMoney) + " $";

        if (GameDataManager.Instance.speedButtonLevel == 30)
        {
            speedButton.interactable = false;
            speedButtonLevel.text = "MAX";
            speedButtonPrice.text = "";
        }
        
        else
        {
            speedButtonLevel.text = "LEVEL " + GameDataManager.Instance.speedButtonLevel;
            speedButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.SpeedButtonButtonMoney) + " $";
        }
        

        addCircleButtonPrice.text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.AddCircleButtonMoney) + " $";

        speedInfo.text = (10 +(GameDataManager.Instance.speedButtonLevel) ).ToString();
        incomeInfo.text = (GameDataManager.Instance.incomeMultiplier * 100).ToString();
        PeopleInfo.text = GameDataManager.Instance.numberOfPeople.ToString();
        
    }

    private void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        if (remainingSpeedTime > 1)
        {
            remainingSpeedTime -= Time.deltaTime;
        }
        else
        {
            remainingSpeedTime = 1f;
        }

        if (speedButtonUp)
        {
            if (tapSpeedMultiplier > 1)
            {
                tapSpeedMultiplier -= (Time.deltaTime / remainingSpeedTime) * 8;
                Debug.Log(tapSpeedMultiplier);
            }
            else
            {
                speedButtonUp = false;
            }
        }
        if (shouldCount == true)
        {
            if (fillCounter < fillTime)
            {
                fillCounter += Time.deltaTime;
                FillRainButton();
            }
            else
            {
                fillCounter = fillTime;
                shouldCount = false;
            }
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
    public void FillRainButton()
    {
        rainDropImage.fillAmount = fillCounter / fillTime;
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
        rainParticles.SetActive(true);
        rainButton.interactable = false;
        rainMultiplier = 2f;
        yield return new WaitForSeconds(time);
        rainButton.interactable = true;
        rainParticles.SetActive(false);
        rainMultiplier = 1f;
    }

    public IEnumerator StartFillingRainButton()
    {
        yield return new WaitForSeconds(3);
        shouldCount = true;
        fillCounter = 0;
    }
    


    public void tapIncreaseSpeed()
    {
        StopAllCoroutines();
        if (tapIncreaseSpeedCounter < 5)
        {
            tapIncreaseSpeedCounter += 1;
            tapSpeedMultiplier += 1.1f;
            Debug.Log("Tap: " + tapSpeedMultiplier);
            StartCoroutine(DecreaseSpeed());
        }
        else
        {
            StartCoroutine(DecreaseSpeed());
        }
    }

    IEnumerator DecreaseSpeed()
    {
        yield return new WaitForSeconds(3);
        speedButtonUp = true;
        tapIncreaseSpeedCounter = 0;
    }

    public void UpdateSound()
    {
        isSoundOn = GameDataManager.Instance.playSound;
        if (isSoundOn == 0)
        {
            soundOff.gameObject.SetActive(true);
            SoundsOff();
        }

        if (isSoundOn == 1)
        {
            soundOn.gameObject.SetActive(true);
            SoundsOn();
        }
    }

    public void UpdateMusic()
    {
        isMusicOn = GameDataManager.Instance.playMusic;
        if (isMusicOn == 0)
        {
            musicOff.gameObject.SetActive(true);
            MusicOff();
        }

        if (isMusicOn == 1)
        {
            musicOn.gameObject.SetActive(true);
            MusicOn();
        }
    }

    public void UpdateVibrate()
    {
        isVibrateOn = GameDataManager.Instance.playVibrate;
        if (isVibrateOn == 0)
        {
            vibrationOff.gameObject.SetActive(true);
            VibrationOff();
        }

        if (isVibrateOn == 1)
        {
            vibrationOn.gameObject.SetActive(true);
            VibrationOn();
        }
    }

    public void MusicOff()
    {
        GameDataManager.Instance.playMusic = 0;
        musicOn.gameObject.SetActive(false);
        musicOff.gameObject.SetActive(true);
        gameMusic.SetActive(false);
        //UpdateMusic();

    }

    public void MusicOn()
    {
        GameDataManager.Instance.playMusic = 1;
        musicOff.gameObject.SetActive(false);
        musicOn.gameObject.SetActive(true);
        gameMusic.SetActive(true);
        //UpdateMusic();
    }

    public void SoundsOff()
    {
        GameDataManager.Instance.playSound = 0;
        soundOn.gameObject.SetActive(false);
        soundOff.gameObject.SetActive(true);
        //UpdateSound();
    }

    public void SoundsOn()
    {
        GameDataManager.Instance.playSound = 1;
        soundOff.gameObject.SetActive(false);
        soundOn.gameObject.SetActive(true);
        //UpdateSound();
    }

    public void VibrationOff()
    {
        GameDataManager.Instance.playVibrate = 0;
        vibrationOn.gameObject.SetActive(false);
        vibrationOff.gameObject.SetActive(true);
        Handheld.Vibrate();
        //UpdateVibrate();
    }

    public void VibrationOn()
    {
        GameDataManager.Instance.playVibrate = 1;
        vibrationOff.gameObject.SetActive(false);
        vibrationOn.gameObject.SetActive(true);
        Handheld.Vibrate();
       // UpdateVibrate();

    }

    public void VibratePhone(){
        Handheld.Vibrate();
    }
    public void RetryLevel()
    {
        //LoadMainMenu.Instance.LoadSceneMenu(1);
    }
    public void NextLevel()
    {
        //GameDataManager.Instance.levelToLoad += 1;
        //GameDataManager.Instance.SaveData();
        //LoadMainMenu.Instance.LoadSceneMenu(1);

    }

    public void OpenCloseOptionBar()
    {
        if (optionBar.active)
        {
            optionBar.SetActive(false);
        }
        else
        {
            optionBar.SetActive(true);  
        }
    }

}
