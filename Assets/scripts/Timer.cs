using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public bool StartTimer;
    private bool hasWon = false;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Awake()
    {
        remainingTime = 75;
        StartTimer = false;
        timerText.gameObject.SetActive(false);
        StartCoroutine(StartTimerAfterDelay());
    }

    private IEnumerator StartTimerAfterDelay()
    {
        yield return new WaitForSeconds(60f);
        timerText.gameObject.SetActive(true);
        StartTimer = true;
    }

    void Update()
    {
        if (StartTimer && !hasWon)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime <= 0)
            {
                remainingTime = 0;
                timerText.color = Color.red;
                StartTimer = false;
                StartCoroutine(WaitForDeath());
                _audioSource.Play();
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
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

    //  Called only when all enemies are truly dead
    public void TriggerGameWin()
    {
        if (!hasWon) // Prevent double-triggering
        {
            hasWon = true;
            StartCoroutine(GameWinSequence());
        }
    }

    private IEnumerator GameWinSequence()
    {
        yield return new WaitForSeconds(1f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadSceneAsync("GameWinScene");
    }
}