using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public Button BTN_signal;
    public Button BTN_Home;
    public Button BTN_Ranking;
    public Button BTN_Servers;
    public Button BTN_Shop;
    public Button BTN_Messages;
    public Button BTN_Setting;

    public GameObject Content_Signal;
    public GameObject content_Home;
    public GameObject Content_Ranking;
    public GameObject Content_Servers;
    public GameObject Content_Shop;
    public GameObject Content_Message;
    public GameObject Content_Setting;

    public Color Color_select;
    public Color Color_deselect;

    public GameObject Holder;

    GameObject Curent_panel = null;
    Button Curent_BTN_Taped = null;

    public string ID_player
    {
        get
        {
            return PlayerPrefs.GetString("_id");
        }
    }

    void Start()
    {
        Curent_panel = content_Home;
        Curent_BTN_Taped = BTN_Home;

        BTN_signal.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Signal;
            Content_Signal.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_signal;
            BTN_signal.GetComponentInChildren<RawImage>().color = Color_select;
        });

        BTN_Home.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = content_Home;
            content_Home.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Home;
            BTN_Home.GetComponentInChildren<RawImage>().color = Color_select;
        });

        BTN_Ranking.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Ranking;
            Content_Ranking.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Ranking;
            BTN_Ranking.GetComponentInChildren<RawImage>().color = Color_select;

        });

        BTN_Servers.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Servers;
            Content_Servers.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Servers;
            BTN_Servers.GetComponentInChildren<RawImage>().color = Color_select;

        });

        BTN_Shop.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Shop;
            Content_Shop.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Shop;
            BTN_Shop.GetComponentInChildren<RawImage>().color = Color_select;

        });

        BTN_Messages.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Message;
            Content_Message.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Messages;
            BTN_Messages.GetComponentInChildren<RawImage>().color = Color_select;
        });

        BTN_Setting.onClick.AddListener(() =>
        {
            Curent_panel.SetActive(false);
            Curent_panel = Content_Setting;
            Content_Setting.SetActive(true);

            Curent_BTN_Taped.GetComponentInChildren<RawImage>().color = Color_deselect;
            Curent_BTN_Taped = BTN_Setting;
            BTN_Setting.GetComponentInChildren<RawImage>().color = Color_select;
        });


        Cheack_net();

        async void Cheack_net()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://www.google.com/");
            www.SendWebRequest();
            while (true)
            {

                if (www.isDone)
                {
                    www.Abort();

                    BTN_signal.GetComponentInChildren<RawImage>().color = Color.green;

                    break;
                }
                else
                {
                    if (www.isHttpError || www.isNetworkError || www.timeout == 1)
                    {
                        BTN_signal.GetComponentInChildren<RawImage>().color = Color.red;
                        www.Abort();
                        break;
                    }
                    await Task.Delay(1);
                }
            }
        }

    }


    private void Update()
    {
        Holder.transform.position = Vector3.MoveTowards(Holder.transform.position, Curent_BTN_Taped.gameObject.transform.position, 0.1f);

    }




}
