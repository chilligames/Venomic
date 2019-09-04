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

    TextMeshProUGUI Text_MMR;
    TextMeshProUGUI Text_Rank;
    TextMeshProUGUI Text_Servers;
    TextMeshProUGUI Text_near_you;

    Button BTN_enter_mmr_ranking;
    Button BTN_enter_top_player;
    Button BTN_enter_top_active_servers;
    Button BTN_enter_near_by;

    public GameObject Curent_sub_panel = null;


    public void Start()
    {

    }


    /// <summary>
    /// recive mikone rank 
    /// </summary>
    /// <param name="_id"> before use need  intiles </param>
    public void Recive_ranking(string _id)
    {
        Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion { Leader_board_name = "Venomic", _id = _id }, Result =>
        {
            Text_MMR.text = Result;

        }, null);

        Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion { Leader_board_name = "Venomic_Top_player", _id = _id }, Result =>
        {
            Text_Rank.text = Result;

        }, null);


        Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion { Leader_board_name = "Venomic_Servers", _id = _id }, Result =>
        {
            Text_Servers.text = Result;

        }, null);

    }


    public class Schema_other_player
    {
        public object _id = null;
        public object Info = null;
        public object Inventory = null;

        public class DeserilseInfoPlayer
        {
            public string Status = null;
            public string Nickname = null;

        }
    }




}
