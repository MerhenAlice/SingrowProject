using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantsInfoOFGarden : MonoBehaviour
{
    public Text plantsName;
    public List<Sprite> Rank = new List<Sprite>();
    public Image plantsRank;

    public Slider plantsExp;
    public Text plantsExpTxt;

    public GameObject go;
    private void OnEnable()
    {
        plantsName.text = NameSetting(transform.parent.name);
        for (int i = 0; i < DataSave.Instance._data.plantsData.Count; i++)
        {
            if (plantsName.text == DataSave.Instance._data.plantsData[i].plantsname)
            {
                if (DataSave.Instance._data.plantsData[i].plantsClass == "2")
                {
                    plantsRank.sprite = Rank[0];
                    break;
                }
                else if (DataSave.Instance._data.plantsData[i].plantsClass == "3")
                {
                    plantsRank.sprite = Rank[1];
                    break;
                }
                else if (DataSave.Instance._data.plantsData[i].plantsClass == "4")
                {
                    plantsRank.sprite = Rank[2];
                    break;
                }
                else
                {
                    plantsRank.sprite = Rank[0];
                    break;
                }
            }
        }
        for(int i = 0; i<DataSave.Instance._data.plantsData.Count; i++)
        {
            if (go.name == DataSave.Instance._data.plantsData[i].plantsIndex.ToString())
            {
                if (DataSave.Instance._data.plantsData[i].plantsClass == "4")
                {
                        plantsExp.maxValue = 1;
                        plantsExp.value =1;
                        plantsExpTxt.text ="MAX";
                }
                else
                {

                    plantsExp.maxValue = DataSave.Instance._data.plantsData[i].plantsStairExp;
                    plantsExp.value = DataSave.Instance._data.plantsData[i].plantsExp;
                    plantsExpTxt.text = DataSave.Instance._data.plantsData[i].plantsExp.ToString() + "/" + DataSave.Instance._data.plantsData[i].plantsStairExp.ToString();
                }
            }
            
        }
    }
    private void Update()
    {
        plantsName.text = NameSetting(transform.parent.name);
    }

    public string NameSetting(string name)
    {
        string nameReplace = name.Replace("0", "");
        nameReplace = nameReplace.Replace("1", "");
        nameReplace = nameReplace.Replace("2", "");
        nameReplace = nameReplace.Replace("3", "");
        nameReplace = nameReplace.Replace("4", "");
        switch (nameReplace)
        {
            case "Cactus":
                return "선인장";
            case "Sticky":
                return "스투키";
            case "Herb":
                return "허브";
            case "lavender":
                return "라벤더";
            case "Rose":
                return "장미";
            case "Sunflower":
                return "해바라기";
            case "Lettuce":
                return "상추";
            case "carrot":
                return "당근";
            case "Tomato":
                return "토마토";
            case "Blueberries":
                return "블루베리";
        }
        return null;
    }
}
