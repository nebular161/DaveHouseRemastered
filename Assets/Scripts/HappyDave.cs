using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyDave : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] float timeStaring;
    public float maxTimeInSeconds;
    void Update()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform == transform && timeStaring <= maxTimeInSeconds)
        {
            timeStaring += Time.deltaTime;
            if(timeStaring >= maxTimeInSeconds)
            {
                GameManager.Instance.UnlockTrophy(162152);
            }
        }
    }
}
