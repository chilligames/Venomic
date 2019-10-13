using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// playerpref
/// 1: Freeze
/// 2: Minuse
/// 3: Delete
/// 4: Chance
/// 5: Reset
/// 6: Coin
/// 7: Next_reward
/// 8:
/// </summary>
public class Panel_dayli_reward : MonoBehaviour
{
    public Color Color_day;
    public Color Color_night;

    public RawImage Background_panel;

    public Button BTN_Pick_up;

    public TextMeshProUGUI Text_Freeze_number;
    public TextMeshProUGUI Text_minuse_number;
    public TextMeshProUGUI Text_delete_number;
    public TextMeshProUGUI Text_chance_number;
    public TextMeshProUGUI Text_reset_number;
    public TextMeshProUGUI Text_coin_number;

    int random
    {
        get
        {
            return UnityEngine.Random.Range(1, 20);
        }
    }


    void Start()
    {
        if (PlayerPrefs.GetInt("Day_Night") == 1)
        {
            Background_panel.color = Color_night;

        }
        else
        {
            Background_panel.color = Color_day;
        }

        var freeze = random;
        var Minues = random;
        var Delete = random;
        var Chance = random;
        var Reset = random;
        var Coin = UnityEngine.Random.Range(10, 200);

        Text_Freeze_number.text = freeze.ToString();
        Text_minuse_number.text = Minues.ToString();
        Text_delete_number.text = Delete.ToString();
        Text_chance_number.text = Chance.ToString();
        Text_reset_number.text = Reset.ToString();
        Text_coin_number.text = Coin.ToString();


        BTN_Pick_up.onClick.AddListener(() =>
        {
            //change time next reward
            PlayerPrefs.SetFloat("Next_reward", DateTime.Now.AddHours(6).ToFileTime());

            //deposit to acc
            PlayerPrefs.SetInt("Freeze", PlayerPrefs.GetInt("Freeze") + freeze);
            PlayerPrefs.SetInt("Minuse", PlayerPrefs.GetInt("Minuse") + Minues);
            PlayerPrefs.SetInt("Delete", PlayerPrefs.GetInt("Delete") + Delete);
            PlayerPrefs.SetInt("Chance", PlayerPrefs.GetInt("Chance") + Chance);
            PlayerPrefs.SetInt("Reset", PlayerPrefs.GetInt("Reset") + Reset);
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + Coin);

            gameObject.SetActive(false);
        });
    }


}
