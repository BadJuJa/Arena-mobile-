using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ui_EnemyInfo : MonoBehaviour {
    public TextMeshProUGUI EnemyName;
    public TextMeshProUGUI Health;
    public Slider Healthbar;
    public ActiveSetings settings;

    //[Header("Smoothness of value changing (default = 50)")]
    private int Smoothness = 50;

    private float _enemyMaxHealth;

    private EnemyController _enemy;

    private void Awake() {
        _enemy = EnemyController.Instance;
    }

    private void Start() {
        HideUI();
        Smoothness = settings.HealthUpdates;
        _enemy.OnEnemyDamaged += UpdateCurrentHealth;
        _enemy.OnEnemyChanged += UpdateEnemyInfo;
        _enemy.OnEnemyDeath += HideUI;
    }

    private void UpdateCurrentHealth(float currentHealth) {
        Health.text = currentHealth + " / " + _enemyMaxHealth;
        float deltaValue = Healthbar.value - currentHealth / _enemyMaxHealth;
        StartCoroutine(SmoothHealthChanging(deltaValue));
    }

    private void UpdateEnemyInfo(string name, float maxHealth) {
        EnemyName.text = name;
        _enemyMaxHealth = maxHealth;
        Health.text = maxHealth + " / " + maxHealth;
        Healthbar.value = 1f;
        gameObject.SetActive(true);
    }

    private void HideUI() {
        gameObject.SetActive(false);
    }

    private IEnumerator SmoothHealthChanging(float delta) {
        for (int i = 0; i < Smoothness; i++) {
            Healthbar.value -= delta / Smoothness;
            yield return new WaitForSeconds(1 / Smoothness);
        }
    }
}