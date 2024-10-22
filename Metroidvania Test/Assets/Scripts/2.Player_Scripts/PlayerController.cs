using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float _speed = 1.5f;
    private Vector2 movement;

    void Start()
    {
        // Obtén el componente Rigidbody2D
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input del jugador (solo movimiento horizontal)
        movement.x = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // Movimiento del jugador solo en el eje X (horizontal)
        _rb.MovePosition(_rb.position + new Vector2(movement.x, 0) * _speed * Time.fixedDeltaTime);
    }

}
