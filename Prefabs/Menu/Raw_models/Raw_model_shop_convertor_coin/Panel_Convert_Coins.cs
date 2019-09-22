using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chilligames.Json;
using Chilligames.SDK.Model_Client;
using Chilligames.SDK;
using TMPro;
using UnityEngine.UI;


public class Panel_Convert_Coins : MonoBehaviour
{

    public TextMeshProUGUI Text_Coin_number_coin_to_money;
    public TextMeshProUGUI Text_money_number_coin_to_money;


    public TextMeshProUGUI Text_Coin_number_money_to_coin;
    public TextMeshProUGUI Text_money_number_money_to_coin;

    public Slider Slider_Coin_to_money;
    public Slider Slider_money_to_coin;

    public Button BTN_Change_coin_to_money;
    public Button BTN_Change_money_to_coin;
    public Button BTN_close_panel;


    int Coin;
    double Money;

    double Money_change;
    int Coin_change;

    double Money_Change_money_to_coin;
    int Coin_change_money_to_coin;

    string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    /// <summary>
    /// value haro change mikone beyad dashte bash k in bayad farakhani beshe ta entity taghir dashte bashan 
    /// </summary>
    /// <param name="Parent"></param>
    public void Change_value(GameObject Parent)
    {

        Chilligames_SDK.API_Client.Recive_Coin_mony(new Req_recive_coin { _id = _id }, result =>
        {
            Coin = (int)result.Coin;
            Money = (double)result.Money;
            Slider_Coin_to_money.maxValue = Coin;
            Slider_money_to_coin.maxValue = (float)Money;

            Coin_change = Coin;
            Money_Change_money_to_coin = Money;

        }, err => { });

        Slider_Coin_to_money.onValueChanged.AddListener((value) =>
        {
            Coin_change = Coin - (int)value;
            Money_change = value / 500;

        });

        Slider_money_to_coin.onValueChanged.AddListener((Value) =>
        {
            Money_Change_money_to_coin = Money - Value;
            Coin_change_money_to_coin = (int)Value * 470;
        });

        BTN_Change_coin_to_money.onClick.AddListener(() =>
        {
            Chilligames_SDK.API_Client.Convert_wallet(new Req_convert_coin_to_money_money_to_coin { _id = _id, Coin = (int)Slider_Coin_to_money.value, Select_mode = Req_convert_coin_to_money_money_to_coin.Mode.Coin }, () =>
            {
                Instantiate(gameObject).GetComponent<Panel_Convert_Coins>().Change_value(Parent);
                Destroy(gameObject);

            }, err => { });

        });

        BTN_Change_money_to_coin.onClick.AddListener(() =>
        {
            Chilligames_SDK.API_Client.Convert_wallet(new Req_convert_coin_to_money_money_to_coin { Coin = (int)Slider_money_to_coin.value, _id = _id, Select_mode = Req_convert_coin_to_money_money_to_coin.Mode.Money }, () =>
            {
                Instantiate(gameObject).GetComponent<Panel_Convert_Coins>().Change_value(Parent);
                Destroy(gameObject);
            }, err => { });

        });

        BTN_close_panel.onClick.AddListener(() =>
        {
            Parent.GetComponent<Panel_shop>().Recive_entity_wallet();
            Destroy(gameObject);
        });
    }


    void Update()
    {
        Text_Coin_number_coin_to_money.text = Coin_change.ToString();
        Text_money_number_coin_to_money.text = System.Math.Round((Money_change + Money), 1).ToString();

        Text_money_number_money_to_coin.text = System.Math.Round(Money_Change_money_to_coin, 1).ToString();
        Text_Coin_number_money_to_coin.text = (Coin_change_money_to_coin + Coin).ToString();

        if (Slider_money_to_coin.maxValue < 1)
        {
            Slider_money_to_coin.interactable = false;
        }
    }
}

