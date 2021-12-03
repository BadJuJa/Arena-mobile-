using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(1)]
public class ui_Score : MonoBehaviour {
    public TextMeshProUGUI Coins;
    private EnemyController _enemy;
    private PlayerManager _player;

    private void Awake() {
        _enemy = EnemyController.Instance;
        _player = PlayerManager.Instance;
    }

    private void Start() {
        _enemy.OnEnemyDeath += UpdateScore;
        UpdateScore();
    }

    public void UpdateScore() {
        Coins.text = "Coins: " + _player.GetCoins.ToString();
    }
}