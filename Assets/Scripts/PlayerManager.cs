using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public Collider fogTrigger, loadTrigger;
    public void EnableFog()
    {
        RenderSettings.fog = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!RenderSettings.fog && other == fogTrigger)
        {
            EnableFog();
        }
        else if(other == loadTrigger)
        {
            SceneManager.LoadScene("VentHalls");
        }
    }
}
