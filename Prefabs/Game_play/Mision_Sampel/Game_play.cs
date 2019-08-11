﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine.UI;

/// <summary>
/// playerPref
/// 1: Freez_Count
/// 2: Minus_Count
/// 3: Chance
/// </summary>

public class Game_play : MonoBehaviour
{
    public TextMeshProUGUI Text_Time_number;
    public TextMeshProUGUI Text_Level_number;
    public TextMeshProUGUI Text_Freez_number;
    public TextMeshProUGUI Text_Minus_number;
    public TextMeshProUGUI Text_Time_Panel_Pass;
    public TextMeshProUGUI text_chance_number;

    public GameObject Panel_ui;
    public GameObject Game_object_Panel_pass;
    public RawImage[] Game_objects_Stars_panel_pass;
    Panel_pass_model panel_Pass;
    public GameObject Game_object_Panel_In_zoom;
    public TextMeshProUGUI[] Texts_panel_in_zoom;
    public RawImage[] Stars_in_zoom;
    Panel_Zoom_in panel_Zoom_In;

    public GameObject Panel_BTNs;
    public GameObject[] BTNS;
    public GameObject Game_object_BTN_sampel;

    public Slider Game_object_Slider;
    public RawImage[] Star_slider;
    Slider_model Slider;


    public int Level;
    public int State_pass;
    public int Star;
    public int Reset;

    object[] Pass_map;
    object[] pass_sampel;
    int TotallClick;

    public float Time_mision;
    float Time_local;
    public int start_mision = 0;


    private void Start()
    {
        PlayerPrefs.SetInt("Chance", 20);//cheack

        panel_Pass = new Panel_pass_model(Game_object_Panel_pass, Game_objects_Stars_panel_pass, Text_Time_Panel_Pass, Time_mision);
        panel_Zoom_In = new Panel_Zoom_in(Game_object_Panel_In_zoom, Texts_panel_in_zoom, Stars_in_zoom);
        Text_Freez_number.text = PlayerPrefs.GetInt("Freez_Count").ToString();
        Text_Minus_number.text = PlayerPrefs.GetInt("Minus_Count").ToString();
        Slider = new Slider_model(Game_object_Slider, TotallClick, Star_slider);


        Chek_setup();

        Text_Level_number.text = Level.ToString();

        Text_Time_number.text = Time_mision.ToString();

        Animation_spawn();
        Time_collect();

        async void Animation_spawn()
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
                        if (transform.localScale != Vector3.one)
                        {
                            await Task.Delay(1);
                            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, 0.1f);
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

        //Chek mikone k mission pass shode ya na age nashode baashe mision mision shoro mishe
        async void Time_collect()
        {
            while (true)
            {
                if (State_pass == 0)
                {

                    if (start_mision == 1)
                    {
                        await Task.Delay(100);//sabet bayad 100 bashe

                        Text_Time_number.text = System.Math.Round(Time_local, 1).ToString();
                        Time_local += 0.1f;
                        Slider.Change_entity_slider(Time_local, TotallClick);
                    }
                    else
                    {
                        Time_local = 0;

                        await Task.Delay(1);

                    }
                }
                else
                {
                    break;
                }
            }


        }



    }


    private void Update()
    {

        text_chance_number.text = PlayerPrefs.GetInt("Chance").ToString();

        //check zoom age zoom bashe panel zoom true mikone
        panel_Zoom_In.Show_panel_zoom(Star, Time_mision, Level);

        //Check ta mission pass beshe
        if (State_pass == 0)
        {
            for (int i = 0; i < pass_sampel.Length; i++)
            {
                if (pass_sampel[i] == null && BTNS[i].GetComponent<BTN_sample>().Passed != null)
                {
                    pass_sampel[i] = 1;

                    break;
                }
            }


            if (pass_sampel.SequenceEqual(Pass_map))
            {
                Panel_BTNs.SetActive(false);
                Panel_ui.SetActive(false);
                State_pass = 1;
                Star = Result_mission(Time_local, TotallClick);

                panel_Pass.Show_panel_pass(Star, Time_local);

                panel_Zoom_In.Update_panle_zoom(Star, Time_local);

                Time_mision = Time_local;

                if (Reset == 1)
                {
                    Player.Cam.Move_camera();
                    Reset = 0;
                    Player.mission_Collection.Update_singel_mision(Level);
                    print("reset_mision");
                }
                else
                {
                    Player.Insert_mission(transform.position);
                    print("creat mision");
                }
            }
        }
    }


