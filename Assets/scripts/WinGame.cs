using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject victoryPanel; 
    public TextMeshProUGUI victoryText;

    [Header("Enemy Settings")]
    public string enemyTag = "Pig Animation"; 
    public float checkInterval = 1f;  

    private bool hasWon = false;

    void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        StartCoroutine(CheckEnemies());
    }

    IEnumerator CheckEnemies()
    {
        while (!hasWon)
        {
            yield return new WaitForSeconds(checkInterval);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            if (enemies.Length == 0)
            {
                hasWon = true;
                ShowVictoryMessage();
            }
        }
    }

    void ShowVictoryMessage()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        if (victoryText != null)
        {
            victoryText.text = "Congratulations! You have successfully eliminated all the enemies!";
        }

        
        Time.timeScale = 0f;
    }
}
