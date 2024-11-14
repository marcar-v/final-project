using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] GameObject[] _lives;

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
    }
    public void DeactivateLife(int index)
    {
        _lives[index].SetActive(false);
    }

    public void ActivateLife(int index)
    {
        _lives[index].SetActive(true);
    }
}
