using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Player : MonoBehaviour
{
    public static Camera cam;
    public Transform Place_missions;
    GameObject[] Mission_Collector;
    public GameObject Raw_mision;
    void Start()
    {
        cam = Camera.main;


        read_data_from_local();

        void read_data_from_local()
        {
            print(Application.persistentDataPath);
            if (File.Exists(Application.persistentDataPath + "/Info.Chi"))
            {
                StreamReader read_data = new StreamReader(Application.persistentDataPath + "/Info.Chi");
                string Data_string = read_data.ReadToEnd();
                read_data.Close();

                var Data = JsonUtility.FromJson<Entity_player_model>(Data_string);
                Mission_Collector = new GameObject[Data.Pos_G.Length];
                for (int i = 0; i < Data.Pos_G.Length; i++)
                {
                    Mission_Collector[i] = Instantiate(Raw_mision, Place_missions);
                    Mission_Collector[i].transform.position = Data.Pos_G[i];
                    Mission_Collector[i].GetComponent<Game_play>().Time = Data.T_M[i];
                    Mission_Collector[i].GetComponent<Game_play>().Level = i;
                }
            }
            else
            {
                Entity_player_model New_model = new Entity_player_model();
                New_model.Pos_G = new Vector3[] { new Vector3(10, 0, 0), new Vector3(10, 10, 0) };
                New_model.ST_P = new int[] { 0, 0 };
                New_model.T_M = new float[] { 3.1f, 4.1f };
                StreamWriter Creat_file = File.CreateText(Application.persistentDataPath + "/Info.Chi");
                Creat_file.Write(JsonUtility.ToJson(New_model));
                Creat_file.Close();
                read_data_from_local();
            }
        }
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

    public struct Entity_player
    {
        public static Vector3[] Position_game_plays;
        public static float Time_mision;
        public static int Status_pass_mision;

    }

    public class Entity_player_model
    {
        public Vector3[] Pos_G = { };
        public float[] T_M = { };
        public int[] ST_P = { };
    }


}

