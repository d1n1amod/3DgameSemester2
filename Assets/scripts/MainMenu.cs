using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("LoadingScene");
        Time.timeScale = 1;
    }

    public void Tutorial(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;

    }
        

    public void Quit()
    {
        Application.Quit();
    }
}
