using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public Collider daveSpeakTrigger, daveAngry;
    public bool beenJumpscared;
    public GameObject daveJumpscare, angryDave;

    Move move;
    Look look;

    private void Start()
    {
        move = GetComponent<Move>();
        look = GetComponentInChildren<Look>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == daveSpeakTrigger)
        {
            GameManager.Instance.OnEnteredHouse();
        }
        if(other == daveAngry)
        {
            if(!beenJumpscared)
            {
                Jumpscared();
            }
        }
    }
    public void Jumpscared()
    {
        daveJumpscare.SetActive(true);
        angryDave.SetActive(false);
        StartCoroutine(Jumpscare());
        move.lockPos = true;
        look.lockRot = true;
    }
    IEnumerator Jumpscare()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }

  
}
