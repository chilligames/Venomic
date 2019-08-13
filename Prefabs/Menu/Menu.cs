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


    public GameObject[] Panels;
    public GameObject[] BTN_tabs;
    public GameObject Holder_background;

    public GameObject[] Sub_panel_home;

    GameObject Curent_panel;
    GameObject Curent_Tab;

    Status_Stars_model status_Stars;
    Panel_Signal Signal;
    Panel_Ranking Ranking;
    User_areas user_panels;


    void Start()
    {

        user_panels = new User_areas(Text_Username, Panels[1], transform, Sub_panel_home);

        status_Stars = new Status_Stars_model(Text_Stars_num, Panel_stars);

        Signal = new Panel_Signal(Panels[0].GetComponentInChildren<Button>(), BTN_tabs[0].GetComponent<Button>());

        Ranking = new Panel_Ranking(Panels[2]);

        Curent_panel = Panels[1];
        Curent_Tab = BTN_tabs[1];

        user_panels.Quick_Login(() =>
        {
            Text_Username.text = user_panels.Info_desrialize().Nickname.ToString();
            user_panels.Update_user_data();
            user_panels.Send_Score_to_leader_board();

        });
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
        Destroy(user_panels.Sub_panels);

        Change_color_tab_BTN();

        animation_curent_panel();

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

    class User_areas
    {
        TextMeshProUGUI Text_username;
        TextMeshProUGUI Text_Rank_number;
        TextMeshProUGUI Text_Stars_number;
        TextMeshProUGUI Text_level_number;
        Button BTN_edit_profile;
        GameObject Curent_panel;
        public GameObject Sub_panels;
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


        public User_areas(TextMeshProUGUI Text_user_name, GameObject Panel_home, Transform Place_instant_sub_panel, GameObject[] Sub_panel)
        {
            Text_username = Text_user_name;
            _id = PlayerPrefs.GetString("_id");
            Curent_panel = Panel_home;

            foreach (var item in Panel_home.GetComponentsInChildren<TextMeshProUGUI>())
            {
                switch (item.name)
                {
                    case "Text_Stars_number":
                        {
                            Text_Stars_number = item;
                        }
                        break;
                    case "Text_Rank_number":
                        {
                            Text_Rank_number = item;
                        }
                        break;
                    case "Text_level_number":
                        {
                            Text_level_number = item;
                        }
                        break;
                }

            }


            foreach (var BTN_panel_home in Panel_home.GetComponentsInChildren<Button>())
            {
                switch (BTN_panel_home.name)
                {
                    case "BEP":
                        {
                            Button.ButtonClickedEvent Event_BTN = new Button.ButtonClickedEvent();

                            TMP_InputField inputFild_nickname = null;
                            TMP_InputField inputFild_username = null;
                            TMP_InputField inputFild_email = null;
                            TMP_InputField inputField_password = null;
                            TMP_InputField inputField_status = null;

                            Event_BTN.AddListener(Press_btn__Edit);

                            BTN_edit_profile = BTN_panel_home;
                            BTN_edit_profile.onClick = Event_BTN;

                            void Press_btn__Edit()
                            {
                                Sub_panels = Instantiate(Sub_panel[0], Place_instant_sub_panel);
                                Curent_panel.SetActive(false);

                                foreach (var Inputfilds in Sub_panels.GetComponentsInChildren<TMP_InputField>())
                                {
                                    switch (Inputfilds.name)
                                    {
                                        case "IFNEP":
                                            {
                                                inputFild_nickname = Inputfilds;
                                            }
                                            break;
                                        case "IFUEP":
                                            {
                                                inputFild_username = Inputfilds;

                                            }
                                            break;
                                        case "IFEEP":
                                            {
                                                inputFild_email = Inputfilds;
                                            }
                                            break;
                                        case "IFPEP":
                                            {
                                                inputField_password = Inputfilds;

                                            }
                                            break;
                                        case "IFSEP":
                                            {
                                                inputField_status = Inputfilds;
                                            }
                                            break;
                                    }

                                }

                                foreach (var BTNS in Sub_panels.GetComponentsInChildren<Button>())
                                {
                                    switch (BTNS.name)
                                    {
                                        case "BCEP":
                                            {
                                                Button.ButtonClickedEvent Event_Close = new Button.ButtonClickedEvent();
                                                Event_Close.AddListener(() =>
                                                {
                                                    Curent_panel.SetActive(true);
                                                    Destroy(Sub_panels);

                                                });

                                                BTNS.onClick = Event_Close;
                                            }
                                            break;
                                        case "BSEP":
                                            {
                                                Button.ButtonClickedEvent Submit_change = new Button.ButtonClickedEvent();
                                                Submit_change.AddListener(() =>
                                                {


                                                    Chilligames_SDK.API_Client.Update_User_Info(new Req_Update_User_Info { Email = inputFild_email.text, Nickname = inputFild_nickname.text, Password = inputField_password.text, status = inputField_status.text, Username = inputFild_username.text, _id = _id }, () =>
                                                    {
                                                        SceneManager.LoadScene(0);


                                                    }, null);

                                                });

                                                BTNS.onClick = Submit_change;

                                            }
                                            break;
                                    }
                                }

                            }


                        }
                        break;
                }
            }

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
        /// data player update  az file dakheli  
        /// </summary>
        public void Update_user_data()
        {

            StreamReader reader = new StreamReader(Application.persistentDataPath + "/Info.Chi");

            string data_for_send = ChilligamesJson.EscapeToJavascriptString(reader.ReadToEnd());

            Chilligames_SDK.API_Client.Send_Data_user(new Req_send_data { _id = _id, Name_app = "Venomic", Data_user = data_for_send }, () =>
            {

            }, null);

        }


        /// <summary>
        /// score mohasebe mikone baed send mikone to leader board 
        /// meghdar hay rank star mision update mishe 
        /// </summary>
        public void Send_Score_to_leader_board()
        {
            send_score();

            async void send_score()
            {
                int Score = 0;

                StreamReader reader = new StreamReader(Application.persistentDataPath + "/Info.Chi");
                string data_user = await reader.ReadToEndAsync();
                var data = JsonUtility.FromJson<Player.Entity_player_model>(data_user);

                foreach (var item in data.S)
                {
                    Score += item;
                }

                Text_Stars_number.text = Score.ToString();
                Text_level_number.text = (data.Pos_G.Length - 1).ToString();


                Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion { Leader_board_name = "Venomic", _id = _id }, result =>
                {

                    Text_Rank_number.text = result;

                }, err => { });

                Chilligames_SDK.API_Client.Send_Score_to_leader_board(new Req_send_score { _id = _id, Leader_board_name = "Venomic", Score = Score }, () =>
                 {
                     print("score send");

                 }); ;

            }
        }


        /// <summary>
        /// seay mikone recive kone info player
        /// </summary>
        /// <param name="Select_identite"></param>
        /// <returns></returns>
        public Info_model Info_desrialize()
        {
            Info_model Info_desrialise = ChilligamesJson.DeserializeObject<Info_model>(Info.ToString());

            return Info_desrialise;
        }

    }


    class Panel_Signal
    {
        Button Button_cheack_net;
        TextMeshProUGUI Text_button;
        Button Button_tab_signal;
        Button.ButtonClickedEvent ButtonClickedEvent = new Button.ButtonClickedEvent();

        public Panel_Signal(Button button_cheack_network, Button Tab_signal)
        {
            ButtonClickedEvent.AddListener(Cheack_net);
            button_cheack_network.onClick = ButtonClickedEvent;
            Button_cheack_net = button_cheack_network;
            Text_button = button_cheack_network.GetComponentInChildren<TextMeshProUGUI>();
            Button_tab_signal = Tab_signal;
            Cheack_net();
        }


        /// <summary>
        /// net cheack mikone
        /// </summary>
        void Cheack_net()
        {
            Animation_cheack();
            Cheack();
            async void Animation_cheack()
            {


                while (true)
                {
                    if (Text_button.transform.localScale != Vector3.zero)
                    {
                        await Task.Delay(1);
                        Text_button.transform.localScale = Vector3.MoveTowards(Text_button.transform.localScale, Vector3.zero, 0.1f);
                    }
                    else
                    {
                        Text_button.text = ". . .";
                        while (true)
                        {
                            if (Text_button.transform.localScale != Vector3.one)
                            {
                                await Task.Delay(1);
                                Text_button.transform.localScale = Vector3.MoveTowards(Text_button.transform.localScale, Vector3.one, 0.1f);
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

            async void Cheack()
            {
                UnityWebRequest www = UnityWebRequest.Get("http://google.com");
                www.SendWebRequest();
                while (true)
                {
                    if (www.isDone)
                    {
                        Button_cheack_net.GetComponent<Image>().fillAmount = 1;
                        Button_cheack_net.GetComponent<Image>().color = Color.green;
                        Text_button.text = "Good Connection";
                        while (true) //animation color_signal
                        {
                            if (Button_tab_signal.GetComponentInChildren<RawImage>().color != Color.green)
                            {
                                await Task.Delay(1);
                                Button_tab_signal.GetComponentInChildren<RawImage>().color = Color.Lerp(Button_tab_signal.GetComponentInChildren<RawImage>().color, Color.green, 0.1f);
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    }
                    else
                    {
                        Button_tab_signal.GetComponentInChildren<RawImage>().color = Color.red;
                        Button_cheack_net.GetComponent<Image>().fillAmount = www.downloadProgress;
                        await Task.Delay(1);
                        if (www.isNetworkError || www.isHttpError || www.timeout == 1)
                        {
                            print("error_signal");
                            break;
                        }
                    }
                }
            }
        }

    }




    class Panel_Ranking
    {

        Button BTN_Right;
        Button BTN_left;
        Button BTN_MMR;
        Button BTN_Top_player;
        Button BTN_servers;
        Button BTN_nearby;


        public Panel_Ranking(GameObject Panel_ranking)
        {
         
        }


    }

}
