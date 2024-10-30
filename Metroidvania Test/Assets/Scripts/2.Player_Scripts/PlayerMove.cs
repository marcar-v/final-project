using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerController
{
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _runSpeed = 1;
            _spriteRenderer.flipX = false;
            _animator.SetBool("Run", true);
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _runSpeed = -1;
            _spriteRenderer.flipX = true;
            _animator.SetBool("Run", true);
        }

        else
        {
            _runSpeed = 0;
            _animator.SetBool("Run", false);
        }
        transform.position = new Vector2(transform.position.x + _runSpeed * _speed * Time.deltaTime, transform.position.y);
    }
}
