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

    [SerializeField] AudioSource _hitSound;
    [SerializeField] GameOverPanel _gameOverPanelScript;
    [SerializeField] GameObject _gameOverPanel;

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

            _hitSound.Play();

            _lives.DeactivateLife();

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
    public void ResetCollisionAfterDeath()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
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
            Debug.Log("Enemigo detectado");
        }
    }

    void PlayerDeath()
    {
        if (_currentLife <= 0 && _isDead == true)
        {
            // Resetea las vidas del jugador
            Lives._livesInstance.ResetLives();

            // Detener el movimiento del jugador
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // Desactivar al jugador
            gameObject.SetActive(false);
        }

        PlayerPrefs.SetInt("TotalCoins", CoinManager.coinManagerInstance.TotalCoins);
        PlayerPrefs.Save();

        // Espera 1 segundo antes de mostrar el panel de Game Over
        Invoke("GameOverPanel", 0.5f);
    }

    public void GameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }
}
