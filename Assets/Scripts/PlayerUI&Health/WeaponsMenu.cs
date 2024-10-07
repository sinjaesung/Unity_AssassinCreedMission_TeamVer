using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
    public GameObject weaponsMenuUI;
    public bool weaponsMenuActive = false;
    public GameObject mainCamera;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon4StockUI;

    [Header("Rations")]
    public Inventory Inventory;

    [Header("Menus")]
    public GameObject playerUI;
    public GameObject miniMapCanvas;
    public GameObject currentmenuUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive == false)
        {
            //open weapon menu
            playerUI.SetActive(false);
            miniMapCanvas.SetActive(false);
            currentmenuUI.SetActive(false);

            weaponsMenuUI.SetActive(true);
            weaponsMenuActive = true;
            Time.timeScale = 0;
            mainCamera.GetComponent<MainCameraController>().enabled = false;
        }

        else if(Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive == true)
        {
            //close weapon menu
            playerUI.SetActive(true);
            miniMapCanvas.SetActive(true);
            currentmenuUI.SetActive(true);

            weaponsMenuUI.SetActive(false);
            weaponsMenuActive = false;
            Time.timeScale = 1;
            mainCamera.GetComponent<MainCameraController>().enabled = true;
        }

        WeaponsCheck();
    }

    void WeaponsCheck()
    {
        if(Inventory.isWeapon1Picked == true)
        {
            weapon1.SetActive(true);
        }
        if (Inventory.isWeapon2Picked == true)
        {
            weapon2.SetActive(true);
        }
        if (Inventory.isWeapon3Picked == true)
        {
            weapon3.SetActive(true);
        }
        if (Inventory.isWeapon4Picked == true)
        {
            weapon4.SetActive(true);
            weapon4StockUI.SetActive(true);
        }
    }
}
