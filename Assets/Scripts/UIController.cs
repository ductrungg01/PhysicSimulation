using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField V_input;
    [SerializeField] private TMP_InputField T_input;
    [SerializeField] private Text V_curr;
    [SerializeField] private Text time_curr;
    [SerializeField] private Text a_curr;
    [SerializeField] private Text movingFaster_text;
    [SerializeField] private Text uniformMotion_text;
    [SerializeField] private Text movingSlowdown_text;
    [SerializeField] private Color normal_text_color = Color.white;
    [SerializeField] private Color highlight_text_color = Color.green;
    
    public void SetNoChangeInput()
    {
        V_input.readOnly = true;
        T_input.readOnly = true;
    }

    public void SetVCurr(float v_curr)
    {
        V_curr.text = v_curr.ToString();
    }

    public void SetTimeCurr(float time_curr)
    {
        this.time_curr.text = time_curr.ToString();
    }

    public void SetACurr(float a_curr)
    {
        this.a_curr.text = a_curr.ToString();
    }

    public float GetVInput()
    {
        return float.Parse(this.V_input.text);
    }

    public float GetTInput()
    {
        return float.Parse(this.T_input.text);
    }

    public void HighLightTheText(MoveMode mode)
    {
        movingFaster_text.color = normal_text_color;
        uniformMotion_text.color = normal_text_color;
        movingSlowdown_text.color = normal_text_color;
        
        switch (mode)
        {
            case MoveMode.MovingFaster:
                movingFaster_text.color = highlight_text_color;
                break;
            case MoveMode.UniformMotion:
                uniformMotion_text.color = highlight_text_color;
                break;
            case MoveMode.MovingSlowdown:
                movingSlowdown_text.color = highlight_text_color;
                break;
        }
    }
}
