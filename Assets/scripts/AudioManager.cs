using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource nextSound;

    void Start()
    {
        // Play the first sound
        if (gameOverSound != null)
        {
            gameOverSound.Play();
            // Start coroutine that waits for it to finish
            StartCoroutine(PlayNextAfter(gameOverSound.clip.length));
        }
    }

    private System.Collections.IEnumerator PlayNextAfter(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Play the next sound immediately after the first finishes
        if (nextSound != null)
        {
            nextSound.Play();
        }
    }
}

