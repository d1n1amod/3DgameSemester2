using TMPro;
using UnityEngine;
using System.Collections;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI BulletText;

    [SerializeField] private PlayerInventory playerInventory;
    [Header("Pickup Message UI")]
    [SerializeField] private TextMeshProUGUI pickupMessageText;
    [SerializeField] private float messageDuration = 2f;

    void Start()
    {
        BulletText = GetComponent<TextMeshProUGUI>();

        if (playerInventory != null)
        {
            
            playerInventory.OnBulletChanged.AddListener(UpdateBulletText);
            playerInventory.OnItemCollected.AddListener(ShowPickupMessage);
            
            UpdateBulletText(playerInventory);
        }

        if (pickupMessageText != null)
        {
            pickupMessageText.gameObject.SetActive(false);
        }
    }

    public void UpdateBulletText(PlayerInventory playerInventory)
    {
        BulletText.text = "Ammo: " + playerInventory.NumberOfBullets;
    }

    private void ShowPickupMessage(string message)
    {
        if (pickupMessageText != null)
        {
            pickupMessageText.text = message;
            pickupMessageText.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(HideMessageAfterDelay());
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        pickupMessageText.gameObject.SetActive(false);
    }
}
