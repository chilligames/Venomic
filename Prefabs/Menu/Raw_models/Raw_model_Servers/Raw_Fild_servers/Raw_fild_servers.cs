using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using TMPro;
using UnityEngine;
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
    public Button BTN_Enter_server;
    public Button BTN_Like;

    public Color Color_dislike;
    public Color Color_Like;

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
            string Player = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Player.ToString();
            string Like = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).like.ToString();
            int Active_days = (int)ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Active_Days;
            string Level = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Level.ToString();
            string Coines = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Coine.ToString();
            object[] leader_board = ChilligamesJson.DeserializeObject<Deserilse_data_server.Desrilise_setting_server>(result.Setting.ToString()).Leader_board;

            Active_days = Mathf.Abs(Active_days) / 60 / 60 / 24 + 1;

            Text_Name_server.text = Name_server;
            Text_Freeze.text = Freeze;
            Text_Mines.text = Mines;
            Text_Delete.text = Delete;
            Text_Chance.text = Chance;
            Text_Reset.text = Reset;
            Text_Active_day.text = Active_days.ToString();
            Text_player.text = Player;
            Text_Like.text = Like;
            Text_Levels.text = Level;
            Text_Coines.text = Coines;

            Chilligames_SDK.API_Client.Cheack_Server_In_Profile(new Req_cheack_server_in_profile { Name_App = "Venomic", _id = _id_player, _id_server = _id_server }, Status =>
            {

                if (Status == "1")
                {
                    BTN_Exit_server.gameObject.SetActive(true);
                }
                if (Status == "0")
                {
                    BTN_Enter_server.gameObject.SetActive(true);
                }

            }, ERR => { });

            //change action btns

            BTN_info.onClick.AddListener(() =>
            {
                GameObject Info_server = Instantiate(Raw_model_info_server);
                Info_server.GetComponent<Raw_model_info_server>().Change_Values(Name_server, Freeze, Mines, Delete, Chance, Reset, Active_days, Player, Like, Coines, Level, leader_board, _id_server);

            });


            BTN_Exit_server.onClick.AddListener(() =>
            {
                Destroy(gameObject);

                Chilligames_SDK.API_Client.Exit_server(new Req_Exit_server { _id = _id_player, _id_server = _id_server, Name_App = "Venomic" }, () =>
                   {

                   }, Error => { });
                Chilligames_SDK.API_Client.Pluse_or_minuse_value_fild_server(new Req_change_server_data_fild { Name_app = "Venomic", Pipe_line_data = "Setting.Player", _id_server = _id_server, Data_inject = "-1" }, null, null);

            });


            BTN_Enter_server.onClick.AddListener(() =>
            {
                int count_player = int.Parse(Text_player.text);

                count_player += 1;
                Text_player.text = count_player.ToString();
                Chilligames_SDK.API_Client.Enter_to_server(new Req_enter_to_server { Name_App = "Venomic", _id = _id_player, _id_server = _id_server }, () => { }, ERR => { });
                BTN_Enter_server.gameObject.SetActive(false);
                BTN_Exit_server.gameObject.SetActive(true);
                Chilligames_SDK.API_Client.Pluse_or_minuse_value_fild_server(new Req_change_server_data_fild { Data_inject = "1", _id_server = _id_server, Pipe_line_data = "Setting.Player", Name_app = "Venomic" }, null, null);
            });


            BTN_Like.onClick.AddListener(() =>
            {
                BTN_Like.gameObject.SetActive(false);
                Chilligames_SDK.API_Client.Pluse_or_minuse_value_fild_server(new Req_change_server_data_fild { _id_server = _id_server, Name_app = "Venomic", Pipe_line_data = "Setting.like", Data_inject = "1" }, () => { }, () => { });
                Text_Like.text = (int.Parse(Like) + 1).ToString();
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
