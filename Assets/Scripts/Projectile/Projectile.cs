using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f; // Waktu hidup projectile sebelum dinonaktifkan
    private Rigidbody2D rb;     // Rigidbody2D untuk gerakan projectile
    private Animator animator;  // Animator untuk kontrol animasi

    void Awake()
    {
        // Ambil komponen Rigidbody2D dan Animator
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        // Reset kondisi saat diaktifkan
        StartCoroutine(DisableAfterTime());
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false); // Nonaktifkan daripada menghancurkan
    }

    // Atur animasi berdasarkan kecepatan
    void Update()
    {
    }

    public void Launch(Vector2 direction, float speed)
    {
        // Atur kecepatan projectile
        rb.velocity = direction * speed;
    }

    public void Explode() {
        animator.SetTrigger("explode");
    }

    public void disable() {
        gameObject.SetActive(false);
    }
}
