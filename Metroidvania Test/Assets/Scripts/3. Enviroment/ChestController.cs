using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    CircleCollider2D _triggerCollider;
    [SerializeField] Animator _chestAnim;


    [SerializeField] Animator _heartAnimator;
    [SerializeField] SpriteRenderer _heartSprite;
    [SerializeField] Transform _heartTransform;
    bool _heartReleased;

    private void Awake()
    {
        _triggerCollider = this.GetComponent<CircleCollider2D>();
        _chestAnim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player") || (collision.tag == "PlayerBullets"))
        {
            Debug.Log("Player in");

            if (!_heartReleased)
            {
                _chestAnim.SetBool("Open", true);
                _heartReleased = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Player") || (collision.tag == "PlayerBullets"))
        {
            _chestAnim.SetBool("Open", false);
        }
    }

    IEnumerator HeartStandsController()
    {
        yield return new WaitForSeconds(1f);
        _heartAnimator.SetBool("Exit", false);
    }

    void ShowHeart()
    {
        _heartSprite.enabled = true;
        _heartAnimator.SetBool("Exit", true);

        StartCoroutine(HeartStandsController());
    }
}
