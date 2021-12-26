using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(10)]
public class PlayersHealthBar : MonoBehaviour {
    public Slider Healthbar;
    public TextMeshProUGUI HealthValue;
    public ActiveSetings settings;

    //[Header("Smoothness of value changing (default = 50)")]
    private int Smoothness;

    private PlayerManager _player;

    private void Awake() {
        _player = PlayerManager.Instance;
        _player.OnHealthChanged += UpdateHealth;
    }

    private void Start() {
        Smoothness = settings.HealthUpdates;
        Healthbar.value = _player.HealthPercent;
        UpdateHealth();
    }

    public void UpdateHealth() {
        float deltaValue = Healthbar.value - _player.HealthPercent;
        HealthValue.text = _player.Health.ToString();
        StartCoroutine(SmoothHealthChanging(deltaValue));
    }

    private IEnumerator SmoothHealthChanging(float delta) {
        for (int i = 0; i < Smoothness; i++) {
            Healthbar.value -= delta / Smoothness;
            yield return new WaitForSeconds(1 / Smoothness);
        }
    }
}