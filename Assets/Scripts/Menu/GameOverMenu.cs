using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject GameOverMenuUI;
    public GameObject NewGameButton;
    Player Player;

    public void LoadNewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameOverMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(NewGameButton);
    }
}
