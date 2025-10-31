using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 130f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthBar;

    [Header("Health Bar Smoothness")]
    public float healthBarSpeed = 0.8f; // higher = faster

    private float displayedHealth; // for smooth lerp
    private Animator animator;

    public delegate void EnemyDeath();
    public event EnemyDeath OnEnemyDied;

    void Start()
    {
        currentHealth = maxHealth;
        displayedHealth = maxHealth;
        animator = GetComponent<Animator>();
        
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

