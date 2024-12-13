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
#if UNITY_EDITOR || UNITY_STANDALONE
        // Detecta si se presionan las teclas en PC
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            StartCrouching();
        }
        else
        {
            StopCrouching();
        }
#endif

#if UNITY_ANDROID || UNITY_IOS
        // Detecta si el joystick apunta hacia abajo en dispositivos m�viles
        if (_joystick != null && _joystick.Vertical < -0.5f)
        {
            StartCrouching();
        }
        else
        {
            StopCrouching();
        }
#endif
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
