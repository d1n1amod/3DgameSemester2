using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime); // auto destroy
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check enemy health on the object or its parent
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy == null)
        {
            enemy = collision.gameObject.GetComponentInParent<EnemyHealth>();
        }

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Hit enemy! Damage applied: " + damage);
        }

        Destroy(gameObject); // destroy bullet on any hit
    }
}
