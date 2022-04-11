using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;

    void Start()
    {
        menu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        menu.SetActive(!menu.activeSelf);
        if (menu.activeSelf)
        {
            GameManager.Instance.ScaleTime(0);
        }
        else
        {
            GameManager.Instance.ScaleTime(1);
        }
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }

    public void ReturnToMain()
    {
        GameManager.Instance.MainMenu();
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }
}
