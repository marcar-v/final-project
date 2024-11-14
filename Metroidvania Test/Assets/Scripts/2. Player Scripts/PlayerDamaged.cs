using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamaged : PlayerController
{
    public static PlayerDamaged _playerDamagedInstance;

    public int _maxLife = 3;
    int _currentLife;
    [SerializeField] Lives _lives;

    float _invulnerabilityDuration = 0.5f;
    bool _isInvulnerable = false;

    bool _isDead;

    Animator _anim;
    [SerializeField] CapsuleCollider2D _playerCollider;

    private void Awake()
    {
        if (_playerDamagedInstance == null)
        {
            _playerDamagedInstance = this;
        }

        else
        {
            Debug.Log("Más de un Player Damaged en escena");
        }

        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentLife = _maxLife;
    }

    public void PlayerIsDamaged(int damage)
    {
        if (!_isInvulnerable)
        {
            _currentLife -= damage;

            _lives.DeactivateLife(_currentLife);

            if (_currentLife <= 0 && !_isDead)
            {
                _isDead = true;
                _anim.SetTrigger("Death");
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), true);
                Invoke("PlayerDeath", 1.5f);
            }
            else if(!_isDead)
            {
                _anim.SetTrigger("Hurt");
                StartCoroutine(InvulnerabilityCoroutine());
            }
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        _isInvulnerable = true; 
        Debug.Log("Jugador invulnerable.");

        yield return new WaitForSeconds(_invulnerabilityDuration);

        _isInvulnerable = false;
        Debug.Log("Jugador vulnerable de nuevo.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerIsDamaged(1);
        }
    }

    void PlayerDeath()
    {
        if (_currentLife <= 0 && _isDead == true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
        Invoke("RestartGame", 2f);
    }

    public void RestartGame()
    {
        TransitionController._transitionInstance.ReloadCurrentScene();
    }
}
