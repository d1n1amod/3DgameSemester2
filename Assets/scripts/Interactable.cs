using UnityEngine;

public class Interactable : MonoBehaviour
{

    [Header("UI Message")]
    public string interactionMessage = "Press F";

    [Header("Panel To Open")]
    public GameObject panelToOpen;   // Assign a UI Panel in Inspector

    [Header("Gate Settings")]
    public Animator gateAnimator;    // Assign your gate Animator here
    public string openAnimationTrigger = "GateOpen"; // Trigger name in Animator
    private bool gateOpened = false;

    // Called by FPController when the player interacts
    public void Interact()
    {
        // If there’s a UI panel, open it
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(true);
            Debug.Log("Opened panel from: " + name);
        }

        // If this is a gate, trigger animation once
        if (gateAnimator != null && !gateOpened)
        {
            gateAnimator.SetTrigger(openAnimationTrigger);
            gateOpened = true;
            Debug.Log("Gate opened: " + name);
        }
    }
}
