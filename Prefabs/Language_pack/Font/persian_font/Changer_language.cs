using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace Script_game.Game_Class
{


    /// <summary>
    /// prefer
    /// 1:Language
    /// </summary>

    public class Changer_language : MonoBehaviour
    {
        public bool Boild;
        public Font[] Font = new Font[2];

        [TextArea()]
        public string Text_for_change;

        void Start()
        {


            if (PlayerPrefs.GetInt("Language") == 2)
            {

                if (Boild)
                {

                    GetComponent<Text>().font = Font[0];
                }
                else
                {
                    GetComponent<Text>().font = Font[1];
                }

                GetComponent<Text>().text = Text_for_change;
            }


        }

    }

}


