using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantsCheck : MonoBehaviour
{
    public Text check;

    private void Update()
    {
        check.text = DataSave.Instance._data.plantsData.Count.ToString() + " / 5 ";
    }
}
