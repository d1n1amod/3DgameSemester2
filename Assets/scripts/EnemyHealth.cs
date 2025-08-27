using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthBar; 

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        Debug.Log("{gameObject.name} took {amount} damage. Health = {currentHealth}");

        if (healthBar != null)
        {
            healthBar.value = currentHealth; // update bar
        }
        else
        {
            Debug.LogWarning("HealthBar is NOT assigned in Inspector!");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("{gameObject.name} has died!");
        Destroy(gameObject);
    }
}
