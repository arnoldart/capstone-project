using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;  // Waktu hidup projectile sebelum dihancurkan
    private Rigidbody2D rb;      // Rigidbody2D untuk gerakan projectile
    private Animator animator;   // Animator untuk kontrol animasi

    void Start()
    {
        // Ambil komponen Rigidbody2D dan Animator
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Hancurkan projectile setelah waktu tertentu
        Destroy(gameObject, lifetime);

        // Aktifkan animasi bergerak
        SetAnimationMovement();
    }

    // Fungsi untuk mengatur animasi berdasarkan kecepatan
    void SetAnimationMovement()
    {
        if (animator != null)
        {
            // Set parameter "isMoving" berdasarkan kecepatan
            animator.SetBool("isMoving", rb.velocity != Vector2.zero);
        }
    }

    // Fungsi ini bisa digunakan untuk menangani tabrakan dengan objek lain
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Misalnya, hancurkan projectile saat menabrak musuh atau objek lain
        Destroy(gameObject);
    }

    // Optional: Pengaturan untuk mematikan animasi jika diperlukan
    void Update()
    {
        // Jika animator ada dan projectile bergerak, set "isMoving" menjadi true
        if (animator != null)
        {
            SetAnimationMovement();
        }
    }
}
