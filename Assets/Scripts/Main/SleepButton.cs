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
        // ��������� ����� � ������� ������
        GetComponent<Button>().onClick.AddListener(ToggleSleepRestoration);
        darkenImage.gameObject.SetActive(false);
    }

    private void ToggleSleepRestoration()
    {
        // ����������� ���������
        sleepRestorationActive = !sleepRestorationActive;

        if (sleepRestorationActive)
        {
            // ���� ������ ������������, �������� ������� �������������� ��� ������ 5 ������
            InvokeRepeating("RestoreSleepOverTime", 0f, 5f);
            darkenImage.gameObject.SetActive(true);
        }
        else
        {
            // ���� ������ ��������������, �������� ����� �������
            CancelInvoke("RestoreSleepOverTime");
            darkenImage.gameObject.SetActive(false);
        }
    }

    private void RestoreSleepOverTime()
    {
        // ����������� ��� �� ������������ ���������� ������
        playerStats.IncreaseSleep(1f);
        playerStats.Boredom(1f);
    }
}
