using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// playerpref
/// 1: Day_Night
/// </summary>
public class Day_night_controler : MonoBehaviour
{
    public float Float_Shadow;

    public Color Color_camera_in_day;
    public Color Color_camera_in_night;


    public Color Color_day;
    public Color Color_night;

    public Color Color_day_shadow;
    public Color Color_night_shadow;

    private void Start()
    {

        if (PlayerPrefs.GetInt("Day_Night") == 1)
        {
            Camera.main.backgroundColor = Color_camera_in_night;
        }
        else if (PlayerPrefs.GetInt("Day_Night") == 0)
        {
            Camera.main.backgroundColor = Color_camera_in_day;

        }

        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetInt("Day_Night") == 1)
            {
                PlayerPrefs.SetInt("Day_Night", 0);
            }
            else if (PlayerPrefs.GetInt("Day_Night") == 0)
            {
                PlayerPrefs.SetInt("Day_Night", 1);
            }
            SceneManager.LoadScene(0);
        });

    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("Day_Night") == 1)
        {
            foreach (var Raw_Image in GameObject.FindGameObjectsWithTag("Change_Color_RawImage"))
            {
                Raw_Image.GetComponent<RawImage>().color = Color_night;
                Raw_Image.GetComponent<Shadow>().effectColor = Color_night_shadow;
                Raw_Image.GetComponent<Shadow>().effectDistance = new Vector2(Input.acceleration.x / 8, Input.acceleration.y / Float_Shadow);
            }

            foreach (var Image in GameObject.FindGameObjectsWithTag("Change_Color_Image"))
            {
                Image.GetComponent<Image>().color = Color_night;
                Image.GetComponent<Shadow>().effectColor = Color_night_shadow;
                Image.GetComponent<Shadow>().effectDistance = new Vector2(Input.acceleration.x / 8, Input.acceleration.y / Float_Shadow);
            }

            foreach (var Texts in GameObject.FindGameObjectsWithTag("Change_Color_Texts"))
            {
                Texts.GetComponent<TextMeshProUGUI>().color = Color_night;
            }

        }
        else if (PlayerPrefs.GetInt("Day_Night") == 0)
        {
            foreach (var Raw_Image in GameObject.FindGameObjectsWithTag("Change_Color_RawImage"))
            {
                Raw_Image.GetComponent<RawImage>().color = Color_day;
                Raw_Image.GetComponent<Shadow>().effectDistance = new Vector2(Input.acceleration.x / 8, Input.acceleration.y / Float_Shadow);
            }

            foreach (var Image in GameObject.FindGameObjectsWithTag("Change_Color_Image"))
            {
                Image.GetComponent<Image>().color = Color_day;
                Image.GetComponent<Shadow>().effectDistance = new Vector2(Input.acceleration.x / 8, Input.acceleration.y / Float_Shadow);
            }

            foreach (var Texts in GameObject.FindGameObjectsWithTag("Change_Color_Texts"))
            {
                Texts.GetComponent<TextMeshProUGUI>().color = Color_day;
            }

        }
    }

}
