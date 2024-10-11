using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenu : MonoBehaviour
{
    public GameObject mainCamera;

    [Header("Weapons")]
    public GameObject weapon1;
    public GameObject weapon2;
   
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
        WeaponsCheck();
    }

    void WeaponsCheck()
    {
        if (Inventory.isWeapon1Active == true)
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);    
        }
        else if (Inventory.isWeapon2Active == true)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);     
        }   
    }
}
