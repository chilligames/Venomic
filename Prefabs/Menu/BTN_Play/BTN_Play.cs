using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Script_game.menu
{

    public class BTN_Play : Menu, IPointerEnterHandler, IPointerExitHandler, IDragHandler
    {
        [Header("Envorment")]
        Envorment_dot Dot_envorment = new Envorment_dot();
        public RawImage Dot_shape;
        public Transform Place_Dots;
        public LineRenderer[] Lines;
        public float Speed_dot;
        public float Degress_dot_internal;
        public Vector3[] Pos_dots;
        Vector3[] pos_dots_internal;
        Vector3[] Frist_Pos = new Vector3[6];
        Vector3[] Frist_Pos_internal_dot = new Vector3[6];
        RawImage[] Dots = new RawImage[6];
        RawImage[] Dots_internal = new RawImage[6];
        [Space(30f)]
        [Header("Lines")]

        public LineRenderer line_raw;
        public Transform Place_line_instatnt;
        LineRenderer[] line_collector = new LineRenderer[10];
        Vector3[] Target_line = new Vector3[10];

        private void Start()
        {

            pos_dots_internal = new Vector3[] { new Vector2(Pos_dots[0].x, Pos_dots[0].y - Degress_dot_internal - 0.2f), new Vector2(Pos_dots[1].x - Degress_dot_internal, Pos_dots[1].y - Degress_dot_internal), new Vector2(Pos_dots[2].x - Degress_dot_internal, Pos_dots[2].y + Degress_dot_internal), new Vector2(Pos_dots[3].x, Pos_dots[3].y + Degress_dot_internal + 0.2f), new Vector2(Pos_dots[4].x + Degress_dot_internal, Pos_dots[4].y + Degress_dot_internal), new Vector2(Pos_dots[5].x + Degress_dot_internal, Pos_dots[5].y - Degress_dot_internal) };
            for (int i = 0; i < Pos_dots.Length; i++)
            {
                Dots[i] = Instantiate(Dot_shape, Place_Dots);
                Dots_internal[i] = Instantiate(Dot_shape, Place_Dots);
            }
            for (int i = 0; i < Pos_dots.Length; i++)
            {
                Frist_Pos[i] = Pos_dots[i];
                Frist_Pos_internal_dot[i] = pos_dots_internal[i];
            }


        }


        private void Update()
        {
            pos_dots_internal = new Vector3[] { new Vector2(Pos_dots[0].x, Pos_dots[0].y - Degress_dot_internal - 0.2f), new Vector2(Pos_dots[1].x - Degress_dot_internal, Pos_dots[1].y - Degress_dot_internal), new Vector2(Pos_dots[2].x - Degress_dot_internal, Pos_dots[2].y + Degress_dot_internal), new Vector2(Pos_dots[3].x, Pos_dots[3].y + Degress_dot_internal + 0.2f), new Vector2(Pos_dots[4].x + Degress_dot_internal, Pos_dots[4].y + Degress_dot_internal), new Vector2(Pos_dots[5].x + Degress_dot_internal, Pos_dots[5].y - Degress_dot_internal) };
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
            int count = Random.Range(5, 10);
            for (int i = 0; i < count; i++)
            {
                Target_line[i] = new Vector3(Random.Range(-4, 4f), Random.Range(-4, 4f));
            }

            for (int i = 0; i < count; i++)
            {
                line_collector[i] = Instantiate(line_raw, Place_line_instatnt);
            }

            while (true)
            {
                if (Vector3.Distance(Pos_dots[0], Vector3.zero) > 0)
                {
                    for (int i = 0; i < Pos_dots.Length; i++)
                    {
                        Pos_dots[i] = Vector3.MoveTowards(Pos_dots[i], Vector3.zero, 0.1f);
                        pos_dots_internal[i] = Vector3.MoveTowards(pos_dots_internal[i], Frist_Pos_internal_dot[i], 0.1f);
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
            for (int i = 0; i < line_collector.Length; i++)
            {

                if (line_collector[i] != null)
                {
                    Destroy(line_collector[i].gameObject);
                }
            }

            while (true)
            {

                if (Vector3.Distance(Pos_dots[0], Vector3.zero) < Vector3.Distance(Frist_Pos[0], Vector3.zero))
                {
                    for (int i = 0; i < Pos_dots.Length; i++)
                    {
                        Pos_dots[i] = Vector3.MoveTowards(Pos_dots[i], Frist_Pos[i], 0.1f);
                        pos_dots_internal[i] = Vector3.MoveTowards(pos_dots_internal[i], Frist_Pos_internal_dot[i], 0.1f);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            for (int i = 0; i < line_collector.Length; i++)
            {
                if (line_collector[i] != null)
                {
                    
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

