using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;               // Kecepatan gerak musuh
    public Rigidbody2D rb;                     // Referensi ke komponen Rigidbody2D
    public Animator animator;                  // Referensi ke komponen Animator
    public Transform target;                   // Target musuh, biasanya pemain
    public float detectionRange;          // Jarak deteksi musuh terhadap pemain
    private Vector2 movement;                  // Arah pergerakan saat ini

    private Vector2 randomDirection;           // Arah acak untuk gerakan berkelana
    public float changeDirectionTime = 2f;     // Interval waktu untuk mengubah arah
    private float directionTimer;              // Timer untuk pelacakan perubahan arah

    void Start()
    {
        directionTimer = changeDirectionTime;  // Setel timer ke interval awal
        ChangeRandomDirection();               // Set arah acak awal
    }

    void Update()
    {
        // Menghitung jarak antara musuh dan pemain
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= detectionRange) // Jika pemain berada dalam jarak deteksi
        {
            // Gerak menuju pemain
            Vector2 direction = (target.position - transform.position).normalized; // Hitung arah menuju pemain
            movement = direction; // Set pergerakan berdasarkan arah menuju pemain
            
            // Update parameter animator untuk pergerakan
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude); // Kecepatan berdasarkan arah
        }
        else
        {
            // Berjalan secara acak jika pemain di luar jangkauan deteksi
            directionTimer -= Time.deltaTime;

            if (directionTimer <= 0) // Jika waktunya untuk mengubah arah
            {
                ChangeRandomDirection(); // Ubah ke arah acak baru
                directionTimer = changeDirectionTime; // Atur ulang timer
            }

            movement = randomDirection; // Set gerakan ke arah acak

            // Update parameter animator untuk pergerakan acak
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude); // Set kecepatan berdasarkan arah
        }
    }

    void FixedUpdate()
    {
        // Pindahkan musuh ke arah pergerakan saat ini
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void ChangeRandomDirection()
    {
        // Pilih arah acak untuk pergerakan musuh
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; // Randomisasi arah
    }
}
