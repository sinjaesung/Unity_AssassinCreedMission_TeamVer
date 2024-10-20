using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool startGame;

    public bool IsOpen = false;

    private void Awake()
    {
    }
    public void OnStartButton()
    {
        Debug.Log("Starting");
        SceneManager.LoadScene("StartMedia_");
    }

    public void OnQuitButton()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void GetOpen()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            IsOpen = false;
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
