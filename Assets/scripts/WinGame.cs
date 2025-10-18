using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class WinGame : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText;

    [Header("Pig Animation Settings")]
    [Tooltip("Drag pig animation prefabs or active enemies here. Works dynamically too.")]
    public List<GameObject> enemiesToTrack = new List<GameObject>();

    [Tooltip("How often to check for pig animation deaths (seconds).")]
    public float checkInterval = 0.5f;

    [Tooltip("How long the victory text takes to fade in.")]
    public float fadeDuration = 2f;

    private bool hasWon = false;

    void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        StartCoroutine(CheckEnemies());
    }

    void Update()
    {
        // Optional safety net — automatically find enemies with "Pig Animation" tag
        if (enemiesToTrack.Count == 0)
        {
            GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("Pig Animation");
            foreach (GameObject enemy in foundEnemies)
            {
                if (!enemiesToTrack.Contains(enemy))
                    enemiesToTrack.Add(enemy);
            }
        }
    }

    IEnumerator CheckEnemies()
    {
        while (!hasWon)
        {
            yield return new WaitForSeconds(checkInterval);

            // Clean up null or destroyed enemies
            enemiesToTrack.RemoveAll(enemy => enemy == null);

            // If list is empty, all enemies are gone
            if (enemiesToTrack.Count == 0)
            {
                hasWon = true;
                StartCoroutine(FadeInVictory());
            }
        }
    }

    IEnumerator FadeInVictory()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (victoryText != null)
        {
            victoryText.text = "Congratulations! All enemies have been defeated!";
            Color c = victoryText.color;
            c.a = 0;
            victoryText.color = c;

            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                c.a = Mathf.Clamp01(elapsed / fadeDuration);
                victoryText.color = c;
                yield return null;
            }

            c.a = 1;
            victoryText.color = c;
        }

        Time.timeScale = 0f;
    }
}