using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;


public class Panel_Chatroom : MonoBehaviour
{
    public GameObject Raw_model_profile;
    public GameObject Raw_model_message_Chatroom;
    public GameObject Raw_model_Category_message;
    public GameObject Raw_model_Chat;

    public Transform Place_messages;
    public Transform Place_massegase_chatroom;

    public Button BTN_Chatroom;
    public Button BTN_Messages;
    public Button BTN_Notifaction;

    public Button BTN_Send_message;
    public TMP_InputField Input_Message;

    public TMP_FontAsset Font_select;
    public TMP_FontAsset Font_deselect;

    public GameObject Content_Chatroom;
    public GameObject Content_Messages;
    public GameObject Content_Notifactions;

    GameObject Curent_content;
    Button Curent_BTN_tab;

    GameObject[] Messages_Chatroom = null;
    GameObject[] Messages = null;
    public string _id_player
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    void Start()
    {
        StartCoroutine(Recive_messages_in_chatroom());

        Curent_content = Content_Chatroom;
        Curent_BTN_tab = BTN_Chatroom;

        BTN_Chatroom.onClick.AddListener(() =>
        {
            Curent_content.SetActive(false);
            Curent_content = Content_Chatroom;
            Curent_content.SetActive(true);

            Curent_BTN_tab.GetComponentInChildren<TextMeshProUGUI>().font = Font_deselect;
            Curent_BTN_tab = BTN_Chatroom;

            BTN_Chatroom.GetComponentInChildren<TextMeshProUGUI>().font = Font_select;

            StartCoroutine(Recive_messages_in_chatroom());

        });

        BTN_Messages.onClick.AddListener(() =>
        {
            Curent_content.SetActive(false);
            Curent_content = Content_Messages;
            Curent_content.SetActive(true);

            Curent_BTN_tab.GetComponentInChildren<TextMeshProUGUI>().font = Font_deselect;
            Curent_BTN_tab = BTN_Messages;
            BTN_Messages.GetComponentInChildren<TextMeshProUGUI>().font = Font_select;



            Chilligames_SDK.API_Client.Recive_message_User(new Req_recive_messages { _id = _id_player }, result =>
            {

                Messages = new GameObject[result.Length];

                for (int i = 0; i < result.Length; i++)
                {
                    Messages[i] = Instantiate(Raw_model_Category_message, Place_messages);
                    Messages[i].AddComponent<Raw_model_Category>().Change_value(result[i].Message, result[i].ID, result[i].Last_Date, result[i].Status, Raw_model_Chat);
                }

            }, ERRORS => { });

        });


        BTN_Notifaction.onClick.AddListener(() =>
        {
            Curent_content.SetActive(false);
            Curent_content = Content_Notifactions;
            Curent_content.SetActive(true);

            Curent_BTN_tab.GetComponentInChildren<TextMeshProUGUI>().font = Font_deselect;
            Curent_BTN_tab = BTN_Notifaction;
            BTN_Notifaction.GetComponentInChildren<TextMeshProUGUI>().font = Font_select;
        });


        BTN_Send_message.onClick.AddListener(() =>
        {
            if (Input_Message.text != "")
            {
                Chilligames_SDK.API_Client.Send_message_to_Chatroom(new Req_send_message_to_chatroom { _id = _id_player, Name_App = "Venomic", Message = Input_Message.text }, () =>
                {

                }, ERR => { });

                Input_Message.text = "";
            }

        });

    }

    void Update()
    {

        if (Content_Chatroom.activeInHierarchy != true)
        {
            for (int i = 0; i < Messages_Chatroom.Length; i++)
            {
                Destroy(Messages_Chatroom[i]);

            }

            StopCoroutine(Recive_messages_in_chatroom());
        }

    }



