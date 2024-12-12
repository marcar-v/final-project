using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPP : MonoBehaviour
{
    [SerializeField] private bool resetCoinsOnStart = true;

    private void Awake()
    {
        if (resetCoinsOnStart)
        {
            ResetCoins();
        }
    }

    private void ResetCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", 0); // Reinicia las monedas a 0
        PlayerPrefs.Save(); // Asegura que se guarden los cambios
        Debug.Log("Las monedas han sido reiniciadas a 0.");
    }
}
