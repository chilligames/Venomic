using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using TapsellSDK;

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

    public TextMeshProUGUI Text_Count_freeze_ads;
    public TextMeshProUGUI Text_count_minuse_ads;
    public TextMeshProUGUI Text_Count_delete_ads;
    public TextMeshProUGUI Text_count_chance_ads;
    public TextMeshProUGUI Text_Count_reset_ads;
    public TextMeshProUGUI Text_Count_Coin_ads;
    public TextMeshProUGUI Text_count_money_ads;


    public Button BTN_Buy;
    public Button BTN_Close_panel;
    public Button BTN_Buy_with_ads;
    public Button BTN_Buy_with_coin;

    public Button BTN_Freeze_ads;
    public Button BTN_minuse_ads;
    public Button BTN_delete_ads;
    public Button BTN_chance_ads;
    public Button BTN_reset_ads;
    public Button BTN_coin_ads;
    public Button BTN_money_ads;

    public GameObject Content_buy_with_coin;
    public GameObject Content_buy_with_ads;


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
        Tapsell.Initialize("htkkqnefselstgtrfsobqncrsplijfccjaitpqnskqqpjirtbrsbnaljqfneqdcpjbpsmb");
        var Ads = new TapsellAd();

        Tapsell.RequestAd(" 5d88d35a9a75990001f83f22 ", true, avaible =>
        {

            Ads = avaible;

        }, notavaible => { }, err => { }, nonet => { }, expri => { });


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
            int coin = ((int)Slider_freeze.value )+ ((int)Slider_Minuse.value )+( (int)Slider_delete.value )+((int) Slider_chance.value )+((int) Slider_reset.value);
            
            Send_Offer(rand_num_offer,coin);

            Instantiate(gameObject).GetComponent<Panel_shop_entity>().Change_value(Parent);
            Parent.GetComponent<Panel_shop>().Change_value_entity_shop_category();
            Destroy(gameObject);

        });

        BTN_Close_panel.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });

        BTN_Buy_with_ads.onClick.AddListener(() =>
        {
            Content_buy_with_coin.SetActive(false);
            Content_buy_with_ads.SetActive(true);
            BTN_Buy.gameObject.SetActive(false);

        });

        BTN_Buy_with_coin.onClick.AddListener(() =>
        {
            Content_buy_with_ads.SetActive(false);
            Content_buy_with_coin.SetActive(true);
            BTN_Buy.gameObject.SetActive(true);
        });


        //buy with ads

        int count_freeze = Random.Range(1, 9);
        int count_minuse = Random.Range(1, 9);
        int count_delete = Random.Range(1, 9);
        int count_chance = Random.Range(1, 9);
        int Count_reset = Random.Range(1, 9);
        int count_coin = Random.Range(10, 40);
        float count_money = Random.Range(0.1f, 0.4f);

        Text_Count_freeze_ads.text = count_freeze.ToString();
        Text_count_minuse_ads.text = count_minuse.ToString();
        Text_Count_delete_ads.text = count_delete.ToString();
        Text_count_chance_ads.text = count_chance.ToString();
        Text_Count_reset_ads.text = Count_reset.ToString();
        Text_Count_Coin_ads.text = count_coin.ToString();
        Text_count_money_ads.text = System.Math.Round(count_money, 2).ToString();

        BTN_Freeze_ads.onClick.AddListener(() =>
        {
            show_ads("Freeze", count_freeze);
        });
        BTN_minuse_ads.onClick.AddListener(() =>
        {
            show_ads("Minuse", count_minuse);
        });
        BTN_delete_ads.onClick.AddListener(() =>
        {
            show_ads("Delete", count_delete);
        });
        BTN_chance_ads.onClick.AddListener(() =>
        {
            show_ads("Chance", count_chance);
        });
        BTN_reset_ads.onClick.AddListener(() =>
        {
            show_ads("Reset", Count_reset);
        });

        void show_ads(string Entity, int count)
        {
            Tapsell.ShowAd(Ads);
            Tapsell.SetRewardListener(finish =>
            {
                if (finish.completed)
                {
                    PlayerPrefs.SetInt(Entity, PlayerPrefs.GetInt(Entity) + count);
                    Tapsell.RequestAd("5d88d35a9a75990001f83f22", true, avaible =>
                    {
                        Ads = avaible;
                    }, noavaible => { }, err => { }, nonet => { }, expire => { });
                }
            });

        }

        void Send_Offer(int Rand_Num,int coin)
        {
            switch (Rand_Num)
            {
                case 1:
                    {
                        int count_Entity = coin / 4;
                        Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user { Coin = (int)(coin / 100 * 20), Count = count_Entity, Key = "F", Name_App = "Venomic", Name_Entity = "Freeze", ID_entity = "F" + coin+count_Entity });
                    }
                    break;
                case 2:
                    {
                        int Count_Entity = coin / 2;
                        Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user { Coin = (int)(coin / 100 * 20), Count = Count_Entity, Key = "M", Name_App = "Venomic", Name_Entity = "Minuse", ID_entity = "M" + coin + Count_Entity });
                    }
                    break;
                case 3:
                    {
                        int Count_Entity = coin / 3;
                        Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user {Coin=(int)(coin/100*20),Count=Count_Entity,Key="D",Name_App="Venomic",Name_Entity="Delete" ,ID_entity="D"+coin+Count_Entity});
                    }
                    break;
                case 4:
                    {
                        int Count_Entity = coin / 2;
                        Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user {Coin=(int)(coin/100*20),Count=Count_Entity,Key="C",Name_App="Venomic",Name_Entity="Chance",ID_entity="C"+coin+Count_Entity });
                    }
                    break;
                case 5:
                    {
                        int Count_Entity = coin / 4;
                        Chilligames_SDK.API_Client.Push_Offer_to_all_player(new Req_Push_offer_to_all_user { Coin = (int)(coin / 100 * 20), Count = Count_Entity, Key = "R", Name_App = "Venomic", Name_Entity = "Reset", ID_entity = "R" + coin + Count_Entity });
                    }
                    break;
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

    }



}
