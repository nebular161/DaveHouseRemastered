using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class WinScreen : MonoBehaviour
{
    public TMP_Text timeText;
    private void Start()
    {
        if (PlayerPrefs.GetString("Gamemode") == "Timed")
        {
            timeText.text = $"Final Time: {PlayerPrefs.GetInt("FinalTimeForSession")}";
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
