using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using System.IO;

public class Panel_home : MonoBehaviour
{
    public GameObject Raw_model_edit_profile;
    public GameObject Raw_model_fild_server_play;
    
    public TextMeshProUGUI Text_Stars_number;
    public TextMeshProUGUI Text_ranking_number;
    public TextMeshProUGUI Text_level_number;
    public TextMeshProUGUI text_nickname;

    public Transform Place_server;

    public Button BTN_edit_profile;


    public string _id
    {
        get
        {
            return PlayerPrefs.GetString("_id");
        }
    }

    public void Start()
    {
        if (PlayerPrefs.GetString("_id").Length < 2)
        {
            Chilligames_SDK.API_Client.Quick_register(result =>
            {
                print("register_new_user");
                PlayerPrefs.SetString("_id", result._id);
                Start();

            }, ERROR => { });

        }
        else
        {
            Chilligames_SDK.API_Client.Quick_login(new Req_Login { _id = PlayerPrefs.GetString("_id") }, Result_login =>
            {

                if (Result_login == "1")
                {
                    Data_reader_and_sender();

                    Change_entitys_stars();

                    BTN_edit_profile.onClick.AddListener(() =>
                    {
                        Instantiate(Raw_model_edit_profile).GetComponent<Panel_edit_profile>();
                    });

                    Chilligames_SDK.API_Client.Recive_List_server_user(new Req_recive_list_servers_User { Name_app = "Venomic", _id = _id }, Result_server =>
                    {
                        for (int i = 0; i < Result_server.Length; i++)
                        {
                            Instantiate(Raw_model_fild_server_play, Place_server).GetComponent<Raw_model_fild_server_play>().Change_value(Result_server[i].ToString());
                        }

                    }, err => { });




                    async void Data_reader_and_sender()
                    {
                        StreamReader reader = new StreamReader(Application.persistentDataPath + "/Info.Chi");
                        var data = await reader.ReadToEndAsync();
                        Chilligames_SDK.API_Client.Send_Data_user(new Req_send_data { Data_user = data, Name_app = "Venomic", _id = _id }, () =>
                        {
                            print("Data send to server");

                        }, () => { }); ;

                    }

                    async void Change_entitys_stars()
                    {
                        int entity_stars = 0;
                        StreamReader Reader = new StreamReader(Application.persistentDataPath + "/Info.Chi");

                        string data = await Reader.ReadToEndAsync();

                        int[] stars = JsonUtility.FromJson<Player.Entity_player_model>(data).S;
                        for (int i = 0; i < stars.Length; i++)
                        {
                            entity_stars += stars[i];
                        }
                        Text_Stars_number.text = entity_stars.ToString();

                        Chilligames_SDK.API_Client.Send_Score_to_leader_board(new Req_send_score { Leader_board_name = "Venomic_top_Player", Score = entity_stars, _id = _id }, () => { print("Score_send"); });
                        Chilligames_SDK.API_Client.Recive_rank_postion(new Req_recive_rank_postion { Leader_board_name = "Venomic_top_Player", _id = _id }, Result =>
                        {
                            Text_ranking_number.text = Result;
                        }, null);



                        var Level = "Change model_entity player   ";
                        Text_level_number.text = Level;

                        Chilligames_SDK.API_Client.Recive_info_user(new Req_recive_Info_player { _id = _id }, result =>
                        {
                            text_nickname.text = result.Nickname;
                        }, err => { });


                    }
                }
                else if (Result_login == "0")
                {
                    print("Code not login here");
                }


            }, ERR => { });

        }




    }



}