using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.IO;

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

    private void Start()
    {
        Text_Level_number.text = Level.ToString();
        Text_Time_number.text = Time.ToString();

        Animation_spawn();
        Check_pass();


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
}
