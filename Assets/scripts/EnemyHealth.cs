using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthBar;

    [Header("Health Bar Smoothness")]
    public float healthBarSpeed = 2f; // higher = faster

    private float displayedHealth; // for smooth lerp

    public delegate void EnemyDeath();
    public event EnemyDeath OnEnemyDied;

    void Start()
    {
        currentHealth = maxHealth;
        displayedHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    void Update()
    {
        // Smoothly update the health bar
        if (healthBar != null)
        {
            displayedHealth = Mathf.Lerp(displayedHealth, currentHealth, Time.deltaTime * healthBarSpeed);
            healthBar.value = displayedHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnEnemyDied?.Invoke(); // notify the spawner
        Destroy(gameObject);
    }
}

