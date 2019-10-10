using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// playerprefe:
/// 1: Freeze
/// 2: Minuse
/// 3: Delete
/// 4: Chance
/// 5: Reset
/// 6: Coin
/// </summary>
public class Raw_model_game_play_offline : MonoBehaviour
{

    public GameObject Raw_BTN_mission;

    public TextMeshProUGUI Text_level_number;
    public TextMeshProUGUI Text_freeze_number;
    public TextMeshProUGUI Text_minus_number;
    public TextMeshProUGUI Text_delete_number;
    public TextMeshProUGUI Text_chance_number;
    public TextMeshProUGUI Text_reset_number;
    public TextMeshProUGUI Text_coin_number;



    public Button Leave_mission;
    public Button BTN_Freeze;
    public Button BTN_Minese;
    public Button BTN_Delete;
    public Button BTN_Reset;

    public Transform Place_BTN;


    public int Level;


    GameObject[] BTNS_mission;
    GameObject Parent;
    int[] Count_map;
    int[] Tap_map;

    int pass_mission = 0;

    public void Change_value(int level, GameObject parent)
    {
        Level = level;
        Parent = parent;

        //spawener btn
        if (Level <= 10)
        {
            print("P1");
            int Count_btn = Random.Range(1, 4);
            BTNS_mission = new GameObject[Count_btn];

            for (int i = 0; i < Count_btn; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }else if (level>=11&&level<20)
        {
            print("P2");
            int Count_btn = Random.Range(2, 5);
            BTNS_mission = new GameObject[Count_btn];
            for (int i = 0; i < Count_btn; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level>=21&&level<=40)
        {
            print("P3");
            int count_btn = Random.Range(2, 6);
            BTNS_mission = new GameObject[count_btn];
            for (int i = 0; i < BTNS_mission.Length; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level>=41&&level<=60)
        {
            print("P4");
            int Count_btn = Random.Range(3, 7);
            BTNS_mission = new GameObject[Count_btn];
            for (int i = 0; i < BTNS_mission.Length; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level>=61&&level<=100)
        {
            print("P5");
            int count_btn = Random.Range(3, 8);
            BTNS_mission = new GameObject[count_btn];
            for (int i = 0; i < count_btn; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level >=101 && level <= 200)
        {
            print("P6");
            int Count_BTN = Random.Range(4, 10);
            BTNS_mission = new GameObject[Count_BTN];
            for (int i = 0; i < Count_BTN; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level >= 201)
        {
            print("P7");
            int Count_BTN = Random.Range(3, 11);
            BTNS_mission = new GameObject[Count_BTN];
            for (int i = 0; i < Count_BTN; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }

        for (int i = 0; i < BTNS_mission.Length; i++)
        {
            BTNS_mission[i].AddComponent<BTN>();
            BTNS_mission[i].GetComponent<BTN>().Change_value(transform, gameObject);
        }
    }
    private void Start()
    {
        
        BTN_Reset.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Reset") >= 1)
            {
                for (int i = 0; i < BTNS_mission.Length; i++)
                {
                    Destroy(BTNS_mission[i]);
                }

                PlayerPrefs.SetInt("Reset", PlayerPrefs.GetInt("Reset") - 1);
                Change_value(Level, Parent);
            }
            else
            {
                print("Cant reset here");
            }

        });

        BTN_Freeze.onClick.AddListener(() =>
        {
            foreach (var BTNS in BTNS_mission)
            {
                BTNS.GetComponent<BTN>().Time_animation = 0.003f;
            }

        });

        BTN_Minese.onClick.AddListener(() =>
        {
            foreach (var BTNS in BTNS_mission)
            {
                if (BTNS.GetComponent<BTN>().Count > 1 && PlayerPrefs.GetInt("Minuse") >= 1)
                {
                    if (BTNS.GetComponent<BTN>().Count - 1 < BTNS.GetComponent<BTN>().Tap)
                    {
                        BTNS.GetComponent<BTN>().Tap -= 1;
                    }
                    BTNS.GetComponent<BTN>().Count = BTNS.GetComponent<BTN>().Count - 1;
                    PlayerPrefs.SetInt("Minuse", PlayerPrefs.GetInt("Minuse") - 1);
                }
                else
                {
                    print("Cant mines btn here");
                }
            }

        });

        BTN_Delete.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Delete") >= 1 && BTNS_mission.Length - 1 > 1)
            {
                Destroy(BTNS_mission[BTNS_mission.Length - 1]);

                GameObject[] BTN_new_mission = new GameObject[BTNS_mission.Length - 1];
                for (int i = 0; i < BTN_new_mission.Length; i++)
                {
                    if (BTNS_mission[i] != null)
                    {
                        BTN_new_mission[i] = BTNS_mission[i];
                    }
                }

                BTNS_mission = BTN_new_mission;
                PlayerPrefs.SetInt("Delete", PlayerPrefs.GetInt("Delete") - 1);
            }
            else
            {
                print("Cod cant delete here");
            }
        });

        Leave_mission.onClick.AddListener(() =>
        {
            Player.Cam.Move_Camera_To_Menu();

            Parent.GetComponent<Panel_home>().Send_data_to_server();

            Destroy(Parent.GetComponent<Panel_home>().Missions);
        });
    }
    private void Update()
    {
        Text_level_number.text = PlayerPrefs.GetInt("Level").ToString();
        Text_freeze_number.text = PlayerPrefs.GetInt("Freeze").ToString();
        Text_minus_number.text = PlayerPrefs.GetInt("Minuse").ToString();
        Text_delete_number.text = PlayerPrefs.GetInt("Delete").ToString();
        Text_chance_number.text = PlayerPrefs.GetInt("Chance").ToString();
        Text_reset_number.text = PlayerPrefs.GetInt("Reset").ToString();
        Text_coin_number.text = PlayerPrefs.GetInt("Coin").ToString();

        Count_map = new int[BTNS_mission.Length];
        for (int i = 0; i < BTNS_mission.Length; i++)
        {
            Count_map[i] = BTNS_mission[i].GetComponent<BTN>().Count;
        }

        Tap_map = new int[BTNS_mission.Length];
        for (int i = 0; i < BTNS_mission.Length; i++)
        {
            Tap_map[i] = BTNS_mission[i].GetComponent<BTN>().Tap;
        }

        for (int i = 0; i < BTNS_mission.Length; i++)
        {
            if (Tap_map[i] != Count_map[i])
            {
                pass_mission = 0;
                break;
            }
            else if (Tap_map[i] == Count_map[i])
            {
                pass_mission = 1;
                for (int a = 0; a < BTNS_mission.Length; a++)
                {
                    if (Tap_map[i] == Count_map[i])
                    {
                        pass_mission = 1;
                    }
                    else
                    {
                        pass_mission = 0;
                    }
                }
            }
        }


        if (pass_mission == 1)
        {
            Parent.GetComponent<Panel_home>().Insert_mission_Offline(transform.position);
            int rand_bounce = Random.Range(1, 6);

            int Rand_coin = Random.Range(1, 10);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + Rand_coin);

            int rand_count_bounce = Random.Range(1, 6);

            switch (rand_bounce)
            {
                case 1:
                    {
                        PlayerPrefs.SetInt("Freeze", PlayerPrefs.GetInt("Freeze") + rand_count_bounce);
                    }
                    break;
                case 2:
                    {
                        PlayerPrefs.SetInt("Minuse", PlayerPrefs.GetInt("Munuse") + rand_count_bounce);
                    }
                    break;
                case 3:
                    {
                        PlayerPrefs.SetInt("Delete", PlayerPrefs.GetInt("Delete") + rand_count_bounce);
                    }
                    break;
                case 4:
                    {
                        PlayerPrefs.SetInt("Chance", PlayerPrefs.GetInt("Chance") + rand_count_bounce);
                    }
                    break;
                case 5:
                    {
                        PlayerPrefs.SetInt("Reset", PlayerPrefs.GetInt("Reset") + rand_count_bounce);
                    }
                    break;
            }

            Destroy(gameObject);
        }
    }


    class BTN : MonoBehaviour
    {
        TextMeshProUGUI Text_BTN
        {
            get
            {
                return GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        Button BTN_click
        {
            get
            {
                return GetComponent<Button>();
            }
        }

        Transform Place_mission;
        public float Time_animation = 0.01f;

        public int Count;
        public int Tap;
        public int show_hint = 0;
        public int show_off = 1;

        public void Change_value(Transform Place_mission, GameObject Parent)
        {
            this.Place_mission = Place_mission;

            Count = Random.Range(1, 9);
            Text_BTN.text = Count.ToString();
            BTN_click.onClick.AddListener(() =>
            {
                if (Tap + 1 > Count)
                {
                    if (PlayerPrefs.GetInt("Chance") < 1)
                    {
                        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") - 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Chance", PlayerPrefs.GetInt("Chance") - 1);
                    }
                }
                else
                {
                    Tap += 1;
                    Text_BTN.text = Tap.ToString();
                }

            });
        }


        public void Update()
        {
            if (Player.cam.transform.position == Place_mission.position && show_hint == 0)
            {
                if (Text_BTN.transform.localScale != Vector3.zero)
                {
                    Text_BTN.transform.localScale = Vector3.MoveTowards(Text_BTN.transform.localScale, Vector3.zero, Time_animation);
                }
                else
                {
                    show_hint = 1;
                    show_off = 0;
                }
            }
            else if (Player.cam.transform.position == Place_mission.position && show_off == 0)
            {
                Text_BTN.text = Tap.ToString();
                Text_BTN.transform.localScale = Vector3.MoveTowards(Text_BTN.transform.localScale, Vector3.one, 0.5f);
                if (Text_BTN.transform.localScale == Vector3.one)
                {
                    show_off = 1;
                }
            }
            else if (show_hint == 1 && show_off == 1)
            {
                Text_BTN.text = Tap.ToString();
            }
        }

    }


}