using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Threading.Tasks;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using Chilligames.Json;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject Panel_stars;
    public TextMeshPro[] Text_Stars_num;
    public TextMeshProUGUI Text_Username;
    public Color Color_select_tab;
    public Color Color_deselect_tab;

    public Button BTN_signal;

    public GameObject[] Panels;
    public GameObject[] BTN_tabs;
    public GameObject Holder_background;

    public GameObject[] Sub_panel_home;
    public GameObject Sub_panel_Ranking;



    GameObject Curent_panel;
    GameObject Curent_sub_panel;
    GameObject Curent_Tab;

    Status_Stars_model status_Stars;
    Panel_Ranking Ranking;

    public string ID_player
    {
        get
        {
            return GetComponentInChildren<Panel_home>()._id;
        }
    }

    void Start()
    {

        status_Stars = new Status_Stars_model(Text_Stars_num, Panel_stars);


        Curent_panel = Panels[1];
        Curent_Tab = BTN_tabs[1];


        Cheack_net();

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
        status_Stars.Change_entity_number();

    }


    /// <summary>
    /// animation close va animation open ejra mikone
    /// </summary>
    /// <param name="Tab_number"></param>
    public void Press_BTN_tab(int Tab_number)
    {
        Close_sub_panels();

        Change_color_tab_BTN();

        animation_curent_panel();

        if (Tab_number == 2)
        {
            Ranking.Recive_ranking(ID_player);
        }

        void Close_sub_panels()
        {
            Destroy(Ranking.Curent_sub_panel);
        }

        async void animation_curent_panel()
        {
            while (true)
            {
                if (Curent_panel.transform.localScale != Vector3.zero)
                {
                    await Task.Delay(1);
                    Curent_panel.transform.localScale = Vector3.MoveTowards(Curent_panel.transform.localScale, Vector3.zero, 0.2f);
                }
                else
                {
                    Curent_panel.SetActive(false);
                    show_animation_panel();
                    break;
                }

            }
        }

        async void Change_color_tab_BTN()
        {
            while (true)
            {
                if (BTN_tabs[Tab_number].GetComponentInChildren<RawImage>().color != Color_select_tab)
                {
                    await Task.Delay(1);
                    BTN_tabs[Tab_number].GetComponentInChildren<RawImage>().color = Color.Lerp(BTN_tabs[Tab_number].GetComponentInChildren<RawImage>().color, Color_select_tab, 0.5f);
                }
                else
                {
                    break;
                }
            }

            while (true)
            {

                if (Curent_Tab.GetComponentInChildren<RawImage>().color != Color_deselect_tab)
                {
                    await Task.Delay(1);
                    Curent_Tab.GetComponentInChildren<RawImage>().color = Color.Lerp(Curent_Tab.GetComponentInChildren<RawImage>().color, Color_deselect_tab, 0.5f);
                }
                else
                {
                    Curent_Tab = BTN_tabs[Tab_number];
                    break;
                }
            }
        }

        async void show_animation_panel()
        {
            Panels[Tab_number].SetActive(true);
            while (true)
            {
                if (Holder_background.transform.position != BTN_tabs[Tab_number].transform.position)
                {
                    await Task.Delay(1);
                    Holder_background.transform.position = Vector3.MoveTowards(Holder_background.transform.position, BTN_tabs[Tab_number].transform.position, 0.3f);
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                if (Panels[Tab_number].transform.localScale != Vector3.one)
                {
                    await Task.Delay(1);
                    Panels[Tab_number].transform.localScale = Vector3.MoveTowards(Panels[Tab_number].transform.localScale, Vector3.one, 0.2f);

                }
                else
                {
                    Curent_panel = Panels[Tab_number];

                    break;
                }

            }

        }

    }



    class Status_Stars_model
    {
        GameObject panel_stars;
        TextMeshPro[] Text_stars_number;
        int[] Stars = new int[4];
        int recive;

        public Status_Stars_model(TextMeshPro[] Text_Stars_num, GameObject panel_stars)
        {
            this.panel_stars = panel_stars;
            Text_stars_number = Text_Stars_num;
            Change_entity_number();
        }


        /// <summary>
        /// Data recive mikone 
        /// meghdar hay star search mikone jaygozin mikone
        /// </summary>
        public void Change_entity_number()
        {
            if (Player.Cam.Zoom == 1)
            {
                Animation_open();

                panel_stars.transform.position = new Vector3(Player.cam.transform.position.x, Player.cam.transform.position.y + 30);


                StreamReader Reader_data = new StreamReader(Application.persistentDataPath + "/Info.Chi");
                string Data = Reader_data.ReadToEnd();
                Reader_data.Close();
                Player.Entity_player_model Reciver = JsonUtility.FromJson<Player.Entity_player_model>(Data);

                if (recive == 0)
                {
                    for (int i = 0; i < Reciver.S.Length; i++)
                    {
                        if (Reciver.S[i] == 1)
                        {
                            Stars[0] += 1;
                        }
                        else if (Reciver.S[i] == 2)
                        {
                            Stars[1] += 1;
                        }
                        else if (Reciver.S[i] == 3)
                        {
                            Stars[2] += 1;
                        }
                        else if (Reciver.S[i] == 4)
                        {
                            Stars[3] += 1;
                        }
                    }

                    for (int i = 0; i < Stars.Length; i++)
                    {
                        Text_stars_number[i].text = Stars[i].ToString();
                    }
                    recive = 1;
                }

                panel_stars.transform.position = Vector3.MoveTowards(panel_stars.transform.position, new Vector3(Player.cam.transform.position.x, Player.cam.transform.position.y + 30), 0.01f);


                async void Animation_open()
                {

                    while (true)
                    {
                        if (panel_stars.transform.localScale != Vector3.one)
                        {
                            await Task.Delay(1);
                            panel_stars.transform.localScale = Vector3.MoveTowards(panel_stars.transform.localScale, Vector3.one, 0.1f);
                        }
                        else
                        {
                            panel_stars.SetActive(true);
                            break;
                        }
                    }
                }

            }
            else
            {

                animation_Close();

                recive = 0;
                for (int i = 0; i < Stars.Length; i++)
                {
                    Stars[i] = 0;
                }

                async void animation_Close()
                {
                    while (true)
                    {
                        if (panel_stars.transform.localScale != Vector3.zero)
                        {
                            await Task.Delay(1);
                            panel_stars.transform.localScale = Vector3.MoveTowards(panel_stars.transform.localScale, Vector3.zero, 0.01f);
                        }
                        else
                        {
                            panel_stars.SetActive(false);
                            break;
                        }
                    }

                }

            }
        }

    }

}
