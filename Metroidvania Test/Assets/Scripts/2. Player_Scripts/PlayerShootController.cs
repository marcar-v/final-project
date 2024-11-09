using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    [SerializeField] GameObject _playerBullet;
    [SerializeField] GameObject _bulletPosition1;

    [SerializeField] Animator _anim;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale > 0)
        {
            _playerBullet.GetComponent<BulletPool>().ShootBullet();
            _anim.SetTrigger("Shoot");
        }
    }

    public void ResetShootTrigger()
    {
        _anim.ResetTrigger("Shoot");
    }
}
