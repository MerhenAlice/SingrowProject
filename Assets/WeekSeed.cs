using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekSeed : MonoBehaviour
{
    public List<string> namse = new List<string>();
    public int index;
    public PlantsData plantsData;
    public LambdaPublic lambdaPublic;
    // Start is called before the first frame update
    void Start()
    {
        index = UnityEngine.Random.Range(0, namse.Count);
        
    }

    public void GetPlants()
    {
        if (DataSave.Instance._data.plantsData.Count < 5)
        {
            plantsData.plantsname = namse[index];
            plantsData.plantsIdentification = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
            plantsData.isDead = false;
            plantsData.isSell = false;
            plantsData.plantsExp = 0;
            plantsData.plantsStairExp = 10;
            plantsData.plantsClass = "0";
            plantsData.lastExpDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
            plantsData.plantsIndex = DataSave.Instance._data.plantsData.Count;
            plantsData.pots = null;

            DataSave.Instance._data.plantsData.Add(plantsData);
            lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        }
#if UNITY_EDITOR
        Debug.Log($"plantsname={namse[index]}");
#endif
        
    }
   
}
