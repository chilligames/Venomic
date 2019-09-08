using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK;
using static Chilligames.Json.ChilligamesJson;
public class Raw_model_fild_server_play : MonoBehaviour
{
    public TextMeshProUGUI Text_nameserver_number;
    public TextMeshProUGUI Text_freeze_number;
    public TextMeshProUGUI Text_mines_number;
    public TextMeshProUGUI Text_delete_number;
    public TextMeshProUGUI Text_chance_number;
    public TextMeshProUGUI Text_reset_number;
    public TextMeshProUGUI Text_Active_day_number;
    public TextMeshProUGUI Text_level_number;
    public TextMeshProUGUI Text_Coin_number;


    public GameObject Raw_model_mission_online;



    public Button BTN_Play_mission_server;

    Transform Place_mission
    {
        get { return GameObject.Find("Player").GetComponent<Player>().Place; }
    }
    GameObject[] Mission;

    public int test;

    public string Name_server;
    public int? Freeze, Mines, Delete, Chance, Reset, Active_day, Levels, Levels_remine, Coin, Player_;

    public int Star_missions;


    public void Change_value(string _id_server)
    {
        Chilligames_SDK.API_Client.Recive_data_server<Panel_Servers.Model_server>(new Chilligames.SDK.Model_Client.Req_data_server { Name_app = "Venomic", _id_server = _id_server }, Result =>
        {
            Name_server = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Name_server;
            Freeze = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Freeze;
            Mines = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Mines;
            Delete = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Delete;
            Chance = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Chance;
            Reset = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Reset;
            Active_day = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Active_Days;
            Levels_remine = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Level;
            Coin = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Coine;
            Star_missions = (int)Levels_remine * 3;

            Text_nameserver_number.text = Name_server;
            Text_freeze_number.text = Freeze.ToString();
            Text_mines_number.text = Mines.ToString();
            Text_delete_number.text = Delete.ToString();
            Text_chance_number.text = Chance.ToString();
            Text_reset_number.text = Reset.ToString();
            Text_Active_day_number.text = Active_day.ToString();
            Text_level_number.text = Levels_remine.ToString();
            Text_Coin_number.text = Coin.ToString();



            BTN_Play_mission_server.onClick.AddListener(() =>
            {
                Mission = new GameObject[(int)Levels_remine];

                Vector3 Last_pos = new Vector3(0, 0, 0);
                for (int i = 0; i < Levels_remine; i++)
                {
                    Mission[i] = Instantiate(Raw_model_mission_online, Place_mission);
                    Mission[i].GetComponent<Raw_model_Game_play_online>().Change_value(Name_server,Levels_remine,i,gameObject);
                    Mission[i].transform.position = new Vector3(Last_pos.x + 10, Last_pos.y + 10, 0);
                    Last_pos = Mission[i].transform.position;
                }

                Player.Cam.Move_camera(new Vector3(10, 10, 0));

            });


        }, err => { });

    }

    private void Update()
    {
        if (Mission != null)
        {
        

           
        }

    }



}
