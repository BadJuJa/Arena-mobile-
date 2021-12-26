[System.Serializable]
public class PlayerData {
    public float CriticalHitMult;
    public int MaxHealth;
    public int Health;
    public int Damage;
    public int Coins;
    public int HealthUpgrades;
    public int DamageUpgrades;
    public int CritMultUpgrades;

    public PlayerData() {
        MaxHealth = DefaultSetups.MaxHealth;
        Damage = DefaultSetups.Damage;
        CriticalHitMult = DefaultSetups.CriticalHitMult;
        Health = MaxHealth;
        Coins = 0;
        HealthUpgrades = 0;
        DamageUpgrades = 0;
        CritMultUpgrades = 0;
    }

    public PlayerData(PlayerManager player) {
        MaxHealth = player.MaxHealth;
        Health = player.Health;
        Damage = player.Damage;
        CriticalHitMult = player.CriticalHitMult;
        Coins = player.Coins;
        HealthUpgrades = player.HealthUpgrades;
        DamageUpgrades = player.DamageUpgrades;
        CritMultUpgrades = player.CritMultUpgrades;
    }
}