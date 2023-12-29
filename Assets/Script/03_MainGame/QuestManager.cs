using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TodayQuest
{
   public string firstQuest;
   public string secondQuest;
}
public class QuestManager : MonoBehaviour
{
    #region SingleTone
    private static QuestManager instance;
    private void Awake()
    {
       // DontDestroyOnLoad(this.gameObject);
        uIDData = DataSave.Instance.uIDData;
        questData = DataSave.Instance.questData;

        int size = 5;
        tempList = new List<string>(new string[size]);
    }
    public static QuestManager Instance
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
    #endregion

    public List<Sprite> nonClear = new List<Sprite>();
    public List<Sprite> Clear= new List<Sprite>();
    public List<Sprite> disable = new List<Sprite>();

    public List<Sprite> visangQuestDefaultSprite= new List<Sprite>();
    public List<Sprite> normalQuestDefaultSprite = new List<Sprite>();

    public List<Sprite> visangQuestClearSprite = new List<Sprite>();
    public List<Sprite> normalQuestClearSprite = new List<Sprite>(); 

    public List<Image> questImages = new List<Image>();

    public List<Sprite> leavesImg = new List<Sprite>();
    public Image leaves;

    Data _data;
    public QuestDatas questData;
    public List<string> SoyesQuestNames = new List<string>();
    public List<string> VisangQuestNames = new List<string>();
    public List<int> giveExp = new List<int>();
    public TodayQuest todayQuest;
    public List<string> tempList = new List<string>();
    public LambdaPublic LambdaPublic;
    public UIDData uIDData;
    private void Start()
    {
        if (todayQuest.firstQuest != string.Empty)
        {
            //test();
        }
        else
        {
            //QuestSetting(); 
            TodayQuestSetting();
        }
        this.gameObject.SetActive(false);
    }
    public void OnEnable()
    {
        if (todayQuest.firstQuest != string.Empty)
        {
            todayQuest = LambdaPublic.today;
        }
        else
        {
            //QuestSetting(); 
            TodayQuestSetting();
        }

    }
    private void OnDisable()
    {
        if (todayQuest.firstQuest != string.Empty)
        {
            
        }
    }
    IEnumerator delay()
    {

        yield return new WaitForSecondsRealtime(3.0f);
    }
    public Text text;
    private void Update()
    {
        TodayQuestSetting();
        leaves.sprite = leavesImg[ConsensusUIEvent.leapCount];
        questData.isNutrients = DataSave.Instance._data.isnutrients;
        todayQuest = LambdaPublic.today;
        uIDData.uid = DataSave.Instance._data.uid;
        uIDData.Uuid = DataSave.Instance._data.Uuid;
        if (tempList[0] != string.Empty)
        { 
            tempList[0] = LambdaPublic.today.firstQuest; 
        }
        if (tempList[1] != string.Empty)
        { 
            tempList[1] = LambdaPublic.today.secondQuest; 
        }
        text.text = "총 " + DataSave.Instance._data.plantsData[DataSave.Instance.index].leapcount.ToString() + "/7회";
        tempList[2] = "공부했어요";
        tempList[3] = "오늘의 학습";
        tempList[4] = "참 잘했어요";
        TodayQuestSetting();

    }
    //public void QuestSetting()
    //{
    //    for(int i =0; i<2; i++) 
    //    { 
    //    int index = UnityEngine.Random.Range(0, SoyesQuestNames.Count);
    //    todayQuest.Add(SoyesQuestNames[index]);
    //    SoyesQuestNames.RemoveAt(index);
    //    }
    //    for(int i =0; i<VisangQuestNames.Count; i++)
    //    {
    //        todayQuest.Add(VisangQuestNames[i]);
    //    }
    //    DataSave.Instance._data.todayQuest= todayQuest;
    //    DataSave.Instance.Save();
    //}
    public async void test()
    {
        await Task.Delay(1000);
    }
    public void TodayQuestSetting()
    {
        for (int i = 0; i < questImages.Count; i++)
        {
            for (int j = 0; j < normalQuestDefaultSprite.Count; j++)
            {
                if (normalQuestDefaultSprite[j].name == QuestNameKorToEN(tempList[i]))
                {
                    questImages[i].sprite = normalQuestDefaultSprite[j];
                }
                else if (normalQuestClearSprite[j].name == QuestNameKorToEN(tempList[i]))
                {
                    questImages[i].sprite = normalQuestClearSprite[j];
                }
            }
            for (int j = 0; j < visangQuestDefaultSprite.Count; j++)
            {
                if (visangQuestDefaultSprite[j].name == QuestNameKorToEN(tempList[i]))
                {
                    questImages[i].sprite = visangQuestDefaultSprite[j];
                }
                else if (visangQuestClearSprite[j].name == QuestNameKorToEN(tempList[i]))
                {
                    questImages[i].sprite = visangQuestClearSprite[j];
                }
            }
        }
    }
    public string QuestNameKorToEN(string name)
    {

        switch (name)
        {
            case "Journaling":
                {
                    if(DataSave.Instance._data.iswriting == true)
                    {
                        return "JournalingClear";
                        
                    }
                    else
                    {
                        return "Journaling";
                    }
                }
            case "Nutrients":
                {
                    if (DataSave.Instance._data.isnutrients == true)
                    {
                        return "NutrientsClear";
                    }
                    else
                    {
                        return "Nutrients";
                    }
                }
            case "Photosynthesis":
                {
                    if (DataSave.Instance._data.isSun == true)
                    {
                        
                        return "PhotosynthesisClear";
                    }
                    else
                    {
                        return "Photosynthesis";
                    }
                }
            case "Watering":
                {
                    if (DataSave.Instance._data.iswatering == true)
                    {
                        return "WateringClear";
                    }
                    else
                    {
                        return "Watering";
                    }
                }
            case "Weeding":
                {
                    if (DataSave.Instance._data.isweeding == true)
                    {
                      
                        return "WeedingClear";
                    }
                    else
                    {
                        return "Weeding";
                    }
                }
            case "공부했어요":
                {
                    if (DataSave.Instance._data.isStudy == true)
                    {
                       return "IStudiedClear";
                    }
                    else
                    {
                        return "IStudied";
                    }
                }
            case "오늘의 학습":
                {
                    if (DataSave.Instance._data.istodaystudy == true)
                    {
                        
                        return "TodayStudyClear";
                    }
                    else
                    {
                        return "TodayStudy";
                    }
                }
            case "참 잘했어요":
                {
                    if (DataSave.Instance._data.isVeryGood == true)
                    {
                        return "VeryGoodClear";
                    }
                    else
                    {
                        return "VeryGood";
                    }
                }

        }
        return null;
    }
}
