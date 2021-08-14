using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class bambiDoorTrigger : MonoBehaviour
{
    public SpriteRenderer bambi;
    public Transform firstPoint;
    public Transform endPoint;
    public Sprite eyes;
    bool started;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & !started)
        {
            StartCoroutine(tweeenBAMBIFUCKINGHELL());
            started = true;
        }
    }

    IEnumerator tweeenBAMBIFUCKINGHELL()
    {
        bambi.DOColor(Color.white, 2f);
        bambi.transform.DOMove(firstPoint.position + Vector3.up * 5, 1f, false);
        yield return new WaitForSecondsRealtime(1.5f);
        bambi.transform.DOMove(endPoint.position + Vector3.up * 5, 2.5f, false);
        bambi.DOColor(Color.black, 2f);
        yield return new WaitForSecondsRealtime(3f);
        bambi.color = Color.black;
        bambi.sprite = eyes;
        bambi.DOColor(Color.red, 1f);
        yield break;
    }
}
