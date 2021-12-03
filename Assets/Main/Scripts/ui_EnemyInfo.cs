using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ui_EnemyInfo : MonoBehaviour {
    public TextMeshProUGUI EnemyName;
    public TextMeshProUGUI Health;
    public Slider HealthSlider;

    private float _enemyMaxHealth;

    private EnemyController _enemy;

    private void Awake() {
        _enemy = EnemyController.Instance;
    }

    private void Start() {
        HideUI();
        _enemy.OnEnemyDamaged += UpdateCurrentHealth;
        _enemy.OnEnemyChanged += UpdateEnemyInfo;
        _enemy.OnEnemyDeath += HideUI;
    }

    private void UpdateCurrentHealth(float currentHealth) {
        Health.text = currentHealth + " / " + _enemyMaxHealth;
        HealthSlider.value = currentHealth / _enemyMaxHealth;
    }

    private void UpdateEnemyInfo(string name, float maxHealth) {
        EnemyName.text = name;
        _enemyMaxHealth = maxHealth;
        UpdateCurrentHealth(maxHealth);
        gameObject.SetActive(true);
    }

    private void HideUI() {
        gameObject.SetActive(false);
    }
}