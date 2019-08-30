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
            Chilligames_SDK.API_Client.Send_message_to_Chatroom(new Req_send_message_to_chatroom { _id = _id_player, Name_App = "Venomic", Message = Input_Message.text }, () =>
            {
                print("send_message");

            }, ERR => { });

        });

    }

    void Update()
    {


    }


    IEnumerator Recive_messages_in_chatroom()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Chilligames_SDK.API_Client.Recive_Chatroom_messages(new Req_recive_chatroom_messages { Name_App = "Venomic" }, Result =>
            {

                if (Messages != null)
                {
                    for (int i = 0; i < Messages.Length; i++)
                    {
                        Destroy(Messages[i]);

                    }

                    Messages = new GameObject[Result.Length];
                    for (int i = 0; i < Result.Length; i++)
                    {
                        Messages[i] = Instantiate(Raw_model_message_Chatroom, Place_massegase_chatroom);
                        Messages[i].AddComponent<Raw_model_message_chatroom>().Change_value(Result[i]._id, Result[i].ID, _id_player, Result[i].Nick_Name, Result[i].Message, Result[i].Time, Result[i].Report, Raw_model_profile);
                    }

                }
                else
                {
                    Messages = new GameObject[Result.Length];
                    for (int i = 0; i < Result.Length; i++)
                    {
                        Messages[i] = Instantiate(Raw_model_message_Chatroom, Place_massegase_chatroom);
                        Messages[i].AddComponent<Raw_model_message_chatroom>().Change_value(Result[i]._id, Result[i].ID, _id_player, Result[i].Nick_Name, Result[i].Message, Result[i].Time, Result[i].Report, Raw_model_profile);
                    }

                }


            }, ERR => { });
        }
    }


    class Raw_model_message_chatroom : MonoBehaviour
    {

        public TextMeshProUGUI Text_Message
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

        public TextMeshProUGUI Text_nickname
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

        public TextMeshProUGUI Text_Time
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

        public Button BTN_Report
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

        public Button BTN_Profile
        {
            get
            {
                return GetComponent<Button>();
            }
        }


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
                print("ban");
                gameObject.GetComponent<RawImage>().color = Color.red;
            }


        }


    }
}
