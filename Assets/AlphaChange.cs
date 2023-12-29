using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChange : MonoBehaviour
{
    public GameObject go;
    public Color color;
    void Update()
    {
        if (go.activeInHierarchy == false)
        {
            this.gameObject.SetActive(false);
            this.gameObject.GetComponent<Image>().color = new Color(255,255,255, 0);
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }
}
