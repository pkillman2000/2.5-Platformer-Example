using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInputActions _inputActions;

    [Header("Player")]
    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _jumpDuration;
    private Vector2 _movementInput;
    private Vector3 _movementDirection;
    private Vector3 _movementVelocity;
    private bool _isJumping = false;

    [Header("Environment")]
    [SerializeField]
    private float _gravity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null )
        {
            Debug.LogWarning("Character Controller is Null!");
        }

        _inputActions = new PlayerInputActions(); // New input system
        if (_inputActions == null )
        {
            Debug.LogWarning("Character Controller is Null!");
        }
        else
        {
            _inputActions.Player.Enable();
        }

        _inputActions.Player.Jump.performed += Jump_performed;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _movementInput = _inputActions.Player.Movement.ReadValue<Vector2>();
        _movementDirection = new Vector3(_movementInput.x, 0, 0); // Direction

        if (_isJumping) // Do not use gravity
        {
                _movementDirection.y += _jumpHeight;
        }
        else if (!_isJumping && !_characterController.isGrounded) // Use gravity
        {
            _movementDirection.y -= _gravity;
        }
        _movementVelocity = _movementDirection * _walkSpeed * Time.deltaTime; // Add walk speed
        _characterController.Move(_movementVelocity); // Move player
    }
   
    private void Jump_performed(InputAction.CallbackContext obj)
    {
        StartCoroutine(JumpingCoroutine());
    }
    
    IEnumerator JumpingCoroutine()
    {
        if (_characterController.isGrounded)
        {
            _isJumping = true;
        }

        yield return new WaitForSeconds(_jumpDuration);
        _isJumping = false;
    }

    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= Jump_performed;
    }
}