    /// <summary>
    /// mission misaze va tay reset shodan mission raw estefade mishe
    /// </summary>
    public void Chek_setup()
    {
        if (State_pass == 1)
        {
            panel_Pass.Show_panel_pass(Star, Time_mision);

            Panel_BTNs.SetActive(false);
            Panel_ui.SetActive(false);

        }
        else
        {
            panel_Pass.Show_off_panel_pass();
            Panel_BTNs.SetActive(true);

            for (int i = 0; i < BTNS.Length; i++)//vaghti reset mishe toy zoom bayad btn ghabli destroy beshan
            {
                Destroy(BTNS[i]);
            }

            if (Level < 100)
            {

                int Count = Random.Range(2, 5);
                BTNS = new GameObject[Count];
                for (int i = 0; i < Count; i++)
                {
                    BTNS[i] = Instantiate(Game_object_BTN_sampel, Panel_BTNs.transform);
                    BTNS[i].GetComponent<BTN_sample>().Sampel_Click = Random.Range(1, 11);
                }
            }
            else if (Level < 300)
            {

                int Count = Random.Range(4, 8);
                BTNS = new GameObject[Count];
                for (int i = 0; i < Count; i++)
                {
                    BTNS[i] = Instantiate(Game_object_BTN_sampel, Panel_BTNs.transform);
                    BTNS[i].GetComponent<BTN_sample>().Sampel_Click = Random.Range(1, 11);
                }
            }
            else if (Level > 500)
            {
                int Count = Random.Range(6, 11);
                BTNS = new GameObject[Count];
                for (int i = 0; i < Count; i++)
                {
                    BTNS[i] = Instantiate(Game_object_BTN_sampel, Panel_BTNs.transform);
                    BTNS[i].GetComponent<BTN_sample>().Sampel_Click = Random.Range(1, 11);
                }
            }


            Pass_map = new object[BTNS.Length];
            pass_sampel = new object[BTNS.Length];

            for (int i = 0; i < Pass_map.Length; i++)
            {
                Pass_map[i] = 1;
            }


            //tedad Click hay mision dar miarde baray mohasebe
            for (int i = 0; i < BTNS.Length; i++)
            {
                TotallClick += BTNS[i].GetComponent<BTN_sample>().Sampel_Click;
            }

            Slider = new Slider_model(Game_object_Slider, TotallClick, Star_slider);

        }

    }


    /// <summary>
    /// 1: meghdar haro 0 mikone 
    /// 2:reset 1 mikone baray inke load mishe reset bashe
    /// 3:reset mission run mishe
    /// 4: run ejra mishe bara chek
    /// </summary>
    public void Reset_mision()
    {
        State_pass = 0;
        Star = 0;
        Time_mision = 0;
        Time_local = 0;
        start_mision = 0;
        TotallClick = 0;
        Panel_ui.SetActive(true);
        Reset = 1;
        Slider.Reset_slider();
        panel_Zoom_In.Update_panle_zoom(Star, Time_local);
        Player.mission_Collection.Reset_mision(Level);
        panel_Pass.Reset_Panel_pass();
        Start();
    }


    /// <summary>
    /// mission jadid ba meghdarhay jadid misaze
    /// </summary>
    public void Press_BTN_Reset_Mission_raw()
    {
        State_pass = 0;
        start_mision = 0;
        Time_local = 0;
        Time_mision = 0;
        TotallClick = 0;
        Star = 0;
        Text_Time_number.text = "0";
        Slider.Reset_slider();
        panel_Zoom_In.Update_panle_zoom(Star, Time_local);
        panel_Pass.Reset_Panel_pass();
        Chek_setup();
    }


    /// <summary>
    /// mission mire be on migheiat
    /// </summary>
    public void Press_panel_in_zoom()
    {
        panel_Zoom_In.Go_to_mission(transform.position);
    }


