using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;                   // Kesehatan musuh

    private Animator animator;                 // Referensi ke komponen Animator

    void Start()
    {
        animator = GetComponent<Animator>();   // Mengambil referensi ke Animator
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Mengurangi kesehatan
        health = Mathf.Max(health, 0); // Memastikan kesehatan tidak negatif
        Debug.Log("Enemy health after damage: " + health);

        if (health <= 0)
        {
            Die(); // Memanggil fungsi kematian jika kesehatan nol
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetTrigger("Die"); // Memicu animasi kematian
        StartCoroutine(WaitAndDestroy(2f)); // Tunggu 2 detik sebelum musuh dihancurkan
    }

    private IEnumerator WaitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(delay); // Menunggu durasi yang ditentukan
        Destroy(gameObject); // Menghancurkan musuh
    }
}
