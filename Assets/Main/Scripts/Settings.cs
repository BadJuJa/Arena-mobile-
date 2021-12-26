[System.Serializable]
public class Settings {
    public int HealthUpdates;

    public Settings() {
        HealthUpdates = DefaultSetups.BasicHealthUpdatesRate;
    }

    public Settings(ActiveSetings settings) {
        HealthUpdates = settings.HealthUpdates;
    }
}