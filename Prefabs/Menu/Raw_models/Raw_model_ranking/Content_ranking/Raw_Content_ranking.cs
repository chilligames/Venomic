using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using UnityEngine.UI;

public class Raw_Content_ranking : MonoBehaviour
{
    public string Name_leader_board;

    public Button BTN_close_Ranking;
    public TextMeshProUGUI Text_header;
    public Transform Place_contetn;
    public GameObject Raw_model_ranking;
    public GameObject Raw_model_profile;


    void Start()
    {
        BTN_close_Ranking.onClick.AddListener(() => { Destroy(gameObject); });

        Chilligames_SDK.API_Client.Recive_leader_board(new Req_recive_leader_board { Count = 50, Name_leader_board = Name_leader_board }, result =>
        {
            for (int i = 0; i < result.Length; i++)
            {

                GameObject player_fild = Instantiate(Raw_model_ranking, Place_contetn);

                string ID_player = result[i].ID;
                player_fild.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameObject profile_user = Instantiate(Raw_model_profile);
                    profile_user.GetComponent<Raw_model_user_profile>()._id_other_player = ID_player;
                    profile_user.GetComponent<Raw_model_user_profile>()._id = GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
                    

                });

                foreach (var Text_fild in player_fild.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    switch (Text_fild.name)
                    {
                        case "Postion":
                            {
                                Text_fild.text = i.ToString();
                            }
                            break;
                        case "Name_player":
                            {
                                Text_fild.text = result[i].Nick_name;
                            }
                            break;
                        case "Score":
                            {
                                Text_fild.text = result[i].Score.ToString();
                            }
                            break;
                    }

                }
            }
        }, ERR => { });

    }

}
