using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HideButtonWhenNotEnoughData : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField_V;
    [SerializeField] private TMP_InputField inputField_T;
    [SerializeField] private GameObject startButton;
    
    void Update()
    {
        if (inputField_V.text.Length > 0 && inputField_T.text.Length > 0)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }
}
