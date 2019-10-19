using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public GameObject Raw_model_panel_buy_coin;

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
    public Button BTN_Close_panel;
    public Button BTN_Buy_coin;

    public string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    int Coin;

    int Coin_new;



    private void Start()
    {
    }

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

            int rand_num_offer = Random.Range(1, 7);
            int coin = ((int)Slider_freeze.value) + ((int)Slider_Minuse.value) + ((int)Slider_delete.value) + ((int)Slider_chance.value) + ((int)Slider_reset.value);

            Send_Offer(rand_num_offer, coin);

            Instantiate(gameObject).GetComponent<Panel_shop_entity>().Change_value(Parent);
            Parent.GetComponent<Panel_shop>().Change_value_entity_shop_category();

            Destroy(gameObject);

        });


        BTN_Close_panel.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });


        BTN_Buy_coin.onClick.AddListener(() =>
        {
            Instantiate(Raw_model_panel_buy_coin);
            Destroy(gameObject);
        });
        void Send_Offer(int Rand_Num, int coin)
        {
            if (coin / 100 * 80 > 1)
            {
                switch (Rand_Num)
                {
                    case 1:
                        {
                            int count_Entity = coin / 4;

                            Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user { Coin = (int)(coin / 100 * 80), Count = count_Entity, Key = "F", Name_App = "Venomic", Name_Entity = "Freeze", ID_entity = "F" + coin + count_Entity });
                        }
                        break;
                    case 2:
                        {
                            int Count_Entity = coin / 2;
                            Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user { Coin = (int)(coin / 100 * 80), Count = Count_Entity, Key = "M", Name_App = "Venomic", Name_Entity = "Minuse", ID_entity = "M" + coin + Count_Entity });
                        }
                        break;
                    case 3:
                        {
                            int Count_Entity = coin / 3;
                            Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user { Coin = (int)(coin / 100 * 80), Count = Count_Entity, Key = "D", Name_App = "Venomic", Name_Entity = "Delete", ID_entity = "D" + coin + Count_Entity });
                        }
                        break;
                    case 4:
                        {
                            int Count_Entity = coin / 2;
                            Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user { Coin = (int)(coin / 100 * 80), Count = Count_Entity, Key = "C", Name_App = "Venomic", Name_Entity = "Chance", ID_entity = "C" + coin + Count_Entity });
                        }
                        break;
                    case 5:
                        {
                            int Count_Entity = coin / 4;
                            Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user { Coin = (int)(coin / 100 * 80), Count = Count_Entity, Key = "R", Name_App = "Venomic", Name_Entity = "Reset", ID_entity = "R" + coin + Count_Entity });
                        }
                        break;
                }
            }
        }
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


        //show btn buy coin 
        if (PlayerPrefs.GetInt("Coin") <= 2)
        {
            BTN_Buy_coin.gameObject.SetActive(true);
        }

    }



}
