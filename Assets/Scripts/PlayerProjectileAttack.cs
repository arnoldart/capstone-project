using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileAttack : MonoBehaviour
{
    public Animator animator; // Animator untuk animasi tembakan
    public GameObject projectilePrefab; // Prefab untuk projectile
    public Transform attackPoint; // Titik dari mana projectile ditembakkan
    public float projectileSpeed = 10f; // Kecepatan tembakan

    private PlayerHealth playerHealth; // Skrip kesehatan pemain

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Pastikan pemain tidak mati sebelum melakukan tembakan
        if (playerHealth != null && playerHealth.IsDead())
            return;

        // Periksa input untuk tembakan (klik kanan)
        if (Input.GetMouseButtonDown(1)) // Klik kanan untuk tembakan
        {
            PerformShoot();
        }
    }

    // Melakukan tembakan
    void PerformShoot()
    {
        // Trigger animasi tembakan menggunakan trigger "Shoot"
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        // Instansiasi projectile
        GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);

        // Menghitung arah tembakan berdasarkan posisi mouse
        Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - attackPoint.position).normalized;

        // Memberikan kecepatan dan arah pada projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * projectileSpeed; // Tentukan kecepatan dan arah projectile
    }
}
