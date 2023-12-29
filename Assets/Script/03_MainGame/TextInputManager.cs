using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class TextInputManager : MonoBehaviour
{
    public Text writeText;
    public InputField inputText;
    public Text readText;
    public Text readText2;
    public Text limite;
    public Button buttons;
    public Image images;
    private void Update()
    {
        limite.text = "±ÛÀÚ¼ö "+inputText.text.Length.ToString() + "/200";
        writeText.text = inputText.text;
        readText.text = writeText.text;
        readText2.text = writeText.text;
    }
    public void SettingInputfield(GameObject objects)
    {
        if(inputText.text == string.Empty)
        {
            images.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            objects.SetActive(true);
        }
    }

}
