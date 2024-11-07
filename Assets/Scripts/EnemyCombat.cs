using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float attackRange;             // Jarak serangan musuh
    public int attackDamage;              // Kerusakan yang diberikan ke pemain
    public float attackRate;              // Frekuensi serangan musuh
    private float nextAttackTime;         // Waktu untuk serangan berikutnya

    public int maxAttackCount;             // Jumlah maksimum serangan sebelum cooldown
    private int currentAttackCount;        // Jumlah serangan saat ini
    public float attackCooldown;          // Waktu cooldown antara serangan
    private bool isCooldown = false;           // Apakah musuh dalam keadaan cooldown?

    private Transform player;                  // Referensi ke pemain
    private Animator animator;                 // Referensi ke animator musuh

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Mencari pemain berdasarkan tag
        animator = GetComponent<Animator>();                           // Mengambil komponen Animator
    }

    void Update()
    {
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
        // Debug.Log("Enemy attacks!");
        animator.SetTrigger("Attack"); // Memicu animasi serangan
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage); // Memberikan kerusakan pada pemain
    }

    private IEnumerator AttackCooldown()
    {
        isCooldown = true; // Mulai cooldown
        // Debug.Log("Enemy is on cooldown!");
        yield return new WaitForSeconds(attackCooldown); // Tunggu hingga cooldown selesai
        currentAttackCount = 0; // Reset jumlah serangan
        isCooldown = false; // Reset cooldown
        Debug.Log("Enemy cooldown finished. Ready to attack again!");
    }
}
