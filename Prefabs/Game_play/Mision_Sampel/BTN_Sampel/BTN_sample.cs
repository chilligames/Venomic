using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.IO;
public class BTN_sample : MonoBehaviour
{
    public int Tap_count;
    public int Sampel_count;
    public object Passed;
    public TextMeshProUGUI text;
    Vector3 pos_BTN;

    public int Status_show;

    private void Start()
    {
        animation_spawn();
        async void animation_spawn()
        {
            while (true)
            {
                if (transform.localScale != Vector3.zero)
                {
                    await Task.Delay(10);
                    transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 0.2f);
                }
                else
                {
                    while (true)
                    {
                        await Task.Delay(10);
                        if (transform.localScale != Vector3.one)
                        {
                            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, 0.2f);
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
        Show_hint();
    }

    private void Update()
    {
        Show_hint();
    }

    /// <summary>
    /// 1:anim_press run mishe 
    /// 2:tap ++ mishe
    /// 3:chekmikone dead 
    /// </summary>
    public void Press_BTN()
    {
        anim_Press();
        Tap_count++;

        GetComponentInParent<Game_play>().start_mision = 1;

        if (Tap_count == Sampel_count)
        {
            Passed = 1;
            print("passed");
        }
        else if (Tap_count > Sampel_count)
        {
            print("dead");
        }


        async void anim_Press()
        {
            while (true)
            {
                if (text.transform.localScale != Vector3.zero)
                {
                    await Task.Delay(10);
                    text.transform.localScale = Vector3.MoveTowards(text.transform.localScale, Vector3.zero, 0.4f);
                }
                else if (Vector3.Distance(text.transform.localScale, Vector3.zero) == 0)
                {
                    while (true)
                    {
                        if (text.transform.localScale != Vector3.one)
                        {
                            await Task.Delay(10);

                            text.text = Tap_count.ToString();

                            text.transform.localScale = Vector3.MoveTowards(text.transform.localScale, Vector3.one, 0.4f);
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
    }


    public void Show_hint()
    {
        if (GetComponentInParent<Game_play>().transform.position == Player.cam.transform.position&&Status_show==0)
        {
            Animation_show();
            Status_show = 1;
        }


        async void Animation_show()
        {
            for (int i = 0; i < Sampel_count; i++)
            {
                await Task.Delay(700);
                animation_hint();
            }
        }

        async void animation_hint()
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
                        await Task.Delay(10);
                        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, 0.1f);
                        if (transform.localScale == Vector3.one)
                        {
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}

