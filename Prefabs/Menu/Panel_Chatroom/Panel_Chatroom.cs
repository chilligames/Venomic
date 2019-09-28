using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Chatroom : MonoBehaviour
{
    public GameObject Raw_model_profile;
    public GameObject Raw_model_message_Chatroom;

    public GameObject Raw_model_Category_message;
    public GameObject Raw_model_each_message;
    public GameObject Raw_model_Chat;

    public GameObject Raw_model_search;
    public TextMeshProUGUI Text_not_find;

    public GameObject Raw_model_notifactions;


    public Transform Place_messages;
    public Transform Place_massegase_chatroom;
    public Transform Place_notifaction;
    public Transform Place_search;

    public Button BTN_Chatroom;
    public Button BTN_Messages;
    public Button BTN_Notifaction;

    public Button BTN_Remove_notifactions;

    public Button BTN_Send_message;
    public TMP_InputField Input_Message;

    public TMP_InputField Input_search;

    public TMP_FontAsset Font_select;
    public TMP_FontAsset Font_deselect;

    public GameObject Content_Chatroom;
    public GameObject Content_Messages;
    public GameObject Content_Notifactions;


    GameObject Curent_content;
    Button Curent_BTN_tab;

    GameObject[] Messages_Chatroom = null;
    GameObject[] Messages = null;
    GameObject[] Notifactions = null;
    GameObject Result_search = null;
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
                    Messages[i].AddComponent<Raw_model_Category>().Change_value(result[i].Message, result[i].ID, result[i].Last_Date, result[i].Status, Raw_model_Chat, Raw_model_each_message);
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

            Chilligames_SDK.API_Client.Recive_notifactions(new Req_recive_notifactions { _id = _id_player, Name_App = "Venomic" }, Result =>
                  {
                      Notifactions = new GameObject[Result.Length];

                      for (int i = 0; i < Result.Length; i++)
                      {
                          Notifactions[i] = Instantiate(Raw_model_notifactions, Place_notifaction);
                          Notifactions[i].AddComponent<Raw_model_notifaction>().Change_value(Result[i].Body, Result[i].Title);
                      }

                  }, ERR => { });

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


        BTN_Remove_notifactions.onClick.AddListener(() =>
        {
            Chilligames_SDK.API_Client.Remove_Notifaction(new Req_remove_notifactions { Name_App = "Venomic", _id = _id_player }, () =>
            {
            }, ERRORS => { });

            for (int i = 0; i < Notifactions.Length; i++)
            {
                Destroy(Notifactions[i]);
            }
        });

        Input_search.onValueChanged.AddListener((Text_typed) =>
        {

            Chilligames_SDK.API_Client.Search_Users(new Req_search_user { Nickname = Text_typed },
            () =>
            {
                Text_not_find.gameObject.SetActive(true);

            }, result =>
            {
                string Nickname = ChilligamesJson.DeserializeObject<Chilligames_SDK.API_Client.Result_search_user.Deserilseinfo>(result.Info.ToString()).Nickname;

                string _id_other_player = result._id;
                Text_not_find.gameObject.SetActive(false);
                Result_search = Instantiate(Raw_model_search, Place_search);
                Result_search.AddComponent<Raw_model_fild_search>().Change_value(_id_player, _id_other_player, Nickname, Raw_model_profile);
            }, err => { });

        });

        Input_search.onEndEdit.AddListener(s =>
        {
            Destroy(Result_search, 0.5f);
            Input_search.text = null;
            Text_not_find.gameObject.SetActive(false);
        });

    }

    private void Update()
    {
        if (Content_Chatroom.activeInHierarchy != true && Messages_Chatroom != null)
        {
            for (int i = 0; i < Messages_Chatroom.Length; i++)
            {
                Destroy(Messages_Chatroom[i]);

            }
        }
        if (Content_Messages.activeInHierarchy != true && Messages != null)
        {
            for (int i = 0; i < Messages.Length; i++)
            {
                Destroy(Messages[i]);
            }
        }
    }

    public void OnDisable()
    {
        for (int i = 0; i < Messages_Chatroom.Length; i++)
        {
            Destroy(Messages_Chatroom[i]);

        }

        StopCoroutine(Recive_messages_in_chatroom());

        if (Content_Messages.activeInHierarchy != true && Messages != null)
        {
            for (int i = 0; i < Messages.Length; i++)
            {
                Destroy(Messages[i]);
            }
        }

    }

    public void OnEnable()
    {
        StartCoroutine(Recive_messages_in_chatroom());

        Chilligames_SDK.API_Client.Mark_all_messages_as_read(new Req_mark_messeges_as_read { _id = _id_player });
    }

    /// <summary>
    /// cheack mikone age pm jadid bashe va status messsage 0 bashe icon roshan mishe
    /// </summary>
    /// <param name="Icon_status_messege">icon status bayad dad </param>
    public void Cheack_new_message(RawImage Icon_status_messege)
    {
        Chilligames_SDK.API_Client.Cheack_status_new_message(new Req_cheack_new_message { _id = _id_player }, result =>
        {
            if (result == "1")
            {
                try
                {

                    Icon_status_messege.gameObject.SetActive(true);
                }
                catch (System.Exception)
                {

                }
            }
            else if (result == "0")
            {
                try
                {

                    Icon_status_messege.gameObject.SetActive(false);
                }
                catch (System.Exception)
                {

                }
            }
        }, err => { });

    }


    IEnumerator Recive_messages_in_chatroom()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            Chilligames_SDK.API_Client.Recive_Chatroom_messages(new Req_recive_chatroom_messages { Name_App = "Venomic" }, Result =>
            {
                if (gameObject.activeInHierarchy)
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
                }
                else
                {

                    StopAllCoroutines();
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

        GameObject Raw_model_each_messege = null;

        public void Change_value(object[] messages, string ID, string Last_date, int? status, GameObject Raw_model_chat, GameObject Raw_model_each_message)
        {

            Chilligames_SDK.API_Client.Recive_Info_other_User<schema_other_player>(new Req_recive_Info_player { _id = ID }, result =>
            {
                Text_sender.text = ChilligamesJson.DeserializeObject<schema_other_player.deserilise_info>(result.Info.ToString()).Nickname;

            }, err => { });

            
            Text_Last_messege.text =ChilligamesJson.DeserializeObject<Chilligames_SDK.API_Client.Result_each_messege>( messages[messages.Length - 1].ToString()).PM;

            Text_Last_date.text = Last_date;

            Raw_model_each_messege = Raw_model_each_message;

            BTN_Enter_chatroom.onClick.AddListener(() =>
            {

                Curent_panel_chat = Instantiate(Raw_model_chat);
                Curent_panel_chat.AddComponent<Raw_model_chat>().Change_values(ID, Raw_model_each_messege, Text_sender.text);

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
            GameObject Raw_model_each_message;

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
                        if (Texts.name == "TTN")
                        {
                            Text_nick = Texts;
                        }
                    }
                    return Text_nick;
                }
            }

            Transform Place_each_message
            {
                get
                {
                    Transform Place_messeges = null;
                    foreach (var Places in GetComponentsInChildren<Transform>())
                    {
                        if (Places.name == "PM")
                        {
                            Place_messeges = Places;
                        }
                    }
                    return Place_messeges;
                }

            }

            TMP_InputField InputField_messages
            {
                get
                {
                    TMP_InputField input_messeges = null;

                    foreach (var Inputs in GetComponentsInChildren<TMP_InputField>())
                    {
                        if (Inputs.name == "IFM")
                        {
                            input_messeges = Inputs;
                        }
                    }
                    return input_messeges;
                }
            }

            GameObject[] Content_Raw_model_each_message;

            string id_player
            {

                get
                {
                    return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
                }
            }
            string _id_other_player = null;



            public void Change_values(string _id_other_player, GameObject raw_model_each_message, string Nickname)
            {
                this._id_other_player = _id_other_player;

                Raw_model_each_message = raw_model_each_message;

                Text_Nickname.text = Nickname;

                BTN_Close.onClick.AddListener(() =>
                {
                    Destroy(gameObject);
                });

                BTN_Send_message.onClick.AddListener(() =>
                {
                    Chilligames_SDK.API_Client.Send_messege_to_player(new Req_send_message { Message_body = InputField_messages.text, _id = id_player, _id_other_users = _id_other_player }, () => { }, err => { });
                    InputField_messages.text = "";
                });

                StartCoroutine(Recive_messages_each_user());
            }


            IEnumerator Recive_messages_each_user()
            {
                while (true)
                {
                    yield return new WaitForSeconds(1f);
                    Chilligames_SDK.API_Client.Recive_messege_each_user(new Req_recive_message_each_user { _id = id_player, _id_other_player = _id_other_player }, result =>
                          {


                              if (Content_Raw_model_each_message != null)
                              {
                                  if (result.Length != Content_Raw_model_each_message.Length)
                                  {
                                      Place_each_message.position = new Vector3(Place_each_message.position.x, Place_each_message.position.y + 3000, 0);
                                  }

                                  for (int i = 0; i < Content_Raw_model_each_message.Length; i++)
                                  {
                                      Destroy(Content_Raw_model_each_message[i]);
                                  }
                              }
                              else
                              {
                                  Vector3 frist_location = new Vector3(Place_each_message.position.x, Place_each_message.position.y + result.Length * 2000, 0);
                                  Place_each_message.position = frist_location;
                              }



                              Content_Raw_model_each_message = new GameObject[result.Length];

                              for (int i = 0; i < result.Length; i++)
                              {
                                  Content_Raw_model_each_message[i] = Instantiate(Raw_model_each_message, Place_each_message);
                                  Content_Raw_model_each_message[i].AddComponent<Each_message>().Change_value(result[i].PM, result[i].ID, result[i].Time);
                              }



                          }, err => { });
                }
            }


            class Each_message : MonoBehaviour
            {
                TextMeshProUGUI Text_PM
                {
                    get
                    {
                        TextMeshProUGUI Text_PM = null;
                        foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                        {
                            if (Texts.name == "TPM")
                            {
                                Text_PM = Texts;
                            }
                        }
                        return Text_PM;
                    }
                }

                TextMeshProUGUI Text_Sender
                {
                    get
                    {
                        TextMeshProUGUI Text_sender = null;
                        foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                        {
                            if (Texts.name == "TNN")
                            {
                                Text_sender = Texts;
                            }

                        }
                        return Text_sender;
                    }
                }

                TextMeshProUGUI Text_Time
                {

                    get
                    {
                        TextMeshProUGUI Text_Time = null;
                        foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                        {
                            if (Texts.name == "TTNS")
                            {
                                Text_Time = Texts;
                            }

                        }
                        return Text_Time;
                    }
                }

                string _id
                {
                    get
                    {
                        return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
                    }
                }

                public void Change_value(string PM, string sender, string Time)
                {
                    Text_PM.text = PM;
                    Text_Sender.text = sender;
                    Text_Time.text = Time;

                    if (sender == _id)
                    {
                        Text_Sender.text = "You";
                    }
                    else
                    {
                        Text_Sender.text = "Other";
                    }

                }
            }
        }
    }


    class Raw_model_notifaction : MonoBehaviour
    {

        TextMeshProUGUI Text_Title
        {
            get
            {
                TextMeshProUGUI Text_title = null;

                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "TTM")
                    {
                        Text_title = Texts;
                    }

                }

                return Text_title;
            }
        }
        TextMeshProUGUI Text_messege_body
        {
            get
            {
                TextMeshProUGUI Text_message_body = null;
                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "TMB")
                    {
                        Text_message_body = Texts;
                    }
                }
                return Text_message_body;
            }
        }


        public void Change_value(string Messege_body, string Text_title)
        {
            Text_Title.text = Text_title;
            Text_messege_body.text = Messege_body;
        }

    }


    class Raw_model_fild_search : MonoBehaviour
    {
        Button BTN_info
        {
            get
            {
                return GetComponent<Button>();
            }
        }

        TextMeshProUGUI Text_nick_name
        {
            get
            {
                TextMeshProUGUI Text_Nickname = null;
                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "TNN")
                    {
                        Text_Nickname = Texts;
                    }

                }
                return Text_Nickname;

            }
        }

        public void Change_value(string _id, string _id_other_player, string Nick_name, GameObject Raw_model_profile)
        {
            Text_nick_name.text = Nick_name;
            BTN_info.onClick.AddListener(() =>
            {
                Instantiate(Raw_model_profile).GetComponent<Raw_model_user_profile>().Change_value(_id, _id_other_player);

            });
        }

    }
}

