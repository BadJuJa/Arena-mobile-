using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour {
    public GameObject EnemyInfo;
    public GameObject PauseMenu;

    public static bool GameIsPaused;

    public void OpenPauseMenu() {
        //EnemyInfo.SetActive(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume() {
        //EnemyInfo.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadMenu() {
        //load main menu...if have one
    }

    public void QuitGame() {
        Application.Quit();
    }
}