using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Sun,
    Nutrients,
    Water,
    Seed
}
public class WeekDayCheck : MonoBehaviour
{
    public List<GameObject> weekPanelObj = new List<GameObject>();
    public Item item = new Item();
    int currentDay = 0; 
    public List<Text> dateText = new List<Text>();
    DateTime nowDate = DateTime.Now;
    public ItemManager itemManager;
    public LambdaPublic lambdaPublic;
    public List<GameObject> Monday = new List<GameObject>();
    public List<GameObject> TuseDay = new List<GameObject>();
    public List<GameObject> WendsDay = new List<GameObject>();
    public List<GameObject> Thursday = new List<GameObject>();
    private void Awake()
    {
        if(DataSave.Instance._data.isMonDay == true)
        {
            for (int i = 0; i < Monday.Count; i++)
            {
                Monday[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < Monday.Count; i++)
            {
                Monday[i].gameObject.SetActive(false);
            }
        }
        if (DataSave.Instance._data.isTuseDay == true)
        {
            for (int i = 0; i < TuseDay.Count; i++)
            {
                TuseDay[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < TuseDay.Count; i++)
            {
                TuseDay[i].gameObject.SetActive(false);
            }
        }
        if (DataSave.Instance._data.isWendsDay == true)
        {
            for (int i = 0; i < WendsDay.Count; i++)
            {
                WendsDay[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < WendsDay.Count; i++)
            {
                WendsDay[i].gameObject.SetActive(false);
            }
        }
        if (DataSave.Instance._data.isThursday == true)
        {
            for (int i = 0; i < Thursday.Count; i++)
            {
                Thursday[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < Thursday.Count; i++)
            {
                Thursday[i].gameObject.SetActive(false);
            }
        }
        if (DataSave.Instance.isRecive == true)
        {
            itemManager.item.isReceiveItem = true;
            DataSave.Instance.item.isReceiveItem = true;
            DataSave.Instance.isRecive = false;
        }
        if (DataSave.Instance.item.isReceiveItem == false)
        {
            DayOfWeek();
            ChangePanel();
            for (int i = 0; i < dateText.Count; i++)
            {
                dateText[i].text = nowDate.ToString("yyyy") + "³â " + nowDate.ToString("MM") + "¿ù " + nowDate.ToString("dd") + "ÀÏ";
            }
        }
        else if(DataSave.Instance.item.isReceiveItem == true)
        {
            for (int i = 0; i < weekPanelObj.Count; i++)
            {
                weekPanelObj[i].gameObject.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if(DataSave.Instance.item.isReceiveItem ==true)
        {
            for (int i = 0; i < weekPanelObj.Count; i++)
            {
                weekPanelObj[i].gameObject.SetActive(false);
            }
        }
    }
    public void DayOfWeek()
    {
        switch (System.DateTime.Now.DayOfWeek)
        {
            case System.DayOfWeek.Monday:
                currentDay = 0;
                break;
            case System.DayOfWeek.Tuesday:
                currentDay = 1;
                break;
            case System.DayOfWeek.Wednesday:
                currentDay = 2;
                break;
            case System.DayOfWeek.Thursday:
                currentDay = 3;
                break;
            case System.DayOfWeek.Friday:
                currentDay = 4;
                break;
            case System.DayOfWeek.Saturday:
                currentDay = 5;
                break;
            case System.DayOfWeek.Sunday:
                currentDay = 6;
                break;
        }
    }
    public void ChangePanel()
    {
        for(int i =0; i< weekPanelObj.Count; i++)
        {
            weekPanelObj[i].gameObject.SetActive(false); 
        }
        if (currentDay == 6||currentDay == 7)
        {
            weekPanelObj[5].gameObject.SetActive(true);
        }
        else
        {

            weekPanelObj[currentDay].SetActive(true);
        }
    }
    public void ReceiveItem(string itemName)
    {
        switch (itemName)
        {
            case "Sun":
                {
                    DataSave.Instance.item.isReceiveItem = true;
                    itemManager.item.isReceiveItem = true;
                    item.Sun += 1;
                    DataSave.Instance.item = item;
                    //itemManager.ItemDataCopy(item);
                    itemManager.AttendPlantsItem("Sun");
                    img.sprite = boxResult[0];
                    img.SetNativeSize();
                    DataSave.Instance.item.isReceiveItem = true;
                    itemManager.item.isReceiveItem = true;
                    DataSave.Instance.isRecive = true;
                    Debug.Log("Sun");
                    break;
                }
            case "Nutrients":
                {
                    DataSave.Instance.item.isReceiveItem = true;
                    itemManager.item.isReceiveItem = true;
                    item._Gnutrients++;
                    DataSave.Instance.item = item;
                    // itemManager.ItemDataCopy(item);
                    itemManager.AttendPlantsItem("_Gnutrients");
                    img.sprite = boxResult[1];
                    img.SetNativeSize();
                    DataSave.Instance.item.isReceiveItem = true;
                    itemManager.item.isReceiveItem = true;
                    Debug.Log("Nutrients");
                    DataSave.Instance.isRecive = true;
                    break;
                }
            case "Water":
                {
                    DataSave.Instance.item.isReceiveItem = true;
                    itemManager.item.isReceiveItem = true;
                    item.water++;
                    DataSave.Instance.item = item;
                    //itemManager.ItemDataCopy(item);
                    itemManager.AttendPlantsItem("water");
                    img.sprite = boxResult[2];
                    img.SetNativeSize();
                    DataSave.Instance.item.isReceiveItem = true;
                    itemManager.item.isReceiveItem = true;
                    Debug.Log("Water");
                    DataSave.Instance.isRecive = true;
                    break;
                }
            case "Seed":
                DataSave.Instance.item.isReceiveItem = true;
                itemManager.item.isReceiveItem = true;
                // itemManager.ItemDataCopy(item);
                itemManager.AttendPlantsItem("seed");
                img.sprite = boxResult[3];
                img.SetNativeSize();
                Debug.Log("Seed");
                DataSave.Instance.item.isReceiveItem = true;
                itemManager.item.isReceiveItem = true;
                weekSeed.GetPlants();
                DataSave.Instance.isRecive = true;
                break;
            case "None":
                {
                    DataSave.Instance.item.isReceiveItem = true;
                    itemManager.item.isReceiveItem = true;

                    DataSave.Instance.isRecive = true;
                    itemManager.AttendPlantsItem("None");
                    break;
                }
        }
    }
    public int RamdomInt;
    public List<string> RandomList1 = new List<string>();
    public List<string> RamdomList2 = new List<string>();
    public List<string> RamdomList3 = new List<string>();
    public List<Sprite> boxResult = new List<Sprite>();
    public Image img;
    public WeekSeed weekSeed;
    public void RandomBox()
    {

        itemManager.item.isReceiveItem = true;
        DataSave.Instance.item.isReceiveItem = true;
        if (DataSave.Instance._data.plantsData.Count<3)
        {
            RamdomInt = UnityEngine.Random.Range(0, RandomList1.Count);
            ReceiveItem(RandomList1[RamdomInt]);
            DataSave.Instance.item.isReceiveItem = true;
            itemManager.item.isReceiveItem = true;
        }
        else if(DataSave.Instance._data.plantsData.Count >= 3)
        {
            RamdomInt = UnityEngine.Random.Range(0, RamdomList2.Count);
            ReceiveItem(RamdomList2[RamdomInt]);
            DataSave.Instance.item.isReceiveItem = true;
            itemManager.item.isReceiveItem = true;
        }
        else if(DataSave.Instance._data.plantsData.Count>4)
        {
            RamdomInt = UnityEngine.Random.Range(0, RamdomList3.Count);
            ReceiveItem(RamdomList3[RamdomInt]);
            DataSave.Instance.item.isReceiveItem = true;
            itemManager.item.isReceiveItem = true;
        }
    }
    public void GettingSeedIt(GameObject go)
    {
        if(DataSave.Instance.item.AllSeed == true)
        {
            go.SetActive(true);
        }
    }
    public void visangReceiveItem(string itemName)
    {
        switch (itemName)
        {
            case "Sun":
                {
                    item.Sun += 1;
                    DataSave.Instance.item = item;
                    //itemManager.ItemDataCopy(item);
                    itemManager.GetItem();
                    Debug.Log("Sun");
                    break;
                }
            case "RNutrients":
                {
                    item._Rnutrients++;
                    DataSave.Instance.item = item;
                    // itemManager.ItemDataCopy(item);
                    itemManager.GetItem();
                    Debug.Log("_Rnutrients ");
                    break;
                }
            case "BNutrients":
                {
                    item._Bnutrients++;
                    DataSave.Instance.item = item;
                    // itemManager.ItemDataCopy(item);
                    itemManager.GetItem();
                    Debug.Log("Nutrients");
                    break;
                }
            case "Water":
                {
                    item.water++;
                    DataSave.Instance.item = item;
                    //itemManager.ItemDataCopy(item);
                    itemManager.GetItem();
                    Debug.Log("Water");
                    break;
                }
        }
    }
}

