using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossGameOver : MonoBehaviour
{
    public AudioClip[] gameOverClipsDaveCanSayLol;
    public AudioSource sourceLmao;

    int gameOverClipIndex;

    public GameObject buttons;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOverClipIndex = Random.Range(0, gameOverClipsDaveCanSayLol.Length - 1);
        StartCoroutine(GameOverSequence());
    }
    IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(1);
        sourceLmao.PlayOneShot(gameOverClipsDaveCanSayLol[gameOverClipIndex]);
        yield return new WaitForSeconds(gameOverClipsDaveCanSayLol[gameOverClipIndex].length);
        buttons.SetActive(true);
    }
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
