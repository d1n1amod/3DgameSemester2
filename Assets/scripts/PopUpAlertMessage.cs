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
    public float displayDuration = 5f;

    [Tooltip("How fast the text fades in/out")]
    public float fadeSpeed = 2f;

    [Header("Dependencies")]
    public TimerScript timerScript;

    private bool hasShown = false;

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
        if (alertText == null) yield break;

        alertText.text = "ALERT! ALERT! ALERT! INTRUDERS DETECTED";

        // Fade IN
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetTextAlpha(alpha);
            yield return null;
        }

        // Wait visible
        yield return new WaitForSeconds(displayDuration);

        // Fade OUT
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            SetTextAlpha(alpha);
            yield return null;
        }

        // Hide completely
        SetTextAlpha(0);
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
