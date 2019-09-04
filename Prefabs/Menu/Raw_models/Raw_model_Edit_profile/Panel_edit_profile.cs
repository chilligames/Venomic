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

    string _id
    {
        get
        {
            return GameObject.Find("Content_Home").GetComponent<Panel_home>()._id;
        }
    }

    void Start()
    {

        InputField_nickname.onSelect.AddListener((text) =>
        {
            InputField_nickname.GetComponent<Image>().color = Color_edit;
        });
        InputField_nickname.onDeselect.AddListener((Text) =>
        {

            if (InputField_nickname.text == "")
            {
                InputField_nickname.GetComponent<Image>().color = Color_Pass;
            }
            else if(InputField_nickname.text.Length>=5)
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
            else if(InputField_nickname.text.Length<5)
            {
                InputField_nickname.GetComponent<Image>().color = Color_error;
            }


        });


        InputField_Username.onSelect.AddListener((Text) => {

            InputField_Username.GetComponent<Image>().color = Color_edit;
        });

        InputField_Username.onDeselect.AddListener((Text) => {
            if (InputField_Username.text=="")
            {
                InputField_Username.GetComponent<Image>().color = Color_Pass;
            }
            else if (InputField_Username.text.Length>=5)
            {
                //last change
            }

        });

        BTN_Change.onClick.AddListener(() =>
        {
            Chilligames_SDK.API_Client.Update_User_Info(new Req_Update_User_Info { Nickname = InputField_nickname.text, Username = InputField_Username.text, Email = InputField_Email.text, Password = InputField_Password.text, status = InputFieldSatatus.text, _id = _id }, () =>
            {

                SceneManager.LoadScene(0);

            }, err => { });
        });

    }


    private void Update()
    {

    }
}
