using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;

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


    public void Change_Values(string Server_name, string Freeze_number, string mines_number, string delet_number, string Chace_number, string Reset_number, string Active_day, string Player_number, string Like_number, string Coin_number, int? Level_number, object[] Leader_board_server, string _id)
    {
        Text_Name_server.text = Server_name;
        Text_Freeze_number.text = Freeze_number;
        Text_Mines_number.text = mines_number;
        Text_Delete_number.text = delet_number;
        Text_Chance_number.text = Chace_number;
        Text_Reset_number.text = Reset_number;
        Text_Active_day_number.text = Active_day;
        Text_Player_number.text = Player_number;
        Text_Like_number.text = Like_number;
        Text_Coine_number.text = Coin_number;
        Text_Level_number.text = Level_number.ToString();


        for (int i = 0; i < Leader_board_server.Length; i++)
        {
            GameObject filds = Instantiate(Raw_model_fild_ranking, Place_fild_ranking);
            string ID_other_player = ChilligamesJson.DeserializeObject<Deserilies_leader_board>(Leader_board_server[i].ToString()).ID;
            foreach (var Text_Fild_ranking in filds.GetComponentsInChildren<TextMeshProUGUI>())
            {
                switch (Text_Fild_ranking.name)
                {
                    case "Postion":
                        {
                            Text_Fild_ranking.text = i.ToString();
                        }
                        break;
                    case "Name_player":
                        {
                            Text_Fild_ranking.text = "recive name from data base";
                        }
                        break;
                    case "Score":
                        {
                            Text_Fild_ranking.text = ChilligamesJson.DeserializeObject<Deserilies_leader_board>(Leader_board_server[i].ToString()).Score.ToString();
                        }
                        break;
                }
            }

            filds.GetComponent<Button>().onClick.AddListener(() =>
            {
               GameObject Profile= Instantiate(Raw_model_profile);
                Profile.GetComponent<Raw_model_user_profile>()._id = GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
                Profile.GetComponent<Raw_model_user_profile>()._id_other_player = ID_other_player;

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


    class Deserilies_leader_board
    {
        public string ID = null;
        public int? Score = null;
    }


}
