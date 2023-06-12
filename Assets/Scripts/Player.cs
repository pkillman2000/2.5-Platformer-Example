using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInputActions _inputActions;

    [SerializeField]
    private float _walkSpeed;
    private Vector2 _movementInput;
    private Vector3 _movementDirection;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null )
        {
            Debug.LogWarning("Character Controller is Null!");
        }

        _inputActions = new PlayerInputActions();
        if (_inputActions == null )
        {
            Debug.LogWarning("Character Controller is Null!");
        }
        else
        {
            _inputActions.Player.Enable();
        }
    }

    
    private void Update()
    {
        // get horizontal input
        _movementInput = _inputActions.Player.Movement.ReadValue<Vector2>();
        // define direction based on that input
        _movementDirection = new Vector3(_movementInput.x, _movementInput.y, 0);
        Debug.Log(_movementDirection);

        // Move based on that direction
        _characterController.Move(_movementDirection * _walkSpeed * Time.deltaTime);
    }
}
