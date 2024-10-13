using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //��������,���۸޴�,����Ʃ�丮��UI���� ��� �ִ� ���
    public bool startGame;

    public static MainMenu instance;
    public GameObject GameTut;

    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//�ٸ������� ���� ���� ������Ʈ ���������ʴ´�
        }

        else
            Destroy(gameObject);//�ٸ� �� ���ٰ� ������ awake�� ����Ǵµ�, �̶� awake�� ���� ��������µ� ���θ�������°͸� ����
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
