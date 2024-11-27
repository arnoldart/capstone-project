using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float attackRange;             // Jarak serangan musuh
    public int attackDamage;              // Kerusakan yang diberikan ke pemain
    public float attackRate;              // Frekuensi serangan musuh
    private float nextAttackTime;         // Waktu untuk serangan berikutnya

    public int maxAttackCount;            // Jumlah maksimum serangan sebelum cooldown
    private int currentAttackCount;       // Jumlah serangan saat ini
    public float attackCooldown;          // Waktu cooldown antara serangan
    private bool isCooldown = false;      // Apakah musuh dalam keadaan cooldown?

    private Transform player;             // Referensi ke pemain
    private Animator animator;            // Referensi ke animator musuh

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // Cari pemain berdasarkan tag
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found! Ensure the player GameObject is tagged as 'Player'.");
        }

        animator = GetComponent<Animator>(); // Mengambil komponen Animator
        if (animator == null)
        {
            Debug.LogError("Animator component not found on Enemy! Please attach an Animator component.");
        }
    }

    void Update()
    {
        if (player == null) return; // Jika player tidak ditemukan, jangan lakukan apa-apa

        float distanceToPlayer = Vector2.Distance(transform.position, player.position); // Hitung jarak ke pemain

        if (distanceToPlayer <= attackRange) // Jika pemain berada dalam jangkauan serangan
        {
            if (!isCooldown && Time.time >= nextAttackTime) // Jika tidak dalam cooldown dan bisa menyerang
            {
                AttackPlayer(); // Lakukan serangan
                nextAttackTime = Time.time + 1f / attackRate; // Setel waktu untuk serangan berikutnya
                currentAttackCount++;

                if (currentAttackCount >= maxAttackCount) // Jika sudah mencapai batas serangan
                {
                    StartCoroutine(AttackCooldown()); // Mulai cooldown
                }
            }
        }
    }

    void AttackPlayer()
    {
        if (player == null) return; // Pastikan player masih valid

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            animator.SetTrigger("Attack"); // Memicu animasi serangan
            playerHealth.TakeDamage(attackDamage); // Memberikan kerusakan pada pemain
        }
        else
        {
            Debug.LogError("PlayerHealth component not found on Player! Please attach a PlayerHealth component.");
        }
    }

    private IEnumerator AttackCooldown()
    {
        isCooldown = true; // Mulai cooldown
        yield return new WaitForSeconds(attackCooldown); // Tunggu hingga cooldown selesai
        currentAttackCount = 0; // Reset jumlah serangan
        isCooldown = false; // Reset cooldown
    }
}
