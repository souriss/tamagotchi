using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bar : MonoBehaviour
{
    [SerializeField] private Image foodBarImage;
    [SerializeField] private Image joyBarImage;
    [SerializeField] private Image sleepBarImage;
    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        // «агрузка сохраненных значений из PlayerPrefs при старте
        UpdateFoodBar();
        UpdateJoyBar();
        UpdateSleepBar();
    }

    public void UpdateFoodBar()
    {
        float maxFood = playerStats.GetMaxFood();
        float currentFood = PlayerPrefs.GetFloat("Food");
        Debug.Log("Before fillAmount: " + foodBarImage.fillAmount);
        foodBarImage.fillAmount = Mathf.Clamp(currentFood / maxFood, 0, 1f);
        Debug.Log("After fillAmount: " + foodBarImage.fillAmount);
    }

    public void UpdateJoyBar()
    {
        float maxJoy = playerStats.GetMaxJoy();
        float currentJoy = PlayerPrefs.GetFloat("Joy");
        joyBarImage.fillAmount = Mathf.Clamp(currentJoy / maxJoy, 0, 1f);
        Debug.Log("JoyBar updated");
    }

    public void UpdateSleepBar()
    {
        float maxSleep = playerStats.GetMaxSleep();
        float currentSleep = PlayerPrefs.GetFloat("Sleep");
        sleepBarImage.fillAmount = Mathf.Clamp(currentSleep / maxSleep, 0, 1f);
        Debug.Log("SleepBar updated");
    }
}

