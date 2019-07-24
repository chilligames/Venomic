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
    public GameObject Panel_pass;
    public GameObject Panel_BTNs;
    public float Time;
    public int Level;
    public int State_pass;

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
                Panel_pass.SetActive(false);
                Panel_BTNs.SetActive(true);
            }

        }
    }


}
