using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Jobs;

/// <summary>
/// Playerpref
/// 1: Language
/// </summary>
public class Language_changer : MonoBehaviour
{
    [TextArea()]
    public string Text;

    public bool Boild_font;

    public TMP_FontAsset Font_boild_persian;
    public TMP_FontAsset Font_normal_persian;


    void Start()
    {

        if (PlayerPrefs.GetInt("Language") == 1)
        {
            if (Boild_font)
            {
                GetComponent<TextMeshProUGUI>().text = Text;
                GetComponent<TextMeshProUGUI>().font = Font_boild_persian;
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = Text;
                GetComponent<TextMeshProUGUI>().font = Font_normal_persian;
            }
        }
    }



}



