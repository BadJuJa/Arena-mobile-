using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class UIButtonHandler : MonoBehaviourSingleton<UIButtonHandler> {

    public delegate void SpawnEnemyButtonClicked();

    public SpawnEnemyButtonClicked OnSpawnButtonClicked;

    public void SpawnEnemy() {
        Debug.Log("Button clicked!");
        if (OnSpawnButtonClicked != null)
            OnSpawnButtonClicked();
    }
}