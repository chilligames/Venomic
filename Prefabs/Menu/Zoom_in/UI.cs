using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Threading.Tasks;
public class UI : MonoBehaviour
{


    /// <summary>
    /// 1: chek mikone status zoom 
    /// </summary>
    public void Press_BTN_zoom()
    {
        Player.Cam.Zoom_Back();
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

            while (true)
            {
                if (Player.cam.transform.position != next_pos_move)
                {
                    await Task.Delay(10);
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

            while (true)
            {
                if (Player.cam.transform.position != Pos_previsue)
                {
                    await Task.Delay(10);
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
