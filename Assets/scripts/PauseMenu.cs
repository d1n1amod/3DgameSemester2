using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject controlsMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Exit(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
        Time.timeScale = 0;
    }


    public void Continue()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Pause has been presed");

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void Restart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
