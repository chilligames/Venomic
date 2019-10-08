using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// playerprefe:
/// 1: Freeze
/// 2: Minuse
/// 3: Delete
/// 4: Chance
/// 5: Reset
/// 6: Coin
/// 7: Level
/// 8: _id
/// </summary>
/// 

public class Panel_profile : MonoBehaviour
{
    public GameObject Raw_model_friend;
    public GameObject Raw_model_profile;

    public TMP_InputField InputField_nickname;
    public TMP_InputField InputField_Username;
    public TMP_InputField InputField_Email;
    public TMP_InputField InputField_Password;
    public TMP_InputField InputFieldSatatus;


    public Color Color_edit;
    public Color Color_error;
    public Color Color_Pass;


    public Button BTN_Change;
    public Button BTN_Editprofile;
    public Button BTN_Friend_list;
    public Button BTN_Login;

    public GameObject Content_panel_editprofile;
    public GameObject Content_panel_firend;
    public GameObject Content_panel_login;

    public Transform Place_friend_list;

    GameObject Curent_panel;
    Button Curent_BTN_tap;

    GameObject[] Friend_list;

    string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    void Start()
    {
        Curent_panel = Content_panel_editprofile;
        Curent_BTN_tap = BTN_Editprofile;

        BTN_Editprofile.onClick.AddListener(() =>
        {

            Curent_panel.SetActive(false);
            Curent_panel = Content_panel_editprofile;
            Curent_panel.SetActive(true);

            Curent_BTN_tap.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            Curent_BTN_tap = BTN_Editprofile;

            BTN_Editprofile.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        });

        BTN_Friend_list.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_panel_firend;
            Curent_panel.SetActive(true);

            Curent_BTN_tap.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            Curent_BTN_tap = BTN_Friend_list;

            BTN_Friend_list.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;

            Chilligames_SDK.API_Client.Recive_list_friend(new Req_recive_list_friend { _id = _id }, result =>
            {

                Friend_list = new GameObject[result.Friends.Length];
                for (int i = 0; i < result.Friends.Length; i++)
                {
                    Friend_list[i] = Instantiate(Raw_model_friend, Place_friend_list);
                    Friend_list[i].AddComponent<Raw_model_Friend>().Change_value(Raw_model_profile, _id, ChilligamesJson.DeserializeObject<Chilligames_SDK.API_Client.Result_list_freind.Deserilse_friend>(result.Friends[i].ToString()).ID, (int)ChilligamesJson.DeserializeObject<Chilligames_SDK.API_Client.Result_list_freind.Deserilse_friend>(result.Friends[i].ToString()).Status);
                }

            }, ERRORS => { });

        });

        BTN_Login.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_panel_login;
            Curent_panel.SetActive(true);

            Curent_BTN_tap.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            Curent_BTN_tap = BTN_Login;

            BTN_Login.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        });


        InputField_nickname.onSelect.AddListener(text =>
        {
            InputField_nickname.GetComponent<Image>().color = Color_edit;
        });
        InputField_nickname.onDeselect.AddListener(Text =>
        {

            if (InputField_nickname.text == "")
            {
                InputField_nickname.GetComponent<Image>().color = Color_Pass;
            }
            else if (InputField_nickname.text.Length >= 5)
            {
                Chilligames_SDK.API_Client.Cheack_nick_name(new Req_cheack_nickname { Nickname = InputField_nickname.text }, result =>
                {
                    if (result == "1")
                    {
                        InputField_nickname.GetComponent<Image>().color = Color_Pass;
                    }
                    else if (result == "0")
                    {
                        InputField_nickname.GetComponent<Image>().color = Color_error;
                    }
                }, err => { });

            }
            else if (InputField_nickname.text.Length < 5)
            {
                InputField_nickname.GetComponent<Image>().color = Color_error;
            }


        });


        InputField_Username.onSelect.AddListener(Text =>
        {

            InputField_Username.GetComponent<Image>().color = Color_edit;
        });
        InputField_Username.onDeselect.AddListener(Text =>
        {
            if (InputField_Username.text == "")
            {
                InputField_Username.GetComponent<Image>().color = Color_Pass;
            }
            else if (InputField_Username.text.Length >= 5)
            {
                Chilligames_SDK.API_Client.Cheack_user_name(new Req_cheack_username { Username = Text }, result =>
                {
                    if (result == "1")
                    {
                        InputField_Username.GetComponent<Image>().color = Color_Pass;
                    }
                    else if (result == "0")
                    {
                        InputField_Username.GetComponent<Image>().color = Color_error;
                    }

                }, ERR => { });
            }
            else if (InputField_Username.text.Length < 5)
            {
                InputField_Username.GetComponent<Image>().color = Color_error;
            }

        });


        InputField_Email.onSelect.AddListener(Text =>
        {
            InputField_Email.GetComponent<Image>().color = Color_edit;
        });
        InputField_Email.onDeselect.AddListener(Text =>
        {
            if (InputField_Email.text == "")
            {
                InputField_Email.GetComponent<Image>().color = Color_Pass;
            }

            if (InputField_Email.text.Length >= 1 && InputField_Email.text.Length < 5)
            {
                InputField_Email.GetComponent<Image>().color = Color_error;
            }
            else
            {
                InputField_Email.GetComponent<Image>().color = Color_Pass;
            }

            int find = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '@')
                {
                    find = 1;
                }
            }

            if (find == 1)
            {
                InputField_Email.GetComponent<Image>().color = Color_Pass;
            }
            else if (find == 0 && Text.Length > 0)
            {
                InputField_Email.GetComponent<Image>().color = Color_error;
            }
        });


        InputField_Password.onSelect.AddListener(text =>
        {
            InputField_Password.GetComponent<Image>().color = Color_edit;
        });
        InputField_Password.onDeselect.AddListener(text =>
        {
            if (InputField_Password.text == "")
            {
                InputField_Password.GetComponent<Image>().color = Color_Pass;
            }
            else if (InputField_Password.text.Length <= 5)
            {
                InputField_Password.GetComponent<Image>().color = Color_error;
            }
            else if (InputField_Password.text.Length > 5)
            {
                InputField_Password.GetComponent<Image>().color = Color_Pass;
            }
        });


        InputFieldSatatus.onSelect.AddListener(text =>
        {
            InputFieldSatatus.GetComponent<Image>().color = Color_edit;
        });
        InputFieldSatatus.onDeselect.AddListener(Text =>
        {
            InputFieldSatatus.GetComponent<Image>().color = Color_Pass;
        });


        BTN_Change.onClick.AddListener(() =>
        {
            Chilligames_SDK.API_Client.Update_User_Info(new Req_Update_User_Info { Nickname = InputField_nickname.text, Username = InputField_Username.text, Email = InputField_Email.text, Password = InputField_Password.text, status = InputFieldSatatus.text, _id = _id }, () =>
            {
                SceneManager.LoadScene(0);
            }, err => { });
        });


    }

    private void OnDisable()
    {

    }

    class Raw_model_Friend : MonoBehaviour
    {
        Button BTN_profile
        {
            get
            {
                return GetComponent<Button>();
            }
        }

        Button BTN_accept
        {
            get
            {
                Button BTN_accept = null;
                foreach (var BTNs in GetComponentsInChildren<Button>())
                {
                    if (BTNs.name == "BAF")
                    {
                        BTN_accept = BTNs;
                    }
                }

                return BTN_accept;
            }
        }

        Button BTN_reject
        {
            get
            {
                Button BTN_reject = null;
                foreach (var BTNS in GetComponentsInChildren<Button>())
                {
                    if (BTNS.name == "BRF")
                    {
                        BTN_reject = BTNS;
                    }
                }
                return BTN_reject;
            }
        }
        TextMeshProUGUI Text_Nickname
        {
            get
            {

                return GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        public void Change_value(GameObject Raw_model_profile, string _id, string _id_other_player, int Status_friend)
        {
            Chilligames_SDK.API_Client.Recive_info_user(new Req_recive_Info_player { _id = _id_other_player }, resul =>
            {
                Text_Nickname.text = resul.Nickname;

            }, err => { });

            BTN_reject.onClick.AddListener(() =>
            {
                Destroy(gameObject);
                Chilligames_SDK.API_Client.Cancel_and_dellet_friend_requst(new req_cancel_and_dellet_send_freiend { _id = _id, _id_other_users = _id_other_player }, () => { }, err => { });
            });
            BTN_accept.onClick.AddListener(() =>
            {
                Chilligames_SDK.API_Client.Accept_friend_req(new Req_accept_friend_req { _id = _id, _id_other_player = _id_other_player }, () => { }, err => { });

                BTN_reject.gameObject.SetActive(true);
                BTN_accept.gameObject.SetActive(false);
            });


            if (Status_friend == 0)
            {
                BTN_accept.gameObject.SetActive(false);
            }
            if (Status_friend == 1)
            {
                BTN_reject.gameObject.SetActive(true);
                BTN_accept.gameObject.SetActive(true);
            }
            if (Status_friend == 2)
            {
                BTN_accept.gameObject.SetActive(false);
            }


            BTN_profile.onClick.AddListener(() =>
            {
                Instantiate(Raw_model_profile).GetComponent<Raw_model_user_profile>().Change_value(_id, _id_other_player);

            });

        }


    }


}
