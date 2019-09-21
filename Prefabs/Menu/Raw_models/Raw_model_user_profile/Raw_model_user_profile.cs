using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Raw_model_user_profile : MonoBehaviour
{
    public string _id;
    public string _id_other_player;
    public GameObject Panel_send_message;
    public TextMeshProUGUI Nickname;
    public TextMeshProUGUI Status;
    public Button[] BTNs_Connection;
    public Button BTN_Send_massege;
    public Button BTN_close_profile;

    public void Change_value(string _id, string _id_other_player)
    {
        this._id = _id;
        this._id_other_player = _id_other_player;
    }

    void Start()
    {
        BTN_close_profile.onClick.AddListener(() =>
        {
            Destroy(gameObject);

        });

        BTN_Send_massege.onClick.AddListener(() =>
        {

            Panel_send_message.GetComponent<Panel_send_massege>()._id = _id;
            Panel_send_message.GetComponent<Panel_send_massege>()._id_other_player = _id_other_player;
            Instantiate(Panel_send_message);

        });


        Chilligames_SDK.API_Client.Cheack_status_friend(new Req_status_friend { _id = _id, _id_other_player = _id_other_player }, Result =>
        {

            if (Result == 0)
            {
                BTNs_Connection[0].gameObject.SetActive(false);
                BTNs_Connection[1].gameObject.SetActive(true);
                BTNs_Connection[2].gameObject.SetActive(false);
                BTNs_Connection[3].gameObject.SetActive(false);
            }
            else if (Result == 1)
            {
                BTNs_Connection[0].gameObject.SetActive(false);
                BTNs_Connection[1].gameObject.SetActive(false);
                BTNs_Connection[2].gameObject.SetActive(true);
                BTNs_Connection[3].gameObject.SetActive(false);
            }
            else if (Result == 2)
            {
                BTNs_Connection[0].gameObject.SetActive(false);
                BTNs_Connection[1].gameObject.SetActive(false);
                BTNs_Connection[2].gameObject.SetActive(false);
                BTNs_Connection[3].gameObject.SetActive(true);
            }

            Chilligames_SDK.API_Client.Recive_Info_other_User<Schema_other_player>(new Req_recive_Info_player { _id = _id_other_player }, resul =>
            {
                Nickname.text = ChilligamesJson.DeserializeObject<Schema_other_player.DeserilseInfoPlayer>(resul.Info.ToString()).Nickname;

                Panel_send_message.GetComponent<Panel_send_massege>().Nickname_player = ChilligamesJson.DeserializeObject<Schema_other_player.DeserilseInfoPlayer>(resul.Info.ToString()).Nickname;

                Status.text = ChilligamesJson.DeserializeObject<Schema_other_player.DeserilseInfoPlayer>(resul.Info.ToString()).Status;
            }, err => { });



        }, ERR => { });


        BTNs_Connection[1].onClick.AddListener(() =>
        {
            BTNs_Connection[0].gameObject.SetActive(false);
            BTNs_Connection[1].gameObject.SetActive(false);
            BTNs_Connection[2].gameObject.SetActive(true);
            Chilligames_SDK.API_Client.Send_friend_requst(new Req_send_friend_requst { _id = _id, _id_other_player = _id_other_player }, () => { }, err => { });

        });

        BTNs_Connection[2].onClick.AddListener(() =>
        {
            BTNs_Connection[0].gameObject.SetActive(false);
            BTNs_Connection[1].gameObject.SetActive(true);
            BTNs_Connection[2].gameObject.SetActive(false);

            Chilligames_SDK.API_Client.Cancel_and_dellet_friend_requst(new req_cancel_and_dellet_send_freiend { _id = _id, _id_other_users = _id_other_player }, null, null);
        });

        BTNs_Connection[3].onClick.AddListener(() =>
        {

            BTNs_Connection[0].gameObject.SetActive(false);
            BTNs_Connection[1].gameObject.SetActive(true);
            BTNs_Connection[2].gameObject.SetActive(false);
            BTNs_Connection[3].gameObject.SetActive(false);
            Chilligames_SDK.API_Client.Cancel_and_dellet_friend_requst(new req_cancel_and_dellet_send_freiend { _id = _id, _id_other_users = _id_other_player }, null, null);

        });

    }

    public class Schema_other_player
    {
        public object _id = null;
        public object Info = null;
        public object[] Inventory = null;

        public class DeserilseInfoPlayer
        {
            public string Status = null;
            public string Nickname = null;

        }
    }

}
