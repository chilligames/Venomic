using UnityEngine;
using System.Threading;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using UnityEngine.UI;
public class Panel_shop : MonoBehaviour
{
    public GameObject Raw_model_convert_panel;
    public GameObject Raw_model_fild_Offer;
    public Texture[] Icons_offers;

    public TextMeshProUGUI Text_Coin_number;
    public TextMeshProUGUI Text_Mony_number;

    public Transform Place_offers;

    public Button BTN_Enter_convert;

    GameObject[] Offers = null;
    public string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }


    private void OnDisable()
    {
    }
    public void OnEnable()
    {
        Recive_entity_wallet();

        Chilligames_SDK.API_Client.Recive_offers(new Req_recive_offers { Name_App = "Venomic", _id = _id }, result =>
        {
            Offers = new GameObject[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                Offers[i] = Instantiate(Raw_model_fild_Offer, Place_offers);
                Offers[i].AddComponent<Raw_model_fild_offer>().Change_Value(_id, result[i].Key, result[i].Name_Entity, (int)result[i].Count, (int)result[i].Coin, result[i].ID, Icons_offers);
            }


        }, err => { });

        BTN_Enter_convert.onClick.AddListener(() =>
        {
            Instantiate(Raw_model_convert_panel).GetComponent<Panel_Convert_Coins>().Change_value(gameObject);
        });
    }


    public void Recive_entity_wallet()
    {
        Chilligames_SDK.API_Client.Recive_Coin_mony(new Req_recive_coin { _id = _id }, result =>
        {
            Text_Coin_number.text = result.Coin.ToString();
            Text_Mony_number.text = result.Money.ToString();

        }, err => { });
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

        Color Color_Freeze = new Color(0.64f, 0.96f, 0.96f, 1);
        Color Color_Mines = new Color(0.91f, 0, 0.26f, 1);
        Color Color_delet = new Color(0.98f, 0.92f, 0.73f, 1);
        Color Color_Chance = new Color(0.54f, 1, 0.65f);
        Color Color_reset = new Color(0.63f, 0.87f, 0.8f);

        internal void Change_Value(string _id, string Key, string Name_entity, int Count, int Coin, string ID, Texture[] Icons)
        {
            Text_name_enitty.text = Name_entity;
            Text_Count.text = Count.ToString();
            Text_coin.text = Coin.ToString();


            if (Key == "F")
            {
                Icon_entity.texture = Icons[0];
                Icon_entity.color = Color_Freeze;
            }
            else if (Key == "M")
            {
                Icon_entity.texture = Icons[1];
                Icon_entity.color = Color_Mines;
            }
            else if (Key == "D")
            {
                Icon_entity.texture = Icons[2];
                Icon_entity.color = Color_delet;
            }
            else if (Key == "C")
            {
                Icon_entity.texture = Icons[3];
                Icon_entity.color = Color_Chance;
            }
            else if (Key == "R")
            {
                Icon_entity.texture = Icons[4];
                Icon_entity.color = Color_reset;
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




