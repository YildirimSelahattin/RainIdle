using System;
using TMPro;
using UnityEngine;

public class OfflineEarning : MonoBehaviour
{
    public float offlineRewardMoney;
    public GameObject OfflineRewardPanel;
    public GameObject offlineMoneyText;
    public static OfflineEarning Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        OfflinePanelControl();
    }

    public void OfflinePanelControl()
    {
        if (PlayerPrefs.HasKey("LAST_LOGIN"))
        {
            DateTime lastLogIn = DateTime.Parse(PlayerPrefs.GetString("LAST_LOGIN"));

            TimeSpan ts = DateTime.Now - lastLogIn;
            
            Debug.Log(ts.TotalSeconds);

            if (ts.TotalSeconds < 86400)
            {
                offlineRewardMoney = FormatNumbers.RoundNumberLikeText((long)(GameDataManager.Instance.offlineProgressNum * GameDataManager.Instance.incomeMultiplier * (float)ts.TotalSeconds));
                Debug.Log(offlineRewardMoney);
                offlineMoneyText.GetComponent<TextMeshProUGUI>().text = FormatNumbers.AbbreviateNumber((long)offlineRewardMoney);
            }
            else
            {
                offlineRewardMoney = GameDataManager.Instance.offlineProgressNum * GameDataManager.Instance.incomeMultiplier * 86400;
            }
        }
        else
        {
            Debug.Log("First Login");
            OfflineRewardPanel.SetActive(false);
        }
        
        PlayerPrefs.SetString("LAST_LOGIN", DateTime.Now.ToString());
    }
    
    public void OnOfflineReward()
    {
        GameDataManager.Instance.TotalMoney +=(long) offlineRewardMoney;
        UIManager.Instance.totalMoneyText.GetComponent<TextMeshProUGUI>().text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.TotalMoney);
        OfflineRewardPanel.SetActive(false);
    }

    public void OnOffine3MultipleReward()
    {
        GameDataManager.Instance.TotalMoney += (long)offlineRewardMoney * 3;
        UIManager.Instance.totalMoneyText.GetComponent<TextMeshProUGUI>().text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.TotalMoney);
        OfflineRewardPanel.SetActive(false);
    }
}