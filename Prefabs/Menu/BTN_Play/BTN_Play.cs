using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Script_game.menu
{

    public class BTN_Play : Menu
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

        private void Start()
        {


            for (int i = 0; i < Pos_dots.Length; i++)
            {
                Dots[i] = Instantiate(Dot_shape, Place_Dots);
                Dots_internal[i] = Instantiate(Dot_shape, Place_Dots);
            }
            pos_dots_internal = new Vector3[] { new Vector2(Pos_dots[0].x, Pos_dots[0].y - Degress_dot_internal - 0.2f), new Vector2(Pos_dots[1].x - Degress_dot_internal, Pos_dots[1].y - Degress_dot_internal), new Vector2(Pos_dots[2].x - Degress_dot_internal, Pos_dots[2].y + Degress_dot_internal), new Vector2(Pos_dots[3].x, Pos_dots[3].y + Degress_dot_internal + 0.2f), new Vector2(Pos_dots[4].x + Degress_dot_internal, Pos_dots[4].y + Degress_dot_internal), new Vector2(Pos_dots[5].x + Degress_dot_internal, Pos_dots[5].y - Degress_dot_internal) };

        }

        private void Update()
        {
            Dot_envorment.Instant_Dot_Envorment(Dots, Pos_dots, Speed_dot);
            Dot_envorment.Instant_Dot_Envorment(Dots_internal, pos_dots_internal, Speed_dot / 3);

            for (int i = 0; i < Pos_dots.Length; i++)
            {
                Lines[0].SetPosition(i, Dots[i].transform.position);
                Lines[1].SetPosition(i, Dots_internal[i].transform.position);
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

}
