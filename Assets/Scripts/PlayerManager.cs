using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    public Collider daveSpeakTrigger, daveAngry, winLine, stopwatch, playRobot;
    public bool beenJumpscared, inPadGame;
    public GameObject daveJumpscare, angryDave, gameUI;

    Move move;
    Look look;

    Dave dave;

    public Sprite stopwatchSprite;

    public PlayRobot playRobotScript;

    private void Start()
    {
        move = GetComponent<Move>();
        look = GetComponentInChildren<Look>();
        dave = angryDave.GetComponent<Dave>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == daveSpeakTrigger)
        {
            GameManager.Instance.OnEnteredHouse();
        }
        if (other == daveAngry && !dave.pied && !dave.spinning)
        {
            if (!beenJumpscared)
            {
                GameManager.Instance.UnlockTrophy(162193);
                Jumpscared();
            }
        }
        if(other == stopwatch)
        {
            if (!beenJumpscared)
            {
                daveJumpscare.GetComponent<SpriteRenderer>().sprite = stopwatchSprite;
                Jumpscared();
            }
        }
        if(other == winLine && GameManager.Instance.finalMode)
        {
            if(GameManager.Instance.skippedLegDay)
            {
                GameManager.Instance.UnlockTrophy(173715);
            }
            if(GameManager.Instance.gamemode == "Normal")
            {
                PlayerPrefs.SetInt("HasWon", 1);
                GameManager.Instance.UnlockTrophy(162161);
                SceneManager.LoadScene("Win");
            }
            else if(GameManager.Instance.gamemode == "Timed")
            {
                int score = (int)Mathf.Round(GameManager.Instance.timePassed);
                GameManager.Instance.SubmitScore(score, score.ToString(), 722152, $"Submitted on version {Application.version}");
                GameManager.Instance.UnlockTrophy(162513);
                PlayerPrefs.SetInt("HasWon", 1);
                PlayerPrefs.SetInt("FinalTimeForSession", score);
                SceneManager.LoadScene("WinTimed");
            }
        }
        if(other == playRobot && !inPadGame && playRobotScript.gameCooldown <= 0)
        {
            move.lockPos = true;
            look.lockRot = true;
            playRobotScript.StartGame();
            inPadGame = true;
        }
    }
    public void Jumpscared()
    {
        beenJumpscared = true;
        gameUI.SetActive(false);
        daveJumpscare.SetActive(true);
        stopwatch.gameObject.SetActive(false);
        angryDave.SetActive(false);
        StartCoroutine(Jumpscare());
        move.lockPos = true;
        look.lockRot = true;
    }
    public void EndPadGame()
    {
        playRobotScript.aud.PlayOneShot(playRobotScript.end);
        move.lockPos = false;
        look.lockRot = false;
        inPadGame = false;
    }
    IEnumerator Jumpscare()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }

  
}
