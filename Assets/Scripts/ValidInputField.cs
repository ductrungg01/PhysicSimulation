using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValidInputField : MonoBehaviour
{
    private TMP_InputField _inputField;

    private void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
        
        _inputField.onValueChanged.AddListener(CheckValidInput);
    }

    private void CheckValidInput(string input)
    {
        float value = float.Parse(input);
        if (value < 0)
        {
            _inputField.text = "0";
        }
    }
}
