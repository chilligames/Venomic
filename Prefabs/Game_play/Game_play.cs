using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_play : MonoBehaviour
{
    public Camera Camera;
    public static Camera cam;

    private void Start()
    {
        cam = Camera;
    }

    public static class Cam
    {
        public static int move_camera;
        public static Vector3 Target_camera;
        public static int camera_move;

        public static void Move_camera(Stop_camera stop)
        {
            stop();
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, Target_camera, 0.01f);
        }
        public static void Camera_new_pos()
        {
            if (camera_move == 0)
            {
                Target_camera = new Vector3(cam.transform.position.x + 10, 0, -10);
                camera_move = 1;
            }
        }
        public delegate void Stop_camera();
    }
}
