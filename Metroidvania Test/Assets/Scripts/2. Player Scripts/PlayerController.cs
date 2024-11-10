using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected float _speed = 1.5f;
    protected float _runSpeed;
    protected Rigidbody2D _rb;
    protected SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
}
