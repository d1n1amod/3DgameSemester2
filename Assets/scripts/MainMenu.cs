using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject controlsMenu;
    public void Play()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Exit()
    {
        controlsMenu.SetActive(false);
        Time.timeScale = 1;

       
    }

    public void Quit()
    {
        Application.Quit();
    }
}