    IEnumerator Recive_messages_in_chatroom()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Chilligames_SDK.API_Client.Recive_Chatroom_messages(new Req_recive_chatroom_messages { Name_App = "Venomic" }, Result =>
            {


                if (Messages_Chatroom != null)
                {
                    for (int i = 0; i < Messages_Chatroom.Length; i++)
                    {
                        Destroy(Messages_Chatroom[i]);

                    }
                }
                Messages_Chatroom = new GameObject[Result.Length];

                for (int i = 0; i < Result.Length; i++)
                {
                    Messages_Chatroom[i] = Instantiate(Raw_model_message_Chatroom, Place_massegase_chatroom);
                    Messages_Chatroom[i].AddComponent<Raw_model_message_chatroom>().Change_value(Result[i]._id, Result[i].ID, _id_player, Result[i].Nick_Name, Result[i].Message, Result[i].Time, Result[i].Report, Raw_model_profile);
                }


            }, ERR => { });
        }
    }


    class Raw_model_message_chatroom : MonoBehaviour
    {

        TextMeshProUGUI Text_Message
        {
            get
            {
                TextMeshProUGUI finder = null;

                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    switch (Texts.name)
                    {
                        case "TM":
                            {
                                finder = Texts;
                            }
                            break;
                    }

                }
                return finder;
            }
        }

        TextMeshProUGUI Text_nickname
        {
            get
            {
                TextMeshProUGUI finder = null;

                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    switch (Texts.name)
                    {
                        case "NNP":
                            {
                                finder = Texts;
                            }
                            break;
                    }

                }

                return finder;
            }
        }

        TextMeshProUGUI Text_Time
        {
            get
            {
                TextMeshProUGUI Finder = null;

                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    switch (Texts.name)
                    {
                        case "TTN":
                            {
                                Finder = Texts;
                            }
                            break;
                    }
                }
                return Finder;
            }
        }

        Button BTN_Report
        {
            get
            {
                Button report = null;
                foreach (var BTNS in GetComponentsInChildren<Button>())
                {
                    switch (BTNS.name)
                    {
                        case "BR":
                            {
                                report = BTNS;
                            }
                            break;
                    }
                }
                return report;
            }
        }

        Button BTN_Profile
        {
            get
            {
                return GetComponent<Button>();
            }
        }

        Color Color_ban_1 = new Color(0.9f, 0.07f, 0.2f, 0.3f);
        Color Color_ban_2 = new Color(0.9f, 0.07f, 0.2f, 0.5f);
        Color Color_ban_3 = new Color(0.9f, 0.07f, 0.2f, 0.7f);
        /// <summary>
        /// change value mikone va recive mikone darayi message haro
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Message"></param>
        /// <param name="Time"></param>
        /// <param name="report"></param>
        public void Change_value(string _id_message, string _id_other_player, string _id, string Nick_Name, string Message, string Time, int? report, GameObject Raw_model_profile)
        {
            Text_nickname.text = Nick_Name;
            Text_Message.text = Message;
            Text_Time.text = Time;

            BTN_Profile.onClick.AddListener(() =>
            {
                GameObject profile = Instantiate(Raw_model_profile);
                profile.GetComponent<Raw_model_user_profile>()._id = _id;
                profile.GetComponent<Raw_model_user_profile>()._id_other_player = _id_other_player;
            });


            BTN_Report.onClick.AddListener(() =>
            {
                Chilligames_SDK.API_Client.Report_message(new Req_report_message { Name_app = "Venomic", _id_message = _id_message }, () =>
                {


                }, ERR => { });
            });

            if (report == 1)
            {
                gameObject.GetComponent<RawImage>().color = Color_ban_1;
            }
            else if (report == 2)
            {
                gameObject.GetComponent<RawImage>().color = Color_ban_2;
            }
            else if (report == 3)
            {
                gameObject.GetComponent<RawImage>().color = Color_ban_3;
            }

        }


    }


    class Raw_model_Category : MonoBehaviour
    {

        TextMeshProUGUI Text_sender
        {
            get
            {
                TextMeshProUGUI Text_nickname = null;
                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "TNN")
                    {
                        Text_nickname = Texts;
                    }
                }

                return Text_nickname;

            }
        }

        TextMeshProUGUI Text_Last_messege
        {
            get
            {
                TextMeshProUGUI Text_last_message = null;
                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "TLM")
                    {
                        Text_last_message = Texts;

                    }

                }


                return Text_last_message;
            }


        }

        TextMeshProUGUI Text_Last_date
        {
            get
            {
                TextMeshProUGUI text_date = null;


                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "TLDN")
                    {
                        text_date = Texts;
                    }
                }

                return text_date;
            }
        }

        Button BTN_Enter_chatroom
        {
            get
            {
                return GetComponent<Button>();
            }
        }

        GameObject Curent_panel_chat = null;

        public void Change_value(object[] messages, string ID, string Last_date, int? status, GameObject Raw_model_chat)
        {

            Chilligames_SDK.API_Client.Recive_Info_other_User<schema_other_player>(new Req_recive_Info_player { _id = ID }, result =>
            {

                Text_sender.text = ChilligamesJson.DeserializeObject<schema_other_player.deserilise_info>(result.Info.ToString()).Nickname;

            }, err => { });


            Text_Last_messege.text = messages[messages.Length - 1].ToString();

            Text_Last_date.text = Last_date;


            BTN_Enter_chatroom.onClick.AddListener(() =>
            {

                Curent_panel_chat = Instantiate(Raw_model_chat);
                Curent_panel_chat.AddComponent<Raw_model_chat>().Change_values();

            });

        }




        class schema_other_player
        {

            public object Info = null;
            public class deserilise_info
            {
                public string Nickname = null;

            }
        }


        class Raw_model_chat : MonoBehaviour
        {
            Button BTN_Close
            {

                get
                {
                    Button BTN_Close = null;
                    foreach (var BTNS in GetComponentsInChildren<Button>())
                    {
                        if (BTNS.name == "BCC")
                        {
                            BTN_Close = BTNS;
                        }

                    }
                    return BTN_Close;

                }
            }

            Button BTN_Send_message
            {
                get
                {
                    Button BTN_Send_message = null;
                    foreach (var BTNS in GetComponentsInChildren<Button>())
                    {
                        if (BTNS.name == "BSM")
                        {
                            BTN_Send_message = BTNS;
                        }
                    }
                    return BTN_Send_message;
                }
            }

            TextMeshProUGUI Text_Nickname
            {
                get
                {
                    TextMeshProUGUI Text_nick = null;
                    foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                    {
                        if (Texts.name=="TTN")
                        {
                           Text_nick = Texts;
                        }
                    }
                    return Text_nick;
                }
            }

            
            

            public void Change_values()
            {
                BTN_Close.onClick.AddListener(() =>
                {
                    Destroy(gameObject);
                });


            }

            private void Update()
            {


            }



        }

    }

}
