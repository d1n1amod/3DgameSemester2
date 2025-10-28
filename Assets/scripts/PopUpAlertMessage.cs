using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpAlertMessage : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("Drag your TextMeshProUGUI object here")]
    public TextMeshProUGUI alertText;

    [Tooltip("How long the alert should stay fully visible (in seconds)")]
    public float displayDuration = 3f;

    [Tooltip("How fast the text fades in/out")]
    public float fadeSpeed = 2f;

    [Header("Audio Settings")]
    [Tooltip("How fast the sound fades out")]
    public float soundFadeOutSpeed = 2f;

    [Header("Dependencies")]
    public TimerScript timerScript;

    private bool hasShown = false;
    private AudioSource _audioSource;
    private float originalVolume;

    void Start()
    {
        // Start transparent
        if (alertText != null)
        {
            Color c = alertText.color;
            c.a = 0;
            alertText.color = c;
        }

        // Auto-find TimerScript if not linked
        if (timerScript == null)
        {
            timerScript = FindObjectOfType<TimerScript>();
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource != null)
        {
            originalVolume = _audioSource.volume;
        }

        StartCoroutine(CheckTimerAndShowAlert());
    }

    IEnumerator CheckTimerAndShowAlert()
    {
        // Wait until TimerScript.StartTimer = true
        while (timerScript != null && !timerScript.StartTimer)
        {
            yield return null;
        }

        if (!hasShown)
        {
            hasShown = true;
            yield return StartCoroutine(ShowAlert());
        }
    }

    IEnumerator ShowAlert()
    {
        if (alertText == null || _audioSource == null) yield break;

        alertText.text = "ALERT! INTRUDERS DETECTED";

        // Play sound once at the start of fade-in
        _audioSource.volume = originalVolume;
        _audioSource.Play();

        // Fade IN text
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetTextAlpha(alpha);
            yield return null;
        }

        // Keep visible for duration
        yield return new WaitForSeconds(displayDuration);

        // Fade OUT text
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            SetTextAlpha(alpha);
            yield return null;
        }

        // Hide text completely
        SetTextAlpha(0);

        // Start fading out the sound smoothly
        yield return StartCoroutine(FadeOutSound());
    }

    IEnumerator FadeOutSound()
    {
        if (_audioSource == null) yield break;

        float startVolume = _audioSource.volume;

        while (_audioSource.volume > 0.01f)
        {
            _audioSource.volume -= Time.deltaTime * soundFadeOutSpeed;
            yield return null;
        }

        _audioSource.Stop();
        _audioSource.volume = originalVolume; // reset for next use
    }

    void SetTextAlpha(float a)
    {
        if (alertText != null)
        {
            Color c = alertText.color;
            c.a = Mathf.Clamp01(a);
            alertText.color = c;
        }
    }
}