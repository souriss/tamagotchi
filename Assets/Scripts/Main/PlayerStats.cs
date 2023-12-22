using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxSleep;
    [SerializeField] private float sleep;

    [SerializeField] private float maxJoy;
    [SerializeField] private float joy;

    [SerializeField] private float maxFood;
    [SerializeField] private float food;

    [SerializeField] private bool _death = false;

    [SerializeField] private Bar Bar;

    public float GetMaxSleep()
    {
        return maxSleep;
    }
    public float GetSleep()
    {
        return sleep;
    }

    public float GetMaxJoy()
    {
        return maxJoy;
    }
    public float GetJoy()
    {
        return joy;
    }

    public float GetMaxFood()
    {
        return maxFood;
    }
    public float GetFood()
    {
        return food;
    }

    public bool GetDeath()
    {
        return _death;
    }

    private void Awake()
    {
        // Загрузка сохраненных значений из PlayerPrefs
        sleep = PlayerPrefs.GetFloat("Sleep");
        joy = PlayerPrefs.GetFloat("Joy");
        food = PlayerPrefs.GetFloat("Food");

        // Проверка на случай первого запуска игры
        if (sleep <= 0) sleep = maxSleep;
        if (joy <= 0) joy = maxJoy;
        if (food <= 0) food = maxFood;

        Bar.UpdateSleepBar();
        Bar.UpdateFoodBar();
        Bar.UpdateJoyBar();
    }

    private void Start()
    {

    }

    private void OnApplicationQuit()
    {
        // Сохранение значений в PlayerPrefs при выходе из игры
        PlayerPrefs.SetFloat("Sleep", sleep);
        PlayerPrefs.SetFloat("Joy", joy);
        PlayerPrefs.SetFloat("Food", food);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (sleep > 0 && _death) _death = false;
        if (joy > 0 && _death) _death = false;
        if (food > 0 && _death) _death = false;
    }

    public void Tiredness(float amount)
    {
        sleep -= amount;
        if (sleep <= 0)
        {
            sleep = 0;
        }

        // Сохранение значения в PlayerPrefs
        PlayerPrefs.SetFloat("Sleep", sleep);
        Bar.UpdateSleepBar();
    }

    public void Hunger(float amount)
    {
        food -= amount;
        if (food <= 0)
        {
            food = 0;
        }

        // Сохранение значения в PlayerPrefs
        PlayerPrefs.SetFloat("Food", food);
        Bar.UpdateFoodBar();
    }

    public void Boredom(float amount)
    {
        joy -= amount;
        if (joy <= 0)
        {
            _death = true;
            joy = 0;
        }

        // Сохранение значения в PlayerPrefs
        PlayerPrefs.SetFloat("Joy", joy);
        Bar.UpdateJoyBar();
    }


    // Функция для увеличения параметра сна
    public void IncreaseSleep(float amount)
    {
        sleep += amount;
        if (sleep > maxSleep)
        {
            sleep = maxSleep;
        }

        // Сохранение значения в PlayerPrefs
        PlayerPrefs.SetFloat("Sleep", sleep);
        Bar.UpdateSleepBar();
    }

    // Функция для увеличения параметра еды
    public void IncreaseFood(float amount)
    {
        food += amount;
        if (food > maxFood)
        {
            food = maxFood;
        }

        // Сохранение значения в PlayerPrefs
        PlayerPrefs.SetFloat("Food", food);
        Bar.UpdateFoodBar();
    }

    // Функция для увеличения параметра веселья
    public void IncreaseJoy(float amount)
    {
        joy += amount;
        if (joy > maxJoy)
        {
            joy = maxJoy;
        }

        // Сохранение значения в PlayerPrefs
        PlayerPrefs.SetFloat("Joy", joy);
        Bar.UpdateJoyBar();
    }
}