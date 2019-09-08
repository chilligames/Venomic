using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Chilligames.Json;
using Chilligames.SDK.Model_Client;
using Chilligames.SDK;
using UnityEngine.SceneManagement;

public class Panel_edit_profile : MonoBehaviour
{
    public TMP_InputField InputField_nickname;
    public TMP_InputField InputField_Username;
    public TMP_InputField InputField_Email;
    public TMP_InputField InputField_Password;
    public TMP_InputField InputFieldSatatus;

    public Color Color_edit;
    public Color Color_error;
    public Color Color_Pass;

    public Button BTN_Change;
    public Button BTN_Close;

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
    }

}
