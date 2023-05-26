using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeMenu : MonoBehaviour
{
    public GameObject WeaponUpgradeUI;
    public Sprite tierOneArrow;
    public Sprite tierTwoArrow;
    public Sprite tierThreeArrow;
    public Sprite tierFourArrow;
    public GameObject ArrowImage;
    public GameObject UpgradeText;
    public static bool GameIsPaused = false;
    public Player player;

    public void ResumeGame()
    {
        WeaponUpgradeUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        Bow bow = player.instantiatedWeapon.GetComponent<Bow>();
        switch (bow.weaponTier)
        {
            case WeaponTierEnum.ONE:
                UpgradeText.GetComponent<TMPro.TextMeshProUGUI>().text = "The monster drops a strange looking chest...You find a weapon upgrade! Now you will shoot two arrows instead of one!";
                ArrowImage.GetComponent<Image>().sprite = tierTwoArrow;
                bow.weaponTier = WeaponTierEnum.TWO;
                break;
            case WeaponTierEnum.TWO:
                UpgradeText.GetComponent<TMPro.TextMeshProUGUI>().text = "The monster drops a strange looking chest...You find a weapon upgrade! Now you will shoot three spread out arrows at once!";
                ArrowImage.GetComponent<Image>().sprite = tierThreeArrow;
                bow.weaponTier = WeaponTierEnum.THREE;
                break;
            case WeaponTierEnum.THREE:
                UpgradeText.GetComponent<TMPro.TextMeshProUGUI>().text = "The monster drops a strange looking chest...You find a weapon upgrade! The arrows have a strange feel to them... You only see one arrow in your hand, but feel as if you are holding several.";
                ArrowImage.GetComponent<Image>().sprite = tierFourArrow;
                bow.weaponTier = WeaponTierEnum.FOUR;
                break;
        }

        WeaponUpgradeUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
