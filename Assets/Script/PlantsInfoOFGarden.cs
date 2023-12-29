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
                return "������";
            case "Sticky":
                return "����Ű";
            case "Herb":
                return "���";
            case "lavender":
                return "�󺥴�";
            case "Rose":
                return "���";
            case "Sunflower":
                return "�عٶ��";
            case "Lettuce":
                return "����";
            case "carrot":
                return "���";
            case "Tomato":
                return "�丶��";
            case "Blueberries":
                return "��纣��";
        }
        return null;
    }
}
