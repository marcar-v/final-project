using System.Collections;
using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    public delegate void SpecialAttackDelegate();
    public SpecialAttackDelegate specialAttackReleased;

    [SerializeField] GameObject _playerBullet;
    public GameObject _bulletPosition;

    [SerializeField] Animator _anim;
    AudioSource _shootSound;


    [SerializeField] PlayerCrouch _playerCrouch;
    bool _isSpecialShooting = false;
    bool _isCrouching = false;

    private void Awake()
    {
        _shootSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _isCrouching = _playerCrouch.GetComponent<PlayerCrouch>().IsCrouching();

        if (!_isCrouching)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale > 0 && !_isSpecialShooting)
            {
                Shoot();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SpecialShoot();
        }
    }

    public void Shoot()
    {
        _playerBullet.GetComponent<BulletPool>().ShootBullet();
        _shootSound.Play();
        _anim.SetTrigger("Shoot");
        StartCoroutine(ResetShootAnim());
    }

    private IEnumerator ResetShootAnim()
    {
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        _anim.ResetTrigger("Shoot");
        _anim.CrossFade("Idle", 0.1f);
    }

    void SpecialShoot()
    {
        if (specialAttackReleased != null)
        {
            Debug.Log("Bomb Released");
            _isSpecialShooting = true;
            specialAttackReleased?.Invoke();
            StartCoroutine(ResetSpecialShootAnim());
        }
    }
    public void EndSpecialShoot()
    {
        _isSpecialShooting = false;
        _isCrouching = false;
    }

    private IEnumerator ResetSpecialShootAnim()
    {
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length);
        _anim.ResetTrigger("SpecialShoot");
        _anim.CrossFade("Crouch", 0.1f);
    }


}
