using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager> {

    #region Setups

    private float _criticalHitMult;
    private bool _canAttack;
    private int _maxHealth;
    private int _health;
    private int _damage;
    private int _coins;
    private int _healthUpgrades;
    private int _damageUpgrades;
    private int _critMultUpgrades;

    #endregion Setups

    #region Properties

    public int Coins { get => _coins; }
    public int HealthUpgrades { get => _healthUpgrades; }
    public int DamageUpgrades { get => _damageUpgrades; }
    public int CritMultUpgrades { get => _critMultUpgrades; }
    public int MaxHealth { get => _maxHealth; }
    public int Health { get => _health; }
    public float HealthPercent { get => (float)_health / (float)_maxHealth; }
    public int Damage { get => _damage; }
    public float CriticalHitMult { get => _criticalHitMult; }
    public bool CanAttack { get => _canAttack; }

    #endregion Properties

    #region Events

    public delegate void CoinsChange();

    public event CoinsChange OnCoinsChange;

    public delegate void HealthChanged();

    public event HealthChanged OnHealthChanged;

    public delegate void Death();

    public event Death OnPlayersDeath;

    #endregion Events

    private void OnDisable() {
        if (_health <= 0)
            return;
        SaveSystem.Save(this);
    }

    private void Start() {
        LoadData();
        _canAttack = true;
    }

    public void Attacked() {
        _canAttack = false;
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack() {
        yield return new WaitForSeconds(0.7f);
        _canAttack = true;
    }

    public void TakeDamage(int damage) {
        if (damage < _health) {
            _health -= damage;
        } else {
            _health = 0;
        }
        OnHealthChanged?.Invoke();
        if (_health == 0) {
            Die();
        }
    }

    public void Die() {
        OnPlayersDeath?.Invoke();
    }

    public void AddCoins(int value) {
        _coins += value;
        OnCoinsChange?.Invoke();
    }

    public void ReduceCoins(int value) {
        _coins -= value;
        OnCoinsChange?.Invoke();
    }

    public void IncreaseHealth(int value, int price) {
        if (_coins < price)
            return;
        _maxHealth += value;
        _health += value;
        _healthUpgrades++;
        ReduceCoins(price);
        OnHealthChanged?.Invoke();
    }

    public void IncreaseDamage(int value, int price) {
        if (_coins < price)
            return;
        _damage += value;
        _damageUpgrades++;
        ReduceCoins(price);
    }

    public void IncreaseCritMult(float value, int price) {
        if (_coins < price)
            return;
        _criticalHitMult += value;
        _critMultUpgrades++;
        ReduceCoins(price);
    }

    public void LoadData() {
        PlayerData data = SaveSystem.Load();
        _maxHealth = data.MaxHealth;
        _health = data.Health;
        _damage = data.Damage;
        _criticalHitMult = data.CriticalHitMult;
        _coins = data.Coins;
        _healthUpgrades = data.HealthUpgrades;
        _damageUpgrades = data.DamageUpgrades;
        _critMultUpgrades = data.CritMultUpgrades;
    }
}