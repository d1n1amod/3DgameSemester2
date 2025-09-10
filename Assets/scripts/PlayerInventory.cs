using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfBullets { get; private set; }

    public UnityEvent<PlayerInventory> OnBulletChanged;

    public void BulletCollected()
    {
        // each tomato gives 3 bullets
        NumberOfBullets += 3;

        OnBulletChanged?.Invoke(this);
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
