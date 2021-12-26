using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {
    private Animator _animator;
    private EnemyController _enemyController;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _enemyController = EnemyController.Instance;
    }

    private void Start() {
        _enemyController.OnAnimationTriggered += SetTrigger;
    }

    private void OnDestroy() {
        _enemyController.OnAnimationTriggered -= SetTrigger;
    }

    private void SetTrigger(string triggerName) {
        _animator.SetTrigger(triggerName);
    }

    public void SetStun(int _) {
        _enemyController.SetStun(_ == 1);
    }

    public void SetPossibilityToBeParried(int _) {
        _enemyController.SetPossibilityToBeParried(_ == 1);
    }

    public void DealDamage() {
        _enemyController.DealDamage();
    }

    public void AnimationEnd() {
        _enemyController.ResetAttack();
    }
}