using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepButton : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Image darkenImage;

    private bool sleepRestorationActive = false;

    private void Start()
    {
        // Добавляем метод к событию кнопки
        GetComponent<Button>().onClick.AddListener(ToggleSleepRestoration);
        darkenImage.gameObject.SetActive(false);
    }

    private void ToggleSleepRestoration()
    {
        // Инвертируем состояние
        sleepRestorationActive = !sleepRestorationActive;

        if (sleepRestorationActive)
        {
            // Если кнопка активирована, вызываем функцию восстановления сна каждые 5 секунд
            InvokeRepeating("RestoreSleepOverTime", 0f, 5f);
            darkenImage.gameObject.SetActive(true);
        }
        else
        {
            // Если кнопка деактивирована, отменяем вызов функции
            CancelInvoke("RestoreSleepOverTime");
            darkenImage.gameObject.SetActive(false);
        }
    }

    private void RestoreSleepOverTime()
    {
        // Увеличиваем сон на определенное количество единиц
        playerStats.IncreaseSleep(1f);
        playerStats.Boredom(1f);
    }
}
