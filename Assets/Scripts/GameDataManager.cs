using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberOfPeople;
    public static GameDataManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData()
    {
        numberOfPeople = PlayerPrefs.GetInt("numberOfPeople",6);
    }
}
