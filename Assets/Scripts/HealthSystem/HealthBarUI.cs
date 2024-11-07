using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider;      // Referensi langsung ke Slider
    public Vector3 offset;           // Offset untuk posisi di atas musuh
    private Transform target;        // Target yang diikuti oleh health bar (musuh)
    
    // Fungsi untuk menhatur MaxHealth
    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
    }
    
    // Fungsi untuk mengatur target health bar
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Update per frame untuk mengikuti posisi target
    void Update()
    {
        if (target != null)
        {
            // Mengatur posisi slider agar mengikuti target dengan offset
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }
    }

    // Fungsi untuk memperbarui nilai slider sesuai persentase kesehatan
    public void UpdateHealthBar(float healthPercent)
    {
        healthSlider.value = healthPercent;
    }
}