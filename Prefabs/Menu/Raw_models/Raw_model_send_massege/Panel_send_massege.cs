using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_send_massege : MonoBehaviour
{
    public string _id;

    public string _id_other_player;

    public string Nickname_player;

    public TextMeshProUGUI Text_Nick_name;
    public Button BTN_send_massege;
    public Button BTN_Close;
    public TMP_InputField Input_fild_body;

    void Start()
    {
        Text_Nick_name.text = Nickname_player;

        BTN_Close.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });

        BTN_send_massege.onClick.AddListener(() =>
        {
            Chilligames_SDK.API_Client.Send_messege_to_player(new Req_send_message { Message_body = Input_fild_body.text, _id = _id, _id_other_users = _id_other_player }, () =>
            {
                Destroy(gameObject);
                print("message send notifaction here");

            }, null);
        });

    }

}
