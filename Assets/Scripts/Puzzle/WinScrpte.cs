using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class WinScrpte : MonoBehaviour
{

    [SerializeField] private GameObject myPuzzle;
    [SerializeField] private GameObject myPanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject headPanel;

    private int fullElement;
    static int myElement = 0;
    int score = 0;
    float currentJoy;
    float currentFood;

    void Start()
    {
        fullElement = myPuzzle.transform.childCount;
        myPanel.SetActive(false);
        myElement = 0;
        currentJoy = PlayerPrefs.GetFloat("Joy");
        currentFood = PlayerPrefs.GetFloat("Food");
    }

    public void OnClickPlay()
    {
        StartCoroutine(animButtons());
    }

    public void OnClickExit()
    {
        int coins = PlayerPrefs.GetInt("coins");
        PlayerPrefs.SetInt("coins", coins + score);

        PlayerPrefs.Save();

        SceneManager.LoadScene("MainScene");
    }

    void Update()
    {
        if (fullElement == myElement)
        {
            headPanel.GetComponent<Animator>().SetTrigger("Out");

            myPanel.SetActive(false);
            score = 5;

            currentJoy += 5f;
            currentFood -= 4f;

            if (currentJoy < 20) PlayerPrefs.SetFloat("Joy", currentJoy);
            else PlayerPrefs.SetFloat("Joy", 20);

            if (currentFood >= 0) PlayerPrefs.SetFloat("Food", currentFood);
            else PlayerPrefs.SetFloat("Food", 0);
        }

    }

    public static void AddElement()
    {
        myElement++;

    }

    IEnumerator animButtons()
    {
        if (!headPanel.GetComponent<Animator>().enabled) headPanel.GetComponent<Animator>().enabled = true;
        else headPanel.GetComponent<Animator>().SetTrigger("In");
        yield return new WaitForSeconds(1);
        myPanel.SetActive(true);
        yield break;
    }
}
