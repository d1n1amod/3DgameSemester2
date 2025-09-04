using UnityEngine;

public class TomatoBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null )
        {
            playerInventory.BulletCollected();
            gameObject.SetActive(false);
        }
    }
}
