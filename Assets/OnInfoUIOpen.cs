using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class OnInfoUIOpen : MonoBehaviour
{
    public GameObject basicUI;
    public List<GameObject> list = new List<GameObject>();
    public List<GameObject>offlist = new List<GameObject>();

    public GameObject consensusUI;
    public GameObject growUI;
    public GameObject slider;
    public GameObject info;
    public GameObject medi;
    public GameObject media1;
    public GameObject media2;
    public float time = 0;
    private void Update()
    {
        if(basicUI.activeInHierarchy == true)
        {
            for(int i = 0; i< list.Count; i++)
            {
                list[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].SetActive(false);
            }
        }
        if(basicUI.activeInHierarchy == true)
        {
            slider.SetActive(true);
        }
        else
        {
            slider.SetActive(false);
            info.SetActive(false);
        }
        if(medi.activeInHierarchy==false)
        {
            media1.SetActive(false);
            media2.SetActive(false);
            time = 0;
        }
        else
        {
            time += Time.deltaTime;
            media1.SetActive(true);
            if(time >= 3)
            {
                media1.SetActive(false);
                media2.SetActive(true);
                if(time >6)
                {

                    media2.SetActive(false);
                }
            }
        }
    }
    IEnumerator UIOff(GameObject UI)
    {
        yield return new WaitForSeconds(3.0f);
        UI.SetActive(false);
    }
    public void OnClickUI(GameObject UI)
    {
        UI.SetActive(true);
        StartCoroutine(UIOff(UI));
    }
    public void OnClickDoubleImge(GameObject ui)
    {
        StartCoroutine(DoubleUI(ui));
    }
    IEnumerator DoubleUI(GameObject ui)
    {
        yield return new WaitForSeconds(3.0f);
        ui.SetActive(true);
    }
}
