using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    [SerializeField] GameObject _bossGameObject;
    [SerializeField] GameObject _secretWall;
    [SerializeField] BoxCollider2D _wallCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _bossGameObject.SetActive(true);
            _secretWall.SetActive(true);
            _wallCollider.enabled = true;
        }
    }
}
