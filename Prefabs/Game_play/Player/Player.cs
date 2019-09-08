using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public static Camera cam;
    public static Transform Place_mission;
    public Transform Place;
    public GameObject mision;
    public static GameObject Raw_mision;



    void Start()
    {
        Place_mission = Place;
        Raw_mision = mision;
        cam = Camera.main;
    }

    public class Cam
    {
        public static int Zoom;

        /// <summary>
        /// animation move camera mire pos k behesh dade shode 
        /// </summary>
        /// <param name="Pos_new"> pos jadid migire ke bayad -10 bash</param>
        public static void Move_camera(Vector3 Pos_new)
        {
            move();
            async void move()
            {
                while (true)
                {
                    if (Vector3.Distance(cam.transform.position, Pos_new) > 0)
                    {
                        await Task.Delay(1);
                        cam.transform.position = Vector3.MoveTowards(cam.transform.position, Pos_new, 0.5f);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// camera zoom_back mikone be meghdar maelom
        /// </summary>
        public static void Zoom_Back()
        {
            Zoom_in();

            async void Zoom_in()
            {
                while (true)
                {
                    if (cam.orthographicSize < 50)
                    {
                        Zoom = 1;

                        cam.orthographicSize = 6;

                        for (int i = 0; i < 50; i++)
                        {
                            await Task.Delay(1);

                            cam.orthographicSize++;
                            if (cam.orthographicSize == 50)
                            {
                                break;
                            }
                        }

                        break;
                    }
                    else if (cam.orthographicSize > 6)
                    {
                        Zoom = 0;
                        cam.orthographicSize = 50;

                        for (int i = 0; i < 50; i++)
                        {
                            await Task.Delay(1);
                            cam.orthographicSize--;
                            if (cam.orthographicSize == 6)
                            {
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }
    }



    public class Entity_player_model
    {
        public Vector3[] Pos_G = { };
        public float[] T_M = { };
        public int[] ST_P = { };
        public int[] S = { };
        public int[] R_M = { };
    }


}
