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
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _runSpeed = 1;
            FlipCharacter();
            _animator.SetBool("Run", true);
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _runSpeed = -1;
            FlipCharacter();
            _animator.SetBool("Run", true);
        }

        else
        {
            _runSpeed = 0;
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
