using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BazaarPlugin;
using UnityEngine.UI;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using Chilligames.Json;
using TMPro;

public class Panel_coin_shop : MonoBehaviour
{
    public GameObject Raw_model_message_notifaction;

    public Button BTN_buy_800;
    public Button BTN_buy_1600;
    public Button BTN_buy_38000;
    public Button BTN_buy_72000;
    public Button BTN_buy_120000;
    public Button BTN_buy_205000;
    public Button BTN_buy_233000;
    public Button BTN_buy_280000;

    public Button BTN_close;

    private string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    TextMeshProUGUI Text_coin_panel_shop;


    /// <summary>
    /// change value for update after buy coin
    /// </summary>
    /// <param name="text"></param>
    public void Update_entity_coin(TextMeshProUGUI text)
    {
        Text_coin_panel_shop = text;
    }



    void Start()
    {
        BazaarIAB.init("MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBrwCsKayLopsdltsho45vsaVhWeamm89xS62xwub2QU8DF9AOndmaLr3yP+lP53tdwNc5V4wVEyb6/EIZWMEZdWAYH2oNOhLNkK2MBSaQ0fHWnXnVTnAoJUnzVJxzCVPOpXAtOK5SVZiDMjlx3q16eYZe6y1ams6+mIcTDpCogHBeQlKT3jWzhTIdyGsz+d7MhwYa5rNU/CzRN09L70XNWctFdF0VCXZkCCFhIgszExUCAwEAAQ==");

        BTN_close.onClick.AddListener(() =>
        {
            BazaarIAB.unbindService();
            Destroy(gameObject);
        });


        BTN_buy_800.onClick.AddListener(() =>
        {
            BazaarIAB.purchaseProduct("CH-V-C");
        });

        BTN_buy_1600.onClick.AddListener(() =>
        {
            BazaarIAB.purchaseProduct("CH-V-C-1");
        });

        BTN_buy_38000.onClick.AddListener(() =>
        {
            BazaarIAB.purchaseProduct("CH-V-C-2");
        });
        BTN_buy_72000.onClick.AddListener(() =>
        {
            BazaarIAB.purchaseProduct("72000");
        });
        BTN_buy_120000.onClick.AddListener(() =>
        {
            BazaarIAB.purchaseProduct("120000");
        });
        BTN_buy_205000.onClick.AddListener(() =>
        {
            BazaarIAB.purchaseProduct("205000");
        });
        BTN_buy_233000.onClick.AddListener(() =>
        {
            BazaarIAB.purchaseProduct("23300");
        });
        BTN_buy_280000.onClick.AddListener(() =>
        {
            BazaarIAB.purchaseProduct("280000");
        });


        //event manager bazaar

        IABEventManager.purchaseFailedEvent += Event_faild;
        IABEventManager.purchaseSucceededEvent += Even_success;

        void Event_faild(string faild)
        {
            Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment failed", $"{faild}", Raw_model_massage_notifaction.Type.ERRoR, Raw_model_massage_notifaction.Theme.Night);
        }


        void Even_success(BazaarPurchase success)
        {
            //control product 
            switch (success.ProductId)
            {
                case "CH-V-C":
                    {
                        inject_coin_to_server(75);
                        BazaarIAB.consumeProduct("CH-V-C");
                        Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "75 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                    }
                    break;
                case "CH-V-C-1":
                    {
                        inject_coin_to_server(150);
                        BazaarIAB.consumeProduct("CH-V-C-1");
                        Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "800 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                    }
                    break;
                case "CH-V-C-2":
                    {
                        inject_coin_to_server(350);
                        BazaarIAB.consumeProduct("CH-V-C-2");
                        Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "350 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                    }
                    break;
                case "72000":
                    {
                        inject_coin_to_server(1500);
                        BazaarIAB.consumeProduct("72000");
                        Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "1500 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                    }
                    break;
                case "120000":
                    {
                        inject_coin_to_server(1550);
                        BazaarIAB.consumeProduct("120000");
                        Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "1550 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                    }
                    break;
                case "205000":
                    {
                        inject_coin_to_server(2700);
                        BazaarIAB.consumeProduct("205000");
                        Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "2700 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                    }
                    break;
                case "23300":
                    {
                        inject_coin_to_server(6000);
                        BazaarIAB.consumeProduct("23300");
                        Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "6000 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                    }
                    break;
                case "280000":
                    {
                        inject_coin_to_server(10000);
                        BazaarIAB.consumeProduct("280000");
                        Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "10000 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                    }
                    break;

            }

            //update coin panel shop
            Text_coin_panel_shop.text = PlayerPrefs.GetInt("Coin").ToString();

            //add history to server
            Data_buy data_purchase = new Data_buy
            {
                developerid = success.DeveloperPayload,
                Orderid = success.OrderId,
                orginal_Json = success.OriginalJson,
                Purchasses_pakage = success.PackageName,
                Purchasses_productid = success.ProductId,
                Purchasses_state = success.PurchaseState.ToString(),
                Purchasses_time = success.PurchaseTime.ToString(),
                Purchasses_token = success.PurchaseToken,
                Signuture = success.Signature,
                Type = success.Type,
            };

            Chilligames_SDK.API_Client.Add_purchasses_history(new Req_add_purchasses_history { _id = _id, data_purchass = ChilligamesJson.SerializeObject(data_purchase) }, () => { }, err => { });
        }

    }


    void inject_coin_to_server(int count_coin)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + count_coin);
        Chilligames_SDK.API_Client.Coin_Insert(new Req_Insert_coin { _id = _id, Coin = count_coin }, () => { }, err => { });
    }


    public class Data_buy
    {
        public string Type = null;
        public string Signuture = null;
        public string Purchasses_token = null;
        public string Purchasses_time = null;
        public string Purchasses_state = null;
        public string Purchasses_productid = null;
        public string Purchasses_pakage = null;
        public string orginal_Json = null;
        public string Orderid = null;
        public string developerid = null;

    }

}