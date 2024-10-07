using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool startGame;

    public static MainMenu instance;

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
    #endregion

    public void OnStartButton()
    {
        Debug.Log("Starting");
        startGame = true;
        SceneManager.LoadScene("MainScene");
    }

    public void OnQuitButton()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
