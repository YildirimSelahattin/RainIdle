using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomaGames.HomaBelly;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
        DefaultAnalytics.MainMenuLoaded();
        LoadMainMenu.Instance.LoadSceneMenu(2);
    }
}
