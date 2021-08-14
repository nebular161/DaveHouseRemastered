using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneLoadTransition : MonoBehaviour
{
    public RawImage fader;

    public Move move;

    public Look look;

    public static SceneLoadTransition instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // man this script is a clusterfuck
    public void LoadTransition(string scene, float duration)
    {
        look.lockRot = true;
        move.lockPos = true;
        
        StartCoroutine(FRICKFADE(scene, duration));
    }

    public void FadeInTransition(float duration)
    {
        fader.DOFade(0, duration);   
    }

    IEnumerator FRICKFADE(string scene, float duration)
    {
        fader.DOFade(1, duration);
        yield return new WaitForSecondsRealtime(duration);
        SceneManager.LoadSceneAsync(scene);
    }
}
