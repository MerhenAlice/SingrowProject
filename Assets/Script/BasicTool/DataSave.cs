using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class PlantsData
{
    //�Ĺ� �⺻ ����
    public string plantsIdentification;
    public string plantsname;
    public string plantsClass;
    public int plantsExp;
    public int plantsStairExp;
    public string pots;
    public int plantsIndex;

    public bool isSell;
    public string lastExpDate;
    public bool isDead;

    //�����ϱ� ������
    public int leapcount;
    public bool isMeditate;
    public bool isComplimet;
    public bool isEmotion;
    public bool isTalk;
    public bool isCatchClover;
    public bool isGettoKnow;

    public int potsIndex;

    public bool isKing;
}
[Serializable]
public class Data
{
    //UID
    public int uid;
    public string Uuid;
    public string ID;
    //PlantsData
    public List<PlantsData> plantsData;
    //WeekOnCheck
    public List<bool> _loginData = new List<bool>();
    //GrowQuest
    public bool iswatering;
    public bool isweeding;
    public bool iswriting;
    public bool isSun;
    public bool isnutrients;
    public int BGIndex;
    public bool isMonDay;
    public bool isTuseDay;
    public bool isWendsDay;
    public bool isThursday;

    //public bool isnot;
    //VisangQuest
    public bool isStudy;
    public bool istodaystudy;
    public bool isVeryGood;
    public bool isPlantsTalk;
    public int GiveData;
    //Consensus Quest
    //frendship
}
//ItemData
[Serializable]
public class Item
{
    //��� ������ ����
    public int Sun;
    public int _Gnutrients;
    public int _Bnutrients;
    public int _Rnutrients;
    public int _Ynutrients;
    public int water;
    //���� �޾Ҵ��� Ȯ��
    public bool isSeed;
    //5�� �����⼮
    public bool AllSeed;
    //ȭ�и��
    public List<bool>pots = new List<bool>();
    //�ְ� �⼮ ������ ���� ����
    public bool isReceiveItem;
    public string Uuid;
    public int uid;

    //��� ����Ʈ ����
    public bool isStudy;
    public bool istodaystudy;
    public bool isVeryGood;
}
//��󿡼� ���޹޴� UID ������
[Serializable]
public class UIDData
{
    public int uid;
    public string Uuid;
}
//����Ʈ ����
[Serializable]
public class QuestDatas
{
    //�̸��� ���� ��쿡�� ����Ʈ �̸�, bool���� ����Ʈ �Ϸ� ����
    public string Journaling;
    public bool isJournaling;
    public string Nutrients;
    public bool isNutrients;
    public string Photosynthesis;
    public bool isPhotosynthesis;
    public string Watering;
    public bool isWaterin;
    public string Weeding;
    public bool isWeeding;
    //ù��° ����Ʈ
    public string firstQuest;
    //�ι�° ����Ʈ
    public string secondQuest;
}
public class DataSave : MonoBehaviour
{
    public Data _data;
    public Item item;
    public UIDData uIDData;
    private static DataSave instance = null;
    public QuestDatas questData;
    public string StairTemp = string.Empty;
    public int index = 0;
    public List<SpriteRenderer> plants = new List<SpriteRenderer>();
    public List<DiaryDataRoot> diaryDataRoot = new List<DiaryDataRoot>();
    public System.Guid guid = System.Guid.NewGuid();
    public string guis;
    public float Timer;
    public string deeplinkURL;
    bool validScene;
    public string main_scene_name;

    public bool isStudy;
    public bool istodaystudy;
    public bool isVeryGood;

    public bool isFirst = false;
    public bool isRecive = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(popUp);
        }
        else
        {
            Destroy(this.gameObject);
        }
        guis = guid.ToString();
        _data.ID = LoginManager.Instance.loginData.localUserName;
        _data.Uuid = LoginManager.Instance.loginData.localUserId;
    }
    public static DataSave Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    public  string plantsImgName;
    public  int curentstaticExp;
    public string plantPickInGauard;
    public int MaxExp;
    public int  currentExp;
    public static int potindex;
    public  int kingIndex;
    public  bool isKingBoolean;
    public int bgIndex = 0; 
    public float sec = 0;
    public GameObject popUp;
    public bool isOne=false;
    public LambdaPublic lambdaPublic;
    private void Update()
    {
        Timer = Timer+Time.deltaTime;
        uIDData.uid = _data.uid;
        uIDData.Uuid = _data.Uuid;
        item.Uuid = _data.Uuid;
        item.uid = _data.uid;
        if(isOne == false)
        {
            if(item.uid!=0)
            {

                isStudy = item.isStudy;
                istodaystudy = item.istodaystudy;
                isVeryGood = item.isVeryGood;
                isOne = true;
            }
        }    
        for (int i =0; i<_data.plantsData.Count; i++)
        {
            _data.plantsData[i].plantsIndex = i;
        }
        int count = 0;
        for(int i =0; i < _data.plantsData.Count; i++)
        {
            if (_data.plantsData[i].isKing == true)
            {
                count++;
                isKingBoolean = _data.plantsData[i].isKing;
                kingIndex = i;

            }
        }
        sec += Time.deltaTime;
        if(sec>10)
        {

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                popUp.SetActive(true);
            }
            sec = 0;
        }
        if (_data.plantsData.Count-1 >= kingIndex)
        {
            _data.plantsData[kingIndex].isKing = isKingBoolean;
        }

    }
    public void DataInput(PlantsData plantsData)
    {
        _data.plantsData.Add(plantsData);
    }
    public void DataCopy(PlantsData plantsData, int index)
    {
        _data.plantsData[index] = plantsData;
    }
    public void DeleteData(int index)
    {
        _data.plantsData.RemoveAt(index);
    }
    public void NotConnetUID(GameObject go)
    {
        if(_data.Uuid == string.Empty)
        {
            go.SetActive(true);
        }
    }
    public void DestoyAccount()
    {
        lambdaPublic.Invoke("DeletePlantsUser", JsonUtility.ToJson(uIDData), "DestoyAccount");
        LoadingSceneController.LoadScean("0-1_fuck");
    }
}
