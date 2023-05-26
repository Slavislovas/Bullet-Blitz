using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int minutes;
    public int seconds;
    public bool newMinute;
    public DataHandlerScriptableObject DataHandler;
    public GameObject normalModeCompleteCanvas;
    private NormalModeCompleteMenu normalModeCompleteMenu;

    // Start is called before the first frame update
    void Start()
    {
        minutes = 9;
        seconds = 50;
        newMinute = false;
        normalModeCompleteMenu = normalModeCompleteCanvas.GetComponent<NormalModeCompleteMenu>();
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
                newMinute = true;
            }
            if (DataHandler.PlayingNormalMode && minutes == 10)
            {
                string levelName = SceneManager.GetActiveScene().name;
                DataHandler.SaveCompletedNormalMode(levelName);
                normalModeCompleteMenu.PauseGame();
            }

            Debug.Log("Minutes: " + minutes + " Seconds: " + seconds);

        }
    }
}
