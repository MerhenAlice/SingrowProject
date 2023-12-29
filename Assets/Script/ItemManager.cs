using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemData
{
    public int uid;
    public string Uuid;
    public string type;
}
public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }    
    //아이템 정보 클래스
    public Item item;
    //UID정보
    public UIDData uIDData;
    //아이템 정보 텍스트
    public Text waterText;
    public Text WeedText;
    public Text SunText;
    public Text _GNText;
    public Text _BNText;
    public Text _RNText;
    public Text _YNText;
    public Text _SText;
    public Text _WText;
    //영양제 아이템창 텍스트
    public Text _GNText2;
    public Text _BNText2;
    public Text _RNText2;
    public Text _YNText2;

    public ItemData itemData;
    public List<GameObject> potList = new List<GameObject>();
    private void Awake()
    {
        item = DataSave.Instance.item;
        itemData.uid = DataSave.Instance._data.uid;
        itemData.Uuid = DataSave.Instance._data.Uuid;
        LambdaPublic = GameObject.FindGameObjectWithTag("LambdaPublic").GetComponent<LambdaPublic>();
        uIDData.Uuid = DataSave.Instance._data.Uuid;
    }
    private void Update()
    {
        itemData.uid = DataSave.Instance._data.uid;
        itemData.Uuid = DataSave.Instance._data.Uuid;
        uIDData.Uuid = DataSave.Instance._data.Uuid;
        uIDData.uid = DataSave.Instance._data.uid;
        waterText.text = item.water.ToString()+"개";
        SunText.text = item.Sun.ToString() + "개";
        _GNText.text = item._Gnutrients.ToString()+"개";
        _BNText.text = item._Bnutrients.ToString() + "개";
        _RNText.text = item._Rnutrients.ToString() + "개"; 
        _YNText.text = item._Ynutrients.ToString() + "개";
        _GNText2.text = item._Gnutrients.ToString() + "개";
        _BNText2.text = item._Bnutrients.ToString() + "개";
        _RNText2.text = item._Rnutrients.ToString() + "개";
        _YNText2.text = item._Ynutrients.ToString() + "개";
        _SText.text = item.Sun.ToString() + "개";
        _WText.text = item.water.ToString() + "개";
        for(int i = 0; i<potList.Count; i++)
        {
            if (potList[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name == (DataSave.Instance._data.plantsData[DataSave.Instance.index].potsIndex+1).ToString())
            {
                potList[i].GetComponent<Outline>().enabled = true;
                potList[i].GetComponent<Image>().color = new Color(186f/255f, 255f/255f, 148f / 255f, 255);
            }
            else
            {
                potList[i].GetComponent<Outline>().enabled = false;
                potList[i].GetComponent<Image>().color =Color.white;
            }
        }

        item = DataSave.Instance.item;
    }
    public PlantsSpawn plantsSpawn;
    public void PotsSetting(GameObject go)
    {
        Debug.Log("버튼 눌러짐");
        DataSave.Instance._data.plantsData[DataSave.Instance.index].potsIndex = int.Parse(go.transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name)-1;
        DataSave.potindex = int.Parse(go.transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name) - 1;
        plantsSpawn._data.plantsData[DataSave.Instance.index].potsIndex = int.Parse(go.transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name) - 1;
    }
    public void PatchData()
    {
        LambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSend");
    }
    public void ItemDataCopy(Item Ritem)
    {
        item = Ritem;
    }
    public void ItemDataReceive()
    {

    }
    public LambdaPublic LambdaPublic;
    public void UseItems(string type)
    {
        itemData.type = NameChanger(type);
        LambdaPublic.Invoke("PatchPlantsItem2", JsonUtility.ToJson(itemData), "item");
        StartCoroutine(Delay());

    }
    public void GetItem()
    {

        LambdaPublic.Invoke("GetPlantsItem2", JsonUtility.ToJson(uIDData), "item");
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f); 
        LambdaPublic.Invoke("GetPlantsItem2", JsonUtility.ToJson(uIDData), "item");
    }
    public void AttendPlantsItem(string type)
    {
        itemData.type = type;
        LambdaPublic.Invoke("AttendPlantItem2", JsonUtility.ToJson(itemData), "item");
        StartCoroutine(Delay());
    }
    public string NameChanger(string type)
    {
        switch(type)
        {
            case "Water":
                {
                    return "water";
                }
            case "Sun":
                {
                    return "Sun";
                }
            case "Nutrients":
                {
                    return "_Gnutrients";
                }
            case "RNutrients":
                {
                    return "_Rnutrients";
                }
            case "YNutrients":
                {
                    return "_Ynutrients";
                }
            case "BNutrients":
                {
                    return "_Bnutrients";
                }
        }
        return null;
    }
}