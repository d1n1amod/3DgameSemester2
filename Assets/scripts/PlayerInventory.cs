using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfBullets { get; private set; }

    public UnityEvent<PlayerInventory> OnBulletCollected;

    public void BulletCollected()
    {
        NumberOfBullets++;
        OnBulletCollected.Invoke(this);
    }
}
