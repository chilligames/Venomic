using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Panel_Setting : MonoBehaviour
{
    public Button BTN_vibrator;
    public Button BTN_Music;

    public TextMeshProUGUI Text_vibrator;
    public TextMeshProUGUI Text_music;

    public Color Color_Enable;
    public Color Color_Disable;

    public TMP_FontAsset Font_Disable;
    public TMP_FontAsset Font_Enable;



    public void Start()
    {
        if (PlayerPrefs.GetInt("Vibrator") == 0)
        {
            BTN_vibrator.GetComponent<RawImage>().color = Color_Disable;
            Text_vibrator.text = "Off";
            Text_vibrator.font = Font_Disable;
            Text_vibrator.color = Color_Disable;
        }
        else if (PlayerPrefs.GetInt("Vibrator") == 1)
        {
            BTN_vibrator.GetComponent<RawImage>().color = Color_Enable;
            Text_vibrator.text = "On";
            Text_vibrator.color = Color_Enable;
            Text_vibrator.font = Font_Enable;
        }


        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            BTN_Music.GetComponent<RawImage>().color = Color_Disable;
            Text_music.text = "Off";
            Text_music.font = Font_Disable;
            Text_music.color = Color_Disable;
        }
        else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            BTN_Music.GetComponent<RawImage>().color = Color_Enable;
            Text_music.text = "On";
            Text_music.font = Font_Enable;
            Text_music.color = Color_Enable;
        }


        BTN_vibrator.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Vibrator") == 0)
            {
                BTN_vibrator.GetComponent<RawImage>().color = Color_Enable;
                PlayerPrefs.SetInt("Vibrator", 1);
                print("Vibre on");
            }
            else if (PlayerPrefs.GetInt("Vibrator") == 1)
            {
                BTN_vibrator.GetComponent<RawImage>().color = Color_Disable;
                PlayerPrefs.SetInt("Vibrator", 0);
                print("vibre off");
            }
        });

        BTN_Music.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                BTN_Music.GetComponent<RawImage>().color = Color_Enable;
                PlayerPrefs.SetInt("Sound", 1);
                print("sound on");
            }
            else if (PlayerPrefs.GetInt("Sound") == 1)
            {
                BTN_Music.GetComponent<RawImage>().color = Color_Disable;
                PlayerPrefs.SetInt("Sound", 0);
                print("Sound off");
            }

        });

    }

}
