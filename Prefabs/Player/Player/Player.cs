using System.Threading.Tasks;
using UnityEngine;

using System.Threading;

/// <summary>
/// playerpref
/// 1: Day_Night
/// </summary>
public class Player : MonoBehaviour
{
    public static Camera cam;

    public Transform Place_mission;

    public Color[] Colors_cam_Night;
    public Color[] Colors_cam_day;

    public static Color[] Colors_cam_day_;
    public static Color[] Colors_cam_night_;
    void Start()
    {

        Colors_cam_night_ = Colors_cam_Night;
        Colors_cam_day_ = Colors_cam_day;

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
            if (PlayerPrefs.GetInt("Day_Night") == 0)
            {
                Color color_day = new Color(1, 0.8f, 0.2f, 1);
                cam.backgroundColor = color_day;
            }
            else
            {
                Color color_night = new Color(0.13f, 0.15f, 0.19f, 1);
                cam.backgroundColor = color_night;
            }

            move();
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
            if (PlayerPrefs.GetInt("Day_Night") == 0)
            {
                int rand_color = Random.Range(0, Colors_cam_day_.Length);
                cam.backgroundColor = Colors_cam_day_[rand_color];
            }
            else
            {
                int rand_color = Random.Range(0, Colors_cam_night_.Length);
                cam.backgroundColor = Colors_cam_night_[rand_color];
            }

        }
    }
}

