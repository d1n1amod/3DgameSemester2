using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpAlertMessage : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("Drag your TextMeshProUGUI object here")]
    public TextMeshProUGUI alertText;

    [Tooltip("How long the alert should stay visible (in seconds)")]
    public float displayDuration = 3f;

    [Tooltip("How fast the text fades in/out")]
    public float fadeSpeed = 2f;

    [Header("Dependencies")]
    public TimerScript timerScript; // Reference to your TimerScript

    private bool hasShown = false;

    void Start()
    {
        // Make sure text starts invisible
        if (alertText != null)
        {
            Color c = alertText.color;
            c.a = 0;
            alertText.color = c;
        }

        // If not manually assigned, find TimerScript in the scene
        if (timerScript == null)
        {
            timerScript = FindObjectOfType<TimerScript>();
        }

        StartCoroutine(CheckTimerAndShowAlert());
    }

    IEnumerator CheckTimerAndShowAlert()
    {
        // Wait until the timer starts
        while (timerScript != null && !timerScript.StartTimer)
        {
            yield return null;
        }

        // Once timer starts, show the alert
        if (!hasShown)
        {
            hasShown = true;
            yield return StartCoroutine(ShowAlert());
        }
    }

    IEnumerator ShowAlert()
    {
        if (alertText == null) yield break;

        // Set alert text
        alertText.text =  "ALERT! ALERT! ALERT! ENEMIES ARE HERE";

        // Fade in
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            Color c = alertText.color;
            c.a = alpha;
            alertText.color = c;
            yield return null;
        }

        // Wait visible for displayDuration seconds
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            Color c = alertText.color;
            c.a = alpha;
            alertText.color = c;
            yield return null;
        }
    }
}
