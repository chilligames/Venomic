using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Script_game.menu
{
    public class BTN_shop : Menu, IPointerEnterHandler, IPointerExitHandler
    {
        public RawImage[] Dots;
        public LineRenderer[] Lines;
        readonly Vector3[] Frist_pos = new Vector3[4];
        readonly Vector3[] random_pos_dot = new Vector3[4];
        readonly Vector3[] Random_pos_line = new Vector3[4];
        readonly Vector3[] Random_pos_line_2 = new Vector3[4];

        int Status_anim;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Status_anim = 1;

            Random_pos_line[0] = new Vector3(Frist_pos[0].x + Random.Range(0, 1f), Frist_pos[0].y + Random.Range(0f, 1f), 0);
            Random_pos_line[1] = new Vector3(Frist_pos[1].x + Random.Range(0, 1f), Frist_pos[1].y + Random.Range(0, -1f), 0);
            Random_pos_line[2] = new Vector3(Frist_pos[2].x + Random.Range(0, -1f), Frist_pos[2].y + Random.Range(0, -1f), 0);
            Random_pos_line[3] = new Vector3(Frist_pos[3].x + Random.Range(0, -1f), Frist_pos[3].y + Random.Range(0, 1f), 0);

            Random_pos_line_2[0] = new Vector3(Random_pos_line[0].x + Random.Range(0, 2f), Random_pos_line[0].y, 0);
            Random_pos_line_2[1] = new Vector3(Random_pos_line[1].x + Random.Range(0, 2f), Random_pos_line[1].y, 0);
            Random_pos_line_2[2] = new Vector3(Random_pos_line[2].x + Random.Range(0, -2f), Random_pos_line[2].y, 0);
            Random_pos_line_2[3] = new Vector3(Random_pos_line[3].x + Random.Range(0, -2f), Random_pos_line[3].y, 0);
        }

        public void OnPointerExit(PointerEventData eventData)
        {

            Status_anim = 2;

        }
        private void Start()
        {
            for (int i = 0; i < Dots.Length; i++)
            {
                Frist_pos[i] = Dots[i].transform.localPosition;

            }
            StartCoroutine(Random_pos());
        }
        private void Update()
        {
            if (Status_anim == 0)
            {
                for (int i = 0; i < Dots.Length; i++)
                {
                    Dots[i].transform.localPosition = Vector3.MoveTowards(Dots[i].transform.localPosition, random_pos_dot[i], 0.01f);
                    Lines[i].SetPosition(1, Vector3.MoveTowards(Lines[i].GetPosition(1), Frist_pos[i], 0.01f));
                    Lines[i].SetPosition(2, Vector3.MoveTowards(Lines[i].GetPosition(2), Frist_pos[i], 0.01f));
                }
            }


            if (Status_anim == 1)
            {
                for (int i = 0; i < Lines.Length; i++)
                {
                    Lines[i].SetPosition(1, Vector3.MoveTowards(Lines[i].GetPosition(1), Random_pos_line[i], 0.01f));
                    Lines[i].SetPosition(2, Vector3.MoveTowards(Lines[i].GetPosition(2), Random_pos_line[i], 0.01f));
                    if (Lines[i].GetPosition(1) == Random_pos_line[i])
                    {
                        for (int a = 0; a < Lines.Length; a++)
                        {
                            Lines[a].SetPosition(2, Vector3.MoveTowards(Lines[a].GetPosition(2), Random_pos_line_2[a], 0.01f));
                            Dots[a].transform.localPosition = Vector3.MoveTowards(Dots[a].transform.localPosition, Lines[a].GetPosition(2), 0.01f);
                        }

                    }
                }

                for (int i = 0; i < Dots.Length; i++)
                {
                    Dots[i].transform.localPosition = Vector3.MoveTowards(Dots[i].transform.localPosition, Lines[i].GetPosition(1), 0.01f);
                }
            }


            if (Status_anim == 2)
            {
                for (int i = 0; i < Lines.Length; i++)
                {
                    Lines[i].SetPosition(2, Vector3.MoveTowards(Lines[i].GetPosition(2), Lines[i].GetPosition(1), 0.01f));
                    if (Lines[i].GetPosition(2) == Lines[i].GetPosition(1))
                    {
                        Status_anim = 0;
                    }
                }

            }

        }
        IEnumerator Random_pos()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < random_pos_dot.Length; i++)
                {
                    random_pos_dot[i] = new Vector3(Frist_pos[i].x + Random.Range(-0.03f, 0.03f), Frist_pos[i].y + Random.Range(-0.03f, 0.03f));
                }

            }
        }
    }

}
