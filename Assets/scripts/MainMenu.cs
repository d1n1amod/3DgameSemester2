using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject controlsPanel;
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Controls()
    {
        controlsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Exit(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
