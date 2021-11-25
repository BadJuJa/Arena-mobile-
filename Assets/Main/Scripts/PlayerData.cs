using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject {
    public float Health;
    public float Damage;
    public float CriticalHitMult;
    public float AttackSpeed;
}