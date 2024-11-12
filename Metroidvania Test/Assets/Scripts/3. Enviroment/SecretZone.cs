using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretZone : MonoBehaviour
{
    Tilemap _tilemap;
    private Color _originalColor;
    public float hiddenAlpha = 0.2f; // Transparencia cuando la zona está oculta
    public float visibleAlpha = 1.0f; // Transparencia cuando la zona es visible

    private void Start()
    {
        // Obtiene el componente Tilemap
        _tilemap = GetComponent<Tilemap>();
        _originalColor = _tilemap.color;

        // Configura la zona como semi-transparente al inicio
        SetTransparency(visibleAlpha);
    }

    // Se ejecuta cuando el jugador entra en la zona secreta
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tiene el tag "Player"
        {
            SetTransparency(hiddenAlpha);
        }
    }

    // Se ejecuta cuando el jugador sale de la zona secreta
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetTransparency(visibleAlpha);
        }
    }

    // Método para cambiar la transparencia del Tilemap
    private void SetTransparency(float alpha)
    {
        Color color = _tilemap.color;
        color.a = alpha;
        _tilemap.color = color;
    }
}
