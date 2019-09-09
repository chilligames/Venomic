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

    public Button BTN_Play_mission_server;

    Transform Place_mission
    {
        get { return GameObject.Find("Player").GetComponent<Player>().Place; }
    }
    GameObject[] Missions;
    GameObject End_Result_mission;

    public string Name_server;
    public int? Freeze, Mines, Delete, Chance, Reset, Active_day, Levels, Levels_remine, Coin, Player_;

    public int Star_missions;

    int Pass_server;

    /// <summary>
    /// pishniaz hay server inja sakhte mishe;
    /// </summary>
    /// <param name="_id_server"></param>
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
                Missions = new GameObject[(int)Levels_remine];

                Vector3 Last_pos = new Vector3(0, 0, 0);
                for (int i = 0; i < Levels_remine; i++)
                {
                    Missions[i] = Instantiate(Raw_model_mission_online, Place_mission);
                    Missions[i].GetComponent<Raw_model_Game_play_online>().Change_value(Name_server, Levels_remine, i, gameObject);
                    Missions[i].transform.position = new Vector3(Last_pos.x + 10, Last_pos.y + 10, 0);
                    Last_pos = Missions[i].transform.position;

                    if (i + 1 == Levels_remine)
                    {
                        End_Result_mission = Instantiate(Raw_model_End_mission, Place_mission);
                        End_Result_mission.AddComponent<model_End_mission>();
                        End_Result_mission.transform.position = new Vector3(Last_pos.x + 10, Last_pos.y + 10, 0);
                    }

                }

                Player.Cam.Move_camera(new Vector3(10, 10, 0));

            });


        }, err => { });

    }


    /// <summary>
    /// camera mibare b menu bayad cheack beshe ehtemalan bug bashe inja 
    /// </summary>
    public void Leave_server()
    {
        for (int i = 0; i < Missions.Length; i++)
        {
            Destroy(Missions[i]);
            Pass_server = 0;
        }
        Destroy(End_Result_mission);
        Player.Cam.Move_camera(Vector3.zero);
        End_Result_mission = null;
        Missions = null;
    }



    private void Update()
    {
        if (Missions != null)
        {
            foreach (var mission in Missions)
            {
                if (mission.GetComponent<Raw_model_Game_play_online>().Result_mission == 1)
                {
                    Pass_server = 1;
                }
                else
                {
                    Pass_server = 0;
                    break;
                }

            }

            if (Pass_server == 1)
            {
                print("Code Server pass here");
            }

            int? avrege_score = Star_missions + Freeze + Mines + Delete + Chance + Reset;

            End_Result_mission.GetComponent<model_End_mission>().Change_value(Name_server, Star_missions, Freeze, Mines, Delete, Chance, Reset, avrege_score);
        }

    }


    class model_End_mission : MonoBehaviour
    {
        public TextMeshProUGUI Text_Name_server
        {
            get
            {
                TextMeshProUGUI Text_name_server = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {

                    if (Text.name == "TS")
                    {
                        Text_name_server = Text;
                    }
                }
                return Text_name_server;
            }
        }

        TextMeshProUGUI Text_Star_number
        {
            get
            {
                TextMeshProUGUI Text_star_number = null;
                foreach (var text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (text.name == "TSN")
                    {
                        Text_star_number = text;
                    }
                }
                return Text_star_number;
            }
        }

        TextMeshProUGUI Text_freeze_number
        {
            get
            {
                TextMeshProUGUI Text_freeze_number = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Text.name == "TFN")
                    {
                        Text_freeze_number = Text;
                    }
                }
                return Text_freeze_number;
            }
        }

        TextMeshProUGUI Text_minus_number
        {
            get
            {
                TextMeshProUGUI Text_minus_number = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Text.name == "TMN")
                    {
                        Text_minus_number = Text;
                    }
                }
                return Text_minus_number;
            }
        }
        TextMeshProUGUI Text_delete_number
        {
            get
            {
                TextMeshProUGUI Text_delete_number = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Text.name == "TDN")
                    {
                        Text_delete_number = Text;

                    }
                }

                return Text_delete_number;
            }
        }
        TextMeshProUGUI Text_Chance_number
        {
            get
            {
                TextMeshProUGUI Text_Chance = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Text.name == "TCN")
                    {
                        Text_Chance = Text;
                    }
                }
                return Text_Chance;
            }

        }

        TextMeshProUGUI Text_reset_number
        {
            get
            {
                TextMeshProUGUI Text_reset = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Text.name == "TRN")
                    {
                        Text_reset = Text;
                    }
                }
                return Text_reset;
            }
        }

        TextMeshProUGUI Text_avrage
        {
            get
            {
                TextMeshProUGUI Text_Average = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Text.name == "TAN")
                    {
                        Text_Average = Text;
                    }
                }
                return Text_Average;
            }
        }

        /// <summary>
        /// meghdar hay panel end taghir mide 
        /// </summary>
        /// <param name="Name_server"></param>
        /// <param name="Stars"></param>
        /// <param name="freeze"></param>
        /// <param name="mines"></param>
        /// <param name="delete"></param>
        /// <param name="Chance"></param>
        /// <param name="reset"></param>
        /// <param name="average"></param>
        public void Change_value(string Name_server, int? Stars, int? freeze, int? mines, int? delete, int? Chance, int? reset, int? average)
        {
            Text_Name_server.text = Name_server;
            Text_Star_number.text = Stars.ToString();
            Text_freeze_number.text = freeze.ToString();
            Text_minus_number.text = mines.ToString();
            Text_delete_number.text = delete.ToString();
            Text_Chance_number.text = Chance.ToString();
            Text_reset_number.text = reset.ToString();
            Text_avrage.text = average.ToString();
        }




    }
}
