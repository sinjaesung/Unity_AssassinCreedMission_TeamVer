using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Player Things")]
    public GameObject MC;
    public GameObject playerUI;
    public GameObject MMCam;
    public GameObject MMCanvas;
    public GameObject weaponstopick;
    public GameObject weaponsmenu;

    [Header("Timeline")]
    public GameObject cutScene1;

    [Header("HelpMenu")]
    public GameObject MenuCanvas;
    [Header("MissionMenu")]
    public Missions Missions;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Missions.GetMissionDetails();
        }
       /* if ((MainMenu.instance!=null && MainMenu.instance.startGame == true))
        {
           // Debug.Log("MainMenu.instance.startGame>>" + MainMenu.instance.startGame);
            MC.SetActive(false);
           // player.SetActive(false);
            playerUI.SetActive(false);
            MMCam.SetActive(false);
            MMCanvas.SetActive(false);
           // weaponstopick.SetActive(false);
            weaponsmenu.SetActive(false);

            cutScene1.SetActive(true);

            //Debug.Log("MainMenu ÄÆ¾À ½ÃÀÛ°¡´É"+ MainMenu.instance.startGame);
            if (CutSceneEnder.instance.CutSceneEnd == true)
            {
                MainMenu.instance.startGame = false;
                MC.SetActive(true);
                //player.SetActive(true);
                playerUI.SetActive(true);
                MMCam.SetActive(true);
                MMCanvas.SetActive(true);
                //weaponstopick.SetActive(true);
                weaponsmenu.SetActive(true);

                cutScene1.SetActive(false);
            }
        }*/
        if((CharacterSelection.instance != null && CharacterSelection.instance.startGame == true))
        {
            //Debug.Log("CharacterSelection.instance.startGame>>" + CharacterSelection.instance.startGame);
            //MC.SetActive(false);
            //player.SetActive(false);
            //playerUI.SetActive(false);
            //MMCam.SetActive(false);
            //MMCanvas.SetActive(false);
            //weaponstopick.SetActive(false);
            //weaponsmenu.SetActive(false);

            //cutScene1.SetActive(true);

            //Debug.Log("CharacterSelection ÄÆ¾À ½ÃÀÛ°¡´É"+ CharacterSelection.instance.startGame);
            //Debug.Log("MainMenu ÄÆ¾À ½ÃÀÛ°¡´É"+ MainMenu.instance.startGame);
            /*if (CutSceneEnder.instance.CutSceneEnd == true)
            {*/
                CharacterSelection.instance.startGame = false;
                MC.SetActive(true);
                //player.SetActive(true);
                playerUI.SetActive(true);
                MMCam.SetActive(true);
                MMCanvas.SetActive(true);
                //weaponstopick.SetActive(true);
                weaponsmenu.SetActive(true);

                //cutScene1.SetActive(false);
                CharacterSelection.instance.ChildCharacterHide();
            //}
        }
    }
}
