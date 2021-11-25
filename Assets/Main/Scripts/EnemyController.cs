using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour {

    public enum AttackDirection {
        Up,
        Left,
        Right,
        Down
    }

    #region Setups

    public EnemyData ThisEnemyData;

    private SwipeDetection _swipeDetection;
    private PlayerManager _playerManager;

    private Animator _animator;
    private bool _canBeParried;
    private bool _stunned;
    private bool _canAttack;

    private AttackDirection _attackDirection;

    private float _health;
    private float _damage;
    private float _attackSpeed;

    #endregion Setups

    #region Unity Methods

    private void Awake() {
        _animator = GetComponent<Animator>();
        _swipeDetection = SwipeDetection.Instance;
        _playerManager = PlayerManager.Instance;
    }

    private void Start() {
        LoadData();
        _swipeDetection.OnSwipeDone += HandlePlayerActions;
        _canAttack = false;
        Invoke("ResetAttack", 3);
    }

    private void Update() {
        if (_canAttack) {
            Attack();
        }
        if (_health <= 0) {
            Die();
        }
    }

    #endregion Unity Methods

    private void HandlePlayerActions(SwipeDetection.SwipeDirection swipeDirection) {
        if (_canBeParried && swipeDirection.ToString() == _attackDirection.ToString()) {
            _stunned = true;
            _animator.SetTrigger("GetStunned");
            _canBeParried = false;
        } else {
            TakeDamage();
        }
    }

    public void TakeDamage() {
        if (!_stunned) {
            _health -= _playerManager.Damage;
        } else {
            _health -= _playerManager.Damage * _playerManager.CriticalHitMult;
        }
    }

    private void Attack() {
        _canAttack = false;
        _attackDirection = (AttackDirection)Random.Range(0, 3);
        _animator.SetTrigger("Attack" + _attackDirection.ToString());
        Invoke("ResetAttack", 100 / _attackSpeed);
    }

    public void DealDamage() {
        _playerManager.TakeDamage(_damage);
    }

    private void ResetAttack() {
        _canAttack = true;
    }

    public void Die() {
        EnemySpawner.isSpawned = false;
        Destroy(gameObject);
    }

    #region Service Methods

    private void LoadData() {
        _health = ThisEnemyData.Health;
        _damage = ThisEnemyData.Damage;
        _attackSpeed = ThisEnemyData.AttackSpeed;
    }

    public void SetStun(int _) {
        _stunned = _ == 1;
    }

    public void SetPossibilityToBeParried(int _) {
        _canBeParried = _ == 1;
    }

    #endregion Service Methods
}