using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PadGame : MonoBehaviour
{
    AudioSource source;
    public AudioClip intro, correct, incorrect;

    public GameObject startText, gameUI;

    public int problem, solution;

    public string[] sign;

    int num1, num2;

    public TMP_Text questionText, questionNumberText;
    public bool inProgessQuestion;
    public TMP_InputField playerAnswer;

    public Animator animator;

    public PlayerManager playerManager;
    void Start()
    {
        source = GetComponent<AudioSource>();
        GameManager.Instance.UnlockTrophy(173231);
    }
    public IEnumerator BeginSequence()
    {
        yield return new WaitForSeconds(0.3f);
        startText.SetActive(true);
        source.PlayOneShot(intro);
        yield return new WaitForSeconds(intro.length);
        startText.SetActive(false);
        gameUI.SetActive(true);
        MakeNewProblem();
    }
    public void MakeNewProblem()
    {
        playerAnswer.text = string.Empty;
        problem++;
        playerAnswer.ActivateInputField();
        if(problem <= 3)
        {
            questionNumberText.text = $"Question {problem}/3";
            num1 = Random.Range(0, 9);
            num2 = Random.Range(0, 9);
            string curSign = sign[Random.Range(0, sign.Length)];

            if(curSign == "Add")
            {
                solution = num1 + num2;
                questionText.text = $"{num1} + {num2}";
            }
            else if(curSign == "Subtract")
            {
                solution = num1 - num2;
                questionText.text = $"{num1} - {num2}";
            }
            else if(curSign == "Multiply")
            {
                solution = num1 * num2;
                questionText.text = $"{num1} x {num2}";
            }
            inProgessQuestion = true;
            return;
        }
        StartCoroutine(EndPadGame());
    }
    public void CheckYourAnswer()
    {
        if(problem <= 3)
        {
            if(playerAnswer.text == solution.ToString())
            {
                source.PlayOneShot(correct);
                MakeNewProblem();
            }
            else
            {
                source.PlayOneShot(incorrect);
                playerAnswer.ActivateInputField();
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inProgessQuestion = false;
            CheckYourAnswer();
        }
    }
    IEnumerator EndPadGame()
    {
        questionText.text = "Great job!";
        yield return new WaitForSeconds(2);
        gameUI.SetActive(false);
        animator.SetTrigger("CloseTablet");
        yield return new WaitForSeconds(0.3f);
        playerManager.EndPadGame();
        gameObject.SetActive(false);
    }
    public IEnumerator VirusDetected()
    {
        gameUI.SetActive(false);
        animator.SetTrigger("CloseTablet");
        yield return new WaitForSeconds(0.3f);
        playerManager.EndPadGame();
        gameObject.SetActive(false);
    }
}
