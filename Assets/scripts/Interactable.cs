using UnityEngine;

public class Interactable : MonoBehaviour
{

    public enum InteractionType
    {
        Gun,
        Mailbox,
        Tomato,
        Gate
    }

    [Header("Settings")]
    public InteractionType type;

    [Header("UI Message")]
    public string interactionMessage = "";  // Leave blank for auto message

    [Header("Panel To Open")]
    public GameObject panelToOpen;   // Assign a UI Panel in Inspector

    [Header("Gate Settings")]
    public Animator gateAnimator;    // Assign your gate Animator here
    public string openAnimationTrigger = "GateOpen";
    private bool gateOpened = false;

    public string GetMessage()
    {
        // If message isn't customized, set one based on the type
        if (string.IsNullOrEmpty(interactionMessage))
        {
            switch (type)
            {
                case InteractionType.Gun:
                    return "Press E to pick up gun";
                case InteractionType.Mailbox:
                    return "Press F to open mailbox";
                case InteractionType.Tomato:
                    return "Press F to inspect tomato";
                case InteractionType.Gate:
                    return "Press F to open gate";
            }
        }
        return interactionMessage;
    }

    // Called by FPController when player interacts
    public void Interact()
    {
        switch (type)
        {
            case InteractionType.Gun:
                // Handle gun pickup logic here
                Debug.Log("Picked up gun: " + name);
                break;

            case InteractionType.Mailbox:
            case InteractionType.Tomato:
                if (panelToOpen != null)
                {
                    panelToOpen.SetActive(true);
                    Debug.Log("Opened panel: " + name);
                }
                break;

            case InteractionType.Gate:
                if (gateAnimator != null && !gateOpened)
                {
                    gateAnimator.SetTrigger(openAnimationTrigger);
                    gateOpened = true;
                    Debug.Log("Gate opened: " + name);
                }
                break;
        }
    }
}
