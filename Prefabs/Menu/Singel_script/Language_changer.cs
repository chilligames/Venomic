using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Playerpref
/// 1: Language
/// </summary>
public class Language_changer : MonoBehaviour
{
    public bool Boild_font;

    public TMP_FontAsset Font_build_persian;
    public TMP_FontAsset Font_normal_persian;

    void Start()
    {
        if (PlayerPrefs.GetInt("Language") == 1)
        {
            if (Boild_font)
            {
                GetComponent<TextMeshProUGUI>().font = Font_build_persian;

            }
            else
            {
                GetComponent<TextMeshProUGUI>().font = Font_normal_persian;
            }

        }

    }

 

}

public abstract class handd
{
    public void hh()
    {

    }

 

}