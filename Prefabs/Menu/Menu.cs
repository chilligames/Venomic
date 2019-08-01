using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Threading.Tasks;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
public class Menu : MonoBehaviour
{
    public GameObject Panel_stars;
    public TextMeshPro[] Text_Stars_num;
    public TextMeshProUGUI Text_Username;
    Status_Stars_model status_Stars;
    User_Panels user_panels;

    void Start()
    {
        user_panels = new User_Panels(Text_Username);

        status_Stars = new Status_Stars_model(Text_Stars_num, Panel_stars);

        user_panels.Quick_Login();
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
                            await Task.Delay(10);
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
                            await Task.Delay(10);
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

        public string User_name;
        public string Password;
        public string _id;

        public string Avatar;
        public object[] Identities;
        public object[] Ban;
        public object[] Friends;
        public object[] Log;
        public object[] Files;
        public object[] Data;
        public object[] Inventory;
        public object[] Notifactions;
        public object[] Teams;
        public object[] Wallet;
        public object[] Servers;

        public User_Panels(TextMeshProUGUI Text_user_name)
        {
            Text_username = Text_user_name;
            _id = PlayerPrefs.GetString("Token_Player");
        }

        public void Quick_Login()
        {
            if (PlayerPrefs.GetString("Token_Player").Length > 3)
            {
                print("Logined");
                Chilligames_SDK.API_Client.Quick_login(new Req_Login { _id = PlayerPrefs.GetString("Token_Player") }, null, null);

            }
            else
            {
                print("register");
                Chilligames_SDK.API_Client.Quick_register(result =>
                {

                    PlayerPrefs.SetString("Token_Player", result._id);
                    print(PlayerPrefs.GetString("Token_Player"));
                    Quick_Login();

                }, err => { });
            }

        }



    }

}
