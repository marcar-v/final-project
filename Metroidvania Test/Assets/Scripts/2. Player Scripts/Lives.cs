using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] GameObject[] _lives; // Lista de las 5 posibles vidas
    private int _currentLives;

    public static Lives _livesInstance;

    private void Awake()
    {
        if (_livesInstance == null)
        {
            _livesInstance = this;
            DontDestroyOnLoad(gameObject); // Mantener entre escenas si es necesario
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        // Cargar el número de vidas desde PlayerPrefs, predeterminado a 3 si no existe
        _currentLives = PlayerPrefs.GetInt("CurrentLives", 3); // Comienza con 3 vidas activas
        UpdateLivesUI();
    }

    private void OnDestroy()
    {
        if (_livesInstance == this)
        {
            // Guardar el número de vidas restantes
            PlayerPrefs.SetInt("CurrentLives", _currentLives);
            PlayerPrefs.Save();
        }
    }

    public void DeactivateLife()
    {
        if (_currentLives > 0)
        {
            _currentLives--;
            UpdateLivesUI();
            PlayerPrefs.SetInt("CurrentLives", _currentLives);
        }
    }

    public void GainLife()
    {
        if (_currentLives < _lives.Length)
        {
            _currentLives++;
            UpdateLivesUI();
            PlayerPrefs.SetInt("CurrentLives", _currentLives);
        }
    }

    private void UpdateLivesUI()
    {
        // Activar o desactivar vidas en orden
        for (int i = 0; i < _lives.Length; i++)
        {
            _lives[i].SetActive(i < _currentLives); // Las primeras `_currentLives` están activas
        }
    }
}
