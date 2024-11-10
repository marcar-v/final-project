using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    [SerializeField] GameObject[] _lives;

    private void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            DeactivateLife(1);
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
