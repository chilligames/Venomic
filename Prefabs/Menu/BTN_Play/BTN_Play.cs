using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Script_game.menu
{

    public class BTN_Play : Menu, IPointerEnterHandler, IPointerExitHandler
    {


        public RawImage Dot_shape;
        public Transform Place_Dots;
        public float Speed_dot;
        public float Degress_dot_internal;
        public Vector3[] Pos_dots;

        Vector3[] pos_dots_internal;
        RawImage[] Dots = new RawImage[6];
        RawImage[] Dots_internal = new RawImage[6];

        public LineRenderer[] Lines;
        Envorment_dot Dot_envorment = new Envorment_dot();

        int Active_BTN_play;

        Vector3[] Frist_location;
        private void Start()
        {

            Frist_location = new Vector3[6] { Pos_dots[0], Pos_dots[1], Pos_dots[2], Pos_dots[3], Pos_dots[4], Pos_dots[5] };
            print(Frist_location[0]);
            for (int i = 0; i < Pos_dots.Length; i++)
            {
                Dots[i] = Instantiate(Dot_shape, Place_Dots);
                Dots_internal[i] = Instantiate(Dot_shape, Place_Dots);
            }
            pos_dots_internal = new Vector3[] { new Vector2(Pos_dots[0].x, Pos_dots[0].y - Degress_dot_internal - 0.2f), new Vector2(Pos_dots[1].x - Degress_dot_internal, Pos_dots[1].y - Degress_dot_internal), new Vector2(Pos_dots[2].x - Degress_dot_internal, Pos_dots[2].y + Degress_dot_internal), new Vector2(Pos_dots[3].x, Pos_dots[3].y + Degress_dot_internal + 0.2f), new Vector2(Pos_dots[4].x + Degress_dot_internal, Pos_dots[4].y + Degress_dot_internal), new Vector2(Pos_dots[5].x + Degress_dot_internal, Pos_dots[5].y - Degress_dot_internal) };

        }

        private void Update()
        {
            print(Frist_location[0]);
            Dot_envorment.Instant_Dot_Envorment(Dots, Pos_dots, Speed_dot);
            Dot_envorment.Instant_Dot_Envorment(Dots_internal, pos_dots_internal, Speed_dot / 3);

            for (int i = 0; i < Pos_dots.Length; i++)
            {
                Lines[0].SetPosition(i, Dots[i].transform.position);
                Lines[1].SetPosition(i, Dots_internal[i].transform.position);
            }

        }
        /// <summary>
        /// animation Enter
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {

            while (true)
            {
                if (Vector3.Distance(Pos_dots[0], Vector3.zero) > 0)
                {
                    for (int i = 0; i < Pos_dots.Length; i++)
                    {
                        Pos_dots[i] = Vector3.MoveTowards(Pos_dots[i], Vector3.zero, 0.1f);
                    }
                }
                else
                {
                    break;
                }

            }
        }

        /// <summary>
        /// animation EXit
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {

            while (true)
            {

                if (Vector3.Distance(Pos_dots[0], Vector3.zero) < Vector3.Distance(Frist_location[0], Vector3.zero))
                {
                    for (int i = 0; i < Pos_dots.Length; i++)
                    {
                        Pos_dots[i] = Vector3.MoveTowards(Pos_dots[i], Frist_location[i], 0.1f);
                    }

                }
                else
                {
                    break;
                }
            }




        }

    }



    public class Envorment_dot
    {

        public void Instant_Dot_Envorment(RawImage[] Dot, Vector3[] Pos_dots, float Speed)
        {
            for (int i = 0; i < Dot.Length; i++)
            {
                Dot[i].transform.localPosition = Vector3.MoveTowards(Dot[i].transform.localPosition, Pos_dots[i], Speed);
            }
        }
    }
}

