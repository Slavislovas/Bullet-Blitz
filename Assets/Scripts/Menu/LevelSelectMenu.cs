using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public Button ForestEndlessModeButton;
    public DataHandlerScriptableObject DataHandler;

    private void Start()
    {
        DataHandler.Initialize();
        if (!DataHandler.SavedData.isCompletedForestNormalMode)
        {
            ForestEndlessModeButton.interactable = false;
            ForestEndlessModeButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().alpha = 0.5f;
        }
    }

    public void RedirectToMainMenu()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ForestLevelNormalMode()
    {
        Debug.Log("Forest normal mode selected");
        DataHandler.PlayingNormalMode = true;
        NewGame();
    }

    public void ForestLevelEndlessMode()
    {
        Debug.Log("Forest endless mode selected");
        DataHandler.PlayingNormalMode = false;
        NewGame();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
