using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void ContinueButtonClick()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("Продолжить");
    }

    public void NewGameButtonClick()
    {
        
        // Обновляем параметры PlayerPrefs
        PlayerPrefs.SetFloat("Joy", 20);
        PlayerPrefs.SetFloat("Sleep", 20);
        PlayerPrefs.SetFloat("Food", 20);
        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetInt("ActionsExecuted", 0);

        // Сохраняем изменения
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainScene");

        Debug.Log("Новая игра " + PlayerPrefs.GetInt("coins") + PlayerPrefs.GetInt("ActionsExecuted"));
    }

    public void ExitButtonClick()
    {
        Debug.Log("Выход");
        Application.Unload();
    }
}
