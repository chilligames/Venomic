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
    public float Time;
    public int Level;
    public int State_pass;
    public GameObject[] BTNS;
    object[] Pass_map;
    object[] pass_sampel;
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
        Text_Time_number.text = Time.ToString();

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

                    int Count = Random.Range(1, 7);
                    BTNS = new GameObject[Count];
                    for (int i = 0; i < Count; i++)
                    {
                        BTNS[i] = Instantiate(BTN_sampel, Panel_BTNs.transform);
                        BTNS[i].GetComponent<BTN_sample>().Sampel_count = Random.Range(1, Count);
                    }
                }
                else if (Level > 500)
                {
                    int Count = Random.Range(1, 10);
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



            }

        }

    }

}
