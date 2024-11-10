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
            Shoot();
        }
    }

    public void Shoot()
    {
        _playerBullet.GetComponent<BulletPool>().ShootBullet();
        _anim.SetTrigger("Shoot");
        StartCoroutine(ResetShootAnim());
    }

    private IEnumerator ResetShootAnim()
    {
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        _anim.ResetTrigger("Shoot");
        _anim.CrossFade("Idle", 0.1f);

    }
}
