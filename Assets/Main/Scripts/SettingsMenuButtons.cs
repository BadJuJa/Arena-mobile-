using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuButtons : MonoBehaviour {
    public ActiveSetings settings;
    public Button SaveButton;
    public Button CancelButton;

    public void SaveSettings() {
        Settings_SaveLoad.Save(settings);
    }
}