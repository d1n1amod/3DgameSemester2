using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void LoadMenu()
    {
        //this takes you back to the start scene
        SceneManager.LoadScene("StartScene");
    }
}
