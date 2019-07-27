using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Threading;

public class Game_play : MonoBehaviour
{
    public TextMeshProUGUI Text_Time_number;
    public TextMeshProUGUI Text_Level_number;
    public GameObject BTN_sampel;
    public GameObject Panel_pass;
    public GameObject Panel_In_zoom;
    public GameObject Panel_BTNs;
    public int Level;
    public int State_pass;
    public int Star;
    public int Reset;
    public GameObject[] BTNS;
    object[] Pass_map;
    object[] pass_sampel;
    int TotallClick;
    public float Time_mision;
    float Time_local;
    public int start_mision = 0;

    private void Start()
    {

        Result_mission(75f, 30);

        Check_pass();

        Pass_map = new object[BTNS.Length];
        pass_sampel = new object[BTNS.Length];
        for (int i = 0; i < Pass_map.Length; i++)
        {
            Pass_map[i] = 1;
        }

        Text_Level_number.text = Level.ToString();

        Text_Time_number.text = Time_mision.ToString();


        //tedad Click hay mision dar miarde baray mohasebe
        for (int i = 0; i < BTNS.Length; i++)
        {
            TotallClick += BTNS[i].GetComponent<BTN_sample>().Sampel_count;
        }

        Animation_spawn();


        async void Animation_spawn()
        {

            while (true)
            {
                if (transform.localScale != Vector3.zero)
                {
                    await Task.Delay(10);
                    transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 0.1f);
                }
                else
                {
                    while (true)
                    {
                        if (transform.localScale != Vector3.one)
                        {
                            await Task.Delay(10);
                            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, 0.1f);
                        }
                        else
                        {
                            break;
                        }

                    }
                    break;
                }

            }

        }

        //chek mikone mission pass shode ya na age nashode bashe mision misaze
        void Check_pass()
        {
            if (State_pass == 1)
            {
                Panel_pass.SetActive(true);
                Panel_BTNs.SetActive(false);
            }
            else
            {
                if (Level < 100)
                {

                    int Count = Random.Range(1, 5);
                    BTNS = new GameObject[Count];
                    for (int i = 0; i < Count; i++)
                    {
                        BTNS[i] = Instantiate(BTN_sampel, Panel_BTNs.transform);
                        BTNS[i].GetComponent<BTN_sample>().Sampel_count = Random.Range(1, Count);
                    }
                }
                else if (Level < 300)
                {

                    int Count = Random.Range(4, 8);//#check 
                    BTNS = new GameObject[Count];
                    for (int i = 0; i < Count; i++)
                    {
                        BTNS[i] = Instantiate(BTN_sampel, Panel_BTNs.transform);
                        BTNS[i].GetComponent<BTN_sample>().Sampel_count = Random.Range(1, Count);
                    }
                }
                else if (Level > 500)
                {
                    int Count = Random.Range(6, 11);//#check
                    BTNS = new GameObject[Count];
                    for (int i = 0; i < Count; i++)
                    {
                        BTNS[i] = Instantiate(BTN_sampel, Panel_BTNs.transform);
                        BTNS[i].GetComponent<BTN_sample>().Sampel_count = Random.Range(1, Count);
                    }
                }
                Panel_pass.SetActive(false);
                Panel_BTNs.SetActive(true);
            }
        }

        Time_collect();

        //Chek mikone k mission pass shode ya na age nashode baashe mision mision shoro mishe
        async void Time_collect()
        {
            while (true)
            {
                if (State_pass == 0)
                {
                    if (start_mision == 1)
                    {
                        await Task.Delay(50);
                        Time_local += 0.1f;
                        Text_Time_number.text = System.Math.Round(Time_local, 1).ToString();
                    }
                    else
                    {
                        await Task.Delay(50);
                    }
                }
                else
                {
                    break;
                }
            }
        }

    }


    private void Update()
    {
        //check zoom age zoom bashe panel zoom true mikone
        if (Player.Cam.Zoom == 1)
        {
            Panel_In_zoom.SetActive(true);

            Panel_In_zoom.GetComponentInChildren<TextMeshProUGUI>().text = System.Math.Round(Time_mision, 1).ToString();

            Panel_In_zoom.transform.localScale = Vector3.MoveTowards(Panel_In_zoom.transform.localScale, Vector3.one, 0.03f);
        }
        else
        {
            if (Panel_In_zoom.transform.localScale != Vector3.zero)
            {
                Panel_In_zoom.transform.localScale = Vector3.MoveTowards(Panel_In_zoom.transform.localScale, Vector3.zero, 0.03f);
                if (transform.localScale == Vector3.zero)
                {
                    print("delet");
                    Panel_In_zoom.SetActive(false);
                }
            }
        }



        //Check ta mission pass beshe
        if (State_pass == 0)
        {
            for (int i = 0; i < pass_sampel.Length; i++)
            {
                if (pass_sampel[i] == null && BTNS[i].GetComponent<BTN_sample>().Passed != null)
                {
                    pass_sampel[i] = 1;

                    break;
                }
            }


            if (pass_sampel.SequenceEqual(Pass_map))
            {
                Panel_BTNs.SetActive(false);
                Panel_pass.SetActive(true);
                State_pass = 1;




                Time_mision = Time_local;

                if (Reset == 1)
                {
                    Player.Cam.Move_camera();
                    Reset = 0;
                    Player.mission_Collection.Update_singel_mision(Level);
                    print("reset_mision");
                }
                else
                {
                    Player.Insert_mission(transform.position);
                    print("creat mision");
                }
            }




        }




    }


    /// <summary>
    /// 1: meghdar haro 0 mikone 
    /// 2:reset 1 mikone baray inke load mishe reset bashe
    /// 3:reset mission run mishe
    /// 4: run ejra mishe bara chek
    /// </summary>
    public void Reset_mision()
    {
        State_pass = 0;
        Star = 0;
        Time_mision = 0;

        Reset = 1;
        Player.mission_Collection.Reset_mision(Level);
        Start();
    }


    /// <summary>
    /// formul star chek mikone va setare morede nazar bar migardone
    /// </summary>
    /// <param name="Final_time"> Time_player </param>
    /// <param name="Totall_Click"> totall CLick hay mission</param>
    /// <returns>count star</returns>
    int Result_mission(float Final_time, int Totall_Click)
    {
        float master = Totall_Click * 0.75f;
        float Star_3 = Totall_Click * 1.5f;
        float Star_2 = Totall_Click * 2.5f;
        float star_1 = Totall_Click * 3f;

        if (Final_time <= master)
        {
            print("Star4:" + master);
            return 4;
        }
        else if (Final_time <= Star_3)
        {
            print("star3:" + Star_3);
            return 3;
        }
        else if (Final_time <= Star_2)
        {
            print("Star2:" + Star_2);
            return 2;
        }
        else if (Final_time >= star_1)
        {
            print("Star1" + star_1);
            return 1;
        }
        else
        {
            print("Star_1");
            return 1;
        }

    }

}
