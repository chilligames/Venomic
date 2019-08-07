﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Threading.Tasks;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using Chilligames.Json;
public class Menu : MonoBehaviour
{
    public GameObject Panel_stars;
    public TextMeshPro[] Text_Stars_num;
    public TextMeshProUGUI Text_Username;

    public Transform Place_panel_Chart;
    public Transform Place_other_player;

    public GameObject Raw_stars_panel;
    public GameObject Raw_other_player;

    Status_Stars_model status_Stars;
    User_Panels user_panels;
    Chart Chart_player;


    void Start()
    {
        user_panels = new User_Panels(Text_Username);

        status_Stars = new Status_Stars_model(Text_Stars_num, Panel_stars);
        Chart_player = new Chart(Raw_stars_panel, Place_panel_Chart);
        Chart_player.Instant_other_player(Raw_other_player, Place_other_player);

        user_panels.Quick_Login(() =>
        {
            Text_Username.text = user_panels.Identites_split(Info_model.Selector_model.Nickname);
        });



    }


    private void Update()
    {
        status_Stars.Change_entity_number();
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


    class User_Panels
    {
        TextMeshProUGUI Text_username;
        public string _id = "";
        public string Avatar = "";
        public object Info;
        public object[] Ban = { };
        public object[] Friends = { };
        public object[] Log = { };
        public object[] Files = { };
        public object Data;
        public object[] Inventory = { };
        public object[] Notifactions = { };
        public object Teams;
        public object Wallet;
        public object[] Servers = { };

        public User_Panels(TextMeshProUGUI Text_user_name)
        {
            Text_username = Text_user_name;
            _id = PlayerPrefs.GetString("_id");
        }


        /// <summary>
        /// quick login mikone va meghdar hay player por mikone 
        /// </summary>
        public void Quick_Login(System.Action acti)
        {
            if (PlayerPrefs.GetString("_id").Length > 3)
            {
                Chilligames_SDK.API_Client.Quick_login(new Req_Login { _id = PlayerPrefs.GetString("_id") }, Result_login =>
                {
                    _id = Result_login._id;
                    Avatar = Result_login.Avatar;
                    Info = Result_login.Info;
                    Ban = Result_login.Ban;
                    Friends = Result_login.Friends;
                    Log = Result_login.Log;
                    Files = Result_login.Files;
                    Data = Result_login.Data;
                    Inventory = Result_login.Inventory;
                    Notifactions = Result_login.Notifactions;
                    Teams = Result_login.Teams;
                    Wallet = Result_login.Wallet;
                    Servers = Result_login.Servers;
                    print("user_login");
                    acti();

                }, null);
            }
            else
            {
                print("register");
                Chilligames_SDK.API_Client.Quick_register(result =>
                {

                    PlayerPrefs.SetString("_id", result._id);
                    print(PlayerPrefs.GetString("_id"));
                    Quick_Login(null);

                }, err => { });
            }

        }


        /// <summary>
        /// seay mikone recive kone info player
        /// </summary>
        /// <param name="Select_identite"></param>
        /// <returns></returns>
        public string Identites_split(Info_model.Selector_model Select_identite)
        {
            string result = null;

            switch (Select_identite)
            {
                case Info_model.Selector_model.Username:
                    {
                        result = ChilligamesJson.DeserializeObject<Info_model>(Info.ToString()).Username.ToString();
                    }
                    break;

                case Info_model.Selector_model.Password:
                    {
                        result = ChilligamesJson.DeserializeObject<Info_model>(Info.ToString()).Password.ToString();
                    }
                    break;
                case Info_model.Selector_model.Email:
                    {
                        result = ChilligamesJson.DeserializeObject<Info_model>(Info.ToString()).Email.ToString();
                    }
                    break;
                case Info_model.Selector_model.Nickname:
                    {
                        result = ChilligamesJson.DeserializeObject<Info_model>(Info.ToString()).Nickname.ToString();
                    }
                    break;
            }


            return result;




        }



        
    }


    class Chart
    {
        public GameObject[] Panel_star;
        public Transform Place_chart;
        public GameObject[] other_player = new GameObject[5];

        public Chart(GameObject Raw_model_panel_stars, Transform place_Chart)
        {
            Place_chart = place_Chart;
            Panel_star = new GameObject[Player.mission_Collection.Collection.Length];
            for (int i = 0; i < Panel_star.Length; i++)
            {
                Vector3 pos_panel_star = new Vector3(Player.mission_Collection.Collection[i].transform.position.x, Player.mission_Collection.Collection[i].transform.position.y - 30, 0);
                Panel_star[i] = Instantiate(Raw_model_panel_stars, pos_panel_star, Quaternion.identity, place_Chart);
            }
        }


        public void Instant_other_player(GameObject raw_model_other_player, Transform place_other_player)
        {
            for (int i = 0; i < other_player.Length; i++)
            {
                other_player[i] = Instantiate(raw_model_other_player, place_other_player);
            }

            foreach (var item in other_player)
            {
                print(item.GetComponentsInChildren<TextMeshProUGUI>()[0].text);
            }

        }

    }

}
