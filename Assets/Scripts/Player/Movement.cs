using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed = 11f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpStrength = 3.5f;
    [SerializeField] private LayerMask _groundMask;

    private Vector2 _horizontalInput;
    private Vector3 _verticalVelocity = Vector3.zero;
    private bool _isGrounded;
    private bool _isJumping;

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(transform.position, 0.1f, _groundMask);
        if (_isGrounded)
        {
            _verticalVelocity.y = 0;
        }

        //Horizontal Movement
        var horizontalVelocity =
            (transform.right * _horizontalInput.x + transform.forward * _horizontalInput.y) * _speed;
        _controller.Move(horizontalVelocity * Time.deltaTime);

        //Jump
        if (_isJumping && _isGrounded)
        {
            _verticalVelocity.y = Mathf.Sqrt(-2f * _jumpStrength * _gravity);
            _isJumping = false;
        }

        //Gravity
        _verticalVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(_verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 horizontalInput)
    {
        _horizontalInput = horizontalInput;
    }

    public void OnJumpPressed()
    {
        _isJumping = true;
    }
}