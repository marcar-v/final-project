using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] GameObject[] _lives;
    public void DeactivateLife(int index)
    {
        _lives[index].SetActive(false);
    }

    public void ActivateLife(int index)
    {
        _lives[index].SetActive(true);
    }
}
