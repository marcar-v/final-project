using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] bool _scrollLeft;

    float _singleTextureWidth;

    void Start()
    {
        SetupTexture();
        if(_scrollLeft)
        {
            _moveSpeed = -_moveSpeed;
        }
    }

    void SetupTexture()
    {
        Sprite _sprite = GetComponent<SpriteRenderer>().sprite;
        _singleTextureWidth = _sprite.texture.width / _sprite.pixelsPerUnit;
    }

    void Scroll()
    {
        float _delta = _moveSpeed * Time.deltaTime;
        transform.position += new Vector3(_delta, 0f, 0f);
    }

    void CheckReset()
    {
        if((Mathf.Abs(transform.position.x) - _singleTextureWidth) > 0)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
    }

    void Update()
    {
        Scroll();
        CheckReset();
    }
}
