using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;
    private bool _canWallJump = false;
    Vector3 _wallSurfaceNormal;
    [SerializeField]
    private float _pushVelocity;

    private Vector3 _direction;
    private Vector3 _velocity;


    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL."); 
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (_controller.isGrounded == true)
        {
            _canWallJump = true;
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false) // Jump
            {
                if (_canDoubleJump == true) // Double Jump
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == true) // Wall Jump
            {
                _yVelocity += _jumpHeight;
                _velocity = _wallSurfaceNormal * _speed;
                _canWallJump = false;
            }

            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        if (_controller.enabled == true)
        {
            _controller.Move(_velocity * Time.deltaTime);
        }

    }
    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }

    public int GetNumberOfCoins()
    {
        return _coins;
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!_controller.isGrounded && hit.gameObject.tag == "Wall") // Wall Jumping
        {
            _canWallJump = true;
            _wallSurfaceNormal = hit.normal;
        }

        if(hit.gameObject.tag == "Movable") // Pushing Movables
        {
            Rigidbody rigidbody = hit.collider.attachedRigidbody;
            if(rigidbody == null || rigidbody.isKinematic) 
            {
                Debug.LogWarning("Moveable Rigidbody Problems!");
                return;
            }

            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
            rigidbody.velocity = pushDir * _pushVelocity;
        }
    }
}
