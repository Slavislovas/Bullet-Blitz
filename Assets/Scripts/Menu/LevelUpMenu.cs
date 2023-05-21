using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject LevelUpUI;
    public GameObject UpgradeOneImage;
    public GameObject UpgradeOneText;
    public GameObject UpgradeTwoImage;
    public GameObject UpgradeTwoText;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void ResumeGame()
    {
        LevelUpUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PauseGame(GameObject itemOne, GameObject itemTwo)
    {
        UpgradeOneImage.GetComponent<Image>().sprite = itemOne.GetComponent<SpriteRenderer>().sprite;
        UpgradeTwoImage.GetComponent<Image>().sprite = itemTwo.GetComponent<SpriteRenderer>().sprite;
        UpgradeOneText.GetComponent<TMPro.TextMeshProUGUI>().text = itemOne.GetComponent<Item>().description;
        UpgradeTwoText.GetComponent<TMPro.TextMeshProUGUI>().text = itemTwo.GetComponent<Item>().description;

        UpgradeOneImage.GetComponent<Button>().onClick.AddListener(delegate () { SelectItem(itemOne); });
        UpgradeTwoImage.GetComponent<Button>().onClick.AddListener(delegate () { SelectItem(itemTwo); });

        LevelUpUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(UpgradeOneImage);
    }

    void SelectItem(GameObject item)
    {
        player.ApplyItemStats(item.GetComponent<Item>());
        player.RemoveItemFromList(item);
        ResumeGame();
    }
}
