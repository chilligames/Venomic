using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
public class BTN_sample : MonoBehaviour
{
    public int Tap_count;
    public static int Sampel_count;
    public TextMeshProUGUI text;
    Vector3 pos_BTN;


    private void Start()
    {

        animation_spawn();

        Sampel_count = 2;

        async void animation_spawn()
        {
            while (true)
            {
                if (transform.localScale != Vector3.zero)
                {
                    await Task.Delay(10);
                    transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, 0.4f);
                }
                else
                {
                    while (true)
                    {
                        await Task.Delay(10);
                        if (transform.localScale != Vector3.one)
                        {
                            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, 0.4f);
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

    public void Press_BTN()
    {
        anim_Press();
        Tap_count++;


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
                            if (Tap_count > Sampel_count)
                            {
                                print("dead");
                            }
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
}
