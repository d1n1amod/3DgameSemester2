using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigEnemy : MonoBehaviour
{
    private AudioSource pigAudio;

    void Awake()
    {
        // Get AudioSource on the same prefab
        pigAudio = GetComponent<AudioSource>();

        if (pigAudio != null)
        {
            pigAudio.Play(); // Play automatically when the pig spawns
        }
    }

    void OnDestroy()
    {
        // Stop the sound when pig is destroyed
        if (pigAudio != null)
        {
            pigAudio.Stop();
        }
    }
}