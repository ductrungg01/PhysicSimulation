using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicSimulation : MonoBehaviour
{
    private MoveMode mode = MoveMode.None;
    private float V_input, T_input, V_curr = 0f, time_curr = 0f;
    private UIManager uiManager;

    private bool isStarted = false;

    private float time_remain = 0;
    private float v0, a, s_all, s_state;
    private float start_time = 0;
    
    [Header("Product simulate")] [SerializeField]
    private GameObject box;

    private float BoxRealDistance = 0;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        Box boxInfor = box.GetComponent<Box>();
        BoxRealDistance = boxInfor.endPoint.position.x - boxInfor.startPoint.position.x;
    }

    public void StartSimulate()
    {
        Debug.Log("Start simulating!");

        start_time = Time.time;
        isStarted = true;
        
        uiManager.SetNoChangeInput();

        V_input = uiManager.GetVInput();
        T_input = uiManager.GetTInput();
        
        mode = MoveMode.None;

        calcS();
        ChangeMode();
    }

    void calcS()
    {
        s_all = calcDistance(0, T_input, V_input / T_input)
            + calcDistance(V_input, T_input, 0)
            + calcDistance(V_input, T_input, -V_input / T_input);
        
        Debug.Log($"s_all : {s_all}");
        Debug.Log($"BoxRealDistance : {BoxRealDistance}");
    }

    float calcDistance(float v0, float time, float a)
    {
        return v0 * time + (a * time * time) / 2;
    }
    
    void FixedUpdate()
    {
        if (isStarted)
        {
            float t = (T_input - time_remain);
            
            // change v_curr and t_curr
            time_curr = Time.time - start_time;
            V_curr = v0 + a * t;
            
            // calc the distance moving of this frame
            float s_frame = V_curr * Time.deltaTime;
            float s_frame_ui = ((s_frame / s_all) * BoxRealDistance);
            box.transform.Translate(s_frame_ui, 0, 0);
            
            Debug.Log($"s_frame: {s_frame}");
            Debug.Log($"s_frame_ui: {s_frame_ui}");
            
            // Show on UI
            uiManager.SetVCurr(V_curr);
            uiManager.SetTimeCurr(time_curr);
            
            // Check time_remain to change mode
            time_remain -= Time.deltaTime;
            if (time_remain <= 0)
            {
                ChangeMode();
            }
        }    
    }

    void ChangeMode()
    {
        this.time_remain = T_input;

        // Change mode
        switch (this.mode)
        {
            case MoveMode.None:
                this.mode = MoveMode.MovingFaster;
                break;
            case MoveMode.MovingFaster:
                this.mode = MoveMode.UniformMotion;
                break;
            case MoveMode.UniformMotion:
                this.mode = MoveMode.MovingSlowdown;
                break;
            case MoveMode.MovingSlowdown:
                StopSimulation();
                break;
        }        
        
        // Set mode value
        switch (this.mode)
        {
            case MoveMode.MovingFaster:
                v0 = 0;
                a = V_input / T_input;
                break;
            case MoveMode.UniformMotion:
                v0 = V_input;
                a = 0;
                break;
            case MoveMode.MovingSlowdown:
                v0 = V_input;
                a = -(V_input / T_input);
                break;
            case MoveMode.None:
                a = 0;
                break;
        }

        uiManager.SetACurr(a);
        
        s_state = calcDistance(v0, T_input, a);
        
        Debug.Log($"{mode} s_state (before): {s_state}");
        
        s_state = (s_state / s_all) * BoxRealDistance;
        
        Debug.Log($"{mode} s_state (after): {s_state}");
        
        // Highlight text from UI Manager
        uiManager.HighLightTheText(mode);
    }

    void StopSimulation()
    {
        isStarted = false;
    }
}
