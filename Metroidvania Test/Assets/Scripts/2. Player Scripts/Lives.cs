using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Lives : MonoBehaviour
{
    [SerializeField] GameObject[] _lives; // Array de los objetos que representan las vidas en el Canvas.
    private int _currentLifeIndex = 3;    // Índice de la vida actual.

    public static Lives _livesInstance;

    private void Awake()
    {
        if (_livesInstance == null)
        {
            _livesInstance = this;
        }
        else
        {
            Destroy(this);
        }

        // Asegurarse de que las vidas estén correctamente activadas al inicio.
        UpdateLivesUI();
    }

    public void DeactivateLife(int index)
    {
        if (index >= 0 && index < _lives.Length)
        {
            _lives[index].SetActive(false); // Desactivar la vida en el índice especificado.
        }
    }

    public void ActivateLife(int index)
    {
        if (index >= 0 && index < _lives.Length)
        {
            _lives[index].SetActive(true); // Activar la vida en el índice especificado.
        }
    }

    // Método para añadir vidas y actualizar el UI
    public void LivesAdd(int amount)
    {
        // Sumamos las vidas pero no excedemos el número máximo de vidas.
        _currentLifeIndex += amount;
        _currentLifeIndex = Mathf.Clamp(_currentLifeIndex, 0, _lives.Length);  // Asegura que no se exceda el límite.

        UpdateLivesUI(); // Actualizar la interfaz gráfica de las vidas.
    }

    public void LivesRemove(int amount)
    {
        // Restamos las vidas pero no caemos por debajo de cero.
        _currentLifeIndex -= amount;
        _currentLifeIndex = Mathf.Clamp(_currentLifeIndex, 0, _lives.Length); // Asegura que no sea menor que 0.

        UpdateLivesUI(); // Actualizar la interfaz gráfica de las vidas.
    }

    // Actualiza la visibilidad de las vidas en el UI
    private void UpdateLivesUI()
    {
        for (int i = 0; i < _lives.Length; i++)
        {
            if (i < _currentLifeIndex)
            {
                ActivateLife(i);  // Activamos las vidas hasta el índice de la vida actual.
            }
            else
            {
                DeactivateLife(i);  // Desactivamos las vidas que exceden el índice actual.
            }
        }
    }

}
