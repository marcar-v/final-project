using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerController
{
    float _jumpForce = 3f;
    [SerializeField] LayerMask _groundLayer;
    int _totalJumps = 1;
    int _remainingJumps;
    [Header("SFX")]
    [SerializeField] AudioSource _jumpSound;

    PlayerCrouch _playerCrouchScript;

    private void Awake()
    {
        _playerCrouchScript = GetComponent<PlayerCrouch>();
    }

    private void Start()
    {
        _remainingJumps = _totalJumps;
        _playerCollider = GetComponentInChildren<CapsuleCollider2D>();
    }

    private void Update()
    {
        _isCrouching = _playerCrouchScript.IsCrouching();
        if (!_isCrouching)
        {
            Jump();
        }
    }

    bool isGrounded()
    {
        RaycastHit2D _raycastHit = Physics2D.BoxCast(_playerCollider.bounds.center,
            new Vector2(_playerCollider.bounds.size.x, _playerCollider.bounds.size.y), 0f, Vector2.down, 0.2f, _groundLayer);
        return _raycastHit.collider != null;
    }

    void Jump()
    {
        if (isGrounded())
        {
            _remainingJumps = _totalJumps;
        }

        // Detecta si se presiona el botón de salto o si el joystick se mueve hacia arriba
        if ((Input.GetKeyDown(KeyCode.Space) || (_joystick != null && _joystick.Vertical > 0.5f)) && _remainingJumps > 0)
        {
            _jumpSound.Play();
            _remainingJumps--;
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        if (!isGrounded())
        {
            _animator.SetBool("Jump", true);
            _animator.SetBool("Run", false);
        }

        if (isGrounded())
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("Fall", false);
        }

        if (_rb.velocity.y < 0f)
        {
            _animator.SetBool("Fall", true);
        }
        else if (_rb.velocity.y > 0f)
        {
            _animator.SetBool("Fall", false);
        }
    }
}
