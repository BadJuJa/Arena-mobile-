using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenuScripts : MonoBehaviour {
    public GameObject EnemyInfo;
    public GameObject PauseMenu;

    public static bool GameIsPaused;

    public void OpenPauseMenu() {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume() {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadMenu(string mainMenuSceneName) {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame() {
        Application.Quit();
    }
}