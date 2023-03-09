using System;
using TMPro;
using UnityEngine;

public class OfflineProgress : MonoBehaviour
{
    public float offlineRewardMoney;
    public GameObject OfflineRewardPanel;
    public GameObject offlineMoneyText;
    public static OfflineProgress Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
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
                //offlineRewardMoney = FormatNumbers.RoundNumberLikeText((long)(GameDataManager.Instance.offlineProgressNum * (float)ts.TotalSeconds));
                Debug.Log(offlineRewardMoney);
                offlineMoneyText.GetComponent<TextMeshProUGUI>().text = FormatNumbers.AbbreviateNumber((long)offlineRewardMoney);
            }
            else
            {
                //offlineRewardMoney = GameDataManager.Instance.offlineProgressNum * 86400;
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
        //InterstitialAdManager.Instance.ShowInterstitial();
        GameDataManager.Instance.TotalMoney +=(long) offlineRewardMoney;
        //UIManager.Instance.TotalMoneyText.GetComponent<TextMeshProUGUI>().text = FormatNumbers.AbbreviateNumber(GameDataManager.Instance.TotalMoney);
        OfflineRewardPanel.SetActive(false);
    }

    public void OnOffine3MultipleReward()
    {
        //RewardedAdManager.Instance.MultipleOfflineProgressRewardAd();
    }
}