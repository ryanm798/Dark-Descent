using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    void Start()
    {
        NavigateTo(mainMenu);
    }
    
    public void Play()
    {
        GameManager.Instance.Play();
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }

    public void NavigateTo(GameObject menu)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        menu.SetActive(true);
    }
}
