using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WirteManager : MonoBehaviour
{
    public Text Date;
    public Text week;
    public Image Grade;
    public List<Sprite> grades = new List<Sprite>();

    public Image leap;
    public List<Sprite> leapIMG = new List<Sprite>();
    public int leapCount = 0;

    public List<Sprite> sprites = new List<Sprite>();
    public List<Image> images = new List<Image>();
    public List<Image> mainGrades = new List<Image>();
    public List<Text> texts= new List<Text>();

    public InputField wirte;
    public Text writeText;

    private void Start()
    {
        
        week.text = DateTime.Now.ToString("yyyy") + "�� " + DateTime.Now.ToString("MM") + "�� " + ReturnWeek(DateTime.Now); 
        Date.text = DateTime.Now.ToString("yyyy") + "�� "+ DateTime.Now.ToString("MM") + "��"+ DateTime.Now.ToString("dd") + "��";
        for(int i =0; i< images.Count; i++)
        {
            images[i].gameObject.SetActive(false);
        }
        for(int i = 0; i<DataSave.Instance._data.plantsData.Count; i++)
        {
            for (int j = 0; j < sprites.Count; j++)
            {
                if (sprites[j].name == DataSave.Instance._data.plantsData[i].plantsname)
                {
                    images[i].sprite = sprites[j];
                    images[i].gameObject.SetActive(true);
                    texts[i].text = PlantNameEntoKor(DataSave.Instance._data.plantsData[i].plantsname, DataSave.Instance._data.plantsData[i].plantsClass);
                    if (DataSave.Instance._data.plantsData[i].plantsClass == "2")
                    {
                        Grade.sprite = grades[2];
                        mainGrades[i].sprite = grades[2];
                    }
                    else if(DataSave.Instance._data.plantsData[i].plantsClass == "3")
                    {
                        Grade.sprite = grades[1];
                        mainGrades[i].sprite = grades[1];
                    }
                    else if (DataSave.Instance._data.plantsData[i].plantsClass == "4")
                    {
                        Grade.sprite = grades[0];
                        mainGrades[i].sprite = grades[0];
                    }
                }
            }
        }

    }
    public void SettingImage()
    {

    }
    private void Update()
    {
        leap.sprite = leapIMG[ConsensusUIEvent.leapCount];
        writeText.text = wirte.text.ToString();
        DataSetting();
    }
    public void DataSetting()
    {
        if (DataSave.Instance._data.plantsData[DataSave.Instance.index].isEmotion == true)
        {
            leapCount++;
        }
        if (DataSave.Instance._data.plantsData[DataSave.Instance.index].isEmotion == true)
        {
            leapCount++;
        }
        if (DataSave.Instance._data.plantsData[DataSave.Instance.index].isEmotion == true)
        {
            leapCount++;
        }
        if (DataSave.Instance._data.plantsData[DataSave.Instance.index].isEmotion == true)
        {
            leapCount++;
        }
    }

    public static int GetWeekOfMonth(DateTime dt)
    {
        DateTime now = dt;
        int basisWeekOfDay = (now.Day - 1) % 7;
        int thisWeek = (int)now.DayOfWeek;

        double val = Math.Ceiling((double)now.Day / 7);
        if (basisWeekOfDay > thisWeek)
            val++;
        return Convert.ToInt32(val);
    }
    public string ReturnWeek(DateTime dt)
    {
        if(GetWeekOfMonth(dt) == 1)
        {
            return "ù° ��";
        }
        else if (GetWeekOfMonth(dt) == 2)
        {
            return "��° ��";
        }
        else if(GetWeekOfMonth(dt) == 3)
        {
            return "��° ��";
        }
        else if(GetWeekOfMonth(dt) == 4)
        {
            return "��° ��";
        }
        else if (GetWeekOfMonth(dt) == 5)
        {
            return "�ټ�° ��";
        }
        else
        {
            return "null";
        }

    }
    public void Save()
    {
       // DataSave.Instance.WirteSave(wirte.text);
    }
    public List<Sprite> plantsImags = new List<Sprite>();
    public Image plantsPage;
    public int plantsIndex = 0;
    public void NextButton()
    {
        if(plantsIndex<plantsImags.Count-1)
        {
            plantsIndex++;
        }
        else if(plantsIndex == plantsImags.Count-1)
        {
            plantsIndex =0;
        }
        plantsPage.sprite = plantsImags[plantsIndex];
    }
    public void PrevButton()
    {
        if (plantsIndex > 0)
        {
            plantsIndex--;
        }
        else if( plantsIndex ==0)
        {
            plantsIndex = 4;
        }
        plantsPage.sprite = plantsImags[plantsIndex];
    }
    public string PlantNameEntoKor(string name, string Class)
    {
        string tempName = name.Replace(Class, "");
        switch(tempName)
        {
            case "Blueberries":
                {
                    return "��纣��";
                }
            case "Cactus":
                {
                    return "������";
                }
            case "carrot":
                {
                    return "���";
                }
            case "Herb":
                {
                    return "���";
                }
            case "lavender":
                {
                    return "�󺥴�";
                }
            case "Lettuce":
                {
                    return "����";
                }
            case "Rose":
                {
                    return "���";
                }
            case "Sticky":
                {
                    return "����Ű";
                }
            case "Sunflower":
                {
                    return "�عٶ��";
                }
            case "Tomato":
                {
                    return "�丶��";
                }
        }
        return null;
    }
}
