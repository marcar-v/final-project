using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimShootingController : MonoBehaviour
{
    Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ResetShootTrigger()
    {
        _animator.ResetTrigger("Shoot");
    }
}
