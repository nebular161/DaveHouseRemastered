using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public Collider daveSpeakTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other == daveSpeakTrigger)
        {
            GameManager.Instance.OnEnteredHouse();
        }
    }

  
}
