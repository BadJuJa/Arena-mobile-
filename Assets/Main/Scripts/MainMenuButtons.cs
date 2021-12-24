using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {
    public string GameSceneName;
    public PlayerData PlayerData;

    public void Exit() {
        Application.Quit();
    }

    public void NewGame() {
        if (PlayerData != null) {
            PlayerData.Load(100, 15, 5, 0);
        }
        Continue();
    }

    public void Continue() {
        SceneManager.LoadScene(GameSceneName);
    }
}