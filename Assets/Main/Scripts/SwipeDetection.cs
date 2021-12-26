using UnityEngine;

[DefaultExecutionOrder(-1)]
public class SwipeDetection : MonoBehaviourSingleton<SwipeDetection> {

    #region Events

    public delegate void SwipeDone(SwipeDirection swipeDirection);

    public event SwipeDone OnSwipeDone;

    #endregion Events

    public enum SwipeDirection {
        Up,
        Left,
        Right,
        Down
    }

    [SerializeField] private float _minimumDistance = .2f;
    [SerializeField] private float _maximumTime = 1f;
    [SerializeField] private float _directionThreshold = .9f;

    private InputManager _inputManager;

    private SwipeDirection _swipeDirection;
    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;

    private void Awake() {
        _inputManager = InputManager.Instance;
    }

    private void OnEnable() {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable() {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time) {
        _startPosition = position;
        _startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time) {
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe() {
        if (Vector3.Distance(_startPosition, _endPosition) >= _minimumDistance && (_endTime - _startTime) <= _maximumTime) {
            Vector3 direction = _endPosition - _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            ProcessSwipeDirection(direction2D);
        }
    }

    private void ProcessSwipeDirection(Vector2 direction) {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold) {
            _swipeDirection = SwipeDirection.Up;
        } else if (Vector2.Dot(Vector2.down, direction) > _directionThreshold) {
            _swipeDirection = SwipeDirection.Down;
        } else if (Vector2.Dot(Vector2.left, direction) > _directionThreshold) {
            _swipeDirection = SwipeDirection.Left;
            ;
        } else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold) {
            _swipeDirection = SwipeDirection.Right;
        }

        if (OnSwipeDone != null) {
            OnSwipeDone(_swipeDirection);
        }
    }
}