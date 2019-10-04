using Chilligames.SDK;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Chilligames.Json.ChilligamesJson;
public class Raw_model_fild_server_play : MonoBehaviour
{
    public GameObject Raw_model_mission_online;
    public GameObject Raw_model_End_mission;

    public TextMeshProUGUI Text_nameserver_number;
    public TextMeshProUGUI Text_freeze_number;
    public TextMeshProUGUI Text_mines_number;
    public TextMeshProUGUI Text_delete_number;
    public TextMeshProUGUI Text_chance_number;
    public TextMeshProUGUI Text_reset_number;
    public TextMeshProUGUI Text_Active_day_number;
    public TextMeshProUGUI Text_level_number;
    public TextMeshProUGUI Text_Coin_number;


    public Transform Place_mission
    {
        get { return GameObject.Find("Player").GetComponent<Player>().Place_mission; }
    }
    public GameObject Missions;
    public GameObject End_Result_mission;



    public string Name_server;
    public int? Freeze, Mines, Delete, Chance, Reset, Active_day, Levels, Total_level, Coin, Player_;


    /// <summary>
    /// pishniaz hay server inja sakhte mishe;
    /// </summary>
    /// <param name="_id_server"></param>
    public void Change_value(string _id_server, string _id)
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
            Total_level = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Level;
            Coin = DeserializeObject<Panel_Servers.Model_server.Setting_servers>(Result.Setting.ToString()).Coine;


            Active_day = Mathf.Abs((int)Active_day);
            Active_day = Active_day / 60 / 60 / 24;

            Text_nameserver_number.text = Name_server;
            Text_freeze_number.text = Freeze.ToString();
            Text_mines_number.text = Mines.ToString();
            Text_delete_number.text = Delete.ToString();
            Text_chance_number.text = Chance.ToString();
            Text_reset_number.text = Reset.ToString();
            Text_Active_day_number.text = Active_day.ToString();
            Text_level_number.text = Total_level.ToString();
            Text_Coin_number.text = Coin.ToString();

            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                Missions = Instantiate(Raw_model_mission_online, Place_mission);
                Missions.GetComponent<Raw_model_game_play_online>().Change_value(_id, Name_server, (int)Coin, (int)Total_level, 0, (int)Freeze, (int)Mines, (int)Delete, (int)Chance, (int)Reset, _id_server, gameObject);
                Player.Cam.Move_camera(new Vector3(10, 10, 0));

            });

        }, err => { });

    }



}
