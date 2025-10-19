using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [Header("Assign the text element for this object")]
    [SerializeField] private TextMeshProUGUI interactionText; // Drag the text UI for this specific object

    [Header("Default message (optional override)")]
    [SerializeField] private string defaultMessage = "Press F";

    // Show a specific message
    public void ShowMessage(string message)
    {
        if (interactionText != null)
        {
            interactionText.text = message;
            interactionText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"{name} has no interactionText assigned.");
        }
    }

    // Show default message (if you don't specify one)
    public void ShowDefaultMessage()
    {
        ShowMessage(defaultMessage);
    }

    // Hide the message
    public void HideMessage()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}
