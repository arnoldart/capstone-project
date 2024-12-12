using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileAttack : MonoBehaviour
{
    public Animator animator; // Animator untuk animasi tembakan
    public ObjectPool projectilePool; // Referensi ke Object Pool
    public Transform attackPoint; // Titik dari mana projectile ditembakkan
    public float projectileSpeed = 10f; // Kecepatan projectile

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth != null && playerHealth.IsDead())
            return;

        // Klik kanan untuk menembak
        if (Input.GetMouseButtonDown(1))
        {
            PerformShoot();
        }
    }

    void PerformShoot()
    {
        // Trigger animasi tembakan
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        // Ambil projectile dari pool
        GameObject projectile = projectilePool.GetObject();

        // Atur posisi dan rotasi projectile
        projectile.transform.position = attackPoint.position;
        projectile.transform.rotation = attackPoint.rotation;

        // Tentukan arah tembakan
        Vector2 shootDirection = attackPoint.right.normalized;

        // Luncurkan projectile
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.Launch(shootDirection, projectileSpeed);
        }
    }
}
