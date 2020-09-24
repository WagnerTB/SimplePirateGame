using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputValidation : MonoBehaviour
{
    public bool onlyNumbers = true;
    public TMPro.TMP_InputField inputField;

    private void Awake()
    {
        if (inputField != null)
        {
            inputField.onEndEdit.AddListener((txt) =>
            {
                if(onlyNumbers)
                    IsNumber(txt);
            });
        }
    }

    public void IsNumber(string text)
    {
        if (!int.TryParse(text, out int number) && !float.TryParse(text, out float floatNumber))
        {
            inputField.text = "";
        }
    }

    private void Reset()
    {
        var __inputField = GetComponent<TMPro.TMP_InputField>();
        if(__inputField != null)
        {
            inputField = __inputField;
        }
    }
}
