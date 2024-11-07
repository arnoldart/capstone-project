using UnityEngine;

public class PlayerHealth : Health
{
    protected override void Die()
    {
        isDead = true;
        Debug.Log("Player died!");
    }
}
