using Chilligames.SDK;
using Chilligames.SDK.Model_Admin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_update : MonoBehaviour
{
    public TextMeshProUGUI Text_Curent_version_number;
    public TextMeshProUGUI Text_new_update_version;

    public Button BTN_Cafebazar;
    public Button BTN_Google_play;


    void Start()
    {
        Text_Curent_version_number.text = Application.version;

        Chilligames_SDK.API_Admin.Recive_version_game(new Req_recive_version { Name_app = "Venomic" }, result =>
           {
               Text_new_update_version.text = result;
           }, er => { });

        Chilligames_SDK.API_Admin.Recive_link_market(new Req_recive_link_market { Name_app = "Venomic" }, result =>
        {
            BTN_Cafebazar.onClick.AddListener(() =>
            {
                Application.OpenURL(result.CafeBazaar);
            });
            BTN_Google_play.onClick.AddListener(() =>
            {
                Application.OpenURL(result.Google_Play);
            });



        }, err => { });


    }

}
