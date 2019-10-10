using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Report_bug : MonoBehaviour
{
    public TMP_Dropdown Drop_down_select_catrgory;
    public TMP_InputField InputField_subject;
    public TMP_InputField inputField_body_subject;
    public Button BTN_Send_report;

    void Start()
    {
        BTN_Send_report.onClick.AddListener(() =>
        {

            
            Chilligames_SDK.API_Client.Report_Bug(new Req_report_bug { Name_app = "Venomic", Email_admin = "hossynassghary@gmail.com", Data_user = PlayerPrefs.GetString("_id"), Message = $"[Category:]{Drop_down_select_catrgory.captionText.text}    " + inputField_body_subject.text, Key = InputField_subject.text }, () =>
            {
                inputField_body_subject.text = "Tanx for send report  ";    

            }, err => { });
        });

    }


    void Update()
    {

    }
}
