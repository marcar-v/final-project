using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : PlayerController
{
    float _currentSpeed;

    public bool IsCrouching()
    {
        return _isCrouching;
    }

    void Update()
    {
        Crouching();
    }

    void Crouching()
    {
        // Detecta si se presionan las teclas o si el joystick apunta hacia abajo
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || (_joystick != null && _joystick.Vertical < -0.5f))
        {
            StartCrouching();
        }
        else
        {
            StopCrouching();
        }
    }

    public virtual void StartCrouching()
    {
        _isCrouching = true;
        _animator.SetBool("isCrouching", true);
    }

    public virtual void StopCrouching()
    {
        _isCrouching = false;
        _animator.SetBool("isCrouching", false);
    }
}
