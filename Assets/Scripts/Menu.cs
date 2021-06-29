using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void OpenMenu(int indexOpen, int indexClose, GameObject[] menus)
    {
        menus[indexOpen].SetActive(true);
        menus[indexClose].SetActive(false);
    }
}
