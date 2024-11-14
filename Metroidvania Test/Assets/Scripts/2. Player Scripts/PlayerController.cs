using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected float _speed = 1.5f;
    protected float _runSpeed;
    [SerializeField] protected Rigidbody2D _rb;
    protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Animator _animator;

    protected bool _isCrouching = false;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision with: {collision.collider.name}");
    }

}
