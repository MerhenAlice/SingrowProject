using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteRendererClickDetector : MonoBehaviour
{
    bool isOver = false;
    public GameObject UI;
    public UIController UIController;
    public InterfaceCanvas interfaceCanvas;
    public GameObject go;
    private void Update()
    {
        if(isOver)
        {
            UI.SetActive(true);
        }
        else if(isOver==false)
        {
            UI.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        interfaceCanvas.GetIndex(go);

        if(isOver==false)
        {
            isOver = true;
        }
        else if(isOver==true)
        {
            isOver = false;
        }

    }
    private void OnMouseExit()
    {
    }
}
