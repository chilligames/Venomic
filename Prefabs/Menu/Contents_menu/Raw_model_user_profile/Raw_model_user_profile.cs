using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Chilligames.Json;
using Chilligames.SDK.Model_Client;
using Chilligames.SDK;

public class Raw_model_user_profile : MonoBehaviour
{
    public string _id;

    public TextMeshProUGUI Nickname;
    public TextMeshProUGUI Status;
    public Button[] BTN_Connection;
    public Button BTN_Send_massege;
    public Button BTN_close_profile;


    void Start()
    {
        BTN_close_profile.onClick.AddListener(() =>
        {
            Destroy(gameObject);

        });

        Chilligames_SDK.API_Client.Recive_Info_other_User<Schema_other_player>(new Req_recive_Info_player { _id = _id }, resul =>
        {
            Nickname.text = ChilligamesJson.DeserializeObject<Schema_other_player.DeserilseInfoPlayer>(resul.Info.ToString()).Nickname;
            Status.text = ChilligamesJson.DeserializeObject<Schema_other_player.DeserilseInfoPlayer>(resul.Info.ToString()).Status;
        }, err => { });

    }

    class Schema_other_player
    {
        public object _id = null;
        public object Info = null;
        public object Inventory = null;

        public class DeserilseInfoPlayer
        {
            public string Status = null;
            public string Nickname = null;

        }
    }

}
