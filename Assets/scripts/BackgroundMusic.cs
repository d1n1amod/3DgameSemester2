using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    // List the scenes where this music should play
    private readonly string[] allowedScenes = { "StartScene", "TutorialScene", "LoadingScene" };

    void Awake()
    {
        // If one already exists, destroy the duplicate
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If the current scene is not in the allowed list, destroy the music
        bool isAllowed = false;

        foreach (string allowedScene in allowedScenes)
        {
            if (scene.name == allowedScene)
            {
                isAllowed = true;
                break;
            }
        }

        if (!isAllowed)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}