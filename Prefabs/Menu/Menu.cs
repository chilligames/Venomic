using System.Collections;
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
    public GameObject Raw_player_chart;
    Status_Stars_model status_Stars;
    User_Panels user_panels;
    Chart Chart_player;


    void Start()
    {
        user_panels = new User_Panels(Text_Username);

        status_Stars = new Status_Stars_model(Text_Stars_num, Panel_stars);
        Chart_player = new Chart(Raw_player_chart, Place_panel_Chart);

        user_panels.Quick_Login();
        Change_user_name();

        async void Change_user_name()
        {

            while (true)
            {
                try
                {
                    Text_Username.text = user_panels.Identites_split(User_Panels.Identites_selection.Nickname);
                    if (user_panels.Identites_split(User_Panels.Identites_selection.Nickname).Length > 1)
                    {
                        break;
                    }
                }
                catch (System.NullReferenceException)
                {
                    await Task.Delay(1);
                }
            }

        }
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
        public object Identities;
        public object[] Ban = { };
        public object[] Friends = { };
        public object[] Log = { };
        public object[] Files = { };
        public object[] Data = { };
        public object[] Inventory = { };
        public object[] Notifactions = { };
        public object Teams;
        public object[] Wallet = { };
        public object[] Servers = { };

        public User_Panels(TextMeshProUGUI Text_user_name)
        {
            Text_username = Text_user_name;
            _id = PlayerPrefs.GetString("Token_Player");
        }


        /// <summary>
        /// quick login mikone va meghdar hay player por mikone 
        /// </summary>
        public void Quick_Login()
        {
            if (PlayerPrefs.GetString("Token_Player").Length > 3)
            {
                Chilligames_SDK.API_Client.Quick_login(new Req_Login { _id = PlayerPrefs.GetString("Token_Player") }, Result_login =>
                {
                    _id = Result_login._id;
                    Avatar = Result_login.Avatar;
                    Identities = Result_login.Identities;
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

                }, null);
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



        /// <summary>
        /// seay mikone recive kone info player
        /// </summary>
        /// <param name="Select_identite"></param>
        /// <returns></returns>
        public string Identites_split(Identites_selection Select_identite)
        {
            string result = null;
            recive();

            async void recive()
            {
                while (true)
                {
                    try
                    {
                        switch (Select_identite)
                        {
                            case Identites_selection.Username:
                                {
                                    result = ChilligamesJson.DeserializeObject<Identites_model>(Identities.ToString()).Username.ToString();
                                }
                                break;

                            case Identites_selection.Password:
                                {
                                    result = ChilligamesJson.DeserializeObject<Identites_model>(Identities.ToString()).Password.ToString();
                                }
                                break;
                            case Identites_selection.Email:
                                {
                                    result = ChilligamesJson.DeserializeObject<Identites_model>(Identities.ToString()).Email.ToString();
                                }
                                break;
                            case Identites_selection.Nickname:
                                {
                                    result = ChilligamesJson.DeserializeObject<Identites_model>(Identities.ToString()).Nickname.ToString();
                                }
                                break;
                        }

                        if (result != null)
                        {
                            break;
                        }

                    }
                    catch (System.NullReferenceException)
                    {
                        await Task.Delay(1);
                    }
                }
            }
            return result;
        }


        public enum Identites_selection
        {
            Username, Password, Email, Nickname
        }

    }

    class Chart
    {
        public GameObject[] Chart_player = new GameObject[10];
        public Transform Place_chart;

        public Chart(GameObject Raw_model_chart_player, Transform place_Chart)
        {
            Place_chart = place_Chart;

            for (int i = 0; i < Chart_player.Length; i++)
            {
                Chart_player[i] = Instantiate(Raw_model_chart_player, place_Chart);
                Chart_player[i].GetComponentInChildren<LineRenderer>().SetPosition(0, new Vector3(10, 0, 0));
                Chart_player[i].GetComponentInChildren<LineRenderer>().SetPosition(1, new Vector3(Player.mission_Collection.last_pos.x, Player.mission_Collection.last_pos.y - 10, 0));
            }
        }


        public void Draw_chart()
        {


        }
    }//last_change

}
