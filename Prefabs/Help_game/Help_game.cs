using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Help_game : MonoBehaviour
{
    public GameObject Canvas_menu;

    public Button BTN_close_help;

    public Button BTN_En;
    public Button BTN_FA;

    public Button BTN_mission_step_2;
    public Button BTN_next_setep_2;


    public Button BTN_next_mission_3;
    public Button BTN_mission_3_1;
    public Button BTN_mission_3_2;


    public Button BTN_next_misson4;
    public Button BTN_misson_4_1;
    public Button BTN_freeze;


    public Button BTN_next_mission_5;
    public Button BTN_minuse;
    public Button mission_5_1;


    public Button BTN_next_mission_6;
    public Button BTN_delete;
    public Button BTN_mission_6_1;
    public Button BTN_Mission_6_2;


    public Button BTN_next_mission_7;
    public Button BTN_mission_7_1;
    public Button BTN_missin_7_2;
    public TextMeshProUGUI Text_Score_number;
    public TextMeshProUGUI Text_chance_number;


    public Button BTN_next_mission_8;
    public Button BTN_raw_model_BTN_mission;
    public Button BTN_reset_mission;
    public GameObject Place_btn_mission;


    public Button BTN_finish;

    public GameObject Step_1;
    public GameObject Step_login;
    public GameObject Step_2;
    public GameObject Step_3;
    public GameObject Step_4;
    public GameObject Step_5;
    public GameObject Step_6;
    public GameObject Step_7;
    public GameObject Step_8;
    public GameObject Step_9;


    void Start()
    {


        if (PlayerPrefs.GetInt("Help") == 1)
        {
            Canvas_menu.SetActive(true);
            Destroy(gameObject);
        }

        //step1
        BTN_En.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("Language", 0);
            Step_1.SetActive(false);
            Step_2.SetActive(true);
        });
        BTN_FA.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("Language", 1);
            Step_1.SetActive(false);
            Step_2.SetActive(true);
        });

        //stepLogin


        //step2
        BTN_next_setep_2.onClick.AddListener(() =>
        {
            Step_2.SetActive(false);
            Step_3.SetActive(true);

        });
        Step_2.AddComponent<Raw_Step_2>().Change_value(BTN_mission_step_2, BTN_next_setep_2);

        //step3
        BTN_next_mission_3.onClick.AddListener(() =>
        {
            Step_3.SetActive(false);
            Step_4.SetActive(true);

        });
        Step_3.AddComponent<Raw_Step_3>().Change_value(BTN_mission_3_1, BTN_mission_3_2, BTN_next_mission_3);


        //step4
        BTN_next_misson4.onClick.AddListener(() =>
        {
            Step_4.SetActive(false);
            Step_5.SetActive(true);

        });
        Step_4.AddComponent<Raw_step_4>().Change_value(BTN_freeze, BTN_misson_4_1, BTN_next_misson4);


        //step5
        BTN_next_mission_5.onClick.AddListener(() =>
        {
            Step_5.SetActive(false);
            Step_6.SetActive(true);

        });
        Step_5.AddComponent<Raw_step_5>().Change_value(mission_5_1, BTN_minuse, BTN_next_mission_5);


        //step6
        BTN_next_mission_6.onClick.AddListener(() =>
        {
            Step_6.SetActive(false);
            Step_7.SetActive(true);
        });
        Step_6.AddComponent<Raw_step_6>().Change_value(BTN_mission_6_1, BTN_Mission_6_2, BTN_delete, BTN_next_mission_6);


        //step_7
        Step_7.AddComponent<Raw_step_7>().Change_value(Text_Score_number, Text_chance_number, BTN_mission_7_1, BTN_missin_7_2, BTN_next_mission_7);
        BTN_next_mission_7.onClick.AddListener(() =>
        {
            Step_7.gameObject.SetActive(false);
            Step_8.gameObject.SetActive(true);

        });

        //step_8
        BTN_next_mission_8.onClick.AddListener(() =>
        {
            Step_8.SetActive(false);
            Step_9.SetActive(true);
        });
        Step_8.AddComponent<Raw_step_8>().Change_value(BTN_reset_mission, BTN_raw_model_BTN_mission, Place_btn_mission, BTN_next_mission_8);


        //step_9
        BTN_finish.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("Help", 1);
            Destroy(gameObject);
            Canvas_menu.SetActive(true);

        });


        BTN_close_help.onClick = BTN_finish.onClick;

    }


    class Raw_step_login
    {

        public void change_value()
        {

        }

    }

    class Raw_Step_2 : MonoBehaviour
    {
        public Button btn_mission;
        public int sampel;
        public int Taped;


        int anim;

        public void Change_value(Button BTN_mission, Button BTN_next_mission_2)
        {
            btn_mission = BTN_mission;
            sampel = Random.Range(3, 9);
            btn_mission.GetComponentInChildren<TextMeshProUGUI>().text = sampel.ToString();
            btn_mission.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Taped + 1 == sampel)
                {
                    BTN_next_mission_2.gameObject.SetActive(true);
                    btn_mission.interactable = false;
                    Taped += 1;
                }
                else
                {
                    Taped += 1;
                }
                btn_mission.GetComponentInChildren<TextMeshProUGUI>().text = Taped.ToString();

            });
        }


        private void Update()
        {
            if (anim == 0)
            {
                print(anim);
                btn_mission.GetComponentInChildren<TextMeshProUGUI>().rectTransform.localScale = Vector3.MoveTowards(btn_mission.GetComponentInChildren<TextMeshProUGUI>().rectTransform.localScale, Vector3.zero, 0.01f);

                if (btn_mission.GetComponentInChildren<TextMeshProUGUI>().rectTransform.localScale == Vector3.zero)
                {
                    btn_mission.GetComponentInChildren<TextMeshProUGUI>().text = "0";
                    anim = 1;
                }
            }
            else if (anim == 1)
            {
                btn_mission.GetComponentInChildren<TextMeshProUGUI>().text = Taped.ToString();
                btn_mission.GetComponentInChildren<TextMeshProUGUI>().rectTransform.localScale = Vector3.MoveTowards(btn_mission.GetComponentInChildren<TextMeshProUGUI>().rectTransform.localScale, Vector3.one, 0.5f);
                if (btn_mission.GetComponentInChildren<TextMeshProUGUI>().rectTransform.localScale == Vector3.one)
                {

                    anim = 3;
                }
            }
        }

    }


    class Raw_Step_3 : MonoBehaviour
    {
        Button BTN_mission_1;
        Button BTN_mission_2;

        Button BTN_next_mission;


        int Sampel_1;
        int Taped_1;

        int sampel_2;
        int Taped_2;


        int anim;
        public void Change_value(Button BTN_mission_3_1, Button BTN_mission_3_2, Button BTN_next_mission3)
        {

            BTN_mission_1 = BTN_mission_3_1;
            BTN_mission_2 = BTN_mission_3_2;
            BTN_next_mission = BTN_next_mission3;


            Sampel_1 = Random.Range(3, 9);
            sampel_2 = Random.Range(3, 9);


            BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().text = Sampel_1.ToString();
            BTN_mission_2.GetComponentInChildren<TextMeshProUGUI>().text = sampel_2.ToString();


            BTN_mission_1.onClick.AddListener(() =>
            {

                if (Taped_1 + 1 == Sampel_1)
                {
                    Taped_1 += 1;
                    BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().text = Taped_1.ToString();
                    BTN_mission_1.interactable = false;
                }
                else
                {
                    Taped_1 += 1;
                    BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().text = Taped_1.ToString();

                }

            });
            BTN_mission_2.onClick.AddListener(() =>
            {

                if (Taped_2 + 1 == sampel_2)
                {
                    Taped_2 += 1;
                    BTN_mission_2.GetComponentInChildren<TextMeshProUGUI>().text = Taped_2.ToString();
                    BTN_mission_2.interactable = false;
                }
                else
                {
                    Taped_2 += 1;
                    BTN_mission_2.GetComponentInChildren<TextMeshProUGUI>().text = Taped_2.ToString();
                }

            });
        }

        private void Update()
        {
            if (anim == 0)
            {
                BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().transform.localScale = Vector3.MoveTowards(BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().transform.localScale, Vector3.zero, 0.01f);
                BTN_mission_2.GetComponentInChildren<TextMeshProUGUI>().transform.localScale = Vector3.MoveTowards(BTN_mission_2.GetComponentInChildren<TextMeshProUGUI>().transform.localScale, Vector3.zero, 0.01f);

                if (BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().transform.localScale == Vector3.zero)
                {
                    anim = 1;
                }
            }
            else if (anim == 1)
            {
                BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().text = Taped_1.ToString();
                BTN_mission_2.GetComponentInChildren<TextMeshProUGUI>().text = Taped_2.ToString();

                BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().transform.localScale = Vector3.MoveTowards(BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().transform.localScale, Vector3.one, 0.5f);
                BTN_mission_2.GetComponentInChildren<TextMeshProUGUI>().transform.localScale = Vector3.MoveTowards(BTN_mission_2.GetComponentInChildren<TextMeshProUGUI>().transform.localScale, Vector3.one, 0.5f);

                if (BTN_mission_1.GetComponentInChildren<TextMeshProUGUI>().transform.localScale == Vector3.one)
                {
                    anim = 3;
                }

            }
            else if (anim == 3)
            {
                if (Taped_1 == Sampel_1 && Taped_2 == sampel_2)
                {
                    BTN_next_mission.gameObject.SetActive(true);
                }
            }

        }

    }


    class Raw_step_4 : MonoBehaviour
    {
        Button BTN_Freeze;
        Button BTN_mission;

        float speed = 0.2f;

        int Count
        {
            get
            {
                return Random.Range(3, 9);
            }
        }

        int anim;

        public void Change_value(Button btn_freeze, Button btn_mission, Button BTN_nextmission_4)
        {
            BTN_Freeze = btn_freeze;
            BTN_mission = btn_mission;

            BTN_Freeze.onClick.AddListener(() =>
            {
                BTN_nextmission_4.gameObject.SetActive(true);
                speed = 0.01f;
            });

            btn_mission.GetComponentInChildren<TextMeshProUGUI>().text = Count.ToString();
        }

        private void Update()
        {
            if (anim == 0)
            {
                BTN_mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale = Vector3.MoveTowards(BTN_mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale, Vector3.zero, speed);

                if (BTN_mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale == Vector3.zero)
                {
                    anim = 1;
                }
            }
            else if (anim == 1)
            {
                BTN_mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale = Vector3.MoveTowards(BTN_mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale, Vector3.one, speed);

                if (BTN_mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale == Vector3.one)
                {
                    anim = 0;

                }
            }

        }
    }


    class Raw_step_5 : MonoBehaviour
    {
        Button BTN_Mission;

        int Sampel;
        int anim;

        public void Change_value(Button btn_mission, Button btn_minuse, Button BTN_next_mission5)
        {
            BTN_Mission = btn_mission;


            Sampel = Random.Range(3, 9);
            btn_mission.GetComponentInChildren<TextMeshProUGUI>().text = Sampel.ToString();
            btn_minuse.onClick.AddListener(() =>
            {

                if (Sampel >= 1)
                {
                    Sampel -= 1;
                    if (Sampel == 0)
                    {
                        BTN_next_mission5.gameObject.SetActive(true);
                    }
                }

            });
        }


        private void Update()
        {
            if (anim == 0)
            {
                BTN_Mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale = Vector3.MoveTowards(BTN_Mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale, Vector3.zero, 0.01f);

                if (BTN_Mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale == Vector3.zero)
                {
                    anim = 1;
                }
            }
            else if (anim == 1)
            {
                BTN_Mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale = Vector3.MoveTowards(BTN_Mission.GetComponentInChildren<TextMeshProUGUI>().transform.localScale, Vector3.one, 0.5f);
            }
            BTN_Mission.GetComponentInChildren<TextMeshProUGUI>().text = Sampel.ToString();
        }

    }


    class Raw_step_6 : MonoBehaviour
    {
        public void Change_value(Button btn_mission_6_1, Button btn_misson_6_2, Button btn_delete, Button BTN_next_mission6)
        {
            btn_mission_6_1.GetComponentInChildren<TextMeshProUGUI>().text = Random.Range(3, 9).ToString();

            btn_delete.onClick.AddListener(() =>
            {
                btn_misson_6_2.gameObject.SetActive(false);
                BTN_next_mission6.gameObject.SetActive(true);
            });

        }
    }


    class Raw_step_7 : MonoBehaviour
    {
        public void Change_value(TextMeshProUGUI Text_score_number, TextMeshProUGUI Text_chance_number, Button BTN_mission_7_1, Button BTN_mission_7_2, Button BTN_next_missio_7)
        {
            int rand_tap_1 = Random.Range(3, 9);
            int rand_tap_2 = Random.Range(3, 9);
            int rand_tap_chance = Random.Range(3, 9);
            int rand_score = Random.Range(3, 50);

            Text_score_number.text = rand_score.ToString();
            Text_chance_number.text = rand_tap_chance.ToString();

            BTN_mission_7_1.GetComponentInChildren<TextMeshProUGUI>().text = rand_tap_1.ToString();
            BTN_mission_7_2.GetComponentInChildren<TextMeshProUGUI>().text = rand_tap_2.ToString();

            BTN_mission_7_1.onClick.AddListener(() =>
            {
                if (rand_tap_chance >= 1)
                {
                    rand_tap_chance -= 1;
                    Text_chance_number.text = rand_tap_chance.ToString();
                }
                else
                {
                    rand_score -= 1;
                    Text_score_number.text = rand_score.ToString();
                }
                BTN_next_missio_7.gameObject.SetActive(true);

            });

            BTN_mission_7_2.onClick.AddListener(() =>
            {
                if (rand_tap_chance >= 1)
                {

                    rand_tap_chance -= 1;
                    Text_chance_number.text = rand_tap_chance.ToString();
                }
                else
                {
                    rand_score -= 1;
                    Text_score_number.text = rand_score.ToString();
                }
                BTN_next_missio_7.gameObject.SetActive(true);

            });

        }

    }


    class Raw_step_8 : MonoBehaviour
    {
        Button[] BTNS;

        public void Change_value(Button btn_reset, Button Raw_btn_mission, GameObject place_batn_mission, Button BTN_next_misson)
        {

            int count = Random.Range(1, 9);
            BTNS = new Button[count];
            for (int i = 0; i < count; i++)
            {
                BTNS[i] = Instantiate(Raw_btn_mission, place_batn_mission.transform);
                BTNS[i].gameObject.SetActive(true);
                BTNS[i].GetComponentInChildren<TextMeshProUGUI>().text = Random.Range(1, 9).ToString();
            }



            btn_reset.onClick.AddListener(() =>
            {
                for (int i = 0; i < BTNS.Length; i++)
                {
                    Destroy(BTNS[i].gameObject);
                }
                BTNS = null;
                BTN_next_misson.gameObject.SetActive(true);
                Change_value(btn_reset, Raw_btn_mission, place_batn_mission, BTN_next_misson);
            });

        }



    }
}
