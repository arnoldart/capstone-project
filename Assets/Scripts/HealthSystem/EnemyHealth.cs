using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private Animator animator;
    public HealthBarUI healthBarUI; 
    private bool hasTakenDamage = false;        // Menambahkan flag untuk mengecek apakah musuh sudah terkena damage

    private EnemyMovement _enemyMovement;

    protected override void Initialize()
    {
        // HealthBarUI.SetHealth(100, 100);
        animator = GetComponent<Animator>();
        _enemyMovement = GetComponent<EnemyMovement>();
        
        if (healthBarUI != null)
        {
            healthBarUI.SetTarget(transform); // Set musuh sebagai target slider
            healthBarUI.SetMaxHealth(maxHealth); //Set MaxHealth Musuh
            healthBarUI.gameObject.SetActive(false); // Health bar dimulai dalam kondisi tidak aktif
        }
        else
        {
            Debug.LogWarning("HealthBarUI belum diatur di Inspector pada " + gameObject.name);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        
        if (!hasTakenDamage) // Cek apakah damage pertama kali
        {
            hasTakenDamage = true;
            if (healthBarUI != null)
            {
                healthBarUI.gameObject.SetActive(true); // Aktifkan health bar saat musuh pertama kali terkena damage
            }
        }
        
        if (healthBarUI != null) //Cek apakah gameobject healthBarUI sudah ada atau belum
        {
            float healthPercent = Mathf.RoundToInt((float)health / maxHealth * 100); // Gunakan maxHealth untuk perhitungan persentase
            healthBarUI.UpdateHealthBar(healthPercent);
        }
        
    }

    protected override void Die()
    {
        base.Die();
        // Debug.Log("Enemy died!");
        
        if (animator != null)
        {
            _enemyMovement.EnemyStop();
            animator.SetTrigger("Die");
        }

        StartCoroutine(WaitAndDestroy(2f));
    }

    private IEnumerator WaitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (healthBarUI != null)
        {
            healthBarUI.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }
}
