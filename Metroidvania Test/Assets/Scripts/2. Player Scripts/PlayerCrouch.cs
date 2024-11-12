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
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
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
