using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator; // Untuk animasi serangan
    public Transform attackPoint; // Posisi serangan melee
    public float attackRange = 1f; // Jarak serangan melee
    public LayerMask enemyLayers; // Layer musuh yang bisa diserang
    public int attackDamage = 10; // Damage serangan melee

    private PlayerHealth playerHealth; // Skrip kesehatan pemain

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Klik kiri untuk serangan melee
        {
            PerformAttack();
        }
    }

    // Melakukan serangan melee
    public void PerformAttack()
    {
        if (playerHealth != null && playerHealth.IsDead())
            return;

        if (animator != null)
        {
            animator.SetTrigger("singleSlash"); // Trigger animasi melee
        }

        ApplyDamage();
        
    }

    // Memberikan damage kepada musuh dalam radius serangan melee
    void ApplyDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage); // Memberikan damage pada musuh
            }
        }
    }

    // Menampilkan radius serangan melee di editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // Menampilkan area serangan
    }

    public void PlaySwordSFX()
    {
        AudioManager.instance.PlaySFX("pedang1");
    }
    public void PlaySwordSFX2()
    {
        AudioManager.instance.PlaySFX("pedang2");
    }
    public void PlaySwordSFX3()
    {
        AudioManager.instance.PlaySFX("pedang3");
    }



}
