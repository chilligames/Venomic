using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Raw_model_game_play_online : MonoBehaviour
{
    public GameObject Raw_model_BTN;
    public ParticleSystem Partical_freeze;
    public ParticleSystem Partical_Minus;
    public ParticleSystem Partical_delete;
    public ParticleSystem Partical_chance;
    public ParticleSystem Partical_reset;
    public ParticleSystem Partical_reject;

    string _id;
    string _id_server;

    string Name_server;
    int Count_Player;
    int Rank_player;
    int Coin_server;
    int Totall_level;
    int Level;

    int Freeze;
    int Minues;
    int Delete;
    int Chance;
    int Reset;

    public TextMeshProUGUI Text_Name_server;
    public TextMeshProUGUI Text_player_number;
    public TextMeshProUGUI Text_rank_player;
    public TextMeshProUGUI Text_coin_server_number;
    public TextMeshProUGUI Text_total_level;
    public TextMeshProUGUI Text_level;
    public TextMeshProUGUI Text_freeze_number;
    public TextMeshProUGUI Text_minues_number;
    public TextMeshProUGUI Text_delete_number;
    public TextMeshProUGUI Text_Chance_number;
    public TextMeshProUGUI Text_reset_number;

    public Button BTN_Freeze;
    public Button BTN_Minus;
    public Button BTN_Delete;
    public Button BTN_Reset;
    public Button BTN_Leave_mission;

    public Transform Place_BTNS;
    public GameObject Parent;

    GameObject[] BTNS;

    public int[] Count_map;
    public int[] tap_map;

    int Mission_pass;
    public void Change_value(string _id, string name_server, int coin_server, int totall_level, int level, int freeze, int minus, int delete, int chance, int reset, string _id_server, GameObject parent)
    {
        Parent = parent;
        Name_server = name_server;
        Count_Player = 99;
        Rank_player = 99;
        Coin_server = coin_server;
        Totall_level = totall_level;
        Level = level;
        Freeze = freeze;
        Minues = minus;
        Delete = delete;
        Chance = chance;
        Reset = reset;
        this._id = _id;
        this._id_server = _id_server;

        Text_Name_server.text = Name_server;
        Text_total_level.text = Totall_level.ToString();
    }

    private void Start()
    {
        //level QA
        if (Level <= 10)
        {
            print("level_easy");
            int Count = Random.Range(1, 5);
            BTNS = new GameObject[Count];
            for (int i = 0; i < BTNS.Length; i++)
            {
                BTNS[i] = Instantiate(Raw_model_BTN, Place_BTNS);
            }
        }
        else if (Level >= 11 && Level <= 20)
        {
            print("Level_mediom");
            int Count = Random.Range(2, 7);
            BTNS = new GameObject[Count];
            for (int i = 0; i < BTNS.Length; i++)
            {
                BTNS[i] = Instantiate(Raw_model_BTN, Place_BTNS);
            }
        }
        else if (Level >= 21 && Level <= 40)
        {
            print("level_hard");
            int Count = Random.Range(3, 8);
            BTNS = new GameObject[Count];
            for (int i = 0; i < BTNS.Length; i++)
            {
                BTNS[i] = Instantiate(Raw_model_BTN, Place_BTNS);
            }
        }
        else if (Level >= 41)
        {
            print("expert");
            int Count = Random.Range(3, 9);
            BTNS = new GameObject[Count];
            for (int i = 0; i < Count; i++)
            {
                BTNS[i] = Instantiate(Raw_model_BTN, Place_BTNS);
            }
        }

        //insert value to btn
        for (int i = 0; i < BTNS.Length; i++)
        {
            BTNS[i].AddComponent<BTN>();
            BTNS[i].GetComponent<BTN>().Change_value(gameObject);
        }

        BTN_Freeze.onClick.AddListener(() =>
        {
            BTN_Freeze.GetComponent<AudioSource>().Play();
            if (Freeze >= 1)
            {
                foreach (var BTN in BTNS)
                {
                    BTN.GetComponent<BTN>().Freeze_time = 0.005f;
                }
                Freeze -= 1;
                Partical_freeze.Play();
            }
            else
            {
                Partical_reject.Play();
            }
        });

        BTN_Minus.onClick.AddListener(() =>
        {
            BTN_Minus.GetComponent<AudioSource>().Play();
            foreach (var BTN in BTNS)
            {
                if (BTN.GetComponent<BTN>().Count > 1)
                {
                    if (BTN.GetComponent<BTN>().Count - 1 < BTN.GetComponent<BTN>().Tap)
                    {
                        BTN.GetComponent<BTN>().Tap -= 1;
                    }

                    BTN.GetComponent<BTN>().Count -= 1;
                    Minues -= 1;
                    Partical_Minus.Play();
                }
                else
                {
                    Partical_reject.Play();
                }
            }

        });

        BTN_Delete.onClick.AddListener(() =>
        {
            BTN_Delete.GetComponent<AudioSource>().Play();
            if (Delete >= 1 && BTNS.Length > 1)
            {
                Destroy(BTNS[BTNS.Length - 1]);
                Delete -= 1;
                GameObject[] New_BTNS = new GameObject[BTNS.Length - 1];

                for (int i = 0; i < New_BTNS.Length; i++)
                {
                    New_BTNS[i] = BTNS[i];
                }
                BTNS = New_BTNS;
                Partical_delete.Play();
            }
            else
            {
                Partical_reject.Play();
                print("Cant delete here");
            }
        });

        BTN_Reset.onClick.AddListener(() =>
        {
            BTN_Reset.GetComponent<AudioSource>().Play();
            if (Reset >= 1)
            {
                Reset -= 1;
                for (int i = 0; i < BTNS.Length; i++)
                {
                    Destroy(BTNS[i]);
                }
                Partical_reset.Play();
                Start();

            }
            else
            {
                Partical_reject.Play();
            }
        });

        BTN_Leave_mission.onClick.AddListener(() =>
        {
            //camera action
            Player.Cam.Move_Camera_To_Menu();

            //music controller
            Menu.Play_music_menu();

            //destroy game play
            Destroy(Parent.GetComponent<Raw_model_fild_server_play>().Missions);
        });

        Recive_data();


        void Recive_data()
        {
            //recive count player
            Chilligames_SDK.API_Client.Recive_data_server<Panel_Servers.Model_server>(new Chilligames.SDK.Model_Client.Req_data_server { Name_app = "Venomic", _id_server = _id_server }, result =>
                  {
                      Count_Player = (int)ChilligamesJson.DeserializeObject<Panel_Servers.Model_server.Setting_servers>(result.Setting.ToString()).Player;
                  }, ERR => { });

            //recive coin server
            Chilligames_SDK.API_Client.Recive_data_server<Panel_Servers.Model_server>(new Req_data_server { _id_server = _id_server, Name_app = "Venomic" }, result =>
            {
                Coin_server = (int)ChilligamesJson.DeserializeObject<Panel_Servers.Model_server.Setting_servers>(result.Setting.ToString()).Coine;
            }, err => { });

            // recive ranking 
            Chilligames_SDK.API_Client.Recive_data_server<Panel_Servers.Model_server>(new Req_data_server { Name_app = "Venomic", _id_server = _id_server }, result =>
                  {
                      var leader_board = ChilligamesJson.DeserializeObject<Panel_Servers.Model_server.Setting_servers>(result.Setting.ToString()).Leader_board;

                      int[] score = new int[leader_board.Length];

                      for (int i = 0; i < leader_board.Length; i++)
                      {
                          score[i] = (int)ChilligamesJson.DeserializeObject<Raw_model_info_server.Deserilies_leader_board>(leader_board[i].ToString()).Score;
                      }

                      int curent_score = Level + Freeze + Minues + Delete + Chance + Reset;


                      for (int i = 0; i < leader_board.Length; i++)
                      {
                          if (curent_score < score[i])
                          {
                              Rank_player = i;
                          }
                      }
                  }, err => { });
        }
    }

    void Update()
    {
        Text_player_number.text = Count_Player.ToString();
        Text_rank_player.text = Rank_player.ToString();
        Text_coin_server_number.text = Coin_server.ToString();
        Text_level.text = Level.ToString();
        Text_freeze_number.text = Freeze.ToString();
        Text_minues_number.text = Minues.ToString();
        Text_delete_number.text = Delete.ToString();
        Text_Chance_number.text = Chance.ToString();
        Text_reset_number.text = Reset.ToString();

        Count_map = new int[BTNS.Length];
        for (int i = 0; i < BTNS.Length; i++)
        {
            Count_map[i] = BTNS[i].GetComponent<BTN>().Count;

        }

        tap_map = new int[BTNS.Length];

        for (int i = 0; i < BTNS.Length; i++)
        {
            tap_map[i] = BTNS[i].GetComponent<BTN>().Tap;
        }

        //cheack pass mission
        for (int i = 0; i < BTNS.Length; i++)
        {
            if (tap_map[i] == Count_map[i])
            {
                Mission_pass = 1;
            }
            else
            {
                Mission_pass = 0;
                break;
            }

            if (Mission_pass == 1)
            {
                for (int a = 0; a < BTNS.Length; a++)
                {
                    if (tap_map[a] == Count_map[i])
                    {
                        Mission_pass = 1;
                    }
                    else
                    {
                        Mission_pass = 0;
                    }
                }
            }
        }


        if (Mission_pass == 1)
        {
            Player.Cam.Change_color();
            if (Level + 1 > Totall_level)
            {
                //insert new mission
                Parent.GetComponent<Raw_model_fild_server_play>().Missions = Instantiate(Parent.GetComponent<Raw_model_fild_server_play>().End_Result_mission, Parent.GetComponent<Raw_model_fild_server_play>().Place_mission);
                Parent.GetComponent<Raw_model_fild_server_play>().Missions.transform.position = new Vector3(transform.position.x + 10, transform.position.y + 10, 0);
                int average = Totall_level + Freeze + Minues + Delete + Chance + Reset;
                Parent.GetComponent<Raw_model_fild_server_play>().Missions.AddComponent<End_mission>().Change_value(Name_server, Totall_level, Freeze, Minues, Delete, Chance, Reset, average, _id, _id_server, Parent);

                //effect camera
                Player.Cam.Move_camera(new Vector3(transform.position.x + 10, transform.position.y + 10, 0));
              

                //destroy gameplay
                Destroy(gameObject);
            }
            else
            {
                Parent.GetComponent<Raw_model_fild_server_play>().Missions = Instantiate(Parent.GetComponent<Raw_model_fild_server_play>().Raw_model_mission_online, Parent.GetComponent<Raw_model_fild_server_play>().Place_mission);
                Parent.GetComponent<Raw_model_fild_server_play>().Missions.GetComponent<Raw_model_game_play_online>().Change_value(_id, Name_server, Coin_server, Totall_level, Level + 1, Freeze, Minues, Delete, Chance, Reset, _id_server, Parent);
                Parent.GetComponent<Raw_model_fild_server_play>().Missions.transform.position = new Vector3(transform.position.x + 10, transform.position.y + 10, 0);
                Player.Cam.Move_camera(new Vector3(transform.position.x + 10, transform.position.y + 10, 0));
                Destroy(gameObject);
            }
        }

    }



    class BTN : MonoBehaviour
    {
        Button BTN_click
        {
            get
            {
                return GetComponent<Button>();
            }
        }
        TextMeshProUGUI Text_BTN
        {
            get
            {
                return GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        public float Freeze_time = 0.01f;

        public int Count;
        public int Tap;

        int show_hint = 0;
        int show_off = 0;
        internal void Change_value(GameObject Parent)
        {
            Count = Random.Range(1, 9);

            Text_BTN.text = Count.ToString();

            BTN_click.onClick.AddListener(() =>
            {
                //audio controler
                GetComponent<AudioSource>().Play();

                //work
                if (Tap < Count)
                {
                    Tap += 1;
                    Text_BTN.text = Tap.ToString();
                }
                else
                {
                    if (Parent.GetComponent<Raw_model_game_play_online>().Chance >= 1)
                    {
                        Parent.GetComponent<Raw_model_game_play_online>().Chance -= 1;
                        Parent.GetComponent<Raw_model_game_play_online>().Partical_chance.Play();
                    }
                    else
                    {
                        Parent.GetComponent<Raw_model_game_play_online>().Level -= 1;
                        Parent.GetComponent<Raw_model_game_play_online>().Partical_reject.Play();
                    }
                }
            });
        }

        private void Update()
        {
            if (show_hint == 0 && Text_BTN.transform.localScale != Vector3.zero)
            {
                Text_BTN.transform.localScale = Vector3.MoveTowards(Text_BTN.transform.localScale, Vector3.zero, Freeze_time);
            }
            else
            {
                show_hint = 1;
                show_off = 1;
            }

            if (show_off == 1 && Text_BTN.transform.localScale != Vector3.one)
            {
                Text_BTN.transform.localScale = Vector3.MoveTowards(Text_BTN.transform.localScale, Vector3.one, 0.5f);
                Text_BTN.text = Tap.ToString();
                if (Text_BTN.transform.localScale == Vector3.one)
                {
                    show_off = 0;
                }
            }
            else if (show_hint == 1 && show_off == 1)
            {
                Text_BTN.text = Tap.ToString();
            }
        }

    }

    class End_mission : MonoBehaviour
    {
        TextMeshProUGUI Text_name_server
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

        TextMeshProUGUI Text_leve_number
        {
            get
            {
                TextMeshProUGUI Text_level_number = null;
                foreach (var text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (text.name == "TNS")
                    {
                        Text_level_number = text;
                    }
                }
                return Text_level_number;
            }
        }
        TextMeshProUGUI Text_Freeze_number
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

        TextMeshProUGUI Text_minues_number
        {
            get
            {
                TextMeshProUGUI Text_minuse_number = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Text.name == "TMN")
                    {
                        Text_minuse_number = Text;
                    }
                }
                return Text_minuse_number;
            }
        }

        TextMeshProUGUI Text_delete_number
        {
            get
            {
                TextMeshProUGUI Text_delete = null;
                foreach (var text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (text.name == "TDN")
                    {
                        Text_delete = text;
                    }
                }
                return Text_delete;
            }
        }

        TextMeshProUGUI Text_Chance_number
        {
            get
            {
                TextMeshProUGUI Text_chance_number = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Text.name == "TCN")
                    {
                        Text_chance_number = Text;
                    }
                }
                return Text_chance_number;
            }
        }

        TextMeshProUGUI Text_reset_number
        {
            get
            {
                TextMeshProUGUI Text_reset_number = null;
                foreach (var Text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Text.name == "TRN")
                    {
                        Text_reset_number = Text;
                    }
                }
                return Text_reset_number;
            }
        }

        TextMeshProUGUI Text_average
        {
            get
            {
                TextMeshProUGUI Text_avrege = null;
                foreach (var text in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (text.name == "TAN")
                    {
                        Text_avrege = text;
                    }
                }
                return Text_avrege;
            }
        }

        Button BTN_Send_score_to_server
        {
            get
            {
                Button BTN_send_score = null;
                foreach (var BTN in GetComponentsInChildren<Button>())
                {
                    if (BTN.name == "BSSTS")
                    {
                        BTN_send_score = BTN;
                    }
                }
                return BTN_send_score;
            }
        }

        Button BTN_leave_server
        {

            get
            {
                Button BTN_Leave_server = null;
                foreach (var BTN in GetComponentsInChildren<Button>())
                {
                    if (BTN.name == "BLS")
                    {
                        BTN_Leave_server = BTN;
                    }
                }
                return BTN_Leave_server;
            }
        }


        public void Change_value(string name_server, int level, int freeze, int minuse, int delete, int chance, int reset, int avrege, string _id, string _id_server, GameObject parent)
        {
            Text_name_server.text = name_server;
            Text_leve_number.text = level.ToString();
            Text_Freeze_number.text = freeze.ToString();
            Text_minues_number.text = minuse.ToString();
            Text_delete_number.text = delete.ToString();
            Text_Chance_number.text = chance.ToString();
            Text_reset_number.text = reset.ToString();
            Text_average.text = avrege.ToString();

            BTN_leave_server.onClick.AddListener(() =>
            {
                Destroy(parent.GetComponent<Raw_model_fild_server_play>().Missions);
                Player.Cam.Move_Camera_To_Menu();
            });
            BTN_Send_score_to_server.onClick.AddListener(() =>
            {

                Raw_model_info_server.Deserilies_leader_board leader_baord_model = new Raw_model_info_server.Deserilies_leader_board { ID = _id, Score = avrege };
                Chilligames_SDK.API_Client.Push_data_to_server_fild(new Req_push_data_to_server { _id_server = _id_server, Name_app = "Venomic", Pipe_line_data = "Setting.Leader_board", Inject_data = leader_baord_model }, () =>
                {
                    Destroy(parent.GetComponent<Raw_model_fild_server_play>().Missions);
                    Player.Cam.Move_Camera_To_Menu();

                    //audio controler
                    Menu.Play_music_menu();
                }, null);



            });
        }
    }

}
