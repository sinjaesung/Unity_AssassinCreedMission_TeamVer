using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;

    [SerializeField] private string loadSceneName;

    public static CharacterSelection instance;
    public bool startGame;

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
        StartGame();
    }

    public void OnQuitButton()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        startGame = true;

        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        //SceneManager.LoadScene(loadSceneName, LoadSceneMode.Single);
        SceneManager.LoadScene(loadSceneName);
    }
}
