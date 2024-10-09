using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenu : MonoBehaviour
{
    public GameObject weaponsMenuUI;
    public bool weaponsMenuActive = false;
    public GameObject mainCamera;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;

    [Header("Rations")]
    public Inventory Inventory;

    [Header("Menus")]
    public GameObject playerUI;

    public void SetData(Inventory inventory_)
    {
        Inventory = inventory_;
    }
    private void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive == false)
         {
             //open weapon menu
             //playerUI.SetActive(false);
            // miniMapCanvas.SetActive(false);
             //currentmenuUI.SetActive(false);

             weaponsMenuUI.SetActive(true);
             weaponsMenuActive = true;
             Time.timeScale = 0;
             mainCamera.GetComponent<MainCameraController>().enabled = false;
         }

         else if (Input.GetKeyDown(KeyCode.Tab) && weaponsMenuActive == true)
         {
             //close weapon menu
             playerUI.SetActive(true);
             miniMapCanvas.SetActive(true);
             currentmenuUI.SetActive(true);

             weaponsMenuUI.SetActive(false);
             weaponsMenuActive = false;
             Time.timeScale = 1;
             mainCamera.GetComponent<MainCameraController>().enabled = true;
         }*/

        WeaponsCheck();
    }

    void WeaponsCheck()
    {
        if (Inventory.isWeapon1Active == true)
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            weapon4.SetActive(false);
            weapon5.SetActive(false);
            weapon6.SetActive(false);
        }
        else if (Inventory.isWeapon2Active == true)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
            weapon4.SetActive(false);
            weapon5.SetActive(false); 
            weapon6.SetActive(false);
        }
        else if (Inventory.isWeapon3Active == true)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
            weapon4.SetActive(false);
            weapon5.SetActive(false);
            weapon6.SetActive(false);
        }
        else if (Inventory.isWeapon4Active == true)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            weapon4.SetActive(true);
            weapon5.SetActive(false);
            weapon6.SetActive(false);
        }
        else if (Inventory.isWeapon5Active == true)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            weapon4.SetActive(false);
            weapon5.SetActive(true);
            weapon6.SetActive(false);
        }
        else if (Inventory.isWeapon6Active == true)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            weapon4.SetActive(false);
            weapon5.SetActive(false);
            weapon6.SetActive(true);
        }
    }
}
