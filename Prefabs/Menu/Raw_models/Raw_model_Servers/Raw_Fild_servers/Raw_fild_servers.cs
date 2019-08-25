using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using UnityEngine.UI;

public class Raw_fild_servers : MonoBehaviour
{
    public GameObject Raw_model_info_server;

    public TextMeshProUGUI Text_Name_server;
    public TextMeshProUGUI Text_Freeze;
    public TextMeshProUGUI Text_Mines;
    public TextMeshProUGUI Text_Delete;
    public TextMeshProUGUI Text_Chance;
    public TextMeshProUGUI Text_Reset;
    public TextMeshProUGUI Text_Active_day;
    public TextMeshProUGUI Text_player;
    public TextMeshProUGUI Text_Like;
    public TextMeshProUGUI Text_Levels;
    public TextMeshProUGUI Text_Coines;

    public Button BTN_info;
    public Button BTN_Exit_server;

    string _id_player
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;

        }
    }
    public void Change_value(string _id_server)
    {

        Chilligames_SDK.API_Client.Recive_data_server<Deserilse_data_server>(new Req_data_server { Name_app = "Venomic", _id_server = _id_server }, (result) =>
        {
            string Name_server = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Name_server;
            string Freeze = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Freeze.ToString();
            string Mines = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Mines.ToString();
            string Delete = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Delete.ToString();
            string Chance = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Chance.ToString();
            string Reset = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Reset.ToString();
            string Active_days = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Active_Days.ToString();
            string Player = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Player.ToString();
            string Like = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).like.ToString();
            string Level = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Level.ToString();
            string Coines = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Coine.ToString();
            object[] leader_board = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Leader_board;

            Text_Name_server.text = Name_server;
            Text_Freeze.text = Freeze;
            Text_Mines.text = Mines;
            Text_Delete.text = Delete;
            Text_Chance.text = Chance;
            Text_Reset.text = Reset;
            Text_Active_day.text = Active_days;
            Text_player.text = Player;
            Text_Like.text = Like;
            Text_Levels.text = Level;
            Text_Coines.text = Coines;

            BTN_info.onClick.AddListener(() =>
            {
                GameObject Info_server = Instantiate(Raw_model_info_server);
                Info_server.GetComponent<Raw_model_info_server>().Change_Values(Name_server, Freeze, Mines, Delete, Chance, Reset, Active_days, Player, Like, Coines, Level, leader_board, _id_server);

            });

            BTN_Exit_server.onClick.AddListener(() =>
            {

                Destroy(gameObject);
                Chilligames_SDK.API_Client.Exit_server(new Req_Exit_server { _id = _id_player, _id_server = _id_server }, () =>
                {


                }, Error => { });

            });
        }, ERROR => { });


    }





    class Deserilse_data_server
    {
        public string _id = null;
        public string ID = null;
        public object Setting = null;


        public class Desrilise_setting_server
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
