using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Player : MonoBehaviour
{
    public static Camera cam;
    public static Transform Place_mission;
    public Transform Place;
    public static Mission_Collector mission_Collection = new Mission_Collector();
    public GameObject Raw_mision;
    void Start()
    {
        Place_mission = Place;

        cam = Camera.main;


        read_data_from_local();

        void read_data_from_local()
        {


            if (File.Exists(Application.persistentDataPath + "/Info.Chi"))
            {
                StreamReader read_data = new StreamReader(Application.persistentDataPath + "/Info.Chi");
                string Data_string = read_data.ReadToEnd();
                read_data.Close();

                var Data = JsonUtility.FromJson<Entity_player_model>(Data_string);
                mission_Collection.Count = Data.Pos_G.Length;
                print(mission_Collection.Count);
                for (int i = 0; i < mission_Collection.Count; i++)
                {
                    mission_Collection.Add(Raw_mision);
                    mission_Collection[i].transform.position = Data.Pos_G[i];
                    mission_Collection[i].GetComponent<Game_play>().Level = i;
                    mission_Collection[i].GetComponent<Game_play>().Time = Data.T_M[i];
                    mission_Collection[i].GetComponent<Game_play>().State_pass = Data.ST_P[i];
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

    public class Mission_Collector : IList<GameObject>
    {
        GameObject[] Collection;

        public GameObject this[int index]
        {
            get
            {
                return Collection[index];
            }
            set
            {
                Collection[index] = value;
            }
        }

        public int Count
        {
            get { return Collection.Length; }
            set { Collection = new GameObject[value]; }
        }

        public bool IsReadOnly { get; set; }

        public void Add(GameObject item)
        {

            for (int i = 0; i < Count; i++)
            {
                if (Collection[i] == null)
                {
                    Collection[i] = Instantiate(item, Place_mission);
                }
            }

        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(GameObject item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(GameObject[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public int IndexOf(GameObject item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, GameObject item)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(GameObject item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}

