using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class EnemySpawner : MonoBehaviourSingleton<EnemySpawner> {
    public Transform EnemySpawnPoint;
    public List<GameObject> EnemyList;

    private UIButtonHandler _uiButtonHandler;

    public static bool isSpawned;

    private void Awake() {
        _uiButtonHandler = UIButtonHandler.Instance;
    }

    private void Start() {
        _uiButtonHandler.OnSpawnButtonClicked += SpawnEnemy;
    }

    public void SpawnEnemy() {
        if (EnemyList != null && !isSpawned) {
            GameObject enemy = EnemyList[Random.Range(0, EnemyList.Count)];
            Instantiate(enemy, EnemySpawnPoint.position, Quaternion.identity);
            isSpawned = true;
        }
    }
}