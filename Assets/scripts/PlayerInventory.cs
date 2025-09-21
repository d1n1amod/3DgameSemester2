using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfBullets { get; private set; }

    public UnityEvent<PlayerInventory> OnBulletChanged;


    public UnityEvent<string> OnItemCollected;

    public void BulletCollected()
    {        
        NumberOfBullets += 3;
        OnBulletChanged?.Invoke(this);

        OnItemCollected?.Invoke("Tomato Collected");
    }

    public bool UseBullet()
    {
        if (NumberOfBullets > 0)
        {
            NumberOfBullets--;
            OnBulletChanged?.Invoke(this);
            return true;
        }

        return false;
    }
}
