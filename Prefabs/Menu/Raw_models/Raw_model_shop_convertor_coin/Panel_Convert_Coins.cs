using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chilligames.Json;
using Chilligames.SDK.Model_Client;
using Chilligames.SDK;
using TMPro;


public class Panel_Convert_Coins : MonoBehaviour
{

    public TextMeshProUGUI Text_Coin_number_coin_to_money;
    public TextMeshProUGUI Text_money_number_coin_to_money;


    public TextMeshProUGUI Text_Coin_number_money_to_coin;
    public TextMeshProUGUI Text_money_number_money_to_coin;


    string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }
    void Start()
    {

        Chilligames_SDK.API_Client.Recive_Coin_mony(new Req_recive_coin { _id = _id }, result =>
        {
            Text_Coin_number_coin_to_money.text = result.Coin.ToString() ;
            Text_money_number_money_to_coin.text = result.Money.ToString();

        }, err => { });
    }


    void Update()
    {

    }
}

