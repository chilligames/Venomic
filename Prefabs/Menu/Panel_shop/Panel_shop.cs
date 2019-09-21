using UnityEngine;
using System.Threading;
using TMPro;
using Chilligames.Json;
using Chilligames.SDK;
using Chilligames.SDK.Model_Client;

public class Panel_shop : MonoBehaviour
{
    public TextMeshProUGUI Text_Coin_number;
    public TextMeshProUGUI Text_Mony_number;

    public string _id
    {

        get
        {
            return GameObject.Find("Canvas_menu").GetComponent<Menu>().ID_player;
        }
    }

   
    private void OnDisable()
    {
        print("disable");
    }
    private void OnEnable()
    {
        Chilligames_SDK.API_Client.Recive_Coin_mony(new Req_recive_coin { _id = _id }, result =>
        {
            Text_Coin_number.text = result.Coin.ToString();
            Text_Mony_number.text = result.Money.ToString();

        }, err => { });
        print("enable");

        Chilligames_SDK.API_Client.Recive_offers(new Req_recive_offers { Name_App = "Venomic", _id = _id }, result => {

            print(result[0].Coin);

        }, err => { });
    }



}