    /// <summary>
    /// animation_hint freez mikone az btn_migire etelaeatesho
    /// </summary>
    public void Press_BTN_Freez()
    {
        if (PlayerPrefs.GetInt("Freez_Count") > 0)
        {
            PlayerPrefs.SetInt("Freez_Count", PlayerPrefs.GetInt("Freez_Count") - 1);
            Text_Freez_number.text = PlayerPrefs.GetInt("Freez_Count").ToString();
            foreach (var item in BTNS)
            {
                item.GetComponent<BTN_sample>().Freez = 1;
            }
        }
        else
        {
            print("no_freezz");
        }

        print("Code_freez" + "animation change number");
    }


    /// <summary>
    /// tedad sampel_count hay btn kam mikone ta 2
    /// </summary>
    public void Press_BTN_Rebeat()
    {
        PlayerPrefs.SetInt("Minus_Count", 20);

        if (PlayerPrefs.GetInt("Minus_Count") > 0)
        {

            foreach (var item in BTNS)
            {
                if (item.GetComponent<BTN_sample>().Sampel_Click > 1)
                {
                    item.GetComponent<BTN_sample>().Sampel_Click -= 1;
                    PlayerPrefs.SetInt("Minus_Count", PlayerPrefs.GetInt("Minus_Count") - 1);
                    Text_Minus_number.text = PlayerPrefs.GetInt("Minus_Count").ToString();

                    item.GetComponent<BTN_sample>().Animation_Minus();
                    TotallClick -= 1;
                    print(TotallClick);
                    Slider.Update_max_value_slider(TotallClick);
                }
                else
                {
                    print("we cant help_u");
                }

            }

        }

    }


    /// <summary>
    /// vaghti press mishe yeki az gozineha hazf mishe 
    /// </summary>
    public void Press_BTN_Dellima()
    {
        if (BTNS.Length > 1)
        {
            for (int i = 0; i < BTNS.Length; i++)
            {

                if (i + 1 == BTNS.Length)
                {
                    BTNS[i].GetComponent<BTN_sample>().Delete_animation_btn();

                    TotallClick -= BTNS[i].GetComponent<BTN_sample>().Sampel_Click;

                    GameObject[] New_btns = new GameObject[BTNS.Length - 1];
                    object[] new_pass_map = new object[BTNS.Length - 1];
                    object[] new_Pass_sampel = new object[BTNS.Length - 1];
                    for (int a = 0; a < New_btns.Length; a++)
                    {
                        New_btns[a] = BTNS[a];
                        new_pass_map[a] = Pass_map[a];
                        new_Pass_sampel[a] = pass_sampel[a];
                    }

                    BTNS = New_btns;
                    Pass_map = new_pass_map;
                    pass_sampel = new_Pass_sampel;

                    Slider.Update_max_value_slider(TotallClick);
                    Slider.Change_entity_slider(Time_local, TotallClick);
                }

            }
        }

    }


    /// <summary>
    /// chek mikone mission age chance dashte bashe kam mikone nadashte bashe reset mione mission 
    /// </summary>
    public void Minus_Chance()
    {
        Animation_minus_text();

        async void Animation_minus_text()
        {
            while (true)
            {
                if (text_chance_number.transform.localScale != Vector3.zero)
                {
                    await Task.Delay(1);
                    text_chance_number.transform.localScale = Vector3.MoveTowards(text_chance_number.transform.localScale, Vector3.zero, 0.1f);
                }
                else
                {
                    while (true)
                    {
                        if (text_chance_number.transform.localScale != Vector3.one)
                        {
                            text_chance_number.transform.localScale = Vector3.MoveTowards(text_chance_number.transform.localScale, Vector3.one, 0.1f);
                        }
                        else
                        {
                            minuse_chance();

                            break;
                        }
                    }
                    break;
                }
            }

        }

        void minuse_chance()
        {
            if (PlayerPrefs.GetInt("Chance") > 0)
            {
                Handheld.Vibrate();
                PlayerPrefs.SetInt("Chance", PlayerPrefs.GetInt("Chance") - 1);
            }
            else
            {
                Handheld.Vibrate();
                Handheld.Vibrate();
                Press_BTN_Reset_Mission_raw();
            }


        }
    }


