﻿using TMPro;
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
/// 7: Vibrator
/// 8: Top_Score
/// </summary>
public class Raw_model_game_play_offline : MonoBehaviour
{

    public GameObject Raw_BTN_mission;
    public GameObject Quick_offer;


    public ParticleSystem Partical_Freeze;
    public ParticleSystem Partical_minuse;
    public ParticleSystem Partical_delete;
    public ParticleSystem Parical_Chance;
    public ParticleSystem Partical_Reset;
    public ParticleSystem Partica_reject;


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

    public AudioSource music_reject;

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
            int Count_btn = Random.Range(1, 4);
            BTNS_mission = new GameObject[Count_btn];

            for (int i = 0; i < Count_btn; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level >= 11 && level < 20)
        {
            int Count_btn = Random.Range(2, 5);
            BTNS_mission = new GameObject[Count_btn];
            for (int i = 0; i < Count_btn; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level >= 21 && level <= 40)
        {
            int count_btn = Random.Range(2, 6);
            BTNS_mission = new GameObject[count_btn];
            for (int i = 0; i < BTNS_mission.Length; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level >= 41 && level <= 60)
        {
            int Count_btn = Random.Range(3, 7);
            BTNS_mission = new GameObject[Count_btn];
            for (int i = 0; i < BTNS_mission.Length; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level >= 61 && level <= 100)
        {
            print("P5");
            int count_btn = Random.Range(3, 8);
            BTNS_mission = new GameObject[count_btn];
            for (int i = 0; i < count_btn; i++)
            {
                BTNS_mission[i] = Instantiate(Raw_BTN_mission, Place_BTN);
            }
        }
        else if (level >= 101 && level <= 200)
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

        //music win

        GetComponent<AudioSource>().Play();

        //change action btns
        BTN_Freeze.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Freeze") >= 1)
            {
                Partical_Freeze.Play();
                BTN_Freeze.GetComponent<AudioSource>().Play();

                foreach (var BTNS in BTNS_mission)
                {
                    BTNS.GetComponent<BTN>().Time_animation = 0.003f;

                    //show anim freeze
                    BTNS.GetComponent<BTN>().show_anim_freeze();
                }

                PlayerPrefs.SetInt("Freeze", PlayerPrefs.GetInt("Freeze") - 1);
            }
            else
            {
                music_reject.Play();
                Instantiate(Quick_offer, transform);
                Partica_reject.Play();
            }



        });

        BTN_Minese.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Minuse") >= 1)
            {
                BTN_Minese.GetComponent<AudioSource>().Play();
                Partical_minuse.Play();

                foreach (var btn in BTNS_mission)
                {
                    if (btn.GetComponent<BTN>().Count > 1)
                    {
                        btn.GetComponent<BTN>().Count -= 1;

                        if (btn.GetComponent<BTN>().Count - 1 < btn.GetComponent<BTN>().Tap)
                        {
                            btn.GetComponent<BTN>().Tap -= 1;
                        }

                        //show anim minuse
                        btn.GetComponent<BTN>().Show_anim_minuse();
                    }
                }

                PlayerPrefs.SetInt("Minuse", PlayerPrefs.GetInt("Minuse") - 1);
            }
            else
            {
                music_reject.Play();
                Instantiate(Quick_offer, transform);
                Partica_reject.Play();
            }

        });

        BTN_Delete.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Delete") >= 1 && BTNS_mission.Length - 1 > 1)
            {
                BTN_Delete.GetComponent<AudioSource>().Play();
                Partical_delete.Play();

                //anim delete
                BTNS_mission[BTNS_mission.Length - 1].GetComponent<BTN>().Anim = 1;

                //work delet
                GameObject[] BTN_new_mission = new GameObject[BTNS_mission.Length - 1];
                for (int i = 0; i < BTN_new_mission.Length; i++)
                {
                    if (BTNS_mission[i] != null)
                    {
                        BTN_new_mission[i] = BTNS_mission[i];
                    }
                }

                BTNS_mission = BTN_new_mission;

                //minuse entity
                PlayerPrefs.SetInt("Delete", PlayerPrefs.GetInt("Delete") - 1);
            }
            else
            {
                music_reject.Play();
                Partica_reject.Play();
                Instantiate(Quick_offer, transform);
            }
        });

        BTN_Reset.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Reset") >= 1)
            {
                BTN_Reset.GetComponent<AudioSource>().Play();
                Partical_Reset.Play();

                for (int i = 0; i < BTNS_mission.Length; i++)
                {
                    Destroy(BTNS_mission[i]);
                }

                PlayerPrefs.SetInt("Reset", PlayerPrefs.GetInt("Reset") - 1);
                Change_value(Level, Parent);
            }
            else
            {
                music_reject.Play();
                Instantiate(Quick_offer, transform);
                Partica_reject.Play();
            }

        });

        Leave_mission.onClick.AddListener(() =>
        {
            //camera effect
            Player.Cam.Move_Camera_To_Menu();

            //send deta to server
            Parent.GetComponent<Panel_home>().Send_data_to_server();

            //destroy gameplay
            Destroy(Parent.GetComponent<Panel_home>().Missions);

            //audio controler
            Menu.Play_music_menu();
        });
    }

    private void Update()
    {

        //control entitys
        Text_level_number.text = PlayerPrefs.GetInt("Level").ToString();
        Text_freeze_number.text = PlayerPrefs.GetInt("Freeze").ToString();
        Text_minus_number.text = PlayerPrefs.GetInt("Minuse").ToString();
        Text_delete_number.text = PlayerPrefs.GetInt("Delete").ToString();
        Text_chance_number.text = PlayerPrefs.GetInt("Chance").ToString();
        Text_reset_number.text = PlayerPrefs.GetInt("Reset").ToString();
        Text_coin_number.text = PlayerPrefs.GetInt("Coin").ToString();

        //mission conterol
        if (pass_mission == 0)
        {

            if (gameObject.transform.localScale != Vector3.one)
            {
                gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, Vector3.one, 0.1f);
            }
            else
            {
                //control win
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
            }
        }
        else if (pass_mission == 1)
        {
            Parent.GetComponent<Panel_home>().Insert_mission_Offline(transform.position);
            int rand_bounce = Random.Range(1, 6);

            int Rand_coin = Random.Range(1, 10);

            int rand_coin_duplicate_misison = Random.Range(1, 4);

            if (PlayerPrefs.GetInt("Level") < PlayerPrefs.GetInt("Top_Score"))
            {
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + rand_coin_duplicate_misison);
            }
            else
            {
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + Rand_coin);
            }


            int rand_count_bounce = Random.Range(1, 3);

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
            pass_mission = 2;
        }
        else if (pass_mission == 2)
        {
            if (gameObject.transform.position != Vector3.zero)
            {
                gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, Vector3.zero, 0.1f);

                if (gameObject.transform.localScale == Vector3.zero)
                {
                    Destroy(gameObject);
                }
            }

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

        ParticleSystem ParticleFreeze
        {
            get
            {
                ParticleSystem Partical_freeze = null;
                foreach (var Particle in GetComponentsInChildren<ParticleSystem>())
                {
                    if (Particle.name == "P_F")
                    {
                        Partical_freeze = Particle;
                    }
                }
                return Partical_freeze;
            }
        }

        ParticleSystem Particleminuse
        {
            get
            {
                ParticleSystem Partical_minuse = null;
                foreach (var Partical in GetComponentsInChildren<ParticleSystem>())
                {
                    if (Partical.name == "P_M")
                    {
                        Partical_minuse = Partical;

                    }
                }
                return Partical_minuse;
            }
        }

        Transform Place_mission;
        public float Time_animation = 0.01f;

        public int Count;
        public int Tap;
        public int show_hint = 0;
        public int show_off = 1;

        public int Anim;

        public void Change_value(Transform Place_mission, GameObject Parent)
        {
            this.Place_mission = Place_mission;

            Count = Random.Range(1, 9);
            Text_BTN.text = Count.ToString();
            BTN_click.onClick.AddListener(() =>
            {
                //audi0 control
                GetComponent<AudioSource>().Play();

                //tap contorol

                if (Tap + 1 > Count)
                {
                    if (PlayerPrefs.GetInt("Chance") < 1)
                    {
                        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") - 1);
                        Parent.GetComponent<Raw_model_game_play_offline>().Partica_reject.Play();

                        //vibrator control
                        if (PlayerPrefs.GetInt("Vibrator") == 0)
                        {
                            Handheld.Vibrate();

                        }

                        Instantiate(Parent.GetComponent<Raw_model_game_play_offline>().Quick_offer, Parent.transform);
                        Parent.GetComponent<Raw_model_game_play_offline>().music_reject.Play();
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Chance", PlayerPrefs.GetInt("Chance") - 1);
                        Parent.GetComponent<Raw_model_game_play_offline>().Parical_Chance.Play();

                        //Vibrator control
                        if (PlayerPrefs.GetInt("Vibrator") == 0)
                        {
                            Handheld.Vibrate();
                        }
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

            //anim destroy
            if (Anim == 1)
            {
                gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, Vector3.zero, 0.1f);

                if (gameObject.transform.localScale == Vector3.zero)
                {
                    Destroy(gameObject);
                }
            }
        }


        public void show_anim_freeze()
        {

            ParticleFreeze.Play();
        }

        public void Show_anim_minuse()
        {

            Particleminuse.Play();
        }

    }


}