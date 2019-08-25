﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public static Camera cam;
    public static Transform Place_mission;
    public Transform Place;
    public static Mission_Collector mission_Collection = new Mission_Collector();
    public GameObject mision;
    public static GameObject Raw_mision;



    void Start()
    {
        Place_mission = Place;
        Raw_mision = mision;
        cam = Camera.main;
        mission_Collection.Add(Raw_mision);


    }

    private void Update()
    {
        foreach (var item in mission_Collection.Collection)//cheak mikone age faselehay mission ziad bashe active nemishe mission  
        {
            if (Vector3.Distance(item.transform.position, cam.transform.position) > 40)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }

        }
    }

    /// <summary>
    /// 1:camera move mishe to akharim mogheyat+10
    /// 2: mission ezafe mishe
    /// </summary>
    /// <param name="Last_posion"></param>
    public static void Insert_mission(Vector3 Last_posion)
    {
        Cam.Move_camera(new Vector3(Last_posion.x + 10, Last_posion.y + 10, 0));
        mission_Collection.Add(new Vector3(Last_posion.x + 10, Last_posion.y + 10, Last_posion.z)); ;
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
        /// camera move mishe mire b akharin possion mission  k behesh midan 
        /// </summary>
        public static void Move_camera()
        {
            Move();
            async void Move()
            {
                while (true)
                {
                    if (cam.transform.position != mission_Collection.last_pos)
                    {
                        await Task.Delay(1);
                        cam.transform.position = Vector3.MoveTowards(cam.transform.position, mission_Collection.last_pos, 0.4f);
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


    public class Mission_Collector : IList<GameObject>
    {
        public GameObject[] Collection;
        public Vector3 last_pos;
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


        /// <summary>
        /// add new mision after finish misision;
        /// </summary>
        /// <param name="next_pos"></param>
        public void Add(Vector3 next_pos)
        {
            GameObject[] col_n = new GameObject[Collection.Length + 1];
            for (int i = 0; i < col_n.Length; i++)
            {
                if (Collection.Length <= i)
                {
                    col_n[i] = Instantiate(Raw_mision, Place_mission);
                    col_n[i].transform.position = next_pos;
                    col_n[i].GetComponent<Game_play>().Level = i;
                    Collection = col_n;
                    Collection = new GameObject[col_n.Length];
                    Collection = col_n;
                    last_pos = Collection[i].transform.position;
                    break;
                }
                else
                {
                    col_n[i] = Collection[i];
                }
            }


            Entity_player_model new_model = new Entity_player_model();
            new_model.Pos_G = new Vector3[Collection.Length];
            new_model.S = new int[Collection.Length];
            new_model.ST_P = new int[Collection.Length];
            new_model.T_M = new float[Collection.Length];
            new_model.R_M = new int[Collection.Length];

            for (int i = 0; i < Collection.Length; i++)
            {
                new_model.Pos_G[i] = Collection[i].transform.position;
                new_model.S[i] = Collection[i].GetComponent<Game_play>().Star;
                new_model.ST_P[i] = Collection[i].GetComponent<Game_play>().State_pass;
                new_model.T_M[i] = Collection[i].GetComponent<Game_play>().Time_mision;
                new_model.R_M[i] = Collection[i].GetComponent<Game_play>().Reset;
            }

            string new_data = JsonUtility.ToJson(new_model);

            StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/Info.Chi");
            streamWriter.Write(new_data);
            streamWriter.Close();

        }



        /// <summary>
        /// missions_update mikone
        /// </summary>
        /// <param name="raw_mission"></param>
        public void Add(GameObject raw_mission)
        {
            Load_data();

            void Load_data()
            {
                if (File.Exists(Application.persistentDataPath + "/Info.Chi"))
                {
                    StreamReader read_data = new StreamReader(Application.persistentDataPath + "/Info.Chi");
                    string Data_string = read_data.ReadToEnd();
                    read_data.Close();
                    var Data = JsonUtility.FromJson<Entity_player_model>(Data_string);

                    Count = Data.Pos_G.Length;

                    for (int i = 0; i < Count; i++)
                    {
                        if (Collection[i] == null)
                        {
                            Collection[i] = Instantiate(raw_mission, Place_mission);
                        }
                    }

                    for (int i = 0; i < Count; i++)
                    {
                        Collection[i].transform.position = Data.Pos_G[i];
                        Collection[i].GetComponent<Game_play>().Level = i;
                        Collection[i].GetComponent<Game_play>().Time_mision = (float)System.Math.Round(Data.T_M[i], 1);
                        Collection[i].GetComponent<Game_play>().State_pass = Data.ST_P[i];
                        Collection[i].GetComponent<Game_play>().Reset = Data.R_M[i];
                        Collection[i].GetComponent<Game_play>().Star = Data.S[i];

                        //last posion for camera
                        last_pos = Collection[i].transform.position;
                    }
                }
                else
                {
                    Entity_player_model New_model = new Entity_player_model();
                    New_model.Pos_G = new Vector3[] { new Vector3(10, 10, 0) };
                    New_model.ST_P = new int[] { 0 };
                    New_model.T_M = new float[] { 0 };
                    New_model.R_M = new int[] { 0 };
                    New_model.S = new int[] { 0 };

                    string string_data = JsonUtility.ToJson(New_model);

                    StreamWriter Creat_file = new StreamWriter(Application.persistentDataPath + "/Info.Chi");
                    Creat_file.Write(string_data);
                    Creat_file.Close();
                    Load_data();
                }
            }

        }


        /// <summary>
        /// reset mikone mishon ro va file  info taghir mide
        /// </summary>
        /// <param name="Mission_number"> mision number for change</param>
        public void Reset_mision(int level_number)
        {
            for (int i = 0; i < Collection.Length; i++)
            {
                if (level_number == i)
                {
                    Collection[i].GetComponent<Game_play>().Star = 0;
                    Collection[i].GetComponent<Game_play>().State_pass = 0;
                    Collection[i].GetComponent<Game_play>().Time_mision = 0;
                    Collection[i].GetComponent<Game_play>().Game_object_Slider.value = 0;
                    Collection[i].GetComponent<Game_play>().Game_object_Slider.maxValue = 0;
                    Collection[i].GetComponent<Game_play>().Reset = 1;
                }
            }
            Entity_player_model new_model_entity = new Entity_player_model();
            new_model_entity.Pos_G = new Vector3[Collection.Length];
            new_model_entity.S = new int[Collection.Length];
            new_model_entity.ST_P = new int[Collection.Length];
            new_model_entity.T_M = new float[Collection.Length];
            new_model_entity.R_M = new int[Collection.Length];

            for (int i = 0; i < Collection.Length; i++)
            {
                new_model_entity.Pos_G[i] = Collection[i].GetComponent<Game_play>().transform.position;
                new_model_entity.S[i] = Collection[i].GetComponent<Game_play>().Star;
                new_model_entity.ST_P[i] = Collection[i].GetComponent<Game_play>().State_pass;
                new_model_entity.T_M[i] = Collection[i].GetComponent<Game_play>().Time_mision;
                new_model_entity.R_M[i] = Collection[i].GetComponent<Game_play>().Reset;
            }

            StreamWriter File_info_model = new StreamWriter(Application.persistentDataPath + "/Info.Chi");
            string String_data = JsonUtility.ToJson(new_model_entity);
            File_info_model.Write(String_data);
            File_info_model.Close();

        }


        /// <summary>
        /// mision Update mikone 
        /// </summary>
        /// <param name="Level"> level for change</param>
        public void Update_singel_mision(int Level)
        {
            for (int i = 0; i < Collection.Length; i++)
            {
                if (Collection.Length - 1 == Level)
                {
                    Collection[i].GetComponent<Game_play>().Reset = 0;
                    Collection[i].GetComponent<Game_play>().State_pass = 1;
                }
            }

            Entity_player_model New_model = new Entity_player_model();
            New_model.Pos_G = new Vector3[Collection.Length];
            New_model.R_M = new int[Collection.Length];
            New_model.S = new int[Collection.Length];
            New_model.ST_P = new int[Collection.Length];
            New_model.T_M = new float[Collection.Length];

            for (int i = 0; i < Collection.Length; i++)
            {
                New_model.Pos_G[i] = Collection[i].GetComponent<Game_play>().transform.position;
                New_model.R_M[i] = Collection[i].GetComponent<Game_play>().Reset;
                New_model.S[i] = Collection[i].GetComponent<Game_play>().Star;
                New_model.ST_P[i] = Collection[i].GetComponent<Game_play>().State_pass;
                New_model.T_M[i] = Collection[i].GetComponent<Game_play>().Time_mision;
            }

            StreamWriter file_info = new StreamWriter(Application.persistentDataPath + "/Info.Chi");

            string String_data = JsonUtility.ToJson(New_model);
            file_info.Write(String_data);
            file_info.Close();

        }



        public void RemoveAt(int Level)
        {
            ArrayList arrayList = new ArrayList();
            foreach (var item in Collection)
            {
                arrayList.Add(item);
                print(arrayList.Count);
            }

        }


        /// <summary>
        /// collection 0 mishe
        /// </summary>
        public void Clear()
        {
            Collection = new GameObject[] { };
        }


        /// <summary>
        /// search mikone bebeine kasi to pos hast ya na 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(GameObject item)
        {
            bool result_contains = false;
            for (int i = 0; i < Collection.Length; i++)
            {
                if (item.transform.position == Collection[i].transform.position)
                {
                    result_contains = true;
                }
                else
                {
                    result_contains = false;
                }
            }
            return result_contains;
        }


        /// <summary>
        /// copy mikone ye list az gm toy collectio
        /// </summary>
        /// <param name="array"> meghdar collection jadid k jayghozin mishe to collection ghadim</param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(GameObject[] array, int arrayIndex)
        {
            Count = arrayIndex;
            for (int i = 0; i < array.Length; i++)
            {
                Collection[i] = array[i];
            }
        }


        /// <summary>
        /// litrator baray dashtan tamami arry hay collection;
        /// </summary>
        /// <returns> collection member </returns>
        public IEnumerator<GameObject> GetEnumerator()
        {
            for (int i = 0; i < Collection.Length; i++)
            {
                yield return Collection[i];
            }

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


        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
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
