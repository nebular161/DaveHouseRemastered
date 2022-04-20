using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreens : MonoBehaviour
{
    [SerializeField] int hahaHilarious;
    public bool debug;
    bool doScaryThing;
    public Image image;
    public Sprite[] sprites;
    public Sprite sixHundred;

    public AudioSource sound;
    void Start()
    {
        hahaHilarious = Random.Range(0, 666);
        if(hahaHilarious != 666 && !debug)
        { 
            image.sprite = sprites[Random.Range(0, sprites.Length - 1)];
            StartCoroutine(NormalGameOver());
        }
        else
        {
            image.sprite = sixHundred;
            StartCoroutine(ScaryGameOver());
        }
    }
    private void Update()
    {
        if(doScaryThing)
        {
            image.color = Random.ColorHSV();
            image.transform.localScale = new Vector3(Random.Range(1.8f, 0.2f), Random.Range(1.8f, 0.2f), image.transform.localScale.z);
        }
    }
    public IEnumerator NormalGameOver()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
    public IEnumerator ScaryGameOver()
    {
        yield return new WaitForSeconds(5);
        sound.Play();
        doScaryThing = true;
        yield return new WaitForSeconds(sound.clip.length);
        Application.Quit();
    }
}
