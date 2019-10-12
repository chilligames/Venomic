using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Ranking : MonoBehaviour
{
    public GameObject Raw_model_leader_board;
    public GameObject Raw_model_fild_leaderboard;
    public GameObject Raw_model_profile_player;

    public TextMeshProUGUI Text_Rank;

    public Button BTN_Top_player;

    public Transform Place_spawn_score_near_player;

    GameObject[] Fild_leaderboard;
    public string _id
    {
        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }


    private void Start()
    {
        
        BTN_Top_player.onClick.AddListener(() =>
        {
            Instantiate(Raw_model_leader_board).GetComponent<Raw_Content_ranking>().Change_value("Venomic_Top_Player");
        });
    }
    private void OnEnable()
    {
        //destroy after enable
        if (Fild_leaderboard!=null)
        {
            for (int i = 0; i < Fild_leaderboard.Length; i++)
            {
                Destroy(Fild_leaderboard[i]);
            }
        }

        //recive entity ranking
        Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion { Leader_board_name = "Venomic_Top_Player", _id = _id }, result =>
        {
            Text_Rank.text = result;
        }, err => { });
       
        Chilligames_SDK.API_Client.Recive_leader_board_near_user(new Req_recive_leaderboard_near_user { Count = 20, Name_laederboard = "Venomic_Top_Player", _id = _id }, result =>
        {
            Fild_leaderboard = new GameObject[result.Length];

            for (int i = 0; i < result.Length; i++)
            {
                Fild_leaderboard[i] = Instantiate(Raw_model_fild_leaderboard, Place_spawn_score_near_player);
                Fild_leaderboard[i].AddComponent<Raw_Fild_leaderboard>().Change_value(_id, result[i]._id, i, result[i].Nickname, result[i].Score, Raw_model_profile_player);
            }
        }, err => { }); ;
    }
    private void OnDisable()
    {
        //destroy on disable
        if (Fild_leaderboard != null)
        {
            for (int i = 0; i < Fild_leaderboard.Length; i++)
            {
                Destroy(Fild_leaderboard[i]);
            }
        }
    }

    class Raw_Fild_leaderboard : MonoBehaviour
    {
        TextMeshProUGUI Text_postion
        {
            get
            {
                TextMeshProUGUI text_postion = null;
                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "Postion")
                    {
                        text_postion = Texts;
                    }
                }
                return text_postion;
            }
        }

        TextMeshProUGUI Text_Nickname
        {
            get
            {
                TextMeshProUGUI Text_name_player = null;
                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "Name_player")
                    {
                        Text_name_player = Texts;
                    }
                }
                return Text_name_player;
            }
        }

        TextMeshProUGUI Text_Score
        {
            get
            {
                TextMeshProUGUI text_score = null;
                foreach (var Texts in GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (Texts.name == "Score")
                    {
                        text_score = Texts;
                    }
                }
                return text_score;
            }
        }

        Button BTN_Profile
        {
            get
            {
                return GetComponent<Button>();
            }
        }

        public void Change_value(string _id, string _id_other_player, int Postion, string Nickname, int? score, GameObject Profile_player)
        {
            Text_postion.text = Postion.ToString();
            Text_Nickname.text = Nickname;
            Text_Score.text = score.ToString();

            BTN_Profile.onClick.AddListener(() =>
            {
                Instantiate(Profile_player).GetComponent<Raw_model_user_profile>().Change_value(_id, _id_other_player);
            });
        }

    }

}
