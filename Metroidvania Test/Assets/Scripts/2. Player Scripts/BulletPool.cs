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

    [SerializeField] List<Transform> bulletPositions = new List<Transform>();


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
        foreach (Transform bulletPosition in bulletPositions) // Iterar por cada punto de disparo
        {
            // Si todas las balas están activas, reinicia el contador
            if (shootNumber >= bulletPoolSize)
            {
                shootNumber = 0;
            }

            // Toma una bala del pool
            GameObject currentBullet = bulletsPool[shootNumber];

            // Configura su posición y activa la bala
            currentBullet.transform.position = bulletPosition.position;
            currentBullet.transform.rotation = bulletPosition.rotation; // Opcional: ajusta la rotación si es necesario
            currentBullet.SetActive(true);

            // Incrementa el índice del pool
            shootNumber++;

        }
    }
}
