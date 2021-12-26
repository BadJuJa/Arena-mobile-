using UnityEngine;
using TMPro;

public class StatsUpgrade : MonoBehaviour {

    [Header("UpgradeCostMult")]
    public float UpgradeCostBaseMult;

    [Header("BaseUpgradeCost")]
    public int HealthBaseCost = 1;

    public int DamageBaseCost = 1;
    public int CritBaseCost = 1;

    [Header("BonusValues")]
    public int HealthBonusValue;

    public int DamageBonusValue;
    public float CriticalMultBonusValue;

    [Header("Stats counters")]
    public TextMeshProUGUI HealthNow;

    public TextMeshProUGUI HealthCost;
    public TextMeshProUGUI DamageNow;
    public TextMeshProUGUI DamageCost;
    public TextMeshProUGUI CritNow;
    public TextMeshProUGUI CritMult;

    [HideInInspector] public int HealthUpgrades;
    [HideInInspector] public int DamageUpgrades;
    [HideInInspector] public int CritMultUpgrades;

    public int HealthUpgradeCost { get; private set; }
    public int DamageUpgradeCost { get; private set; }
    public int CritMultUpgradeCost { get; private set; }

    private PlayerManager player;

    private void Awake() {
        player = PlayerManager.Instance;
    }

    private void Start() {
        RefreshData();
    }

    private void RefreshUI() {
        HealthNow.text = player.MaxHealth.ToString();
        HealthCost.text = HealthUpgradeCost.ToString();
        DamageNow.text = player.Damage.ToString();
        DamageCost.text = DamageUpgradeCost.ToString();
        CritNow.text = player.CriticalHitMult.ToString();
        CritMult.text = CritMultUpgradeCost.ToString();
    }

    private void RefreshData() {
        HealthUpgrades = player.HealthUpgrades;
        DamageUpgrades = player.DamageUpgrades;
        CritMultUpgrades = player.CritMultUpgrades;

        HealthUpgradeCost = (int)Mathf.Ceil(HealthBaseCost * Mathf.Pow(UpgradeCostBaseMult, HealthUpgrades - 1));
        DamageUpgradeCost = (int)Mathf.Ceil(DamageBaseCost * Mathf.Pow(UpgradeCostBaseMult, DamageUpgrades - 1));
        CritMultUpgradeCost = (int)Mathf.Ceil(CritBaseCost * Mathf.Pow(UpgradeCostBaseMult, CritMultUpgrades - 1));

        RefreshUI();
    }

    public void UpgradeHealth() {
        player.IncreaseHealth(HealthBonusValue, HealthUpgradeCost);
        RefreshData();
    }

    public void UpgradeDamage() {
        player.IncreaseDamage(DamageBonusValue, DamageUpgradeCost);
        RefreshData();
    }

    public void UpgradeCritMult() {
        player.IncreaseCritMult(CriticalMultBonusValue, CritMultUpgradeCost);
        RefreshData();
    }
}