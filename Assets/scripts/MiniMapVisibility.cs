using UnityEngine;

public class MiniMapVisibility : MonoBehaviour
{
    [Header("Cameras")]
    public Camera mainCamera;       // Assign your main camera here
    public Camera minimapCamera;    // Assign your minimap camera here

    [Header("Layer Settings")]
    public string MiniMapOnly = "MinimapOnly"; // The layer we made earlier

    void Start()
    {
        // 1? Find the layer index
        int minimapLayer = LayerMask.NameToLayer(MiniMapOnly);
        if (minimapLayer == -1)
        {
            Debug.LogError("Layer '{MiniMapOnly}' not found! Create it under Edit > Project Settings > Tags and Layers.");
            return;
        }

        // 2? Put this GameObject (and its children) on the minimap layer
        SetLayerRecursively(gameObject, minimapLayer);

        // 3? Adjust camera culling masks automatically
        if (mainCamera != null)
        {
            mainCamera.cullingMask &= ~(1 << minimapLayer); // Hide this layer from main camera
        }

        if (minimapCamera != null)
        {
            minimapCamera.cullingMask |= (1 << minimapLayer); // Show this layer on minimap
        }
    }

    // Helper to set layer for all children too
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
