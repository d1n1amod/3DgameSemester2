using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remianingTime;

    public bool StartTimer;

    void Awake()
    {
        remianingTime = 60;
        StartTimer = false; // Don't start immediately

        // Hide the timer at the start
        timerText.gameObject.SetActive(false);

        StartCoroutine(StartTimerAfterDelay());
    }

    private IEnumerator StartTimerAfterDelay()
    {
        yield return new WaitForSeconds(30f); // wait 30 seconds

        // Show the timer and start counting
        timerText.gameObject.SetActive(true);
        StartTimer = true;
    }
    public void Update()
    {
        if (StartTimer)
        {
            if (remianingTime > 0)
            {
                remianingTime -= Time.deltaTime;
            }
            else if (remianingTime <= 0)
            {
                remianingTime = 0;
                // GameOver();
                timerText.color = Color.red;
                StartTimer = false;
                StartCoroutine(WaitForDeath());

            }
            int minutes = Mathf.FloorToInt(remianingTime / 60);
            int seconds = Mathf.FloorToInt(remianingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(1f);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadSceneAsync("GameOverScene");
    }
}
