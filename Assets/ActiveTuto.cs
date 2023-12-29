using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTuto : MonoBehaviour
{
    public List<GameObject>fuck = new List<GameObject>();
    public GameObject s16s;

    public GameObject tuto;
    public void Onbuttons()
    {
        for(int i =0; i< fuck.Count;i++)
        {
            if(fuck[i].activeInHierarchy == true)
            {
                tuto.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if(s16s.activeInHierarchy == true)
        {
            DataSave.Instance._data.plantsData[0].plantsStairExp = 10;
            DataSave.Instance._data.plantsData[0].plantsExp = 5;
        }
        for (int i = 0; i < fuck.Count; i++)
        {
            if (fuck[i].activeInHierarchy == true)
            {
                tuto.SetActive(false);
            }
        }
    }
}
