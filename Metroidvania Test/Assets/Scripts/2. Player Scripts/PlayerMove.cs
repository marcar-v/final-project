using UnityEngine;

public class PlayerMove : PlayerController
{
    bool _isFacingRight = true;

    PlayerCrouch _playerCrouchScript;

    private void Awake()
    {
        _playerCrouchScript = GetComponent<PlayerCrouch>();
    }

    private void Update()
    {
        _isCrouching = _playerCrouchScript.IsCrouching();

        if (!_isCrouching)
        {
            PlayerMovement();
        }
        else
        {
            _runSpeed = 0;
            _animator.SetBool("Run", false);
        }
    }

    private void PlayerMovement()
    {
        float horizontalInput = 0f;

        // Detecta la entrada del teclado o el joystick
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        else if (_joystick != null)
        {
            horizontalInput = _joystick.Horizontal; // Usar el valor del joystick
        }

        _runSpeed = Mathf.Clamp(horizontalInput, -1f, 1f);

        if (_runSpeed != 0)
        {
            FlipCharacter();
            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }

        transform.position = new Vector2(transform.position.x + _runSpeed * _speed * Time.deltaTime, transform.position.y);
    }

    private void FlipCharacter()
    {
        if (_runSpeed < 0 && _isFacingRight)
        {
            Flip();
        }
        else if (_runSpeed > 0 && !_isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
