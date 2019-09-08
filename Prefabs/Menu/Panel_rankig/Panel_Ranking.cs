using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK.Model_Client;
using Chilligames.SDK;

public class Panel_Ranking : MonoBehaviour
{
    public GameObject Raw_model_leader_board;

    public TextMeshProUGUI Text_MMR;
    public TextMeshProUGUI Text_Rank;
    public TextMeshProUGUI Text_Servers;
    public TextMeshProUGUI Text_near_you;

    public Button BTN_MMR;
    public Button BTN_Top_player;
    public Button BTN_server;
    public Button BTN_Near_you;


    public string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

    public void Start()
    {
        Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion { Leader_board_name = "Venomic_Top_Player", _id = _id }, Result =>
        {
            Text_MMR.text = Result;

        }, null);
        Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion { Leader_board_name = "Venomic_Ranking", _id = _id }, Result =>
        {
            Text_Rank.text = Result;

        }, null);

        Chilligames_SDK.API_Client.Recive_leader_board_near_user(new Req_recive_leaderboard_near_user { Count = 10, Name_laederboard = "Venomic_Top_Player", _id = _id }, result =>
        {
            print(result[0]._id);
            Text_near_you.text = result[0].Score.ToString();

        }, err => { });

        BTN_MMR.onClick.AddListener(() =>
        {
            Instantiate(Raw_model_leader_board).GetComponent<Raw_Content_ranking>().Name_leader_board = "Venomic_Top_Player";
        });

        BTN_Top_player.onClick.AddListener(() =>
        {
            Instantiate(Raw_model_leader_board).GetComponent<Raw_Content_ranking>().Name_leader_board = "Venomic_Ranking";

        });

    }

}
