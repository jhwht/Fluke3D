using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private MouseLook _mouseLook;

    private PlayerControls _controls;
    private PlayerControls.GroundMovementActions _groundMovement;

    private Vector2 _horizontalInput;
    private Vector2 _mouseInput;

    private void Awake()
    {
        Cursor.visible = false;
        _controls = new PlayerControls();
        _groundMovement = _controls.GroundMovement;

        // _groundMovement.[action].performed += context => do something
        _groundMovement.HorizontalMovement.performed += ctx => _horizontalInput = ctx.ReadValue<Vector2>();
        _groundMovement.Jump.performed += _ => _movement.OnJumpPressed();
        _groundMovement.MouseX.performed += ctx => _mouseInput.x = ctx.ReadValue<float>();
        _groundMovement.MouseY.performed += ctx => _mouseInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        _movement.ReceiveInput(_horizontalInput);
        _mouseLook.ReceiveInput(_mouseInput);
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}