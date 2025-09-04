using TMPro;
using UnityEngine;
using System.Collections;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI BulletText;

    void Start()
    {
        BulletText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateBulletText(PlayerInventory playerInventory)
    {
        BulletText.text = playerInventory.NumberOfBullets.ToString();
    }



}
