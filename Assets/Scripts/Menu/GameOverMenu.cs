using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject GameOverMenuUI;
    public GameObject NewGameButton;
    Player Player;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Time.timeScale = 1f;
        GameIsOver = false;
    }

    void Update()
    {
        IsDead();
    }

    public void LoadNewGame()
    {
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

    private void IsDead()
    {
        if (!GameIsOver && Player.Health <= 0)
        {
            GameIsOver = true;
            Time.timeScale = 0f;
            GameOverMenuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(NewGameButton);
        }
    }
}
