using UnityEngine;
using UnityEngine.EventSystems;

public class GateOpenSound : MonoBehaviour
{
    // ------------------------------
    //  GATE SOUND
    // ------------------------------
    [Header("Gate Sound Settings")]
    public AudioSource gateAudioSource;   // Drag AudioSource with gate sound
    public AudioClip gateOpenClip;        // Drag gate open sound here
    public Animator gateAnimator;         // Optional: assign gate animator
    public bool playerNearGate = false;   // Set true via trigger or manually

    // ------------------------------
    //  PANELS TO CONTROL
    // ------------------------------
    [Header("Panels (Deactivate Sound When Shown)")]
    public GameObject speechBubblePanel;
    public GameObject letterPanel;
    public GameObject gunPanel;

    // ------------------------------
    // DRAG AND DROP SYSTEM
    // ------------------------------
    [Header("Drag and Drop")]
    public bool isItem;                   // Tick if this object is draggable
    public bool isSlot;                   // Tick if this object is a slot
    public string correctItemTag;         // Tag that must match the item

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Canvas parentCanvas;

    void Start()
    {
        if (isItem)
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            parentCanvas = GetComponentInParent<Canvas>();
        }

        if (gateAudioSource != null)
        {
            gateAudioSource.loop = false;
            gateAudioSource.playOnAwake = false;
        }
    }

    void Update()
    {
        // Press F to open gate only if near
        if (playerNearGate && Input.GetKeyDown(KeyCode.F))
        {
            PlayGateSound();
        }

        // Stop sound if any of the panels appear
        if ((speechBubblePanel != null && speechBubblePanel.activeSelf) ||
            (letterPanel != null && letterPanel.activeSelf) ||
            (gunPanel != null && gunPanel.activeSelf))
        {
            StopGateSound();
        }
    }

    // ------------------------------
    //  Gate Sound Controls
    // ------------------------------
    public void PlayGateSound()
    {
        if (gateAudioSource == null || gateOpenClip == null) return;

        gateAudioSource.Stop();
        gateAudioSource.PlayOneShot(gateOpenClip);

        if (gateAnimator != null)
        {
            gateAnimator.SetTrigger("Open");
        }
    }

    public void StopGateSound()
    {
        if (gateAudioSource != null && gateAudioSource.isPlaying)
            gateAudioSource.Stop();
    }

    // ------------------------------
    //  Drag-and-Drop Handlers
    // ------------------------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isItem) return;

        originalParent = transform.parent;
        transform.SetParent(parentCanvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isItem) return;

        rectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isItem) return;

        transform.SetParent(originalParent);
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!isSlot) return;

        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null && draggedObject.CompareTag(correctItemTag))
        {
            draggedObject.transform.SetParent(transform);
            draggedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            Debug.Log(" Correct item placed in slot!");
        }
        else
        {
            Debug.Log(" Wrong item for this slot!");
        }
    }
}