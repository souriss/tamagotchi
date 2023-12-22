using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathScript : MonoBehaviour
{
    [SerializeField] private QuestionList[] questions;
    [SerializeField] private Text[] answersText;
    [SerializeField] private Text qText;
    [SerializeField] private GameObject headPanel;
    [SerializeField] private Button[] answerButtons = new Button[3];
    [SerializeField] private Sprite[] TFIcons = new Sprite[2];
    [SerializeField] private Image TFIcon;
    [SerializeField] private Text TFText;

    [SerializeField] private Text scroreText;
    int score = 0;

    List<object> qList;
    QuestionList crnQ;

    int randQ;
    bool defaultColor = false, trueColor = false, falseColor = false;

    float currentJoy;
    float currentFood;

    private void Start()
    {
        currentJoy = PlayerPrefs.GetFloat("Joy");
        currentFood = PlayerPrefs.GetFloat("Food");
    }

    private void Update()
    {
        if (defaultColor) headPanel.GetComponent<Image>().color = Color.Lerp(headPanel.GetComponent<Image>().color, new Color(255/255.0f, 255/255.0f, 255/255.0f), 8* Time.deltaTime);
        if (trueColor) headPanel.GetComponent<Image>().color = Color.Lerp(headPanel.GetComponent<Image>().color, new Color(168 / 255.0f, 255 / 255.0f, 45 / 255.0f), 8 * Time.deltaTime);
        if (falseColor) headPanel.GetComponent<Image>().color = Color.Lerp(headPanel.GetComponent<Image>().color, new Color(255 / 255.0f, 93 / 255.0f, 23 / 255.0f), 8 * Time.deltaTime);

        scroreText.text = score.ToString();
    }

    public void OnClickPlay()
    {
        score = 0;
        qList = new List<object>(questions);
        questionsGenerate();
        if (!headPanel.GetComponent<Animator>().enabled) headPanel.GetComponent<Animator>().enabled = true;
        else headPanel.GetComponent<Animator>().SetTrigger("In");
    }

    public void OnClickExit()
    {
        int coins = PlayerPrefs.GetInt("coins");
        PlayerPrefs.SetInt("coins", coins + score);
        SceneManager.LoadScene("MainScene");
    }
    void questionsGenerate()
    {
        if (qList.Count > 0) 
        {
            randQ = Random.Range(0, qList.Count);
            crnQ = qList[randQ] as QuestionList;
            qText.text = crnQ.question;

            qText.gameObject.GetComponent<Animator>().SetTrigger("In");
            List<string> answers = new List<string>(crnQ.answers);

            for (int i = 0; i < crnQ.answers.Length; i++)
            {
                int rand = Random.Range(0, answers.Count);
                answersText[i].text = answers[rand];
                answers.RemoveAt(rand);
            }
            StartCoroutine(animButtons());
        }
        else
        {
            print("Игра пройдена");

            currentJoy += 3f;
            currentFood -= 5f;

            if (currentJoy < 20) PlayerPrefs.SetFloat("Joy", currentJoy);
            else PlayerPrefs.SetFloat("Joy", 20);

            if (currentFood >= 0) PlayerPrefs.SetFloat("Food", currentFood);
            else PlayerPrefs.SetFloat("Food", 0);

            PlayerPrefs.Save();

            TFIcon.gameObject.GetComponent<Animator>().SetTrigger("Out");
            headPanel.GetComponent<Animator>().SetTrigger("Out");
            falseColor = false;
            defaultColor = true;
        }
    }
    IEnumerator animButtons()
    {
        yield return new WaitForSeconds(1);
        int a = 0;
        while (a < answerButtons.Length)
        {
            if (!answerButtons[a].gameObject.activeSelf) answerButtons[a].gameObject.SetActive(true);
            else answerButtons[a].gameObject.GetComponent<Animator>().SetTrigger("In");
            a++;
            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }

    IEnumerator TrueOrFalse(bool check) 
    {
        yield return new WaitForSeconds(1);
        defaultColor = false;
        for (int i = 0; i < answerButtons.Length; i++) answerButtons[i].gameObject.GetComponent<Animator>().SetTrigger("Out");
        qText.gameObject.GetComponent<Animator>().SetTrigger("Out");

        yield return new WaitForSeconds(0.7f);

        if (!TFIcon.gameObject.activeSelf) TFIcon.gameObject.SetActive(true);
        else TFIcon.gameObject.GetComponent<Animator>().SetTrigger("In");

        if (check)
        {
            trueColor = true;
            TFIcon.sprite = TFIcons[0];
            TFText.text = "Правильный ответ";
            yield return new WaitForSeconds(1);
            TFIcon.gameObject.GetComponent<Animator>().SetTrigger("Out");
            yield return new WaitForSeconds(1);
            qList.RemoveAt(randQ);
            questionsGenerate();
            trueColor= false;
            defaultColor = true;
            yield break;
        }
        else
        {
            falseColor = true;
            TFIcon.sprite = TFIcons[1];
            TFText.text = "Неверный ответ";
            
            currentJoy += 3f;
            currentFood -= 5f;
            
            if (currentJoy < 20) PlayerPrefs.SetFloat("Joy", currentJoy);
            else PlayerPrefs.SetFloat("Joy", 20);

            if (currentFood >= 0) PlayerPrefs.SetFloat("Food", currentFood);
            else PlayerPrefs.SetFloat("Food", 0);
            
            PlayerPrefs.Save();

            TFIcon.gameObject.GetComponent<Animator>().SetTrigger("Out");
            yield return new WaitForSeconds(1);
            headPanel.GetComponent<Animator>().SetTrigger("Out");
            falseColor= false;
            defaultColor= true;
            yield break;
        }

    }

    public void answersButtons(int index)
    {
        if (answersText[index].text.ToString() == crnQ.answers[0])
        {
            print("Верно");
            score++;
            StartCoroutine(TrueOrFalse(true));
        }
        else
        {
            print("Неверно");
            StartCoroutine(TrueOrFalse(false));
        } 
    }
}

[System.Serializable]
public class QuestionList
{
    public string question;
    public string[] answers = new string[3];
}
