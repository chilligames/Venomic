﻿using UnityEngine;
using System.Threading;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using UnityEngine.UI;
using UnityEngine.Rendering;

/// <summary>
/// playerprefe:
/// 1: Freeze
/// 2: Minuse
/// 3: Delete
/// 4: Chance
/// 5: Reset
/// 6: Coin
/// </summary>
public class Panel_shop : MonoBehaviour
{
    public GameObject Raw_model_convert_panel;
    public GameObject Raw_model_fild_Offer;
    public GameObject Raw_model_panel_shop_entity;
    public GameObject Raw_model_panel_shop_coin;


    public Texture[] Icons_offers;

    public TextMeshProUGUI Text_Coin_number;
    public TextMeshProUGUI Text_Mony_number;

    public TextMeshProUGUI Text_freeze_number;
    public TextMeshProUGUI Text_minuse_number;
    public TextMeshProUGUI Text_delete_number;
    public TextMeshProUGUI Text_chance_number;
    public TextMeshProUGUI Text_reset_number;


    public Transform Place_offers;

    public Button BTN_Enter_convert;
    public Button BTN_Enter_shop_entity;
    public Button BTN_enter_to_coin_shop;

    GameObject[] Offers = null;

    public string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    private void Start()
    {
        BTN_Enter_convert.onClick.AddListener(() =>
        {
            Instantiate(Raw_model_convert_panel).GetComponent<Panel_Convert_Coins>().Change_value(gameObject);
        });

        BTN_Enter_shop_entity.onClick.AddListener(() =>
        {

            Instantiate(Raw_model_panel_shop_entity).GetComponent<Panel_shop_entity>().Change_value(gameObject);

        });

        BTN_enter_to_coin_shop.onClick.AddListener(() =>
        {
            Instantiate(Raw_model_panel_shop_coin).GetComponent<Panel_coin_shop>().Update_entity_coin(Text_Coin_number);
        });
    }

    public void OnEnable()
    {
        //stater 
        if (Offers != null)
        {
            for (int i = 0; i < Offers.Length; i++)
            {
                Destroy(Offers[i]);
            }
            Offers = null;
        }


        //recive entity
        Recive_entity_wallet();
        Change_value_entity_shop_category();

        Chilligames_SDK.API_Client.Recive_offers(new Req_recive_offers { Name_App = "Venomic", _id = _id }, result =>
        {
            Offers = new GameObject[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                Offers[i] = Instantiate(Raw_model_fild_Offer, Place_offers);
                Offers[i].AddComponent<Raw_model_fild_offer>().Change_Value(_id, result[i].Key, result[i].Name_Entity, (int)result[i].Count, (int)result[i].Coin, result[i].ID, Icons_offers);
            }


        }, err => { });
    }

    private void OnDisable()
    {
        if (Offers != null)
        {
            for (int i = 0; i < Offers.Length; i++)
            {
                Destroy(Offers[i]);
            }
            Offers = null;
        }
    }



    public void Recive_entity_wallet()
    {
        Chilligames_SDK.API_Client.Recive_Coin_mony(new Req_recive_coin { _id = _id }, result =>
        {
            Text_Coin_number.text = result.Coin.ToString();
            Text_Mony_number.text = System.Math.Round((decimal)result.Money, 1).ToString();

        }, err => { });
    }


    /// <summary>
    /// valure entity categori taghir mide 
    /// </summary>
    public void Change_value_entity_shop_category()
    {
        //reset count can buy player
        Text_freeze_number.text = (PlayerPrefs.GetInt("Coin") / 4).ToString();
        Text_minuse_number.text = (PlayerPrefs.GetInt("Coin") / 2).ToString();
        Text_delete_number.text = (PlayerPrefs.GetInt("Coin") / 3).ToString();
        Text_chance_number.text = (PlayerPrefs.GetInt("Coin") / 2).ToString();
        Text_reset_number.text = (PlayerPrefs.GetInt("Coin") / 4).ToString();

        Text_Coin_number.text = PlayerPrefs.GetInt("Coin").ToString();

    }


