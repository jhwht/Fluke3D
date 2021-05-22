using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _sensitivityX = 8f;
    [SerializeField] private float _sensitivityY = 0.5f;
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private float _xClamp = 85f;

    private float _mouseX, _mouseY;
    private float _xRotation = 0f;

    private void Update()
    {
        transform.Rotate(Vector3.up, _mouseX * Time.deltaTime);

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        var targetRotation = transform.eulerAngles;
        targetRotation.x = _xRotation;
        _playerCamera.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        _mouseX = mouseInput.x * _sensitivityX;
        _mouseY = mouseInput.y * _sensitivityY;
    }
}