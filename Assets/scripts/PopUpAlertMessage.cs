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
    public TimerScript timerScript; 

    private bool hasShown = false;

    void Start()
    {
        
        if (alertText != null)
        {
            Color c = alertText.color;
            c.a = 0;
            alertText.color = c;
        }

        
        if (timerScript == null)
        {
            timerScript = FindObjectOfType<TimerScript>();
        }

        StartCoroutine(CheckTimerAndShowAlert());
    }

    IEnumerator CheckTimerAndShowAlert()
    {
        
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

        
        alertText.text =  "ALERT! ALERT! ALERT! ENEMIES ARE HERE";

        
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            Color c = alertText.color;
            c.a = alpha;
            alertText.color = c;
            yield return null;
        }

        
        yield return new WaitForSeconds(displayDuration);

        
        {
            alpha -= Time.deltaTime * fadeSpeed;
            Color c = alertText.color;
            c.a = alpha;
            alertText.color = c;
            yield return null;
        }
    }
}
