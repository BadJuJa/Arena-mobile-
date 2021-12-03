using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {
    private Animator _animator;
    private EnemyController _enemyControllerInstance;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _enemyControllerInstance = EnemyController.Instance;
    }

    private void Start() {
        _enemyControllerInstance.OnAnimationTriggered += SetTrigger;
    }

    private void OnDestroy() {
        _enemyControllerInstance.OnAnimationTriggered -= SetTrigger;
    }

    private void SetTrigger(string triggerName) {
        _animator.SetTrigger(triggerName);
    }

    public void SetStun(int _) {
        _enemyControllerInstance.SetStun(_ == 1);
    }

    public void SetPossibilityToBeParried(int _) {
        _enemyControllerInstance.SetPossibilityToBeParried(_ == 1);
    }
}