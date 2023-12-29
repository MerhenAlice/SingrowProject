using Amazon.Lambda.Model.Internal.MarshallTransformations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VolumeComponent;

public class PlnatsDead : MonoBehaviour
{
    public GameObject popUp1;
    public GameObject popUp2;
    public GameObject popUp3;
    public GameObject deletePopUp;
    public LambdaPublic lambdaPublic;
    public List<Sprite> images = new List<Sprite>();
    public List<string> names = new List<string>();
    public List<Text> texts = new List<Text>();
    public List<string> identi = new List<string>();
    public Data data;
    public int count = 0;
    private void Awake()
    {
        data = DataSave.Instance._data;
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(data), "DataSave");
        for(int i =0; i<DataSave.Instance._data.plantsData.Count; i++)
        {
            if (DataSave.Instance._data.plantsData[i].isKing ==false)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        if (DataSave.Instance.isFirst == true)
        {
            if (DataSave.Instance._data.plantsData.Count - 1 >= DataSave.Instance.kingIndex)
            {
                DataSave.Instance._data.plantsData[DataSave.Instance.kingIndex].isKing = true;
            }
            else if (DataSave.Instance._data.plantsData.Count - 1 == 0)
            {
                if (DataSave.Instance._data.plantsData[0].isKing == true)
                {

                }
                else
                {

                    DataSave.Instance._data.plantsData[0].isKing = true;
                }
            }
            DataSave.Instance.isFirst = false;
        }
        count = 0;
    }
    private void Update()
    {
        
        for (int i = DataSave.Instance._data.plantsData.Count - 1; i >= 0; i--)
        {
            DateTime dateTime = DateTime.Parse(DataSave.Instance._data.plantsData[i].lastExpDate);
            Debug.Log("DateTime = " + dateTime);
            if ((DateTime.Now - dateTime).Days >= 2 && (DateTime.Now - dateTime).Days < 3)
            {
                popUp1.SetActive(false);
                popUp2.SetActive(true);
                popUp3.SetActive(false);
                for (int j = 0; j < images.Count; j++)
                {
                    if (DataSave.Instance._data.plantsData[i].plantsname == images[j].name)
                    {
                        DataSave.Instance.plants[i].sprite = images[j];
                    }
                }

            }
        }
    }
    private void OnEnable()
    {
        Debug.Log(DateTime.Now.ToString("yyyy-MM-dd HH:mm-ss"));
        DateTime dateTime = DateTime.Parse(DataSave.Instance._data.plantsData[0].lastExpDate);
        index = UnityEngine.Random.Range(0, namse.Count);
        if (DataSave.Instance._data.plantsData.Count > 0)
        {
            Debug.Log((DateTime.Now - dateTime).Hours);
            Debug.Log((DateTime.Now - dateTime).Days);
        }
        for (int i = DataSave.Instance._data.plantsData.Count-1; i >=0; i--)
        {
            dateTime = DateTime.Parse(DataSave.Instance._data.plantsData[i].lastExpDate);
            Debug.Log("DateTime = " + dateTime);
            if ((DateTime.Now - dateTime).Days >=2&& (DateTime.Now - dateTime).Days < 3)
            {
                popUp1.SetActive(false);
                popUp2.SetActive(true);
                popUp3.SetActive(false);
                for(int j =0; j<images.Count; j++)
                {
                    if (DataSave.Instance._data.plantsData[i].plantsname == images[j].name)
                    { 
                        DataSave.Instance.plants[i].sprite = images[j]; 
                    }
                }
                
            }
            else if ((DateTime.Now - dateTime).Days >= 3)
            {
                names.Add(DataSave.Instance._data.plantsData[i].plantsname);
                identi.Add(DataSave.Instance._data.plantsData[i].plantsIdentification);
                DataSave.Instance._data.plantsData.RemoveAt(i);
                popUp1.SetActive(false);
                popUp2.SetActive(false);
                popUp3.SetActive(true);
                deletePopUp.SetActive(true);
            }
        }
        for (int i = 0; i < names.Count; i++)
        {
            texts[i].text = NameChager(names[i].ToString());
            Debug.Log("name[i] = " + names[i]);
            //  Debug.Log("visangPlants.plants_level = " + visangPlantsDead.visangPlants.plants_level);
        }
        for (int i =0; i<texts.Count; i++)
        {
            if (texts[i].text == string.Empty)
            {
                texts[i].gameObject.SetActive(false);
            }
        }
    }
    public List<string> namse = new List<string>();
    public PlantsData plantsData;
    public int index;
    public List<Sprite> plantsinfo = new List<Sprite>();
    public Image image;
    public void GetPlants()
    {
        plantsData.plantsname = namse[index];
        plantsData.plantsIdentification = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
#if UNITY_EDITOR
        Debug.Log($"plantsname={namse[index]}");
#endif
        plantsData.isDead = false;
        plantsData.isSell = false;
        plantsData.plantsExp = 0;
        plantsData.lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        plantsData.plantsStairExp = 10;
        plantsData.plantsClass = "0";
        plantsData.plantsIndex = DataSave.Instance._data.plantsData.Count;
        plantsData.pots = null;

        DataSave.Instance._data.plantsData.Add(plantsData);
        DataSave.Instance._data.plantsData[plantsData.plantsIndex].lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(data), "DataSave");
        DataSave.Instance.isFirst = true;
        for (int i = 0; i < plantsinfo.Count; i++)
        {
            if (plantsinfo[i].name == plantsData.plantsname)
            {
                image.sprite = plantsinfo[i];
                image.gameObject.SetActive(true);
            }
        }
    }
    private string NameChager(string name)
    {
        if(name.Contains("Blueberries"))
        {
            return "블루베리";
        }
        if (name.Contains("Cactus"))
        {
            return "선인장";
        }
        if (name.Contains("carrot"))
        {
            return "당근";
        }
        if (name.Contains("Herb"))
        {
            return "허브";
        }
        if (name.Contains("lavender"))
        {
            return "라벤더";
        }
        if (name.Contains("Lettuce"))
        {
            return "상추";
        }
        if (name.Contains("Rose"))
        {
            return "장미";
        }
        if (name.Contains("Sticky"))
        {
            return "스투키";
        }
        if (name.Contains("Sunflower"))
        {
            return "해바라기";
        }
        if (name.Contains("Tomato"))
        {
            return "토마토";
        }
        return null;
    }
    private string NameChager4(string name)
    {
        if (name.Contains("Blueberries"))
        {
            return "P000";
        }
        if (name.Contains("Cactus"))
        {
            return "P001";
        }
        if (name.Contains("carrot"))
        {
            return "P002";
        }
        if (name.Contains("Herb"))
        {
            return "P003";
        }
        if (name.Contains("lavender"))
        {
            return "P004";
        }
        if (name.Contains("Lettuce"))
        {
            return "P005";
        }
        if (name.Contains("Rose"))
        {
            return "P006";
        }
        if (name.Contains("Sticky"))
        {
            return "P007";
        }
        if (name.Contains("Sunflower"))
        {
            return "P008";
        }
        if (name.Contains("Tomato"))
        {
            return "P009";
        }
        return null;
    }
}