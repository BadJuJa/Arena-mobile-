using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour {

    public delegate void SwipeDelegate(StructsAndEnums.Direction direction);

    public static event SwipeDelegate OnSwipe;

    public float MinSwipeDistance = 80f;

    private Vector2 tapPosition;
    private Vector2 swipeDelta;

    private bool isSwiping;
    private bool isMobile;

    private void Start() {
        isMobile = Application.isMobilePlatform;
    }

    private void Update() {
        if (!isMobile) {
            if (Input.GetMouseButtonDown(0) &&
                Input.mousePosition.y < Screen.height - 350) {
                isSwiping = true;
                tapPosition = Input.mousePosition;
            } else if (Input.GetMouseButtonUp(0) || Input.mousePosition.y > Screen.height - 350) {
                ResetSwipe();
            }
        } else {
            if (Input.touchCount > 0) {
                TouchPhase phase = Input.GetTouch(0).phase;
                if (phase == TouchPhase.Began) {
                    isSwiping = true;
                    tapPosition = Input.GetTouch(0).position;
                } else if (phase == TouchPhase.Ended || phase == TouchPhase.Canceled) {
                    ResetSwipe();
                }
            }
        }

        CheckSwipe();
    }

    private void CheckSwipe() {
        swipeDelta = Vector2.zero;

        if (isSwiping) {
            if (!isMobile && Input.GetMouseButton(0)) {
                swipeDelta = (Vector2)Input.mousePosition - tapPosition;
            } else if (Input.touchCount > 0) {
                swipeDelta = Input.GetTouch(0).position - tapPosition;
            }
        }

        if (swipeDelta.magnitude > MinSwipeDistance) {
            OnSwipe?.Invoke(GetSwipeDirection());

            ResetSwipe();
        }
    }

    private void ResetSwipe() {
        isSwiping = false;
        swipeDelta = Vector2.zero;
    }

    private StructsAndEnums.Direction GetSwipeDirection() {
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) {
            return swipeDelta.x > 0 ? StructsAndEnums.Direction.Right : StructsAndEnums.Direction.Left;
        } else {
            return swipeDelta.y > 0 ? StructsAndEnums.Direction.Up : StructsAndEnums.Direction.Down;
        }
    }
}