using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Item Info")]
    public int itemRadius;
    public string ItemTag;
    private GameObject ItemToPick;

    [Header("Player Info")]
    public Transform player;
    public Inventory inventory;
    public GameManager GM;
    public Player playerdata;

    public GameObject RelWeapon;

    public Missions missions;

    private void Start()
    {
        ItemToPick = GameObject.FindWithTag(ItemTag);
    }

    public void SetData(Transform player_,Inventory inventory_)
    {
        Debug.Log("PickupItem SetData>>" + player_.name+","+ inventory_.name);
        player = player_;
        inventory = inventory_;
    }
 
    private void Update()
    {
        if(player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < itemRadius)
            {
               /* if (Input.GetKeyDown("f"))
                {

                    if (ItemTag == "Sword")
                    {
                        Debug.Log(ItemTag + "Pickup");
                        inventory.isWeapon1Picked = true;
                        RelWeapon.SetActive(true);
                    }

                    else if (ItemTag == "Rifle")
                    {
                        Debug.Log(ItemTag + "Pickup");
                        inventory.isWeapon2Picked = true;
                        RelWeapon.SetActive(true);
                    }

                    else if (ItemTag == "Bazooka")
                    {
                        Debug.Log(ItemTag + "Pickup");
                        inventory.isWeapon3Picked = true;
                        RelWeapon.SetActive(true);
                    }

                    else if (ItemTag == "Grenade")
                    {
                        Debug.Log(ItemTag + "Pickup");
                        GM.numberofGrenades += 5;
                        inventory.isWeapon4Picked = true;
                        RelWeapon.SetActive(true);
                    }

                    else if (ItemTag == "Health")
                    {
                        Debug.Log(ItemTag + "Pickup");
                        GM.numberofHealth += 1;
                    }

                    else if (ItemTag == "Energy")
                    {
                        Debug.Log(ItemTag + "Pickup");
                        GM.numberofEnergy += 1;
                    }

                    ItemToPick.SetActive(false);
                }*/
            }
        }
       
    }
}
