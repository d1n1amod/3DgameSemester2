using UnityEngine;

public class GateOpenSound : MonoBehaviour
{
    [Header("Gate Sound")]
    [Tooltip("Assign the gate opening sound clip once in the Inspector.")]
    public AudioClip gateOpenClip;

    private static AudioSource sharedAudioSource;

    private void Start()
    {
        // Create a shared AudioSource only once
        if (sharedAudioSource == null)
        {
            GameObject soundObject = new GameObject("SharedGateAudio");
            sharedAudioSource = soundObject.AddComponent<AudioSource>();
            sharedAudioSource.playOnAwake = false;
            DontDestroyOnLoad(soundObject);
        }
    }

    public void OpenGate()
    {
        // Just play the sound when door opens
        if (gateOpenClip != null)
        {
            sharedAudioSource.PlayOneShot(gateOpenClip);
        }

        // (Optional) Add door opening animation or logic here
        Debug.Log("Gate opened and sound played.");
    }

    void Update()
    {
        // Press F to open door (for testing)
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenGate();
        }
    }
}