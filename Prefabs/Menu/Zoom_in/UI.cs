using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    Control_zoom Control_Zoom;
    public Texture[] Images_zoom;
    public GameObject BTN_zoom;
    public GameObject[] BTN_up_down;


    private void Start()
    {
        Control_Zoom = new Control_zoom(Images_zoom, BTN_zoom, BTN_up_down);

    }
    private void Update()
    {
        if (Player.Cam.Zoom == 0)
        {
            Force_close_up_down();
        }

    }


    /// <summary>
    /// zoom mikone camera
    /// </summary>
    public void Prees_btn_zoom()
    {
        Control_Zoom.Zoom();
    }


    /// <summary>
    /// balaro control mikone va nemizare bish az had cam bere bala
    /// </summary>
    public void Press_BTN_Up()
    {
        Control_Zoom.Press_BTN_Up();
    }


    /// <summary>
    /// btndown_control mikone va nemizare cam bishaz had bere bala
    /// </summary>
    public void Press_BTN_down()
    {
        Control_Zoom.Press_BTN_Down();
    }



    /// <summary>
    /// age cam zoom shodanesh payan dashte bashe bt jam mishe 
    /// </summary>
    void Force_close_up_down()
    {
        BTN_up_down[0].transform.localScale = Vector3.MoveTowards(BTN_up_down[0].transform.localScale, Vector3.zero, 0.1f);
        BTN_up_down[1].transform.localScale = Vector3.MoveTowards(BTN_up_down[1].transform.localScale, Vector3.zero, 0.1f);

    }

    class Control_zoom
    {
        Texture[] Texture_zoom;
        GameObject BTN_zoom;
        GameObject[] BTN_up_down;

        public Control_zoom(Texture[] Texture_zoom, GameObject BTN_zoom, GameObject[] BTN_up_down)
        {
            this.Texture_zoom = Texture_zoom;
            this.BTN_zoom = BTN_zoom;
            this.BTN_up_down = BTN_up_down;
        }


        /// <summary>
        /// 1: chek mikone status zoom 
        /// </summary>
        public void Zoom()
        {

            Player.Cam.Zoom_Back();

            if (Player.Cam.Zoom == 1)
            {
                Animation_zoom_in();

            }
            else
            {
                animation_zoom_back();
            }

            async void Animation_zoom_in()
            {

                if (BTN_zoom.transform.localScale != Vector3.zero)
                {

                    Show_btn_zoom();
                    while (true)
                    {
                        await Task.Delay(1);
                        BTN_zoom.transform.localScale = Vector3.MoveTowards(BTN_zoom.transform.localScale, Vector3.zero, 0.1f);

                        if (BTN_zoom.transform.localScale == Vector3.zero)
                        {
                            BTN_zoom.GetComponent<RawImage>().texture = Texture_zoom[1];
                            break;
                        }
                    }
                }

                if (BTN_zoom.transform.localScale == Vector3.zero)
                {
                    Show_btn_zoom();
                    while (true)
                    {
                        await Task.Delay(1);
                        BTN_zoom.transform.localScale = Vector3.MoveTowards(BTN_zoom.transform.localScale, Vector3.one, 0.1f);
                        if (BTN_zoom.transform.localScale == Vector3.one)
                        {
                            break;
                        }
                    }
                }
            }

            async void animation_zoom_back()
            {
                if (BTN_zoom.transform.localScale != Vector3.zero)
                {
                    Show_btn_zoom();
                    while (true)
                    {
                        await Task.Delay(1);
                        BTN_zoom.transform.localScale = Vector3.MoveTowards(BTN_zoom.transform.localScale, Vector3.zero, 0.1f);
                        if (BTN_zoom.transform.localScale == Vector3.zero)
                        {
                            BTN_zoom.GetComponent<RawImage>().texture = Texture_zoom[0];
                            break;
                        }

                    }
                }

                if (BTN_zoom.transform.localScale != Vector3.one)
                {
                    while (true)
                    {
                        await Task.Delay(1);
                        BTN_zoom.transform.localScale = Vector3.MoveTowards(BTN_zoom.transform.localScale, Vector3.one, 0.1f);
                        if (BTN_zoom.transform.localScale == Vector3.one)
                        {
                            break;
                        }
                    }

                }
            }

            async void Show_btn_zoom()
            {
                foreach (var item in BTN_up_down)
                {
                    if (Player.Cam.Zoom == 1)
                    {
                        item.SetActive(true);
                        while (true)
                        {
                            await Task.Delay(1);
                            item.transform.localScale = Vector3.MoveTowards(item.transform.localScale, Vector3.one, 0.1f);
                            if (item.transform.localScale == Vector3.one)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {

                        while (true)
                        {
                            await Task.Delay(1);
                            item.transform.localScale = Vector3.MoveTowards(item.transform.localScale, Vector3.zero, 0.1f);
                            if (item.transform.localScale == Vector3.zero)
                            {
                                item.SetActive(false);
                                break;
                            }
                        }
                    }
                }

            }


        }


        /// <summary>
        /// move camera to up
        /// </summary>
        public void Press_BTN_Up()
        {
            Move();

            async void Move()
            {
                Vector3 next_pos_move = new Vector3(Player.cam.transform.position.x + 10, Player.cam.transform.position.y + 10);
                if (next_pos_move.x > Player.mission_Collection.last_pos.x)//control mikone akharin pos k bishtar move nashe
                {
                    next_pos_move = Player.mission_Collection.last_pos;
                }

                while (true)
                {
                    if (Player.cam.transform.position != next_pos_move)
                    {
                        await Task.Delay(1);
                        Player.cam.transform.position = Vector3.MoveTowards(Player.cam.transform.position, next_pos_move, 1f);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// move camera to down
        /// </summary>
        public void Press_BTN_Down()
        {
            Move();
            async void Move()
            {
                Vector3 Pos_previsue = new Vector3(Player.cam.transform.position.x - 10, Player.cam.transform.position.y - 10);
                if (Pos_previsue.x < 0)// chek mikone ke mogheiat camera cam taraz home nashe
                {
                    Pos_previsue = Vector3.zero;
                }
                while (true)
                {
                    if (Player.cam.transform.position != Pos_previsue)
                    {
                        await Task.Delay(1);
                        Player.cam.transform.position = Vector3.MoveTowards(Player.cam.transform.position, Pos_previsue, 1f);
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


