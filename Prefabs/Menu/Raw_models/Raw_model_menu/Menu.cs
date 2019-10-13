using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Chilligames.SDK.Model_Client;
using Chilligames.SDK;
using System;

/// <summary>
/// playerpref
/// 1: Sound
/// 2: Next_reward
/// </summary>
public class Menu : MonoBehaviour
{
    public ParticleSystem Partical_Day;
    public ParticleSystem Partical_Night;

    public static AudioSource Music_menu_;
    public static AudioSource music_game_play_;

    public AudioSource Music_menu;
    public AudioSource Music_game_play;

    public Button BTN_signal;
    public Button BTN_Profile;
    public Button BTN_Home;
    public Button BTN_Ranking;
    public Button BTN_Servers;
    public Button BTN_Shop;
    public Button BTN_Messages;
    public Button BTN_Setting;
    public Button BTN_bug;


    public GameObject Content_Signal;
    public GameObject Content_Profile;
    public GameObject content_Home;
    public GameObject Content_Ranking;
    public GameObject Content_Servers;
    public GameObject Content_Shop;
    public GameObject Content_Message;
    public GameObject Content_Setting;
    public GameObject Content_bug;
    public GameObject Content_gift;

    public RawImage Icon_Cheack_status_new_message;


    public Color Color_select;
    public Color Color_deselect;
    public Color Color_bug;

    public GameObject Holder;


    GameObject Curent_panel = null;
    Button Curent_BTN_Taped = null;

    public string ID_player
    {
        get
        {
            return PlayerPrefs.GetString("_id");
        }
    }

    void Start()
    {
        //daylireward
        if (DateTime.FromFileTime((long)PlayerPrefs.GetFloat("Next_reward")) < DateTime.Now && PlayerPrefs.GetInt("Help") == 1)
        {
            Content_gift.SetActive(true);
            print("Panel_reward_show");
        }
        else
        {
            Content_gift.SetActive(false);
            print("panel hide ");
        }



        //paritical holder change color
        if (PlayerPrefs.GetInt("Day_Night") == 0)
        {
            Partical_Day.gameObject.SetActive(true);
            Partical_Night.gameObject.SetActive(false);

        }
        else if (PlayerPrefs.GetInt("Day_Night") == 1)
        {
            Partical_Day.gameObject.SetActive(false);
            Partical_Night.gameObject.SetActive(true);
        }

        //music controler
        music_game_play_ = Music_game_play;
        Music_menu_ = Music_menu;


        //frist start chagne
        Curent_panel = content_Home;
        Curent_BTN_Taped = BTN_Home;


        //change action btns
        BTN_signal.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Signal;
            Content_Signal.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_signal;
            BTN_signal.GetComponentInChildren<RawImage>().color = Color_select;
        });

        BTN_Profile.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Profile;
            Content_Profile.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Profile;
            BTN_Profile.GetComponentInChildren<RawImage>().color = Color_select;
        });

        BTN_Home.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = content_Home;
            content_Home.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Home;
            BTN_Home.GetComponentInChildren<RawImage>().color = Color_select;
        });

        BTN_Ranking.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Ranking;
            Content_Ranking.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Ranking;
            BTN_Ranking.GetComponentInChildren<RawImage>().color = Color_select;

        });

        BTN_Servers.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Servers;
            Content_Servers.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Servers;
            BTN_Servers.GetComponentInChildren<RawImage>().color = Color_select;

        });

        BTN_Shop.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Shop;
            Content_Shop.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Shop;
            BTN_Shop.GetComponentInChildren<RawImage>().color = Color_select;

        });

        BTN_Messages.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Message;
            Content_Message.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Messages;
            BTN_Messages.GetComponentInChildren<RawImage>().color = Color_select;
        });

        BTN_Setting.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Setting;
            Content_Setting.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Setting;
            BTN_Setting.GetComponentInChildren<RawImage>().color = Color_select;
        });

        BTN_bug.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_bug;
            Content_bug.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_bug;
            BTN_bug.GetComponentInChildren<RawImage>().color = Color_select;
        });

        Cheack_net();

        StartCoroutine(Cheack_new_message());

        async void Cheack_net()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://www.google.com/");
            www.SendWebRequest();
            while (true)
            {

                if (www.isDone)
                {
                    www.Abort();

                    BTN_signal.GetComponentInChildren<RawImage>().color = Color.green;

                    break;
                }
                else
                {
                    if (www.isHttpError || www.isNetworkError || www.timeout == 1)
                    {
                        BTN_signal.GetComponentInChildren<RawImage>().color = Color.red;
                        www.Abort();
                        break;
                    }
                    await Task.Delay(1);
                }
            }
        }

    }

    private void Update()
    {
        Holder.transform.position = Vector3.MoveTowards(Holder.transform.position, Curent_BTN_Taped.gameObject.transform.position, 0.2f);


        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public static void Play_music_menu()
    {
        music_game_play_.Stop();
        Music_menu_.Play();
    }
    public static void Play_music_GamePlay()
    {
        music_game_play_.Play();
        Music_menu_.Stop();
    }

    /// <summary>
    /// cheack mikone new message bashe
    /// </summary>
    /// <returns></returns>
    IEnumerator Cheack_new_message()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Content_Message.GetComponent<Panel_Chatroom>().Cheack_new_message(Icon_Cheack_status_new_message);
        }
    }

}
