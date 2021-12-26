using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class MainMenuButtons : MonoBehaviour {
    public int GameSceneBuildIndex;
    public GameObject SettingsPanel;
    public GameObject MainMenuPanel;
    public Button continueButton;

    public TMP_Dropdown HealthRateDropdown;
    public ActiveSetings ActiveSettingsGO;

    private void Awake() {
        if (DataExists()) {
            continueButton.interactable = true;
        } else {
            continueButton.interactable = false;
        }
    }

    public void Exit() {
        Application.Quit();
    }

    public void NewGame() {
        if (DataExists())
            DeleteData();
        Continue();
    }

    public void Continue() {
        SceneManager.LoadScene(GameSceneBuildIndex);
    }

    private string GetDataPath() {
        return Application.persistentDataPath + "/player.data";
    }

    private bool DataExists() {
        return File.Exists(GetDataPath());
    }

    private void DeleteData() {
        File.Delete(GetDataPath());
    }

    public void ChangeSetts() {
        int value = 1;
        switch (HealthRateDropdown.captionText.text) {
            case "Low":
                value = 1;
                break;

            case "Medium":
                value = 25;
                break;

            case "High":
                value = 50;
                break;
        }
        ActiveSettingsGO.HealthUpdates = value;
    }

    public void ReturnToMainFromSetts() {
        SettingsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void ToSettings() {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
}