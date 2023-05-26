using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalModeCompleteMenu : MonoBehaviour
{
    public GameObject NormalModeCompleteUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        NormalModeCompleteUI.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
