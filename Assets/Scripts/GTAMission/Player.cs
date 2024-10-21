using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    [Header("Player Money")]
    public int playerMoney;
    public int currentkills;

    [Header("Player Inventory")]
    public Inventory inventory;
    public Missions missions;

    public PickupItem[] pickupItems;

    MiniMapScript minimap;

    public Light[] lightObjs;
    public GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        missions = FindObjectOfType<Missions>();
        FindObjectOfType<WantedLevel>().SetData(this);
        FindObjectOfType<MoneyUI>().SetData(this);
        pickupItems = FindObjectsOfType<PickupItem>();
        //saveglow = FindObjectOfType<SaveGlow>();

        minimap = FindObjectOfType<MiniMapScript>();
        minimap.player = transform;
       /* for (int e = 0; e < pickupItems.Length; e++)
        {
            var item = pickupItems[e];
            item.SetPlayerData(this);
        }*/
        //saveglow.SetData(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (gameManager.isNight)
        {
            for(int l=0; l<lightObjs.Length; l++)
            {
                var item = lightObjs[l];
                item.gameObject.SetActive(true);
            }
        }
        else
        {
            for (int l = 0; l < lightObjs.Length; l++)
            {
                var item = lightObjs[l];
                item.gameObject.SetActive(false);
            }
        }
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            playerMoney = data.playerMoney;

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            transform.position = position;

           /* inventory.isWeapon1Picked = data.isWeapon1Picked;
            inventory.isWeapon2Picked = data.isWeapon2Picked;
            inventory.isWeapon3Picked = data.isWeapon3Picked;
            inventory.isWeapon4Picked = data.isWeapon4Picked;*/

            missions.Mission1 = data.Mission1;
            missions.Mission2 = data.Mission2;
            missions.Mission3 = data.Mission3;
            missions.Mission4 = data.Mission4;

        }
    }
}
