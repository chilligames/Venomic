﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;

public class Panel_Servers : MonoBehaviour
{
    [Header("entity server")]
    public GameObject Raw_model_fild_server;
    public GameObject Raw_model_info_server;
    public Transform Place_content_my_servers;

    public TMP_FontAsset Font_select_tab;
    public TMP_FontAsset Font_deselect_tab;


    public TextMeshProUGUI Text_time_number;

    public Button BTN_Servers;
    public Button BTN_My_servers;
    public Button BTN_Creat_Servers;
    public Button BTN_submit_creat_server;
    public Button BTN_Close_Panel_Creat_servers;

    public GameObject Content_Servers;
    public GameObject Content_My_servers;
    public GameObject Content_creat_servers;

    public GameObject Curent_content;
    public Transform Place_instant_servers;

    [Header("Entity creat server")]
    public TMP_InputField Text_name_server;
    public Slider Value_Chance;
    public Slider Value_delete;
    public Slider Value_Freeze;
    public Slider Value_Mines;
    public Slider Value_Reset;
    public Slider Value_Level;
    public Slider Value_Active_Days;
    int? Coin = null;

    public TextMeshProUGUI Text_Min_Chance;
    public TextMeshProUGUI Text_Min_Delete;
    public TextMeshProUGUI Text_Min_Freeze;
    public TextMeshProUGUI Text_Min_Mines;
    public TextMeshProUGUI Text_Min_Reset;
    public TextMeshProUGUI Text_Min_Level;
    public TextMeshProUGUI Text_Min_coin;
    public TextMeshProUGUI Text_Min_Active_days;

    GameObject[] Entity_my_servers;
    GameObject[] Entity_servers;

    public string _id_player
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    void Start()
    {
        Curent_content = Content_Servers;

        Chilligames_SDK.API_Client.Recive_all_servers(new Req_recive_all_server { Count_server = 50, Name_App = "Venomic" }, result =>
        {
            Entity_servers = new GameObject[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                Entity_servers[i] = Instantiate(Raw_model_fild_server, Place_instant_servers);
                Entity_servers[i].GetComponent<Raw_fild_servers>().Change_value(ChilligamesJson.DeserializeObject<Model_server>(result[i].ToString())._id);
            }

        }, ERR => { });


        BTN_Servers.onClick.AddListener(() =>
        {
            Content_Servers.SetActive(true);
            Curent_content.SetActive(false);
            Curent_content = Content_Servers;
            Curent_content.SetActive(true);
            Chilligames_SDK.API_Client.Recive_all_servers(new Req_recive_all_server { Count_server = 50, Name_App = "Venomic" }, result =>
            {
                for (int i = 0; i < result.Length; i++)
                {
                    Entity_servers[i] = Instantiate(Raw_model_fild_server, Place_instant_servers);
                    Entity_servers[i].GetComponent<Raw_fild_servers>().Change_value(ChilligamesJson.DeserializeObject<Model_server>(result[i].ToString())._id);
                }
            }, ERR => { });

        });


        BTN_My_servers.onClick.AddListener(() =>
        {
            Content_My_servers.SetActive(true);
            Curent_content.SetActive(false);
            Curent_content = Content_My_servers;
            Curent_content.SetActive(true);
            Chilligames_SDK.API_Client.Recive_List_server_user(new Chilligames.SDK.Model_Client.Req_recive_list_servers_User { Name_app = "Venomic", _id = _id_player }, result =>
            {
                Entity_my_servers = new GameObject[result.Length];

                for (int i = 0; i < result.Length; i++)
                {
                    Entity_my_servers[i] = Instantiate(Raw_model_fild_server, Place_content_my_servers);
                    Entity_my_servers[i].GetComponent<Raw_fild_servers>().Change_value(result[i].ToString());
                }
            }, ERR => { });
        });


        BTN_Creat_Servers.onClick.AddListener(() =>
        {
            Content_creat_servers.SetActive(false);
            Curent_content.SetActive(false);
            Curent_content = Content_creat_servers;
            Curent_content.SetActive(true);
        });


        BTN_submit_creat_server.onClick.AddListener(() =>
        {
            Model_server.Setting_servers setting = new Model_server.Setting_servers
            {
                Name_server = Text_name_server.text,
                Chance = (int)Value_Chance.value,
                Delete = (int)Value_delete.value,
                Freeze = (int)Value_Freeze.value,
                Mines = (int)Value_Mines.value,
                Reset = (int)Value_Reset.value,
                Active_Days = (int)Value_Active_Days.value,
                Level = (int)Value_Level.value,
                Coine = Coin,
                Player = 0,
                like = 0,
                Leader_board = { },
            };

            Chilligames_SDK.API_Client.Creat_server(new Chilligames.SDK.Model_Client.Req_creat_server { _id = GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player, Setting = setting, Name_App = "Venomic" }, () =>
            {
                print("code agfter creat");
            }, err => { });
        });


        BTN_Close_Panel_Creat_servers.onClick.AddListener(() =>
        {
            Content_My_servers.SetActive(true);
            Curent_content.SetActive(false);
            Curent_content = Content_My_servers;
            Curent_content.SetActive(true);
        });

    }

    void Update()
    {

        if (Content_Servers.activeInHierarchy)
        {
            BTN_Servers.GetComponentInChildren<TextMeshProUGUI>().font = Font_select_tab;
        }
        else
        {
            if (Entity_servers != null)
            {
                for (int i = 0; i < Entity_servers.Length; i++)
                {
                    Destroy(Entity_servers[i]);
                }
            }

            BTN_Servers.GetComponentInChildren<TextMeshProUGUI>().font = Font_deselect_tab;
        }


        if (Content_My_servers.activeInHierarchy)
        {
            BTN_My_servers.GetComponentInChildren<TextMeshProUGUI>().font = Font_select_tab;
        }
        else
        {
            if (Entity_my_servers != null)
            {

                for (int i = 0; i < Entity_my_servers.Length; i++)
                {
                    Destroy(Entity_my_servers[i]);
                }
            }

            BTN_My_servers.GetComponentInChildren<TextMeshProUGUI>().font = Font_deselect_tab;
        }


        if (Text_name_server.text.Length < 4)
        {
            BTN_submit_creat_server.enabled = false;
            BTN_submit_creat_server.GetComponent<Image>().color = Color.red;
        }
        else
        {
            BTN_submit_creat_server.enabled = true;
            BTN_submit_creat_server.GetComponent<Image>().color = Color.green;
        }

        Text_time_number.text = DateTime.UtcNow.ToString();

        Change_value_sliders();


        void Change_value_sliders()
        {
            Text_Min_coin.text = Coin.ToString();
            Text_Min_Chance.text = Value_Chance.value.ToString();
            Text_Min_Delete.text = Value_delete.value.ToString();
            Text_Min_Freeze.text = Value_Freeze.value.ToString();
            Text_Min_Mines.text = Value_Mines.value.ToString();
            Text_Min_Reset.text = Value_Reset.value.ToString();
            Text_Min_Level.text = Value_Level.value.ToString();
            Text_Min_Active_days.text = Value_Active_Days.value.ToString();
        }

    }



    class Model_server
    {
        public string _id = null;
        public object Setting = null;
        public string ID = null;
        public class Setting_servers
        {
            public string Name_server = null;
            public object[] Leader_board = null;
            public int? Active_Days = null;
            public int? Freeze = null;
            public int? Mines = null;
            public int? Delete = null;
            public int? Chance = null;
            public int? Reset = null;
            public int? Level = null;
            public int? Player = null;
            public int? like = null;
            public int? Coine = null;

        }

    }


}
