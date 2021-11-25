using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager> {
    public PlayerData PlayerData;

    private float _health;
    private float _damage;
    private float _criticalHitMult;
    private float _attackSpeed;

    public float Health {
        get {
            return _health;
        }
    }

    public float Damage {
        get {
            return _damage;
        }
    }

    public float AttackSpeed {
        get {
            return _attackSpeed;
        }
    }

    public float CriticalHitMult {
        get {
            return _criticalHitMult;
        }
    }

    private void Start() {
        LoadData();
    }

    public void TakeDamage(float damage) {
        _health -= damage;
    }

    private void LoadData() {
        _health = PlayerData.Health;
        _damage = PlayerData.Damage;
        _criticalHitMult = PlayerData.CriticalHitMult;
        _attackSpeed = PlayerData.AttackSpeed;
    }
}