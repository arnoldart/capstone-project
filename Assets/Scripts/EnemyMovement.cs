using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;           // Kecepatan gerak musuh
    [SerializeField] private float stopThreshold = 0.1f; // Toleransi untuk berhenti di posisi tujuan
    private Rigidbody2D rb;                             // Referensi ke Rigidbody2D
    private Animator animator;                          // Referensi ke Animator

    public Transform target;                           // Target (pemain)
    private Vector2 movement;                           // Arah gerakan musuh
    public bool shouldChasePlayer = true;              // Apakah musuh harus mengejar pemain?

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(Transform playerTarget)
    {
        target = playerTarget; // Set target dari luar
    }

    public void SetChasePlayer(bool chase)
    {
        shouldChasePlayer = chase; // Atur apakah musuh harus mengejar pemain
    }

    public void MoveTowards(Vector2 destination)
    {
        if (rb == null || target == null) return;

        if (!shouldChasePlayer)
        {
            // Jika tidak mengejar, tetap update animasi berdasarkan arah ke pemain
            Vector2 directionToPlayer = (target.position - transform.position).normalized;
            UpdateAnimator(directionToPlayer);
            return;
        }

        // Hitung jarak ke tujuan
        float distanceToDestination = Vector2.Distance(transform.position, destination);

        if (distanceToDestination <= stopThreshold)
        {
            // Jika sudah dekat dengan tujuan, berhenti dan set arah ke (0, 0)
            movement = Vector2.zero;
            UpdateAnimator(new Vector2(0, -1));
            return;
        }

        // Hitung arah
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        movement = direction;

        // Pindahkan musuh
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Update animasi
        UpdateAnimator(direction);
    }

    public void EnemyStop() 
    {
        moveSpeed = 0f;
    }

    private void UpdateAnimator(Vector2 direction)
    {
        if (animator == null) return;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }
}