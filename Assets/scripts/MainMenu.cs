using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("TutorialScene");
        Time.timeScale = 15;
    }

    public void Tutorial(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 15;

    }
        

    public void Quit()
    {
        Application.Quit();
    }
}
