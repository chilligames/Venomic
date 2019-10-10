using Chilligames.SDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Chilligames.SDK.Model_Client;

public class Contact_us : MonoBehaviour
{
    public TMP_InputField Inputfild_Form_contact;

    public TextMeshProUGUI Text_percent_rate;

    public Button BTN_Send_form;
    public Button BTN_Send_rate;
    public Button BTN_close;

    public Slider Slider_rate_us;

    public Color Color_select;
    public Color Color_deselect;

    string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    string messege;



    void Start()
    {
        //change color input fild
        Inputfild_Form_contact.onSelect.AddListener((Text) =>
        {
            Inputfild_Form_contact.GetComponent<Image>().color = Color_select;
        });
        Inputfild_Form_contact.onValueChanged.AddListener((Text) =>
        {
            messege = Text;
            Inputfild_Form_contact.GetComponent<Image>().color = Color_select;
        });
        Inputfild_Form_contact.onDeselect.AddListener((Text) =>
        {
            messege = Text;
            Inputfild_Form_contact.GetComponent<Image>().color = Color_deselect;
        });

        BTN_Send_form.onClick.AddListener(() =>
        {
            string ID = GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;

            Chilligames_SDK.API_Client.Send_contact_us(new Req_contact_us { NameApp = "Venomic", Messege = Inputfild_Form_contact.text, Email_admin = "hossynassghary@gmail.com", Data_use = ID }, () =>
            {
                Inputfild_Form_contact.text = "Message Send";
                print("send here");
            }, err => { });
        });

        BTN_Send_rate.onClick.AddListener(() =>
        {

            Chilligames_SDK.API_Client.Rate_to_game(new Req_rate_to_game { Name_app = "Venomic", Rate = Slider_rate_us.value.ToString(), _id = _id },
                () =>
                {
                    BTN_Send_rate.GetComponentInChildren<TextMeshProUGUI>().text = "Rate send";

                }, ERRORS => { });

        });

        BTN_close.onClick.AddListener(() => {
            Destroy(gameObject);
        });
    }

    void Update()
    {
        Text_percent_rate.text = Slider_rate_us.value.ToString() + "%";
    }
}
