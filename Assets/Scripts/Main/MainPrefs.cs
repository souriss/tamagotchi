using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPrefs : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject gamesPanel;
    [SerializeField] private Image hintImage;
    [SerializeField] private PlayerStats playerStats;


    public Text coinsCount;

    private void Start()
    {
        // ѕровер€ем, были ли действи€ выполнены
        if (PlayerPrefs.GetInt("ActionsExecuted") == 0)
        {
            PlayerPrefs.SetInt("coins", 10);

            // ѕомечаем, что действи€ были выполнены
            PlayerPrefs.SetInt("ActionsExecuted", 1);
            PlayerPrefs.Save();
        }
        hintImage.gameObject.SetActive(false);
        StartCoroutine(animHint());
    }

    private void Update()
    {
        coinsCount.text = (PlayerPrefs.GetInt("coins")).ToString();
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OnClickShop()
    {
        if (!shopPanel.GetComponent<Animator>().enabled) shopPanel.GetComponent<Animator>().enabled = true;
        else shopPanel.GetComponent<Animator>().SetTrigger("In");
    }
    public void OnClickGames()
    {
        if (!gamesPanel.GetComponent<Animator>().enabled) gamesPanel.GetComponent<Animator>().enabled = true;
        else gamesPanel.GetComponent<Animator>().SetTrigger("In");
    }

    public void OnClickGamesBack()
    {
        gamesPanel.GetComponent<Animator>().SetTrigger("Out");
    }
    public void OnClickShopBack()
    {
        shopPanel.GetComponent<Animator>().SetTrigger("Out");
    }

    public void SwitchToPlatformerScene()
    {
        SceneManager.LoadScene("PlatphormerScene");
    }

    public void SwitchToMathScene()
    {
        SceneManager.LoadScene("MathScene");
    }

    public void SwitchToPuzzleScene()
    {
        SceneManager.LoadScene("PuzzleScene");
    }

    IEnumerator animHint()
    {
        hintImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        hintImage.gameObject.SetActive(false);
        yield break;
    }


}
