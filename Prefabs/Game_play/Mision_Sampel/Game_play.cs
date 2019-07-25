using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

public class Game_play : MonoBehaviour
{
    public TextMeshProUGUI Text_Time_number;
    public TextMeshProUGUI Text_Level_number;
    public GameObject BTN_sampel;
    public GameObject Panel_pass;
    public GameObject Panel_BTNs;
    public float Time_mision;
    public int Level;
    public int State_pass;
    public int Star;
    public int Reset;
    public GameObject[] BTNS;
    object[] Pass_map;
    object[] pass_sampel;
    float Time_local;
    public int start_mision = 0;

    private void Start()
    {
        Check_pass();

        Pass_map = new object[BTNS.Length];
        pass_sampel = new object[BTNS.Length];

        for (int i = 0; i < Pass_map.Length; i++)
        {
            Pass_map[i] = 1;
        }
        Text_Level_number.text = Level.ToString();
        Text_Time_number.text = Time_mision.ToString();

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
                        print(Time_mision);
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



}
