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
    public static Mission_Collector mission_Collection = new Mission_Collector();
    public GameObject mision;
    public static GameObject Raw_mision;
    void Start()
    {
        Place_mission = Place;
        Raw_mision = mision;
        cam = Camera.main;
        Load_data();
        void Load_data()
        {


            if (File.Exists(Application.persistentDataPath + "/Info.Chi"))
            {
                StreamReader read_data = new StreamReader(Application.persistentDataPath + "/Info.Chi");
                string Data_string = read_data.ReadToEnd();
                read_data.Close();
                var Data = JsonUtility.FromJson<Entity_player_model>(Data_string);

                mission_Collection.Count = Data.Pos_G.Length;
                for (int i = 0; i < mission_Collection.Count; i++)
                {
                    mission_Collection.Add(Raw_mision);
                    mission_Collection[i].transform.position = Data.Pos_G[i];
                    mission_Collection[i].GetComponent<Game_play>().Level = i;
                    mission_Collection[i].GetComponent<Game_play>().Time_mision = Data.T_M[i];
                    mission_Collection[i].GetComponent<Game_play>().State_pass = Data.ST_P[i];

                    //last posion for camera
                    mission_Collection.last_pos = mission_Collection[i].transform.position;
                }
            }
            else
            {
                Entity_player_model New_model = new Entity_player_model();
                New_model.Pos_G = new Vector3[] { new Vector3(10, 0, 0) };
                New_model.ST_P = new int[] { 0 };
                New_model.T_M = new float[] { 0 };
                StreamWriter Creat_file = File.CreateText(Application.persistentDataPath + "/Info.Chi");
                Creat_file.Write(JsonUtility.ToJson(New_model));
                Creat_file.Close();
                Load_data();
            }
        }
    }

    public static void Insert_mission(Vector3 Last_posion)
    {
        Cam.Move_camera(new Vector3(Last_posion.x + 10, Last_posion.y + 10, -10));
        mission_Collection.Add(new Vector3(Last_posion.x + 10, Last_posion.y + 10, Last_posion.z)); ;
    }


    public static class Cam
    {
        /// <summary>
        /// animation move camera
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
                        await Task.Delay(10);
                        cam.transform.position = Vector3.MoveTowards(cam.transform.position, Pos_new, 0.1f);
                    }
                    else
                    {

                        break;
                    }
                }
            }
        }


        public static void Move_camera()
        {
            Move();
            async void Move()
            {
                while (true)
                {
                    if (cam.transform.position != mission_Collection.last_pos)
                    {
                        await Task.Delay(10);
                        cam.transform.position = Vector3.MoveTowards(cam.transform.position, mission_Collection.last_pos, 0.1f);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }


    public class Mission_Collector : IList<GameObject>
    {
        GameObject[] Collection;
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
                    Collection = col_n;
                    Collection = new GameObject[col_n.Length];
                    Collection = col_n;
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

            for (int i = 0; i < Collection.Length; i++)
            {
                new_model.Pos_G[i] = Collection[i].transform.position;
                new_model.S[i] = Collection[i].GetComponent<Game_play>().Star;
                new_model.ST_P[i] = Collection[i].GetComponent<Game_play>().State_pass;
                new_model.T_M[i] = Collection[i].GetComponent<Game_play>().Time_mision;
            }

            string new_data = JsonUtility.ToJson(new_model);

            StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/Info.Chi");
            streamWriter.Write(new_data);
            streamWriter.Close();

        }



        /// <summary>
        /// add update frist start
        /// </summary>
        /// <param name="item"></param>
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


        /// <summary>
        /// reset mikone mishon ro va file  info taghir mide
        /// </summary>
        /// <param name="Mission_number"> mision number for change</param>
        public void Reset_mision(int Mission_number)
        {
            print(Collection.Length);

            for (int i = 0; i < Collection.Length; i++)
            {
                if (Mission_number == i-1)
                {
                    print(Collection[i].GetComponent<Game_play>().Level);
                    Collection[i].GetComponent<Game_play>().Star = 0;
                    Collection[i].GetComponent<Game_play>().State_pass = 0;
                    Collection[i].GetComponent<Game_play>().Time_mision = 0;
                }

            }

            print(Collection.Length);

            Entity_player_model new_model_entity = new Entity_player_model();

            new_model_entity.Pos_G = new Vector3[Collection.Length];
            new_model_entity.S = new int[Collection.Length];
            new_model_entity.ST_P = new int[Collection.Length];
            new_model_entity.T_M = new float[Collection.Length];

            for (int i = 0; i < Collection.Length; i++)
            {
                new_model_entity.Pos_G[i] = Collection[i].GetComponent<Game_play>().transform.position;
                new_model_entity.S[i] = Collection[i].GetComponent<Game_play>().Star;
                new_model_entity.ST_P[i] = Collection[i].GetComponent<Game_play>().State_pass;
                new_model_entity.T_M[i] = Collection[i].GetComponent<Game_play>().Time_mision;
            }
            
            StreamWriter File_info_model = new StreamWriter(Application.persistentDataPath + "/Info.Chi");
            string String_data = JsonUtility.ToJson(new_model_entity);
            File_info_model.Write(String_data);
            File_info_model.Close();

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
            foreach (var item in Collection)
            {
                yield return item;

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

        public void RemoveAt(int index)
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
    }

}

