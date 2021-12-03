using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class InputManager : MonoBehaviourSingleton<InputManager> {

    #region Events

    public delegate void StartTouch(Vector2 position, float time);

    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);

    public event StartTouch OnEndTouch;

    #endregion Events

    private PlayerControls _playerControls;
    private Camera _mainCamera;

    private void Awake() {
        _playerControls = new PlayerControls();
        _mainCamera = Camera.main;
    }

    private void OnEnable() {
        _playerControls.Enable();
    }

    private void OnDisable() {
        _playerControls.Disable();
    }

    private void Start() {
        _playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx) {
        if (UIHandler.GameIsPaused)
            return;
        OnStartTouch?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx) {
        if (UIHandler.GameIsPaused)
            return;
        OnEndTouch?.Invoke(Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
    }
}