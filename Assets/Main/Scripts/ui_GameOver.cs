using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class ui_GameOver : MonoBehaviour {
    private PlayerManager _player;

    public GameObject GameOverScreen;

    private void Awake() {
        _player = PlayerManager.Instance;
        _player.OnPlayersDeath += GameOver;
    }

    private void GameOver() {
        DeletePlayerData();
        Debug.Log(File.Exists(Application.persistentDataPath + "/player.data"));
        GameOverScreen.SetActive(true);
        Time.timeScale = 0.2f;
    }

    public void TryAgain() {
        Time.timeScale = 1f;
        PlayerManager.Instance.LoadData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu(int mainMenuSceneBuildIndex) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneBuildIndex);
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void DeletePlayerData() {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path)) {
            File.Delete(path);
        }
    }
}