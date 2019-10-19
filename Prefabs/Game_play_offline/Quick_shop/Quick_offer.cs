using BazaarPlugin;
using Chilligames.Json;
using Chilligames.SDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// playerpref
/// 1: Freeze
/// 2: Minuse
/// 3: Delete
/// 4: Chance
/// 5: Reset
/// 6:Coin
/// </summary>
public class Quick_offer : Panel_coin_shop
{
    public GameObject Step_1;
    public GameObject Step_2;
    public GameObject Step_3;

    //step_1
    public Button BTN_reject;
    public Button BTN_accept;

    //step_2
    public Button BTN_close_step_2;
    public Button BTN_pluse_freeze;
    public Button BTN_pluse_minuse;
    public Button BTN_pluse_delete;
    public Button BTN_pluse_chance;
    public Button BTN_pluse_reset;


    int anim = 0;

    string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }
    private void Start()
    {
        //step1
        StartCoroutine(Destroy_offer());
        BTN_reject.onClick.AddListener(() =>
        {
            anim = 1;

        });

        BTN_accept.onClick.AddListener(() =>
        {
            anim = 2;
            StopAllCoroutines();
        });


        //step_2
        BTN_close_step_2.onClick.AddListener(() =>
        {
            anim = 3;

        });

        BTN_pluse_freeze.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Coin") >= 4)
            {
                PlayerPrefs.SetInt("Freeze", PlayerPrefs.GetInt("Freeze") + 1);
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 4);
            }
            else
            {
                anim = 4;
            }

        });
        BTN_pluse_minuse.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Coin") >= 2)
            {
                PlayerPrefs.SetInt("Minuse", PlayerPrefs.GetInt("Minuse") + 1);
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 2);
            }
            else
            {
                anim = 4;
            }
        });
        BTN_pluse_delete.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Coin") >= 3)
            {
                PlayerPrefs.SetInt("Delete", PlayerPrefs.GetInt("Delete") + 1);
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 3);
            }
            else
            {
                anim = 4;
            }
        });
        BTN_pluse_chance.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Coin") >= 2)
            {
                PlayerPrefs.SetInt("Chance", PlayerPrefs.GetInt("Chance") + 1);
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 2);
            }
            else
            {
                anim = 4;
            }
        });
        BTN_pluse_reset.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Coin") >= 4)
            {
                PlayerPrefs.SetInt("Reset", PlayerPrefs.GetInt("Reset") + 1);
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - 4);
            }
            else
            {
                anim = 4;
            }
        });


        //step_3

        BazaarIAB.init("MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBrwCsKayLopsdltsho45vsaVhWeamm89xS62xwub2QU8DF9AOndmaLr3yP+lP53tdwNc5V4wVEyb6/EIZWMEZdWAYH2oNOhLNkK2MBSaQ0fHWnXnVTnAoJUnzVJxzCVPOpXAtOK5SVZiDMjlx3q16eYZe6y1ams6+mIcTDpCogHBeQlKT3jWzhTIdyGsz+d7MhwYa5rNU/CzRN09L70XNWctFdF0VCXZkCCFhIgszExUCAwEAAQ==");
        BTN_close.onClick.AddListener(() =>
        {
            anim = 5;
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


        //bazaar event
        IABEventManager.purchaseSucceededEvent +=
            Success =>
            {
                switch (Success.ProductId)
                {
                    case "CH-V-C":
                        {
                            inject_coin(75);
                            BazaarIAB.consumeProduct("CH-V-C");
                            Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "75 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                        }
                        break;
                    case "CH-V-C-1":
                        {
                            inject_coin(150);
                            BazaarIAB.consumeProduct("CH-V-C-1");
                            Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "150 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                        }
                        break;
                    case "CH-V-C-2":
                        {
                            inject_coin(350);
                            BazaarIAB.consumeProduct("CH-V-C-2");
                            Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "350 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                        }
                        break;
                    case "72000":
                        {
                            inject_coin(1500);
                            BazaarIAB.consumeProduct("72000");
                            Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "1500 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                        }
                        break;
                    case "120000":
                        {
                            inject_coin(1550);
                            BazaarIAB.consumeProduct("120000");
                            Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "1550 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                        }
                        break;
                    case "205000":
                        {
                            inject_coin(2700);
                            BazaarIAB.consumeProduct("205000");
                            Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "2700 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                        }
                        break;
                    case "23300":
                        {
                            inject_coin(6000);
                            BazaarIAB.consumeProduct("23300");

                            Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "6000coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                        }
                        break;
                    case "280000":
                        {
                            inject_coin(10000);
                            BazaarIAB.consumeProduct("280000");
                            Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment Successful", "10000 coins have been added to your account\n" + "Thank you for your purchase", Raw_model_massage_notifaction.Type.Success, Raw_model_massage_notifaction.Theme.Night);
                        }
                        break;


                }

                Data_buy data_Buy = new Data_buy
                {
                    developerid = Success.DeveloperPayload,
                    Orderid = Success.OrderId,
                    orginal_Json = Success.OriginalJson,
                    Purchasses_pakage = Success.PackageName,
                    Purchasses_productid = Success.ProductId,
                    Purchasses_state = Success.PurchaseState.ToString(),
                    Purchasses_time = Success.PurchaseTime.ToString(),
                    Purchasses_token = Success.PurchaseToken,
                    Signuture = Success.Signature,
                    Type = Success.Type

                };

                Chilligames_SDK.API_Client.Add_purchasses_history(new Chilligames.SDK.Model_Client.Req_add_purchasses_history { data_purchass = ChilligamesJson.SerializeObject(data_Buy), _id = _id, Name_app = "Venomic" }, () => { }, err => { }); ;
            };

        IABEventManager.purchaseFailedEvent +=
            faild =>
            {
                Instantiate(Raw_model_message_notifaction).GetComponent<Raw_model_massage_notifaction>().Change_value("Payment failed", $"{faild}", Raw_model_massage_notifaction.Type.ERRoR, Raw_model_massage_notifaction.Theme.Night);
            };


        //inject to server and game
        void inject_coin(int Coin_count)
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + Coin_count);

            Chilligames_SDK.API_Client.Coin_Insert(new Chilligames.SDK.Model_Client.Req_Insert_coin { }, () => { }, err => { });
        }

    }


    void Update()
    {
        //anim control

        if (anim == 0)
        {
            Step_1.transform.localPosition = Vector3.MoveTowards(Step_1.transform.localPosition, new Vector3(0, -2.5f, 0), 0.5f);
        }
        else if (anim == 1)
        {
            Step_1.transform.localPosition = Vector3.MoveTowards(Step_1.transform.localPosition, new Vector3(0, -10, 0), 0.5f);
            if (Step_1.transform.localPosition == new Vector3(0, -10, 0))
            {
                Destroy(gameObject);
            }
        }
        else if (anim == 2)
        {
            Step_1.transform.localPosition = Vector3.MoveTowards(Step_1.transform.localPosition, new Vector3(0, -10f, 0), 0.5f);
            Step_2.transform.localPosition = Vector3.MoveTowards(Step_2.transform.localPosition, new Vector3(0, -4.7f, 0), 0.5f);
        }
        else if (anim == 3)
        {
            //close step2
            Step_3.transform.localPosition = Vector3.MoveTowards(Step_3.transform.localPosition, new Vector3(0, 10, 0), 0.5f);
            Step_2.transform.localPosition = Vector3.MoveTowards(Step_2.transform.localPosition, new Vector3(0, -10, 0), 0.5f);
            if (Step_2.transform.localPosition == new Vector3(0, -10, 0))
            {
                Destroy(gameObject);
            }
        }
        else if (anim == 4)
        {
            Step_3.transform.localPosition = Vector3.MoveTowards(Step_3.transform.localPosition, new Vector3(0, 3, 0), 0.5f);
        }
        else if (anim == 5)
        {
            //close step3
            Step_3.transform.localPosition = Vector3.MoveTowards(Step_3.transform.localPosition, new Vector3(0, 10, 0), 0.5f);
        }

    }


    IEnumerator Destroy_offer()
    {
        yield return new WaitForSeconds(5);
        anim = 1;
    }
}
