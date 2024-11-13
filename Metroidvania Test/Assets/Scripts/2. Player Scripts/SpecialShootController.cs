using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialShootController : MonoBehaviour
{
    PlayerShootController _playerShoot;
    [SerializeField] Animator _anim;
    [SerializeField] GameObject _specialAttackBullet;

    [SerializeField] PlayerCrouch _playerCrouch;

    bool _isCrouching = false;

    [SerializeField] GameObject _shootingPoint;
    [SerializeField] SpriteRenderer _bulletSpriteRenderer;

    void Start()
    {
        _isCrouching = _playerCrouch.IsCrouching();
        _playerShoot = GetComponent<PlayerShootController>();
        if (_playerShoot != null)
        {
            _playerShoot.specialAttackReleased = PerformSpecialAttack;
        }
    }

    void PerformSpecialAttack()
    {
        _anim.SetTrigger("SpecialShoot");
        SpecialAttackBullet();
        StartCoroutine(EndSpecialAttack());
    }

    void SpecialAttackBullet()
    {
        GameObject specialBullet = (GameObject)Instantiate(_specialAttackBullet);
        specialBullet.transform.position = _playerShoot._bulletPosition.transform.position;

        Rigidbody2D rb = specialBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Verificar la dirección del personaje
            float bulletSpeed = 10f; // Puedes ajustar la velocidad de la bala aquí
            if (_shootingPoint.transform.rotation.y < 0) // Si el personaje está mirando a la izquierda
            {
                rb.velocity = Vector2.left * bulletSpeed;
                _bulletSpriteRenderer.flipX = true;
            }
            else // Si el personaje está mirando a la derecha
            {
                rb.velocity = Vector2.right * bulletSpeed;
                _bulletSpriteRenderer.flipX = false;
            }
        }

        Destroy(specialBullet, 2f);
    }

    private IEnumerator ResetShootAnim()
    {
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        _anim.ResetTrigger("SpecialShoot");
        _anim.CrossFade("Crouch", 0.1f);
    }

    IEnumerator EndSpecialAttack()
    {
        yield return new WaitForSeconds(0.5f);
        _playerShoot.EndSpecialShoot();
    }
}
