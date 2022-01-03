using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ActiveSetings : MonoBehaviour {
    public int HealthUpdates;

    private void Start() {
        Settings setts = Settings_SaveLoad.Load();
        HealthUpdates = setts.HealthUpdates;
    }
}