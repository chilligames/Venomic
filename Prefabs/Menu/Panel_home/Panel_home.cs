using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// playerprefe:
/// 1: Freeze
/// 2: Minuse
/// 3: Delete
/// 4: Chance
/// 5: Reset
/// 6: Coin
/// 7: Level
/// 8: _id
/// </summary>
/// 
public class Panel_home : MonoBehaviour
{

    public GameObject Raw_model_fild_server_play;
    public GameObject Raw_model_mission_offline;

    public TextMeshProUGUI Text_ranking_number;
    public TextMeshProUGUI Text_level_number;
    public TextMeshProUGUI Text_nickname;
    public TextMeshProUGUI Text_Coin_number;
    public GameObject Offline_mode;

    public Transform Place_server;
    public GameObject[] Server_fild;


    public Button BTN_Play_offline;
    public Button BTN_return_online;

    public GameObject[] object_hide_offline;

    public Transform Place_missons;

    public GameObject Missions;

    public string _id
    {
        get
        {
            return PlayerPrefs.GetString("_id");
        }
    }

    public void OnEnable()
    {
        //starter
        // destroy  no net  and recive after recive
        if (Server_fild != null)
        {
            for (int i = 0; i < Server_fild.Length; i++)
            {
                Destroy(Server_fild[i]);
            }
            Server_fild = null;
        }


        /*....online....*/

        if (PlayerPrefs.GetString("_id").Length < 2)
        {
            Chilligames_SDK.API_Client.Quick_register(result =>
            {
                print("register_new_user");
                PlayerPrefs.SetString("_id", result._id);
                SceneManager.LoadScene(0);
            }, ERROR =>
            {
                Offline_mode.SetActive(true);

                for (int i = 0; i < object_hide_offline.Length; i++)
                {
                    object_hide_offline[i].gameObject.SetActive(false);
                }
            });
        }
        else
        {
            Chilligames_SDK.API_Client.Quick_login(new Req_Login { _id = PlayerPrefs.GetString("_id") }, Result_login =>
            {
                if (Result_login == "1")
                {

                    Send_data();

                    Chilligames_SDK.API_Client.Recive_List_server_user(new Req_recive_list_servers_User { Name_app = "Venomic", _id = _id }, Result_server =>
                    {
                        Server_fild = new GameObject[Result_server.Length];
                        for (int i = 0; i < Result_server.Length; i++)
                        {
                            Server_fild[i] = Instantiate(Raw_model_fild_server_play, Place_server);
                            Server_fild[i].GetComponent<Raw_model_fild_server_play>().Change_value(Result_server[i].ToString(), _id, gameObject);
                        }

                    }, err => { });

                    Chilligames_SDK.API_Client.Recive_info_user(new Req_recive_Info_player { _id = _id }, result =>
                    {
                        Text_nickname.text = result.Nickname;

                    }, err => { });

                    void Send_data()
                    {
                        Chilligames_SDK.API_Client.Sync_coin_with_server(new Req_sync_coin_with_server { Coin = PlayerPrefs.GetInt("Coin"), _id = _id }, () => { }, err => { });

                        Chilligames_SDK.API_Client.Send_Data_user(new Req_send_data
                        {
                            _id = _id,
                            Name_app = "Venomic",
                            Data_user = ChilligamesJson.SerializeObject(new Entity_Player
                            {
                                Chance = PlayerPrefs.GetInt("Chance"),
                                Delete = PlayerPrefs.GetInt("Delete"),
                                Freeze = PlayerPrefs.GetInt("Freeze"),
                                Level = PlayerPrefs.GetInt("Level"),
                                Minus = PlayerPrefs.GetInt("Minuse"),
                                Reset = PlayerPrefs.GetInt("Reset")
                            })
                        }, () => { }, () => { });

                        Chilligames_SDK.API_Client.Send_Score_to_leader_board(new Req_send_score { Leader_board_name = "Venomic_Top_Player", Score = PlayerPrefs.GetInt("Level"), _id = _id }, () => { });

                        Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion { _id = _id, Leader_board_name = "Venomic_Top_Player" }, result =>
                        {
                            Text_ranking_number.text = result;
                        }, err => { });
                    }
                }
                else if (Result_login == "0")
                {

                    print("Code not login here");
                }
            }, ERR =>
            {
                Offline_mode.SetActive(true);

                for (int i = 0; i < object_hide_offline.Length; i++)
                {
                    object_hide_offline[i].gameObject.SetActive(false);
                }

            });
        }
    }
    private void Start()
    {
        /*....offline....*/
        BTN_Play_offline.onClick.AddListener(() =>
        {
            Missions = Instantiate(Raw_model_mission_offline, Place_missons);
            Missions.GetComponent<Raw_model_game_play_offline>().Change_value(PlayerPrefs.GetInt("Level"), gameObject);
            Player.Cam.Move_camera(new Vector3(10, 10, 0));

            //soundcontrol
            Menu.Play_music_GamePlay();
        });
        BTN_return_online.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        BTN_return_online.onClick.AddListener(() =>
        {
            Chilligames_SDK.API_Client.Quick_register(result =>
            {
                print("Register_new_user");
                PlayerPrefs.SetString("_id", result._id);
            }, err =>
            {
                print(err);
            });

        });

    }
    private void OnDisable()
    {
        if (Server_fild != null)
        {
            for (int i = 0; i < Server_fild.Length; i++)
            {
                Destroy(Server_fild[i]);
            }
        }
    }


    public void Update()
    {
        Text_level_number.text = PlayerPrefs.GetInt("Level").ToString();
        Text_Coin_number.text = PlayerPrefs.GetInt("Coin").ToString();
    }

    /// <summary>
    /// mission insert mikone vaghti offline hast player
    /// </summary>
    public void Insert_mission_Offline(Vector3 Pos_mission)
    {
        Missions = Instantiate(Raw_model_mission_offline, Place_missons);
        Missions.transform.position = new Vector3(Pos_mission.x + 10, Pos_mission.y + 10, 0);
        Player.Cam.Move_camera(Missions.transform.position);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        Missions.GetComponent<Raw_model_game_play_offline>().Change_value(PlayerPrefs.GetInt("Level"), gameObject);
        Player.Cam.Change_color();
    }


    /// <summary>
    /// send data to server after mission end
    /// </summary>
    public void Send_data_to_server()
    {
        if (PlayerPrefs.GetString("_id").Length > 2)
        {
            Chilligames_SDK.API_Client.Send_Data_user(new Req_send_data
            {
                _id = _id,
                Name_app = "Venomic",
                Data_user = ChilligamesJson.SerializeObject(new Entity_Player
                {
                    Freeze = PlayerPrefs.GetInt("Freeze"),
                    Minus = PlayerPrefs.GetInt("Minuse"),
                    Chance = PlayerPrefs.GetInt("Chance"),
                    Delete = PlayerPrefs.GetInt("Delete"),
                    Reset = PlayerPrefs.GetInt("Reset"),
                    Level = PlayerPrefs.GetInt("Level"),
                })

            }, () => { }, () => { });

            Chilligames_SDK.API_Client.Sync_coin_with_server(new Req_sync_coin_with_server { Coin = PlayerPrefs.GetInt("Coin"), _id = _id }, () => { }, err => { });
        }
    }

    public class Entity_Player
    {
        public int Level = 0;
        public int Freeze = 0;
        public int Minus = 0;
        public int Delete = 0;
        public int Chance = 0;
        public int Reset = 0;
    }
}