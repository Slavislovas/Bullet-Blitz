using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject Time;
    public GameObject Level;
    public Slider healthBarSlider;
    public Slider XPBarSlider;

    public void SetMaxHealth(int maxHealth)
    {
        healthBarSlider.maxValue = maxHealth;
    }

    public void SetHealth(int health)
    {
        healthBarSlider.value = health;
    }

    public void SetMaxXP(int maxXp)
    {
        XPBarSlider.maxValue = maxXp;
    }

    public void SetXP(int xp)
    {
        XPBarSlider.value = xp;
    }

    public void SetLevel(int level)
    {
        Level.GetComponent<TMPro.TextMeshProUGUI>().text = level.ToString();
    }

    public void SetTime(int minutes, int seconds)
    {
        Time.GetComponent<TMPro.TextMeshProUGUI>().text = $"{minutes}:{seconds.ToString("00")}";
    }
}
