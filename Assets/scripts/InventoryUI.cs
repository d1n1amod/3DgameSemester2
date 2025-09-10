using TMPro;
using UnityEngine;
using System.Collections;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI BulletText;

    [SerializeField] private PlayerInventory playerInventory;

    void Start()
    {
        BulletText = GetComponent<TextMeshProUGUI>();

        if (playerInventory != null)
        {
            // Subscribe to event
            playerInventory.OnBulletChanged.AddListener(UpdateBulletText);
            // Set initial text
            UpdateBulletText(playerInventory);
        }
    }

    public void UpdateBulletText(PlayerInventory playerInventory)
    {
        BulletText.text = "Ammo: " + playerInventory.NumberOfBullets;
    }
}
