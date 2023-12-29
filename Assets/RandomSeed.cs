using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSeed : MonoBehaviour
{
    //식물정보 이미지 리스트
    public List<Sprite> plantinos = new List<Sprite>();
    //이미지
    public Image plantinfo;
    
    public PlantsData plantsData;
    public void GetPlants()
    {
        DataSave.Instance.plantPickInGauard = NameSetting( plantinfo.sprite.name);
        plantsData.plantsname = NameSetting(plantinfo.sprite.name);
        plantsData.plantsExp = 5;
        plantsData.plantsClass = "0";
        plantsData.plantsIndex= 0;
        plantsData.isSell = false;
        plantsData.plantsStairExp = 10;
        plantsData.lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        DataSave.Instance._data.plantsData.Add(plantsData);
    }
    public string NameSetting(string name)
    {
        switch(name)
        {
            case "Angiospermae":
                {
                   return "Rose0";
                }
            case "fruit":
                {
                   return "Tomato0";
                }
            case "Herb":
                {
                    return "Herb0";
                }
            case "Succulent":
                {
                    return "Cactus0";

                }
            case "Vegetable":
                {
                    return "carrot0";
                }
        }

        return null;
    }
}
