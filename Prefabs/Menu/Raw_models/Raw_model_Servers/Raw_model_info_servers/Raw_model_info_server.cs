using System.Collections.Generic;
using System.Collections;
using Chilligames.Json;
using Chilligames.SDK;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Raw_model_info_server : MonoBehaviour
{

    public TextMeshProUGUI Text_Name_server;
    public TextMeshProUGUI Text_Freeze_number;
    public TextMeshProUGUI Text_Mines_number;
    public TextMeshProUGUI Text_Delete_number;
    public TextMeshProUGUI Text_Chance_number;
    public TextMeshProUGUI Text_Reset_number;
    public TextMeshProUGUI Text_Active_day_number;
    public TextMeshProUGUI Text_Player_number;
    public TextMeshProUGUI Text_Like_number;
    public TextMeshProUGUI Text_Coine_number;
    public TextMeshProUGUI Text_Level_number;
    public Button BTN_close_panel;
    public GameObject Raw_model_fild_ranking;
    public GameObject Raw_model_profile;
    public Transform Place_fild_ranking;

    public string id_player
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    public void Change_Values(string Server_name, string Freeze_number, string mines_number, string delet_number, string Chace_number, string Reset_number, int Active_day, string Player_number, string Like_number, string Coin_number, string Level_number, object[] Leader_board_server, string _id)
    {
        Text_Name_server.text = Server_name;
        Text_Freeze_number.text = Freeze_number;
        Text_Mines_number.text = mines_number;
        Text_Delete_number.text = delet_number;
        Text_Chance_number.text = Chace_number;
        Text_Reset_number.text = Reset_number;
        Text_Active_day_number.text = Active_day.ToString();
        Text_Player_number.text = Player_number;
        Text_Like_number.text = Like_number;
        Text_Coine_number.text = Coin_number;
        Text_Level_number.text = Level_number;


        //sort score player
        int?[] scores = new int?[Leader_board_server.Length];
        string[] IDs = new string[Leader_board_server.Length];

        for (int i = 0; i < Leader_board_server.Length; i++)
        {
            scores[i] = ChilligamesJson.DeserializeObject<Deserilies_leader_board>(Leader_board_server[i].ToString()).Score;
        }

        for (int i = 0; i < Leader_board_server.Length; i++)
        {

            IDs[i] = ChilligamesJson.DeserializeObject<Deserilies_leader_board>(Leader_board_server[i].ToString()).ID;
        }

        Array.Sort(scores);
        Array.Reverse(scores);

        string[] id_sort = new string[Leader_board_server.Length];

        for (int i = 0; i < Leader_board_server.Length; i++)
        {

            for (int A = 0; A < Leader_board_server.Length; A++)
            {
                if (scores[i] == ChilligamesJson.DeserializeObject<Deserilies_leader_board>(Leader_board_server[A].ToString()).Score)
                {
                    id_sort[i] = ChilligamesJson.DeserializeObject<Deserilies_leader_board>(Leader_board_server[A].ToString()).ID;
                    break;
                }
            }
        }

        //instatn fild rankking
        for (int i = 0; i < id_sort.Length; i++)
        {
            var fild_rankin = Instantiate(Raw_model_fild_ranking, Place_fild_ranking);

            foreach (var Texts in fild_rankin.GetComponentsInChildren<TextMeshProUGUI>())
            {

                switch (Texts.name)
                {
                    case "Postin":
                        {
                            Texts.text = i.ToString();
                        }
                        break;
                    case "Name_player":
                        {
                            Chilligames_SDK.API_Client.Recive_info_user(new Chilligames.SDK.Model_Client.Req_recive_Info_player { _id = id_sort[i] }, result =>
                               {
                                   Texts.text = result.Nickname;
                               }, err => { });
                            Texts.text = id_sort[i];
                        }
                        break;
                    case "Score":
                        {
                            Texts.text = scores[i].ToString();
                        }
                        break;
                }
            }
            var id_other_player = id_sort[i];

            fild_rankin.GetComponent<Button>().onClick.AddListener(() =>
            {
                Instantiate(Raw_model_profile).GetComponent<Raw_model_user_profile>().Change_value(id_player, id_other_player);
            });
        }

    }



    void Start()
    {
        BTN_close_panel.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });

    }


    public class Deserilies_leader_board
    {
        public string ID = null;
        public int? Score = null;
    }


}

