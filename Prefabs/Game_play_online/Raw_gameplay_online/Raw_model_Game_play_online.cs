using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class Raw_model_Game_play_online : MonoBehaviour
{
    public GameObject Raw_model_BTN_mission;

    public TextMeshProUGUI Text_name_server;
    public TextMeshProUGUI Text_player_number;
    public TextMeshProUGUI Text_Ranking_number;
    public TextMeshProUGUI Text_Coin_number;
    public TextMeshProUGUI Text_level_number;
    public TextMeshProUGUI Text_level_remine_number;
    public TextMeshProUGUI Text_stars_number;
    public TextMeshProUGUI Text_Freeze_number;
    public TextMeshProUGUI Text_mines_number;
    public TextMeshProUGUI Text_delet_number;
    public TextMeshProUGUI Text_chance_number;
    public TextMeshProUGUI Text_reset_number;

    public Button BTN_Freeze;
    public Button BTN_mines;
    public Button BTN_delete;
    public Button BTN_reset;

    public Button Leader_server;
    public Button Zoomback;

    public RawImage Image_signal;

    public Transform Place_BTNs;

    int? Level_remine;
    int? Level;
    int show_hint = 0;
    GameObject[] BTNS;
    GameObject Parent;

    public int[] Count_patern;//remove public 
    public int[] Tap_Patern;//remove public
    public int Result_mission;
    public int star = 3;


    public void Change_value(string Name_server, int? level_remine, int? level, GameObject Parent)
    {
        Text_name_server.text = Name_server;
        Text_level_number.text = level.ToString();
        Text_level_remine_number.text = level_remine.ToString();

        Level_remine = level_remine;
        Level = level;

        this.Parent = Parent;
    }

    public void Start()
    {
        int? Level_Easy = Level_remine / 3;
        int? Level_mediom = Level_Easy + 4;
        int? Level_hard = Level_mediom + 1;

        if (Level < Level_Easy)
        {
            int count = Random.Range(2, 4);
            BTNS = new GameObject[count];
            for (int i = 0; i < count; i++)
            {
                BTNS[i] = Instantiate(Raw_model_BTN_mission, Place_BTNs);
            }
        }
        else if (Level <= Level_mediom)
        {
            int count = Random.Range(2, 5);
            BTNS = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                BTNS[i] = Instantiate(Raw_model_BTN_mission, Place_BTNs);
            }
        }
        else if (Level >= Level_hard)
        {
            int count = Random.Range(2, 6);
            BTNS = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                BTNS[i] = Instantiate(Raw_model_BTN_mission, Place_BTNs);
            }
        }

        for (int i = 0; i < BTNS.Length; i++)
        {
            BTNS[i].AddComponent<BTN>();

        }

        Tap_Patern = new int[BTNS.Length];
        Count_patern = new int[BTNS.Length];


        BTN_Freeze.onClick.AddListener(() =>
        {

            foreach (var BTN in BTNS)
            {
                BTN.GetComponent<BTN>().Freeze_time = 0.005f;
            }

            if (Parent.GetComponent<Raw_model_fild_server_play>().Freeze >= 1)
            {
                Parent.GetComponent<Raw_model_fild_server_play>().Freeze -= 1;
            }
            else
            {
                print("code not freeze here");
            }
        });

        BTN_mines.onClick.AddListener(() =>
        {
            foreach (var Btn in BTNS)
            {
                if (Btn.GetComponent<BTN>().Count > 1)
                {
                    Btn.GetComponent<BTN>().Count -= 1;
                    print("animation mines");
                }
                else
                {
                    print("cant minuse here");
                }
            }

            Parent.GetComponent<Raw_model_fild_server_play>().Mines -= 1;

            for (int i = 0; i < BTNS.Length; i++)
            {
                Count_patern[i] = BTNS[i].GetComponent<BTN>().Count;
            }


        });

        BTN_delete.onClick.AddListener(() =>
        {
            if (BTNS.Length-1 >= 1)
            {
                Destroy(BTNS[BTNS.Length - 1]);

                GameObject[] New_BTN = new GameObject[BTNS.Length - 1];

                for (int i = 0; i < New_BTN.Length; i++)
                {
                    if (BTNS[i] != null)
                    {
                        New_BTN[i] = BTNS[i];
                    }
                }
                BTNS = new GameObject[New_BTN.Length];
                BTNS = New_BTN;

                int[] new_Count = new int[BTNS.Length];

                int[] new_tap_patern = new int[BTNS.Length];

                for (int i = 0; i < new_Count.Length; i++)
                {
                    new_Count[i] = Count_patern[i];

                }

                for (int i = 0; i < new_tap_patern.Length; i++)
                {
                    new_tap_patern[i] = Tap_Patern[i];
                }

                Count_patern = new_Count;

                Tap_Patern = new_tap_patern;


            }
        });

    }

    private void Update()
    {
        Text_stars_number.text = Parent.GetComponent<Raw_model_fild_server_play>().Star_missions.ToString();
        Text_player_number.text = Parent.GetComponent<Raw_model_fild_server_play>().Player_.ToString();
        Text_Freeze_number.text = Parent.GetComponent<Raw_model_fild_server_play>().Freeze.ToString();
        Text_mines_number.text = Parent.GetComponent<Raw_model_fild_server_play>().Mines.ToString();
        Text_delet_number.text = Parent.GetComponent<Raw_model_fild_server_play>().Delete.ToString();
        Text_chance_number.text = Parent.GetComponent<Raw_model_fild_server_play>().Chance.ToString();
        Text_reset_number.text = Parent.GetComponent<Raw_model_fild_server_play>().Reset.ToString();




        if (Player.cam.transform.position == transform.position && show_hint == 0)
        {
            foreach (var BTN in BTNS)
            {
                BTN.GetComponent<BTN>().show_hint = show_hint;
            }

            for (int i = 0; i < BTNS.Length; i++)
            {
                Count_patern[i] = BTNS[i].GetComponent<BTN>().Count;
            }
            show_hint = 1;
        }
        else if (Player.cam.transform.position == transform.position)
        {

            for (int i = 0; i < BTNS.Length; i++)
            {
                Tap_Patern[i] = BTNS[i].GetComponent<BTN>().click;
            }


            for (int i = 0; i < BTNS.Length; i++)
            {
                if (Tap_Patern[i] == Count_patern[i])
                {
                    Result_mission = 1;
                }
                else
                {
                    Result_mission = 0;
                    break;
                }

                if (Result_mission==1)
                {
                    for (int b =0 ; b > BTNS.Length; b++)
                    {
                        if (Tap_Patern[b]!=Count_patern[b])
                        {
                            Result_mission = 0;
                            break;
                        }
                      
                    }

                }

            }


            if (Result_mission == 1)
            {
                print("pass");
               Player.Cam.Move_camera(new Vector3(transform.position.x + 10, transform.position.y + 10, 0));
            }


        }

    }


    public class BTN : MonoBehaviour
    {
        public TextMeshProUGUI Text_BTN
        {
            get
            {
                return GetComponentInChildren<TextMeshProUGUI>();
            }

        }

        public float Freeze_time = 0.02f;

        public int show_hint = 1;
        public int show_off = 0;
        public int show_on = 1;
        public int Count;
        public int click;

        private void Start()
        {
            Count = Random.Range(2, 10);

            Text_BTN.text = Count.ToString();
            GetComponent<Button>().onClick.AddListener(() =>
            {
                click += 1;
                if (click > Count)
                {
                    GetComponentInParent<Raw_model_Game_play_online>().Parent.GetComponent<Raw_model_fild_server_play>().Star_missions -= 1;

                    click = Count;
                }
                Text_BTN.text = click.ToString();


            });

        }

        public void Update()
        {
            if (show_hint == 0)
            {
                if (Text_BTN.transform.localScale != Vector3.zero && show_off == 0)
                {
                    Text_BTN.transform.localScale = Vector3.MoveTowards(Text_BTN.transform.localScale, Vector3.zero, Freeze_time);
                }
                else
                {
                    show_off = 1;
                    show_on = 0;
                }

                if (Text_BTN.transform.localScale != Vector3.one && show_on == 0)
                {
                    Text_BTN.text = 0.ToString();
                    Text_BTN.transform.localScale = Vector3.MoveTowards(Text_BTN.transform.localScale, Vector3.one, 0.5f);
                }
                else if (Text_BTN.transform.localScale == Vector3.one)
                {
                    show_on = 1;
                    show_hint = 1;
                }

            }

        }



    }


}
