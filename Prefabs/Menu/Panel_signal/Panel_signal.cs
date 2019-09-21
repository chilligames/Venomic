using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Panel_signal : MonoBehaviour
{
    public Button Button_cheack_net;
    public TextMeshProUGUI Text_Chack_signal;
    public Button Button_tab_signal;

    private void Start()
    {
        Button_cheack_net.onClick.AddListener(Cheack_net);

        Cheack_net();
    }


    /// <summary>
    /// net cheack mikone
    /// </summary>
    void Cheack_net()
    {
        Cheack();

        async void Cheack()
        {
            UnityWebRequest www = UnityWebRequest.Get("http://google.com");
            www.SendWebRequest();
            while (true)
            {
                if (www.isDone)
                {
                    Button_cheack_net.GetComponent<Image>().fillAmount = 1;
                    Button_cheack_net.GetComponent<Image>().color = Color.green;
                    Button_tab_signal.GetComponentInChildren<RawImage>().color = Color.green;
                    Text_Chack_signal.text = "Good Connection";

                    break;
                }
                else
                {
                    Button_tab_signal.GetComponentInChildren<RawImage>().color = Color.red;
                    Button_cheack_net.GetComponent<Image>().fillAmount = www.downloadProgress;
                    await Task.Delay(1);

                    if (www.isNetworkError || www.isHttpError || www.timeout == 1)
                    {
                        print("error_signal");
                        break;
                    }
                }
            }
        }
    }
}
