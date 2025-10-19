using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound1 : MonoBehaviour
{
    public AudioClip clickSound; // your sound clip
    private static AudioSource audioSource;

    void Start()
    {
        // Create or reuse a global AudioSource
        if (audioSource == null)
        {
            GameObject soundObj = new GameObject("ButtonClickSoundPlayer");
            audioSource = soundObj.AddComponent<AudioSource>();
            DontDestroyOnLoad(soundObj); // persist across scenes
        }

        // Find the Button component on this GameObject
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
