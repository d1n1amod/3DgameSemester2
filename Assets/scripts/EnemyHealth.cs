using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 150f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthBar;

    [Header("Health Bar Smoothness")]
    public float healthBarSpeed = 0.8f; // higher = faster

    private float displayedHealth; // for smooth lerp
    private Animator animator;

    [Header("Blood Effect")]
    [Tooltip("Assign the blood particle prefab here.")]
    public GameObject bloodEffectPrefab;
    [Tooltip("Offset for blood spawn position relative to pig's body.")]
    public Vector3 bloodOffset = new Vector3(0, 1.0f, 0);


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

        if (bloodEffectPrefab != null)
        {
            GameObject bloodFX = Instantiate(
                bloodEffectPrefab,
                transform.position + bloodOffset,
                Quaternion.identity
            );

            // Optionally parent to the pig so it moves with it
            bloodFX.transform.SetParent(transform);
        }

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

