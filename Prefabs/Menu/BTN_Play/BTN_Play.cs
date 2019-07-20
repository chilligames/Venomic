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
        public float Speed_dot_internal;
        public Vector2[] Pos_dots;
        RawImage[] Dots = new RawImage[6];
        RawImage[] Dots_internal = new RawImage[6];

        Envorment_dot Dot_envorment = new Envorment_dot();

        private void Start()
        {
            for (int i = 0; i < Pos_dots.Length; i++)
            {
                Dots[i] = Instantiate(Dot_shape, Place_Dots);
                Dots_internal[i] = Instantiate(Dot_shape, Place_Dots);
            }
        }

        private void Update()
        {
            Dot_envorment.Instant_Dot_Envorment(Dots, Pos_dots, Speed_dot);
            Dot_envorment.Instant_Dot_Envorment(Dots_internal, Pos_dots, Speed_dot);

        }



        public class Envorment_dot
        {

            public void Instant_Dot_Envorment(RawImage[] Dot, Vector2[] Pos_dots, float Speed)
            {
                for (int i = 0; i < Dot.Length; i++)
                {
                    Dot[i].transform.localPosition = Vector2.MoveTowards(Dot[i].transform.localPosition, Pos_dots[i], Speed);
                }
            }
        }
    }

}
