using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJump : PlayerController
{
    float _jumpForce = 3f;
    [SerializeField] LayerMask _groundLayer;
    int _totalJumps = 2;
    int _remainingJumps;
    [Header("SFX")]
    [SerializeField] AudioSource _jumpSound;

    PlayerCrouch _playerCrouchScript;

    [SerializeField] Button jumpButton; // Referencia al botón de la UI

    [SerializeField] CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        _playerCrouchScript = GetComponent<PlayerCrouch>();

        // Asignar el evento del botón para llamar a JumpButtonPressed
        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(JumpButtonPressed);
        }
    }

    private void Start()
    {
        _remainingJumps = _totalJumps;
    }

    private void Update()
    {
        bool isCrouching = _playerCrouchScript.IsCrouching();
        if (!isCrouching)
        {
            // Solo ejecutar el salto si no está agachado
            Jump();
        }
    }

    bool isGrounded()
    {
        // Verifica si el jugador está tocando el suelo con un raycast
        RaycastHit2D _raycastHit = Physics2D.BoxCast(capsuleCollider2D.bounds.center,
            new Vector2(capsuleCollider2D.bounds.size.x, capsuleCollider2D.bounds.size.y), 0f, Vector2.down, 0.2f, _groundLayer);
        return _raycastHit.collider != null;
    }

    void Jump()
    {
        // Restablece los saltos cuando el jugador toca el suelo
        if (isGrounded())
        {
            _remainingJumps = _totalJumps;
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        // Detecta si se presiona la barra espaciadora en PC
        if (Input.GetKeyDown(KeyCode.Space) && _remainingJumps > 0)
        {
            PerformJump();
        }
#endif
        UpdateJumpAnimation();
    }

    void PerformJump()
    {
        _jumpSound.Play();
        _remainingJumps--;  // Reduce el número de saltos disponibles
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);  // Resetea la velocidad vertical
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);  // Aplica la fuerza de salto
    }

    void UpdateJumpAnimation()
    {
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

    // Esta función es llamada cuando el botón de salto en la UI es presionado (Android)
    public void JumpButtonPressed()
    {
        // Primero, comprobamos si el jugador está en el suelo y restablecemos los saltos disponibles
        if (isGrounded())
        {
            _remainingJumps = _totalJumps; // Reinicia los saltos cuando el jugador está en el suelo
        }

        // Ahora, verificamos si hay saltos disponibles y realizamos el salto
        if (_remainingJumps > 0)
        {
            PerformJump();  // Si el jugador tiene saltos disponibles, realiza el salto
        }
    }
}

