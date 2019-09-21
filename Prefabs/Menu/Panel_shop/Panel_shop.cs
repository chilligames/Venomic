using UnityEngine;
using System.Threading;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;
using UnityEngine.UI;
public class Panel_shop : MonoBehaviour
{
    public TextMeshProUGUI Text_Coin_number;
    public TextMeshProUGUI Text_Mony_number;

    public Button BTN_Enter_convert;

    public GameObject Raw_model_convert_panel;

    public string _id
    {

        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }


    private void OnDisable()
    {
    }
    public void OnEnable()
    {
        Recive_entity_wallet();

        Chilligames_SDK.API_Client.Recive_offers(new Req_recive_offers { Name_App = "Venomic", _id = _id }, result =>
        {


        }, err => { });

        BTN_Enter_convert.onClick.AddListener(() =>
        {
            Instantiate(Raw_model_convert_panel).GetComponent<Panel_Convert_Coins>().Change_value(gameObject);
        });
    }


    public void Recive_entity_wallet()
    {
        Chilligames_SDK.API_Client.Recive_Coin_mony(new Req_recive_coin { _id = _id }, result =>
        {
            Text_Coin_number.text = result.Coin.ToString();
            Text_Mony_number.text = result.Money.ToString();

        }, err => { });
    }

}




