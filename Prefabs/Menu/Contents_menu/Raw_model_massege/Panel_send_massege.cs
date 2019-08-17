using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;

public class Panel_send_massege : MonoBehaviour
{
    public string _id;

    public string _id_other_player;

    public TextMeshProUGUI Text_Nick_name;
    public Button BTN_send_massege;
    public Button BTN_Close;


    void Start()
    {
        BTN_Close.onClick.AddListener(() =>
        {

            Destroy(gameObject);
        });

        BTN_send_massege.onClick.AddListener(() => {


            Chilligames_SDK.API_Client.
        });

    }

    void Update()
    {

    }
}
