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
public class Panel_edit_profile : MonoBehaviour
{
    public TMP_InputField InputField_nickname;
    public TMP_InputField InputField_Username;
    public TMP_InputField InputField_Email;
    public TMP_InputField InputField_Password;
    public TMP_InputField InputFieldSatatus;

    public TMP_InputField InputField_Username_login;
    public TMP_InputField InputField_password_login;
    public TMP_InputField InputField_Email_recovery;
    public TMP_InputField Input_field_recovery_number;
    public TMP_InputField InputField_new_password;

    public TextMeshProUGUI Text_info_recovery;

    public Color Color_edit;
    public Color Color_error;
    public Color Color_Pass;

    public GameObject Content_edit_profile;
    public GameObject Content_login;

    public Button BTN_Change;
    public Button BTN_Close;
    public Button BTN_old_user;
    public Button BTN_Back_to_profile;
    public Button BTN_login;
    public Button BTN_recovery;


    string _id
    {
        get
        {
            return GameObject.Find("Content_Home").GetComponent<Panel_home>()._id;
        }
    }

    void Start()
    {

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

        BTN_Close.onClick.AddListener(() =>
        {

            Destroy(gameObject);
        });

        BTN_old_user.onClick.AddListener(() =>
        {
            Content_edit_profile.SetActive(false);
            Content_login.SetActive(true);
            BTN_old_user.gameObject.SetActive(false);
        });

        BTN_Back_to_profile.onClick.AddListener(() =>
        {
            Content_edit_profile.SetActive(true);
            Content_login.SetActive(false);
            BTN_old_user.gameObject.SetActive(true);

        });

        BTN_login.onClick.AddListener(() =>
        {
            Chilligames_SDK.API_Client.Login_with_username_Password(new Req_login_with_username_password { Username = InputField_Username_login.text, Password = InputField_password_login.text }, result =>
            {
                print("result");
                if (result != "0")
                {
                    print("Login");
                    PlayerPrefs.SetString("_id", result);
                    Chilligames_SDK.API_Client.Recive_Data_user<Panel_home.Entity_Player>(new Req_recive_data { Name_App = "Venomic", _id = result }, Data_user =>
                    {
                        PlayerPrefs.SetInt("Freeze", Data_user.Freeze);
                        PlayerPrefs.SetInt("Minuse", Data_user.Minus);
                        PlayerPrefs.SetInt("Delete", Data_user.Delete);
                        PlayerPrefs.SetInt("Chance", Data_user.Chance);
                        PlayerPrefs.SetInt("Reset", Data_user.Reset);
                        PlayerPrefs.SetInt("Level", Data_user.Level);
                        SceneManager.LoadScene(0);
                    }, err => { });
                }
                else
                {
                    print("cant login");
                }
            }, err => { });

        });

        BTN_recovery.onClick.AddListener(() =>
        {

            InputField_Email_recovery.gameObject.SetActive(true);

            BTN_recovery.onClick.AddListener(() =>
            {
                InputField_Email_recovery.gameObject.SetActive(false);

                Chilligames_SDK.API_Client.Recovery_email_send(new Req_send_recovery_email { Email = InputField_Email_recovery.text }, result =>
                {
                    if (result == "1")
                    {
                        BTN_recovery.onClick.RemoveAllListeners();

                        Input_field_recovery_number.gameObject.SetActive(true);
                        Text_info_recovery.gameObject.SetActive(true);

                        BTN_recovery.onClick.AddListener(() =>
                        {
                            Chilligames_SDK.API_Client.Submit_recovery_email(new Req_submit_recovery_email { Key = Input_field_recovery_number.text, Email = InputField_Email_recovery.text }, result_submit =>
                            {
                                if (result_submit == "1")
                                {
                                    BTN_recovery.onClick.RemoveAllListeners();
                                    InputField_new_password.gameObject.SetActive(true);

                                    BTN_recovery.onClick.AddListener(() =>
                                    {
                                        Chilligames_SDK.API_Client.Change_password(new Req_change_password { Email = InputField_Email_recovery.text, New_Password = InputField_new_password.text }, () =>
                                              {
                                                  SceneManager.LoadScene(0);
                                              }, err => { });
                                    });

                                }
                                else if (result_submit == "0")
                                {
                                    print("Code not math here");
                                }

                            }, err => { });

                        });
                    }
                    else
                    {
                        InputField_Email_recovery.gameObject.SetActive(true);
                        print("code_not recovery here all pipe recovery normal");
                    }

                }, err => { });

            });


        });

    }



}
