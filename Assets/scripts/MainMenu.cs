using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("TutorialScene");
        Time.timeScale = 1f;
    }

    public void Tutorial(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;

    }
        

    public void Quit()
    {
        Application.Quit();
    }
}
