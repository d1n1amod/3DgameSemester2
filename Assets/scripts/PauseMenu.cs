using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] AudioSource pausePanelAudio; // Assign your pause panel sound in Inspector

    private AudioSource[] allAudioSources;
    private bool isPaused = false;

    void Update()
    {
        // Detect ESC key press to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Continue();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Pause all sounds except pause panel audio
        allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            if (audio != pausePanelAudio)
            {
                audio.Pause();
            }
        }

        // Play the pause panel sound (if not already playing)
        if (pausePanelAudio != null && !pausePanelAudio.isPlaying)
        {
            pausePanelAudio.Play();
        }

        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Resume all other sounds
        if (allAudioSources != null)
        {
            foreach (AudioSource audio in allAudioSources)
            {
                if (audio != pausePanelAudio)
                {
                    audio.UnPause();
                }
            }
        }

        // Stop the pause panel sound
        if (pausePanelAudio != null && pausePanelAudio.isPlaying)
        {
            pausePanelAudio.Stop();
        }

        // Optionally relock the cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void Controls()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void Exit(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}

