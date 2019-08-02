using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.IO;
/// <summary>
/// playerpref
/// 1: Freez
/// </summary>


public class BTN_sample : MonoBehaviour
{
    public int Tap_count;
    public int Sampel_Click;
    public object Passed;
    public int Freez;
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

        if (Tap_count == Sampel_Click)
        {

            Passed = 1;
            print("passed");
        }
        else if (Tap_count > Sampel_Click)
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


    /// <summary>
    /// hit aval bazi mide b player
    /// </summary>
    public void Show_hint()
    {

        if (GetComponentInParent<Game_play>().transform.position == Player.cam.transform.position && Status_show == 0)
        {
            Animation_show();
            Status_show = 1;
        }


        async void Animation_show()
        {
            for (int i = 0; i < Sampel_Click; i++) //chek mikone freez bodano 
            {
                if (Freez == 1)
                {
                    await Task.Delay(2000);
                }
                else
                {
                    await Task.Delay(700);
                }
                animation_hint();
            }
            Freez = 0;
        }

        async void animation_hint()
        {
            while (true)
            {

                if (transform.localScale != Vector3.zero)
                {
                    await Task.Delay(1);

                    transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 0.1f);
                }
                else
                {
                    while (true)
                    {
                        await Task.Delay(1);
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

    public void Delete_animation_btn()
    {
        delet();
        async void delet()
        {

            while (true)
            {
                if (transform.localScale != Vector3.zero)
                {
                    await Task.Delay(1);
                    transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 0.1f);
                }
                else
                {
                    Destroy(gameObject);
                    break;
                }

            }

        }
    }
}