    public TextMeshProUGUI hand()
    {
        return Text_Coin_number;
    }

    class Raw_model_fild_offer : MonoBehaviour
    {
        TextMeshProUGUI Text_name_enitty
        {
            get
            {
                TextMeshProUGUI Text_name_entity = null;
                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "TNE")
                    {
                        Text_name_entity = Texts;
                    }
                }
                return Text_name_entity;
            }
        }
        TextMeshProUGUI Text_Count
        {
            get
            {
                TextMeshProUGUI Text_Count = null;
                foreach (var texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (texts.name == "TCE")
                    {
                        Text_Count = texts;

                    }
                }
                return Text_Count;
            }
        }
        TextMeshProUGUI Text_coin
        {
            get
            {
                TextMeshProUGUI Text_coin = null;
                foreach (var texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (texts.name == "TCN")
                    {
                        Text_coin = texts;
                    }
                }
                return Text_coin;
            }
        }

        Button BTN_buy
        {
            get
            {
                Button BTN_Buy = null;
                foreach (var BTN in GetComponentsInChildren<Button>())
                {
                    if (BTN.name == "BB")
                    {
                        BTN_Buy = BTN;
                    }
                }
                return BTN_Buy;
            }
        }

        RawImage Icon_entity
        {
            get
            {
                RawImage Image_entity = null;
                foreach (var Image in GetComponentsInChildren<RawImage>())
                {
                    if (Image.name == "IN")
                    {
                        Image_entity = Image;
                    }
                }
                return Image_entity;
            }
        }



        internal void Change_Value(string _id, string Key, string Name_entity, int Count, int Coin, string ID, Texture[] Icons)
        {
            Text_name_enitty.text = Name_entity;
            Text_Count.text = Count.ToString();
            Text_coin.text = Coin.ToString();


            if (Key == "F")
            {
                Icon_entity.texture = Icons[0];
                Icon_entity.color = Color.black;
            }
            else if (Key == "M")
            {
                Icon_entity.texture = Icons[1];
                Icon_entity.color = Color.black;
            }
            else if (Key == "D")
            {
                Icon_entity.texture = Icons[2];
                Icon_entity.color = Color.black;
            }
            else if (Key == "C")
            {
                Icon_entity.texture = Icons[3];
                Icon_entity.color = Color.black;
            }
            else if (Key == "R")
            {
                Icon_entity.texture = Icons[4];
                Icon_entity.color = Color.black;
            }


            BTN_buy.onClick.AddListener(() =>
            {
                if (PlayerPrefs.GetInt("Coin") - Count > 0)
                {
                    if (Key == "F")
                    {
                        PlayerPrefs.SetInt("Freeze", PlayerPrefs.GetInt("Freeze") + Count);
                    }
                    else if (Key == "M")
                    {
                        PlayerPrefs.SetInt("Mines", PlayerPrefs.GetInt("Minuse") + Count);
                    }
                    else if (Key == "D")
                    {
                        PlayerPrefs.SetInt("Delete", PlayerPrefs.GetInt("Delete") + Count);
                    }
                    else if (Key == "C")
                    {
                        PlayerPrefs.SetInt("Chance", PlayerPrefs.GetInt("Chance") + Count);
                    }
                    else if (Key == "R")
                    {
                        PlayerPrefs.SetInt("Reset", PlayerPrefs.GetInt("Reset") + Count);
                    }


                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - Count);


                    Chilligames_SDK.API_Client.Sync_coin_with_server(new Req_sync_coin_with_server { Coin = PlayerPrefs.GetInt("Coin"), _id = _id }, () => { }, err => { });

                    Chilligames_SDK.API_Client.Remove_all_offer_match(new Req_remove_offers { ID_entity = ID, Name_App = "Venomic" }, () =>
                    {

                    }, err => { });

                    Destroy(gameObject);
                }
            });

        }

    }

}





