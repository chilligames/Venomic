using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Ranking : MonoBehaviour
{
    public GameObject Raw_model_leader_board;

    public TextMeshProUGUI Text_Rank;
    public TextMeshProUGUI Text_near_you;

    public Button BTN_Top_player;


    public string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }


    private void OnEnable()
    {
        Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion {Leader_board_name= "Venomic_Top_Player", _id=_id }, result => {
            Text_Rank.text = result;
        }, err => { });

        BTN_Top_player.onClick.AddListener(() => {
            Instantiate(Raw_model_leader_board).GetComponent<Raw_Content_ranking>().Change_value("Venomic_Top_Player");
        });

      
    }

}
