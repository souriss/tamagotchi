using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject headPanel;
    [SerializeField] private Platform_Generator platformGenerator;
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private CameraFollow CameraFollow;


    public bool GameStarted()
    {
        return gameStarted;
    }

    public void SetGameStarted(bool value)
    {
        gameStarted = value;
    }

    public void OnClickPlay()
    {
        StartCoroutine(animStartHead());
    }

    public void OnClickCancel()
    {
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator animStartHead()
    {
        yield return new WaitForSeconds(0.5f);
        if (!headPanel.GetComponent<Animator>().enabled) headPanel.GetComponent<Animator>().enabled = true;
        else headPanel.GetComponent<Animator>().SetTrigger("In");
        yield return new WaitForSeconds(1);
        platformGenerator.StartPlatformGeneration(10);
        gameStarted = true;
        yield break;
    }
}
