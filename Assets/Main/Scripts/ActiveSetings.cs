using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSetings : MonoBehaviour {
    public int HealthUpdates;

    private void Start() {
        Settings setts = Settings_SaveLoad.Load();
        HealthUpdates = setts.HealthUpdates;
    }
}