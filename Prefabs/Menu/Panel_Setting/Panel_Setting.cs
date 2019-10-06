using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// playerpref
/// 1: Language
/// 2: Vibrator
/// 3: Sound
/// </summary>


public class Panel_Setting : MonoBehaviour
{
    public GameObject Contact_us;

    public Button BTN_vibrator;
    public Button BTN_Music;
    public Button BTN_Fa;
    public Button BTN_EN;
    public Button BTN_Cotact_us;
    public Button BTN_Other_game;

    public TextMeshProUGUI Text_vibrator;
    public TextMeshProUGUI Text_music;

    public Color Color_Enable;
    public Color Color_Disable;



    public void Start()
    {
        //control on/off vibrator
        if (PlayerPrefs.GetInt("Vibrator") == 0)
        {
            Text_vibrator.text = "Off";
            Text_vibrator.color = Color_Disable;
        }
        else if (PlayerPrefs.GetInt("Vibrator") == 1)
        {
            Text_vibrator.text = "On";
            Text_vibrator.color = Color_Enable;
        }

        //control on/off sound
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            Text_music.text = "Off";
            Text_music.color = Color_Disable;
        }
        else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            BTN_Music.GetComponent<RawImage>().color = Color_Enable;
            Text_music.text = "On";
            Text_music.color = Color_Enable;
        }



        BTN_vibrator.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Vibrator") == 0)
            {
                Text_vibrator.text = "ON";
                Text_vibrator.color = Color_Enable;
                PlayerPrefs.SetInt("Vibrator", 1);
                print("Vibre on");
            }
            else if (PlayerPrefs.GetInt("Vibrator") == 1)
            {
                Text_vibrator.text = "OFF";
                Text_vibrator.color = Color_Disable;
                PlayerPrefs.SetInt("Vibrator", 0);
                print("vibre off");
            }
        });

        BTN_Music.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                Text_music.text = "ON";
                Text_music.color = Color_Enable;
                PlayerPrefs.SetInt("Sound", 1);
                print("sound on");
            }
            else if (PlayerPrefs.GetInt("Sound") == 1)
            {
                Text_music.text = "OFF";
                Text_music.color = Color_Disable;
                PlayerPrefs.SetInt("Sound", 0);
                print("Sound off");
            }

        });

        BTN_Fa.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("Language", 1);
            SceneManager.LoadScene(0);
        });

        BTN_EN.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("Language", 0);
            SceneManager.LoadScene(0);
        });

        BTN_Cotact_us.onClick.AddListener(() =>
        {
            Instantiate(Contact_us);
        });


    }

}
