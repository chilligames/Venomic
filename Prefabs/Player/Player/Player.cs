using System.Threading.Tasks;
using UnityEngine;

using System.Threading;

public class Player : MonoBehaviour
{
    public static Camera cam;
    public Transform Place_mission;

    public Color[] Colors_cam_;
    public static Color[] Colors_cam;

    void Start()
    {
        Colors_cam = Colors_cam_;
        cam = Camera.main;
    }

    public class Cam
    {

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
                        cam.transform.position = Vector3.MoveTowards(cam.transform.position, Pos_new, 0.7f);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// move camera to menu
        /// </summary>
        public static void Move_Camera_To_Menu()
        {
            Color Color_menu = new Color(1, 0.8f, 0.2f, 1);
            cam.backgroundColor = Color_menu;
            move();

            while (cam.backgroundColor != Color_menu)
            {
                cam.backgroundColor = Color_menu;
            }


            async void move()
            {
                cam.transform.position = new Vector3(10, 10, 0);
                while (true)
                {
                    if (cam.transform.position != Vector3.zero)
                    {
                        await Task.Delay(1);
                        cam.transform.position = Vector3.MoveTowards(cam.transform.position, Vector3.zero, 0.7f);
                    }
                    else
                    {
                        break;
                    }

                }
            }
        }


        /// <summary>
        /// Color camera change mikone
        /// </summary>
        public static void Change_color()
        {
            int rand_color = Random.Range(0, Colors_cam.Length);
            Change_color();

            async void Change_color()
            {
                while (true)
                {
                    if (cam.backgroundColor != Colors_cam[rand_color])
                    {
                        cam.backgroundColor = Color.Lerp(cam.backgroundColor, Colors_cam[rand_color], 0.3f);
                        print("cahnge");
                        if (cam.backgroundColor == Colors_cam[rand_color])
                        {
                            break;
                        }
                        await Task.Delay(1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
