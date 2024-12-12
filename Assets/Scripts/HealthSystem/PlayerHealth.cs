using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Slider healthSlider;
    private UIController uiController;

    protected override void Awake()
    {
        base.Awake();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
        //Buat ngambil referensi defeat panel dari UICONTROLLER
        uiController = FindObjectOfType<UIController>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Player died!");
        //Defeat Panel dari UICONTROLLER script
        if (uiController != null)
        {
            uiController.ShowDefeatPanel();
        }
        Time.timeScale = 0;
    }
}
