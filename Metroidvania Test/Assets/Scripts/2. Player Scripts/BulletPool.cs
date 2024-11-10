using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool bulletPoolInstance;

    [SerializeField] int bulletPoolSize = 10;
    [SerializeField] GameObject bullet;
    [SerializeField] List<GameObject> bulletsPool = new List<GameObject>();
    [SerializeField] int shootNumber = 0;

    [SerializeField] GameObject bulletPosition1;


    private void Awake()
    {
        if (bulletPoolInstance == null)
        {
            bulletPoolInstance = this;
        }
    }

    private void Start()
    {
        bulletsPool = new List<GameObject>(10);
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullets = Instantiate(bullet, new Vector2(-10f, 0), Quaternion.identity);
            bulletsPool.Add(bullets);
            bullets.transform.parent = transform;
        }
    }
    public void ShootBullet()
    {
        bulletsPool[shootNumber].transform.position = bulletPosition1.transform.position;
        bulletsPool[shootNumber].SetActive(true);
        shootNumber++;
        if (shootNumber == bulletPoolSize)
        {
            shootNumber = 0;
        }
    }
}
