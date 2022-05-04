using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Secret : MonoBehaviour
{
    bool startRising;
    public GameObject risingThing;
    private void Start()
    {
        StartCoroutine(SecretSeq());
    }
    private void Update()
    {
        if(startRising)
        {
            risingThing.transform.position = new Vector3(risingThing.transform.position.x, risingThing.transform.position.y + 1.25f, risingThing.transform.position.z);
        }
    }
    IEnumerator SecretSeq()
    {
        yield return new WaitForSeconds(64);
        startRising = true;
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}
