using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject {
    public float Health;
    public float Damage;
    public float CriticalHitMult;

    public int Coins;

    public void AddCoins(int value) {
        Coins += value;
    }

    public void Load(float health, float damage, float crit, int coins) {
        Health = health;
        Damage = damage;
        CriticalHitMult = crit;
        Coins = coins;
    }
}