using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LevelSelect;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void RedirectToSelectLevel()
    {
        gameObject.SetActive(false);
        LevelSelect.SetActive(true);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
