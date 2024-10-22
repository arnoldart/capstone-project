using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;  // Titik asal serangan
    public float attackRange;  // Jarak serangan
    public LayerMask enemyLayers;  // Layer untuk mendeteksi musuh
    public int attackDamage;  // Damage serangan
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>(); // Mengambil referensi ke PlayerHealth
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Klik kiri untuk menyerang
        {
            Attack();
        }
    }

    // Fungsi untuk melakukan serangan
    void Attack()
    {
        if (playerHealth.isDead)
            return; // Tidak bisa menyerang jika mati

        // Memicu animasi serangan
        animator.SetTrigger("Attack");

        // Deteksi musuh dalam jarak serangan menggunakan OverlapCircle
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage musuh yang terkena
        foreach (Collider2D enemy in hitEnemies)
        {
            // Ambil komponen EnemyHealth pada musuh yang terkena
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage); // Serang musuh
            }
        }
    }

    // Fungsi untuk menggambar jarak serangan (untuk debugging)
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
