using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpened : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] BoxCollider2D _boxCollider;

    private void OnEnable()
    {
        BossController.OnBossDeath += OpenDoor; // Suscribirse al evento
    }

    private void OnDisable()
    {
        BossController.OnBossDeath -= OpenDoor; // Desuscribirse del evento
    }

    private void OpenDoor()
    {
        // Activar el componente Animator
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        if (_animator != null)
        {
            _boxCollider.enabled = false;
            _animator.enabled = true; // Activar el Animator
            _animator.Play(0, 0, 0f); // Reproducir la animación en el estado predeterminado
        }
    }
}
