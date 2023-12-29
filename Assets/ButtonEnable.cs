using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnable : MonoBehaviour
{
    public Button button;

    public void ButtonEnabled()
    {
        button.enabled = true;
    }
    public void Buttondisabled()
    {
        button.enabled = false;
    }
}
