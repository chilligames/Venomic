using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;


/// <summary>
/// playerprefe:
/// 1: Freeze
/// 2: Minuse
/// 3: Delete
/// 4: Chance
/// 5: Reset
/// </summary>
public class Panel_shop_entity : MonoBehaviour
{
    public TextMeshProUGUI Text_Coin_number;

    public Slider Slider_freeze;
    public Slider Slider_Minuse;
    public Slider Slider_delete;
    public Slider Slider_chance;
    public Slider Slider_reset;

    public TextMeshProUGUI Text_Count_freeze;
    public TextMeshProUGUI Text_Count_minuse;
    public TextMeshProUGUI Text_Count_delete;
    public TextMeshProUGUI Text_Count_chance;
    public TextMeshProUGUI Text_Count_reset;

    public Button BTN_Buy;

    public string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    int Coin;

    int Coin_new;
    public void Change_value(GameObject Parent)
    {
        Coin = PlayerPrefs.GetInt("Coin");
        Coin_new = Coin;

        BTN_Buy.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("Freeze", PlayerPrefs.GetInt("Freeze") + (int)Slider_freeze.value);
            PlayerPrefs.SetInt("Minuse", PlayerPrefs.GetInt("Minuse") + (int)Slider_Minuse.value);
            PlayerPrefs.SetInt("Delete", PlayerPrefs.GetInt("Delete") + (int)Slider_delete.value);
            PlayerPrefs.SetInt("Chance", PlayerPrefs.GetInt("Chance") + (int)Slider_chance.value);
            PlayerPrefs.SetInt("Reset", PlayerPrefs.GetInt("Reset") + (int)Slider_reset.value);
            PlayerPrefs.SetInt("Coin", Coin_new);
            Chilligames_SDK.API_Client.Sync_coin_with_server(new Req_sync_coin_with_server { Coin = PlayerPrefs.GetInt("Coin"), _id = _id }, () => { }, err => { });

            Instantiate(gameObject).GetComponent<Panel_shop_entity>().Change_value(Parent);
            Parent.GetComponent<Panel_shop>().Change_value_entity_shop_category();
            Destroy(gameObject);

        });
    }


    private void Update()
    {
        Text_Coin_number.text = Coin_new.ToString();

        Coin_new = Coin - (int)Slider_freeze.value - (int)Slider_Minuse.value - (int)Slider_delete.value - (int)Slider_chance.value - (int)Slider_reset.value;

        if (Coin_new <= 0)
        {
            Coin_new = 0;
        }

        Slider_freeze.maxValue = Coin_new / 4;
        Slider_Minuse.maxValue = Coin_new / 2;
        Slider_delete.maxValue = Coin_new / 3;
        Slider_chance.maxValue = Coin_new / 2;
        Slider_reset.maxValue = Coin_new / 4;


        Text_Count_freeze.text = Slider_freeze.value.ToString();
        Text_Count_minuse.text = Slider_Minuse.value.ToString();
        Text_Count_delete.text = Slider_delete.value.ToString();
        Text_Count_chance.text = Slider_chance.value.ToString();
        Text_Count_reset.text = Slider_reset.value.ToString();

    }



}
