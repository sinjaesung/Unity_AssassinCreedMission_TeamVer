using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Player player;
    public Text MoneyAmountText;

    public void SetData(Player player_)
    {
        player = player_;
    }
    private void Update()
    {
        if(player != null)
        {
            MoneyAmountText.text = "" + player.playerMoney;
        }
    }
}