    /// <summary>
    /// formul star chek mikone va setare morede nazar bar migardone
    /// </summary>
    /// <param name="Final_time"> Time_player </param>
    /// <param name="Totall_Click"> totall CLick hay mission</param>
    /// <returns>count star</returns>
    int Result_mission(float Final_time, int Totall_Click)
    {
        float master = Totall_Click * 0.75f;
        float Star_3 = Totall_Click * 1.5f;
        float Star_2 = Totall_Click * 2.5f;
        float star_1 = Totall_Click * 3f;

        if (Final_time <= master)
        {
            return 4;
        }
        else if (Final_time <= Star_3)
        {
            return 3;
        }
        else if (Final_time <= Star_2)
        {
            return 2;
        }
        else if (Final_time >= star_1)
        {
            return 1;
        }
        else
        {
            return 1;
        }

    }


    class Panel_pass_model
    {
        public GameObject Panel_pass;
        public RawImage[] Stars;
        TextMeshProUGUI Text_Time;
        float Time_pass;
        Color color_no_pass= new Color(0.63f, 0.87f, 0.8f, 0.2f);
        Color Color_pass = new Color(0.63f, 0.87f, 0.8f, 1f);
        /// <summary>
        /// panel_pass misaze ba panle darkhasti k sazande darre
        /// </summary>
        /// <param name="Panel_pass_gameobject"> Game_object panel_pass</param>
        /// <param name="Stars"> count stars</param>
        public Panel_pass_model(GameObject Panel_pass_gameobject, RawImage[] Stars, TextMeshProUGUI Text_Time_pass, float Time_pass)
        {
            Panel_pass = Panel_pass_gameobject;
            this.Stars = Stars;
            Text_Time = Text_Time_pass;
            this.Time_pass = Time_pass;
        }


        /// <summary>
        /// be tedad setareha stare rangesho avaz mikone
        /// va sho mikone panle pass
        /// </summary>
        /// <param name="star_count"> tedad setarehay be dast omade</param>
        public void Show_panel_pass(int star_count, float Time_mission)
        {
            Text_Time.text = System.Math.Round(Time_pass, 1).ToString();
            Text_Time.text = System.Math.Round(Time_mission, 1).ToString();

            for (int i = 0; i < star_count; i++)
            {
                Stars[i].color =Color_pass;
            }

            Panel_pass.SetActive(true);

        }


        /// <summary>
        /// panle_off mikone
        /// </summary>
        public void Show_off_panel_pass()
        {

            Panel_pass.SetActive(false);

        }

        /// <summary>
        /// color panel_pass reset mikone 
        /// </summary>
        public void Reset_Panel_pass()
        {
            for (int i = 0; i < Stars.Length; i++)
            {
                Stars[i].color = color_no_pass;
            }
        }



    }


    class Panel_Zoom_in
    {
        public GameObject panel_zoom;
        public TextMeshProUGUI[] text_panel_zoom;
        public RawImage[] Stars;
        Color Color_no_pass = new Color(0.63f, 0.87f, 0.8f, 0.2f);
        Color Color_pass = new Color(0.63f, 0.87f, 0.8f, 1f);

        public Panel_Zoom_in(GameObject Game_object_Panel_zoom, TextMeshProUGUI[] Texts_panel_zoom, RawImage[] Stars)
        {
            panel_zoom = Game_object_Panel_zoom;
            this.Stars = Stars;
            text_panel_zoom = Texts_panel_zoom;

        }


        /// <summary>
        /// vaghti to zoom bashe jaygozari ha anjam mishe
        /// </summary>
        /// <param name="Stars">tedad star chek mikone</param>
        /// <param name="Time">tedad time jamizare</param>
        /// <param name="level">meghdar time mizare</param>
        public void Show_panel_zoom(int Stars, float Time, int level)
        {
            text_panel_zoom[0].text = System.Math.Round(Time, 1).ToString();
            text_panel_zoom[1].text = level.ToString();
            for (int i = 0; i < Stars; i++)
            {
                this.Stars[i].color = Color_pass;
            }

            animation_show();

            async void animation_show()
            {
                if (Player.Cam.Zoom == 1)
                {

                    while (true)
                    {

                        if (panel_zoom.transform.localScale != Vector3.one)
                        {
                            await Task.Delay(1);
                            panel_zoom.transform.localScale = Vector3.MoveTowards(panel_zoom.transform.localScale, Vector3.one, 0.1f);
                        }
                        else
                        {
                            panel_zoom.SetActive(true);
                            break;
                        }
                    }
                }
                else if (Player.Cam.Zoom == 0)
                {
                    while (true)
                    {
                        if (panel_zoom.transform.localScale != Vector3.zero)
                        {
                            await Task.Delay(1);
                            panel_zoom.transform.localScale = Vector3.MoveTowards(panel_zoom.transform.localScale, Vector3.zero, 0.1f);
                        }
                        else
                        {
                            panel_zoom.SetActive(false);
                            break;
                        }
                    }
                }
            }

        }


