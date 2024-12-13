using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootController : MonoBehaviour
{
    public delegate void SpecialAttackDelegate();
    public SpecialAttackDelegate specialAttackReleased;

    [SerializeField] GameObject _playerBullet;
    public GameObject _bulletPosition;

    [SerializeField] Animator _anim;
    AudioSource _shootSound;

    [SerializeField] PlayerCrouch _playerCrouch;
    [SerializeField] Button _shootButton; // Botón de UI para disparar en Android
    [SerializeField] Button _specialShootButton; // Botón de UI para disparo especial (opcional)

    bool _isSpecialShooting = false;
    bool _isCrouching = false;

    private void Awake()
    {
        _shootSound = GetComponent<AudioSource>();

        // Configura los eventos de los botones
        if (_shootButton != null)
            _shootButton.onClick.AddListener(Shoot);

        if (_specialShootButton != null)
            _specialShootButton.onClick.AddListener(SpecialShoot);
    }

    private void Update()
    {
        // Actualiza el estado de agachado
        _isCrouching = _playerCrouch.GetComponent<PlayerCrouch>().IsCrouching();

        // Solo permitir disparar si no está agachado
        if (!_isCrouching)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            // En PC: detectar clic izquierdo del ratón para disparar
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale > 0)
            {
                Shoot();
            }
            // En PC: detectar clic derecho para disparo especial
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SpecialShoot();
            }
#endif

#if UNITY_ANDROID || UNITY_IOS
            // En Android no disparar por cualquier toque en pantalla,
            // Solo se permite disparar mediante botones UI ya configurados
#endif
        }
    }

    public void Shoot()
    {
        if (_isCrouching) return; // No disparar si está agachado
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
