using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //게임종료,시작메뉴,게임튜토리얼UI등을 담고 있는 요소
    public bool startGame;

    public static MainMenu instance;
    public GameObject GameTut;

    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//다른씬으로 갈때 현재 오브젝트 삭제하지않는다
        }

        else
            Destroy(gameObject);//다른 씬 갔다가 왔을때 awake또 실행되는데, 이때 awake로 새로 만들어지는데 새로만들어지는것만 삭제
    }
    private void OnEnable()
    {
        Debug.Log("MenuCanvas OnEnable>>");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion

    public void OnStartButton()
    {
        Debug.Log("Starting");
        MenuClose();
        CharacterSelection.instance.CursorActive();
        CharacterSelection.instance.ChildCharacterVisible();
        SceneManager.LoadScene("CharacterSelectScene");
    }

    public void OnQuitButton()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void MenuClose()
    {
        gameObject.SetActive(false);
    }
}
