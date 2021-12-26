using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviourSingleton<EnemyController> {

    public enum AttackDirection {
        Up,
        Left,
        Right,
        Down
    }

    #region Events

    public delegate void AnimationTriggered(string triggerName);

    public event AnimationTriggered OnAnimationTriggered;

    public delegate void EnemyDied();

    public event EnemyDied OnEnemyDeath;

    public delegate void EnemyDamaged(float health);

    public event EnemyDamaged OnEnemyDamaged;

    public delegate void EnemyChanged(string name, float maxHealth);

    public event EnemyChanged OnEnemyChanged;

    #endregion Events

    #region Setups

    public List<EnemyData> EnemyDataList;
    public float RespawnTime;

    private SwipeDetection _swipeDetection;
    private PlayerManager _playerManager;

    private bool _canBeParried;
    private bool _stunned;
    private bool _canAttack;

    private AttackDirection _attackDirection;

    private float _maxHealth;
    private float _attackSpeed;
    private float _timeBetweenAttacks;
    private int _health;
    private int _damage;
    private int _costOfCoins;

    private bool _isSpawned;
    private float _timer;

    #endregion Setups

    #region Unity Methods

    private void Awake() {
        _swipeDetection = SwipeDetection.Instance;
        _playerManager = PlayerManager.Instance;
        _swipeDetection.OnSwipeDone += HandlePlayerActions;
    }

    private void Start() {
        _isSpawned = false;
        _canAttack = false;
        if (EnemyDataList != null) {
            StartCoroutine(LoadEnemy(ChooseEnemy()));
        }
    }

    private void Update() {
        if (!_canAttack || !_isSpawned)
            return;
        if (_timer <= 0f)
            _timer = _timeBetweenAttacks;

        if (_timer > 0f) {
            _timer -= Time.deltaTime;
            if (_timer <= 0) {
                Attack();
            }
        }
    }

    #endregion Unity Methods

    private void HandlePlayerActions(SwipeDetection.SwipeDirection swipeDirection) {
        if (!_playerManager.CanAttack || !_isSpawned)
            return;

        if (_canBeParried && swipeDirection.ToString() == _attackDirection.ToString()) {
            _stunned = true;
            OnAnimationTriggered?.Invoke("GetStunned");
            _canBeParried = false;
        } else {
            TakeDamage();
        }
        _playerManager.Attacked();
    }

    public void TakeDamage() {
        if (!_stunned) {
            _health -= _playerManager.Damage;
        } else {
            _health -= (int)Mathf.Ceil(_playerManager.Damage * _playerManager.CriticalHitMult);
        }
        _health = (int)Mathf.Clamp(_health, 0, _maxHealth);
        OnEnemyDamaged?.Invoke(_health);

        if (_health <= 0) {
            Die();
        }
    }

    private void Attack() {
        _canAttack = false;

        _attackDirection = (AttackDirection)Random.Range(0, 3);
        OnAnimationTriggered?.Invoke("Attack" + _attackDirection.ToString());
    }

    public void DealDamage() {
        _playerManager.TakeDamage(_damage);
    }

    public void ResetAttack() {
        _canAttack = true;
    }

    public void Die() {
        _isSpawned = false;
        _playerManager.AddCoins(_costOfCoins);
        OnEnemyDeath?.Invoke();
        StartCoroutine(LoadEnemy(ChooseEnemy()));
    }

    #region Service Methods

    private IEnumerator LoadEnemy(EnemyData data) {
        foreach (Transform child in transform) {
            if (Application.isEditor) {
                DestroyImmediate(child.gameObject);
            } else {
                Destroy(child.gameObject);
            }
        }
        _maxHealth = data.Health;
        _health = data.Health;
        _damage = data.Damage;
        _attackSpeed = data.AttackSpeed;
        _costOfCoins = data.CostOfCoins;
        _timeBetweenAttacks = 100 / _attackSpeed;
        yield return new WaitForSeconds(RespawnTime);

        OnEnemyChanged?.Invoke(data.Name, data.Health);

        GameObject visuals = Instantiate(data.Prefab);
        visuals.transform.SetParent(transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;
        _isSpawned = true;
        ResetAttack();
    }

    private EnemyData ChooseEnemy() {
        return EnemyDataList[Random.Range(0, EnemyDataList.Count)];
    }

    public void SetStun(bool set) {
        _stunned = set;
    }

    public void SetPossibilityToBeParried(bool set) {
        _canBeParried = set;
    }

    #endregion Service Methods
}