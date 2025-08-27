using UnityEngine;

public class Playershooting : MonoBehaviour
{
    public Camera fpsCam;         // Camera used for raycasting
    public float damage = 20f;    // Damage dealt to enemy
    public float range = 100f;    // Range of shooting
    public Transform firePoint;     // Empty GameObject at gun barrel
    public GameObject bulletPrefab; // Prefab with Rigidbody + Bullet script
    public float bulletForce = 50f;
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Default left-click / Ctrl
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse); 

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}

