using UnityEngine;
using TMPro;

[DefaultExecutionOrder(1)]
public class ui_Score : MonoBehaviour {
    public TextMeshProUGUI Coins;
    private PlayerManager _player;

    private void Awake() {
        _player = PlayerManager.Instance;
    }

    private void Start() {
        _player.OnCoinsChange += UpdateScore;
        UpdateScore();
    }

    public void UpdateScore() {
        Coins.text = "Coins: " + _player.Coins.ToString();
    }
}