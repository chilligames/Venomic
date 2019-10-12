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

    public Button BTN_send_req_friend;
    public Button BTN_Pending;
    public Button BTN_Aceept;
    public Button BTN_Remove_friend;

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
                BTN_send_req_friend.gameObject.SetActive(false);
                BTN_Aceept.gameObject.SetActive(false);
                BTN_Pending.gameObject.SetActive(true);
                BTN_Remove_friend.gameObject.SetActive(false);

            }
            else if (Result == 1)
            {
                BTN_send_req_friend.gameObject.SetActive(false);
                BTN_Aceept.gameObject.SetActive(true);
                BTN_Pending.gameObject.SetActive(false);
                BTN_Remove_friend.gameObject.SetActive(false);
            }
            else if (Result == 2)
            {
                BTN_send_req_friend.gameObject.SetActive(false);
                BTN_Aceept.gameObject.SetActive(false);
                BTN_Pending.gameObject.SetActive(false);
                BTN_Remove_friend.gameObject.SetActive(true);

            }
            else if (Result == 3)
            {
                BTN_send_req_friend.gameObject.SetActive(true);
                BTN_Aceept.gameObject.SetActive(false);
                BTN_Pending.gameObject.SetActive(false);
                BTN_Remove_friend.gameObject.SetActive(false);
            }

            Chilligames_SDK.API_Client.Recive_Info_other_User<Schema_other_player>(new Req_recive_Info_player { _id = _id_other_player }, resul =>
            {
                Nickname.text = ChilligamesJson.DeserializeObject<Schema_other_player.DeserilseInfoPlayer>(resul.Info.ToString()).Nickname;

                Panel_send_message.GetComponent<Panel_send_massege>().Nickname_player = ChilligamesJson.DeserializeObject<Schema_other_player.DeserilseInfoPlayer>(resul.Info.ToString()).Nickname;

                Status.text = ChilligamesJson.DeserializeObject<Schema_other_player.DeserilseInfoPlayer>(resul.Info.ToString()).Status;
            }, err => { });



        }, ERR => { });


        BTN_send_req_friend.onClick.AddListener(() =>
        {
            BTN_send_req_friend.gameObject.SetActive(false);
            BTN_Aceept.gameObject.SetActive(false);
            BTN_Pending.gameObject.SetActive(true);
            BTN_Remove_friend.gameObject.SetActive(false);
            Chilligames_SDK.API_Client.Send_friend_requst(new Req_send_friend_requst { _id = _id, _id_other_player = _id_other_player }, () => { }, err => { });
        });

        BTN_Aceept.onClick.AddListener(() =>
        {
            BTN_send_req_friend.gameObject.SetActive(false);
            BTN_Aceept.gameObject.SetActive(false);
            BTN_Pending.gameObject.SetActive(false);
            BTN_Remove_friend.gameObject.SetActive(true);

            Chilligames_SDK.API_Client.Accept_friend_req(new Req_accept_friend_req { }, () => { }, err => { });
        });

        BTN_Pending.onClick.AddListener(() =>
        {
            BTN_send_req_friend.gameObject.SetActive(true);
            BTN_Aceept.gameObject.SetActive(false);
            BTN_Pending.gameObject.SetActive(false);
            BTN_Remove_friend.gameObject.SetActive(false);

            Chilligames_SDK.API_Client.Cancel_and_dellet_friend_requst(new req_cancel_and_dellet_send_freiend { }, () => { }, err => { });
        });

        BTN_Remove_friend.onClick.AddListener(() =>
        {
            BTN_send_req_friend.gameObject.SetActive(true);
            BTN_Aceept.gameObject.SetActive(false);
            BTN_Pending.gameObject.SetActive(false);
            BTN_Remove_friend.gameObject.SetActive(false);
            Chilligames_SDK.API_Client.Cancel_and_dellet_friend_requst(new req_cancel_and_dellet_send_freiend { }, () => { }, err => { });
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