        /// <summary>
        /// update mikone mission vaghti k mission pass mishe
        /// </summary>
        /// <param name="Star"> star count</param>
        /// <param name="Time_local">time local</param>
        public void Update_panle_zoom(int Star, float Time_local)
        {
            text_panel_zoom[0].text = System.Math.Round(Time_local, 1).ToString();
            for (int i = 0; i < Stars.Length; i++)
            {
                Stars[i].color = Color_no_pass;
            }
        }


        /// <summary>
        /// camera mibare b mooghiati k behesh midan size cam cam mikone
        /// </summary>
        /// <param name="Position_mission"></param>
        public void Go_to_mission(Vector3 Position_mission)
        {


            Move();

            async void Move()
            {
                Player.Cam.Zoom = 0;

                while (true)
                {
                    if (Player.cam.transform.position != Position_mission)
                    {
                        await Task.Delay(1);
                        Player.cam.transform.position = Vector3.MoveTowards(Player.cam.transform.position, Position_mission, 0.1f);
                        panel_zoom.transform.localScale = Vector3.MoveTowards(panel_zoom.transform.localScale, Vector3.zero, 0.2f);

                        if (panel_zoom.transform.localScale == Vector3.zero)
                        {
                            panel_zoom.SetActive(false);
                        }
                    }
                    else
                    {
                        if (Player.cam.orthographicSize >= 6)
                        {
                            await Task.Delay(1);
                            Player.cam.orthographicSize -= 1;
                            if (Player.cam.orthographicSize <= 6)
                            {
                                Player.cam.orthographicSize = 6;
                                break;
                            }

                        }
                    }

                }

            }



        }


    }


    class Slider_model
    {
        Slider Slider;
        RawImage[] Stars;
        Color Color_Off = new Color(0.63f,0.87f,0.8f, 0.2f);
        public Slider_model(Slider slider, float totallcount, RawImage[] Stars)
        {
            Slider = slider;
            Slider.maxValue = totallcount * 3;
            this.Stars = Stars;
        }


        /// <summary>
        /// meghdar value slider taein mikone k bayad *3 beshe bekhater setareha
        /// </summary>
        /// <param name="time"> time local migere baed jagozari mikone</param>
        public void Change_entity_slider(float time, int Total_Click)
        {

            Slider.value = time;
            float Master = Total_Click * 0.75f;
            float Star_3 = Total_Click * 1.5f;
            float Star_2 = Total_Click * 2.5f;


            if (time >= Master)
            {
                Stars[0].color = Color.Lerp(Stars[0].color, Color_Off,0.5f);
            }
            if (time >= Star_3)
            {
                Stars[1].color = Color.Lerp(Stars[1].color, Color_Off,0.5f);
            }
            if (time >= Star_2)
            {
                Stars[2].color = Color.Lerp(Stars[2].color, Color_Off, 0.5f);
            }
           
        }


        /// <summary>
        /// slider reset mikone 
        /// </summary>
        public void Reset_slider()
        {
            Slider.value = 0;
            Slider.maxValue = 0;
            for (int i = 0; i < Stars.Length; i++)
            {
                Stars[i].color = Color.black;
            }


        }


        /// <summary>
        /// update mikone max slider toy destroy khoneha 
        /// </summary>
        /// <param name="Total_click">totall click </param>
        public void Update_max_value_slider(int Total_click)
        {
            Slider.maxValue = Total_click * 3;
        }
    }


}
