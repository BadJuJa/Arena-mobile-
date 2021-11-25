using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject {
    public string Name;
    public float Health = 100;
    public float Damage = 1;
    public float AttackSpeed = 1;
}