using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// playerpref:
/// 1: Coin
/// </summary>
public class Panel_Servers : MonoBehaviour
{
    [Header("entity server")]
    public GameObject Raw_model_fild_server;
    public GameObject Raw_model_info_server;
    public Transform Place_content_my_servers;

    public Color Color_select_tab;
    public Color Color_deselect_tab;


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
    int? Coin = 0;

    public TextMeshProUGUI Text_Min_Chance;
    public TextMeshProUGUI Text_Min_Delete;
    public TextMeshProUGUI Text_Min_Freeze;
    public TextMeshProUGUI Text_Min_Mines;
    public TextMeshProUGUI Text_Min_Reset;
    public TextMeshProUGUI Text_Min_Level;
    public TextMeshProUGUI Text_Min_coin;
    public TextMeshProUGUI Text_Min_Active_days;

    public TextMeshProUGUI Text_max_Chance;
    public TextMeshProUGUI Text_max_delete;
    public TextMeshProUGUI Text_max_freeze;
    public TextMeshProUGUI Text_max_mines;
    public TextMeshProUGUI Text_max_reset;

    GameObject[] Entity_my_servers;
    GameObject[] Entity_servers;

    public string _id_player
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    private void Start()
    {
        //change action btns
        BTN_Servers.onClick.AddListener(() =>
        {
            Content_Servers.SetActive(true);
            Curent_content.SetActive(false);
            Curent_content = Content_Servers;
            Curent_content.SetActive(true);

            Chilligames_SDK.API_Client.Recive_all_servers(new Req_recive_all_server { Count = 50, Name_App = "Venomic" }, result =>
            {
                Entity_servers = new GameObject[result.Length];

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
            Chilligames_SDK.API_Client.Recive_List_server_user(new Req_recive_list_servers_User { Name_app = "Venomic", _id = _id_player }, result =>
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
                Active_Days = (int)DateTime.UtcNow.Subtract(DateTime.UtcNow.AddDays((int)Value_Active_Days.value)).TotalSeconds,
                Level = (int)Value_Level.value,
                Coine = Coin,
                Player = 0,
                like = 0,
                Leader_board = new object[0],
            };

            Chilligames_SDK.API_Client.Creat_server(new Req_creat_server { _id = _id_player, Setting = setting, Name_App = "Venomic" }, () =>
            {
                Content_My_servers.SetActive(true);
                Curent_content.SetActive(false);
                Curent_content = Content_My_servers;
                Curent_content.SetActive(true);
                Chilligames_SDK.API_Client.Recive_List_server_user(new Req_recive_list_servers_User { Name_app = "Venomic", _id = _id_player }, Result =>
                {

                    if (Entity_my_servers != null)
                    {

                        for (int i = 0; i < Entity_my_servers.Length; i++)
                        {
                            Destroy(Entity_my_servers[i]);
                        }
                    }

                    Entity_my_servers = new GameObject[Result.Length];


                    for (int i = 0; i < Result.Length; i++)
                    {
                        Entity_my_servers[i] = Instantiate(Raw_model_fild_server, Place_content_my_servers);
                        Entity_my_servers[i].GetComponent<Raw_fild_servers>().Change_value(Result[i].ToString());
                    }

                }, err => { });
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

    void OnEnable()
    {

        //starter
        if (Entity_my_servers!=null)
        {
            for (int i = 0; i < Entity_my_servers.Length; i++)
            {

            Destroy(Entity_my_servers[i]);
            }
            Entity_my_servers = null;
        }
        if (Entity_servers != null)
        {
            for (int i = 0; i < Entity_servers.Length; i++)
            {
                Destroy(Entity_servers[i]);
            }
            Entity_servers = null;
        }

        Curent_content = Content_Servers;

        //recive all server
        Chilligames_SDK.API_Client.Recive_all_servers(new Req_recive_all_server { Count = 50, Name_App = "Venomic" }, result =>
        {
            Entity_servers = new GameObject[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                Entity_servers[i] = Instantiate(Raw_model_fild_server, Place_instant_servers);
                Entity_servers[i].GetComponent<Raw_fild_servers>().Change_value(ChilligamesJson.DeserializeObject<Model_server>(result[i].ToString())._id);
            }

        }, ERR => { });

    }

    void Update()
    {
        if (Content_Servers.activeInHierarchy)
        {
            BTN_Servers.GetComponentInChildren<TextMeshProUGUI>().color = Color_select_tab;

        }
        else
        {
            if (Entity_servers != null)
            {
                for (int i = 0; i < Entity_servers.Length; i++)
                {
                    Destroy(Entity_servers[i]);
                }
                Entity_servers = null;

            }

            BTN_Servers.GetComponentInChildren<TextMeshProUGUI>().color = Color_deselect_tab;
        }


        if (Content_My_servers.activeInHierarchy)
        {
            BTN_My_servers.GetComponentInChildren<TextMeshProUGUI>().color = Color_select_tab;
        }
        else
        {
            if (Entity_my_servers != null)
            {
                for (int i = 0; i < Entity_my_servers.Length; i++)
                {
                    Destroy(Entity_my_servers[i]);
                }
                Entity_my_servers = null;
            }

            BTN_My_servers.GetComponentInChildren<TextMeshProUGUI>().color = Color_deselect_tab;
        }

        if (Text_name_server.text.Length < 4 || PlayerPrefs.GetInt("Coin") < Coin)
        {
            BTN_submit_creat_server.enabled = false;
            BTN_submit_creat_server.GetComponent<Image>().color = Color.red;
        }
        else
        {
            BTN_submit_creat_server.enabled = true;
            BTN_submit_creat_server.GetComponent<Image>().color = Color.green;
        }

        if (Value_Level.value >= Value_Level.maxValue)
        {
            Value_Level.maxValue = Value_Level.maxValue + 1;
        }

        Text_time_number.text = DateTime.UtcNow.ToString();

        Change_value_sliders_creat_server();


        void Change_value_sliders_creat_server()
        {
            Value_Chance.maxValue = Mathf.RoundToInt(Value_Level.value / 100 * 30);
            Value_delete.maxValue = Mathf.RoundToInt(Value_Level.value / 100 * 80);
            Value_Freeze.maxValue = Mathf.RoundToInt(Value_Level.value / 100 * 10);
            Value_Mines.maxValue = Mathf.RoundToInt(Value_Level.value / 100 * 70);
            Value_Reset.maxValue = Mathf.RoundToInt(Value_Level.value / 100 * 50);

            Coin = (int)(Value_Active_Days.value * 11) + (int)(Value_Level.value + Value_Chance.value + Value_delete.value + Value_Freeze.value + Value_Mines.value + Value_Reset.value);

            Text_Min_coin.text = Coin.ToString();
            Text_Min_Chance.text = Value_Chance.value.ToString();
            Text_Min_Delete.text = Value_delete.value.ToString();
            Text_Min_Freeze.text = Value_Freeze.value.ToString();
            Text_Min_Mines.text = Value_Mines.value.ToString();
            Text_Min_Reset.text = Value_Reset.value.ToString();
            Text_Min_Level.text = Value_Level.value.ToString();
            Text_Min_Active_days.text = Value_Active_Days.value.ToString();

            Text_max_Chance.text = Value_Chance.maxValue.ToString();
            Text_max_delete.text = Value_delete.maxValue.ToString();
            Text_max_freeze.text = Value_Freeze.maxValue.ToString();
            Text_max_mines.text = Value_Mines.maxValue.ToString();
            Text_max_reset.text = Value_Reset.maxValue.ToString();
        }

    }


    private void OnDisable()
    {
        //delet entity recive
        if (Entity_my_servers != null)
        {
            for (int i = 0; i < Entity_my_servers.Length; i++)
            {
                Destroy(Entity_my_servers[i]);
            }
            Entity_my_servers = null;
        }

        if (Entity_servers != null)
        {
            for (int i = 0; i < Entity_servers.Length; i++)
            {
                Destroy(Entity_servers[i]);
            }
            Entity_servers = null;
        }

        //change to defult
        Curent_content.SetActive(false);
        Curent_content = Content_Servers;
        Content_Servers.SetActive(true);
    }


    public class Model_server
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

            public class Deserilise_leader_board
            {
                public string ID;
                public int Score;
            }
        }

    }


}
