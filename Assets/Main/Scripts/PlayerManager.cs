using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager> {
    public PlayerData PlayerData;

    private float _health;
    private float _damage;
    private float _criticalHitMult;
    private bool _canAttack;
    [SerializeField] private int _coins;

    public int GetCoins {
        get {
            return _coins;
        }
    }

    public float GetHealth {
        get {
            return _health;
        }
    }

    public float GetDamage {
        get {
            return _damage;
        }
    }

    public float GetCriticalHitMult {
        get {
            return _criticalHitMult;
        }
    }

    public bool CanAttack {
        get {
            return _canAttack;
        }
    }

    private void Start() {
        LoadData(PlayerData);
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

    public void TakeDamage(float damage) {
        _health -= damage;
    }

    public void AddCoins(int value) {
        PlayerData.AddCoins(value);
        _coins += value;
    }

    private void LoadData(PlayerData data) {
        _health = data.Health;
        _damage = data.Damage;
        _criticalHitMult = data.CriticalHitMult;
        _coins = data.Coins;
    }
}