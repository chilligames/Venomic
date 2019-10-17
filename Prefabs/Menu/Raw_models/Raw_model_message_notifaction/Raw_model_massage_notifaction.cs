using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Raw_model_massage_notifaction : MonoBehaviour
{
    public RawImage Image_type;
    public RawImage Image_backgroundfade;

    public Texture Texture_type_ERR;
    public Texture texture_type_Success;


    public Color Color_day;
    public Color Color_Night;
    public Color Backgroudn_type_success;
    public Color background_type_err;

    public TextMeshProUGUI Text_Subject;
    public TextMeshProUGUI Text_message_body;

    public Button BTN_close;


    public void Change_value(string Subject, string Message_body, Type type, Theme theme)
    {
        
        //btn close action
        BTN_close.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });

        //change image type
        switch (type)
        {
            case Type.Success:
                Image_type.texture = texture_type_Success;
                Image_type.color = Backgroudn_type_success;
                break;
            case Type.ERRoR:
                Image_type.color = background_type_err;
                Image_type.texture = Texture_type_ERR;
                break;
        }

        //cahnge color back gorund and texts
        switch (theme)
        {
            case Theme.Day:
                Image_backgroundfade.color = Color_day;
                Text_Subject.color = Color_Night;
                Text_message_body.color = Color_Night;
                break;
            case Theme.Night:
                Image_backgroundfade.color = Color_Night;
                Text_Subject.color = Color_day;
                Text_message_body.color = Color_day;
                break;
        }

        //change text messages
        Text_Subject.text = Subject;
        Text_message_body.text = Message_body;
    }


    public enum Type
    {
        Success, ERRoR
    }

    public enum Theme
    {
        Day, Night
    }
}
