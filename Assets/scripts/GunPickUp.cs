using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    [HideInInspector] public bool isPickedUp = false;
    private AudioSource _audioSource;
    private bool hasPlayedPickupSound = false;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false; // Make sure the sound doesn't loop
    }

    void Update()
    {
        // When gun is picked up, play sound only once
        if (isPickedUp && !hasPlayedPickupSound)
        {
            _audioSource.Play();
            hasPlayedPickupSound = true;
        }

        // When gun is dropped, reset so it can play again next time
        if (!isPickedUp && hasPlayedPickupSound)
        {
            hasPlayedPickupSound = false;
            _audioSource.Stop();
        }
    }
}

