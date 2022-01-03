using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject {
    public string Name;
    public GameObject Prefab;
    public int CostOfCoins;
    public int Health = 100;
    public int Damage = 1;
    public float AttackSpeed = 1;
}