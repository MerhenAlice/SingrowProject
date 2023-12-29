using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class SetiingTalk : MonoBehaviour
{
    public GameObject consensus;
    public GameObject Talk;
    public Button consesus;
    public GameObject gogo;
    private void Start()
    {

        DateTime dateTime = DateTime.Parse(DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate);
        if ((DateTime.Now - dateTime).Days >= 2 && (DateTime.Now - dateTime).Days < 3)
        {
            gogo.SetActive(true);
        }
    }
    void Update()
    {
        DateTime dateTime = DateTime.Parse(DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate);
        if ((DateTime.Now - dateTime).Days >= 2 && (DateTime.Now - dateTime).Days < 3)
        {
            Talk.SetActive(false);
            consesus.gameObject.SetActive(false);
        }
        else
        {
            if (consensus.activeInHierarchy == true)
            {
                Talk.SetActive(true);
                gogo.SetActive(false);
                consesus.gameObject.SetActive(true);
            }
            else
            {
                Talk.SetActive(false);
            }
        }
    }
}
