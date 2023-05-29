using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public Button ForestEndlessModeButton;
    public Button DungeonNormalModeButton;
    public Button DungeonEndlessModeButton;
    public DataHandlerScriptableObject DataHandler;

    private void Start()
    {
        DataHandler.Initialize();
        if (!DataHandler.SavedData.isCompletedForestNormalMode)
        {
            ForestEndlessModeButton.interactable = false;
            ForestEndlessModeButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().alpha = 0.5f;

            DungeonNormalModeButton.interactable = false;
            DungeonNormalModeButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().alpha = 0.5f;
        }

        if (!DataHandler.SavedData.isCompletedDungeonNormalMode)
        {
            DungeonEndlessModeButton.interactable = false;
            DungeonEndlessModeButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().alpha = 0.5f;
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
        NewGame(1);
    }

    public void ForestLevelEndlessMode()
    {
        Debug.Log("Forest endless mode selected");
        DataHandler.PlayingNormalMode = false;
        NewGame(1);
    }

    public void DungeonLevelNormalMode()
    {
        Debug.Log("Dungeon normal mode selected");
        DataHandler.PlayingNormalMode = true;
        NewGame(2);
    }

    public void DungeonLevelEndlessMode()
    {
        Debug.Log("Dungeon endless mode selected");
        DataHandler.PlayingNormalMode = false;
        NewGame(2);
    }

    public void NewGame(int offset)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + offset);
    }
}
