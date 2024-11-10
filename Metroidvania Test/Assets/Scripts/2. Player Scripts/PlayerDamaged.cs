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

        _animator = GetComponent<Animator>();
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
            _animator.Play("Player_Hurt");
            _lives.DeactivateLife(_currentLife);

            if (_currentLife <= 0)
            {
                _animator.SetBool("Death", true);
                PlayerDeath();
            }
            else
            {
                _animator.SetBool("Hurt", false);
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

    void PlayerDeath()
    {
        Debug.Log("Player Muerto");
        Invoke("RestartGame", 1.5f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
