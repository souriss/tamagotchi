using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class DeadPoint : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _camera;
    [SerializeField] private StartGame startGame;
    [SerializeField] private GameObject headPanel;
    [SerializeField] private Text scroreText;

    int score = 0;
    float currentJoy;
    float currentFood;

    private void Start()
    {
        currentJoy = PlayerPrefs.GetFloat("Joy");
        currentFood = PlayerPrefs.GetFloat("Food");
    }
    public Transform GetTarget()
    {
        return _target;
    }


    private void Update()
    {
        score = (int)(_camera.position.y / 10);
        scroreText.text = (score).ToString();

        if (startGame.GameStarted() && _target.position.y <= transform.position.y)
        {
            StartCoroutine(animEndHead());
            int coins = PlayerPrefs.GetInt("coins");
            PlayerPrefs.SetInt("coins", coins + score);

            currentJoy += 4f;
            currentFood -= 7f;

            if (currentJoy < 20) PlayerPrefs.SetFloat("Joy", currentJoy);
            else PlayerPrefs.SetFloat("Joy", 20);

            if (currentFood >= 0) PlayerPrefs.SetFloat("Food", currentFood);
            else PlayerPrefs.SetFloat("Food", 0);

            PlayerPrefs.Save();
        }
    }

    IEnumerator animEndHead()
    {
        startGame.SetGameStarted(false);
        headPanel.GetComponent<Animator>().SetTrigger("Out");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield break;
    }
}
