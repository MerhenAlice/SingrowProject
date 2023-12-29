using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;
using static Unity.VisualScripting.Member;
using Amazon.Lambda.Model.Internal.MarshallTransformations;

public class PlantsSpawn : MonoBehaviour
{
    //�Ĺ� �̹��� ����Ʈ
    [SerializeField]
    List<Sprite> plantsImg = new List<Sprite>();
    //�Ĺ� ���� �̹��� ����Ʈ
    [SerializeField]
    List<Sprite> deadImage = new List<Sprite>();
    [SerializeField]
    List<Sprite> SeedImg = new List<Sprite>();

    //�Ĺ� �̹���
    public Image img = null;
    //ȭ�� �̹���
    public Image pots = null;
    //�Ĺ����� �޾� ���� ����
    public PlantsData plantsData;
    #region Exp
    //�ִ� ����ġ
    public int MaxExp;
    //���� ����ġ
    public int CurrentExp;
    //�ܰ躰 ����ġ
    public int StairExp;
    //�Ĺ� ���� �ܰ�
    public int stair;
    public Slider Expbar;
    public Text expText;
    #endregion
    #region Weed
    //���� �̹��� ������
    public GameObject _weedPrefabs;
    private int Max_Weed = 4;
    //���� �˾�
    public GameObject popUP;
    #endregion
    #region Button 
    //���ʻ̱� �Ϸ� ��ư
    public Button weedClear;
    //�Ĺ� Ű��� ��ư ����Ʈ 
    public List<Button> GrowButton = new List<Button>();
    #endregion
    public EventSystem UIClickEventSystem;
    #region Manager
    //�Ĺ� ����ġ ���� ��ũ��Ʈ
    public PlantManager plantManager;
    //�Ĺ� ���� ���� �ൿ ��ũ��Ʈ
    public DoingPlantsGrowing plantsGrowing;
    #endregion
    //�Ĺ� ���
    public List<string> plantsTalks = new List<string>();
    public Text talkText;
    public Button Talk;
    public GameObject effect;

    //ȭ�� ����
    public List<Sprite> potsSprite = new List<Sprite>();
    public Image potss;
    //����� �Ĺ� ������
    public Data _data;
  
    public bool isLevelUP;
    private void Awake()
    {
        //�Ĺ����� �ҷ�����
        lambdaPublic.Invoke("GetPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        //Debug.Log("1.0.0 = " + JsonUtility.ToJson(DataSave.Instance._data).ToString());
        //���� ����Ʈ ���� �޾ƿ���
        lambdaPublic.Invoke("AttendDailytQuest2", JsonUtility.ToJson(DataSave.Instance.uIDData), "todayQuest");
        //����Ʈ ���� �޾ƿ���
        lambdaPublic.Invoke("GetDailyQuestInfo2", JsonUtility.ToJson(DataSave.Instance.uIDData), "todayQuest");
        _data = DataSave.Instance._data;
        StairExp = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsStairExp;
        CurrentExp = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp;
        for (int i = 0; i < plantsImg.Count; i++)
        {
            if (DataSave.Instance.plantPickInGauard == plantsImg[i].name)
            {
                img.sprite = plantsImg[i];
                img.name = plantsImg[i].name;
            }
        }
        //ONAWAKE();
        Debug.Log($"0.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
    }
    public void OnClickTalk()
    {
        //DataSave.Instance._data.isPlantsTalk = true;
    }
    public GameObject Popups;
    IEnumerator popUpon()
    {
        Popups.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        Popups.SetActive(false);

    }
    //�� ��ũ��Ʈ�� ������ �Ĺ� �̹��� ���� �� ����ġ ����
    private void OnEnable()
    {
        OnCheckDead();
        Talk.gameObject.SetActive(true);
        talkText.text = plantsTalks[UnityEngine.Random.Range(0, plantsTalks.Count)];
        stair = int.Parse(DataSave.Instance.StairTemp);
        Debug.Log($"1.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
        DateTime dateTime = DateTime.Parse(DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate);
        if (DataSave.Instance._data.Uuid != string.Empty)
        {
            for (int i = 0; i < plantsImg.Count; i++)
            {
                if (DataSave.Instance.plantPickInGauard == plantsImg[i].name)
                {
                    if((DateTime.Now - dateTime).Days >=2 && (DateTime.Now - dateTime).Days < 3)
                    {

                        img.sprite = deadImage[i];
                        img.name = deadImage[i].name;
                        StartCoroutine(popUpon());
                        img.SetNativeSize();
                        Debug.Log($"1.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
                    }
                    else
                    {
                        img.sprite = plantsImg[i];
                        img.name = plantsImg[i].name;
                        img.SetNativeSize();
                        Debug.Log($"1.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");

                    }
                }
            }
            for (int i = 0; DataSave.Instance._data.plantsData.Count > 0; i++)
            {
                if (DataSave.Instance.plantPickInGauard == DataSave.Instance._data.plantsData[i].plantsname)
                {
                    CurrentExp = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp;

                    plantsData.plantsIndex = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsIndex;
                    ExpSetting(DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsname.Replace(stair.ToString(), ""));
                    SeedSeletPage._seedName = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsname.Replace(stair.ToString(), "");

                    potss.sprite = potsSprite[DataSave.potindex];
                    Debug.Log($"3.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
                }

            }

        }
    }
    public FadeInOut fadeIn;
    public bool isFadeIn = false;
    public GameObject go;
    public GameObject gogo;
    public AudioSource audioSource;
    public float delaytime;
    public GameObject panels1;
    public GameObject panels2;
    public GameObject panels3;
    public GameObject Exit;
    public GameObject consensus;
    //�̹��� ��ġ �� ������, ���� ���ε� �۾��� Update
    private void Update()
    {
        if(DataSave.Instance.StairTemp == "4")
        {
            if (DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsStairExp <= CurrentExp)
            {
                CurrentExp = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsStairExp;
                DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp = CurrentExp;
            }
        }
        DateTime dateTime = DateTime.Parse(DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate);
        if (int.Parse(DataSave.Instance.StairTemp) >=3)
        {
            if ((DateTime.Now - dateTime).Days >= 2 && (DateTime.Now - dateTime).Days < 3)
            {
                giveText.SetActive(true);

            }
            else
            {
                giveText.SetActive(false);
            }
        }
        else
        {
            giveText.SetActive(true);
        }
        if (panels1.activeInHierarchy == true || panels2.activeInHierarchy == true || panels3.activeInHierarchy == true)
        {
            Exit.SetActive(false);
        }
        else
        {
            Exit.SetActive(true);
        }
        for (int i = 0; i < plantsImg.Count; i++)
        {
            if (DataSave.Instance.plantPickInGauard == plantsImg[i].name)
            {
                if (consensus.activeInHierarchy != true)
                {
                    if ((DateTime.Now - dateTime).Days >= 2 && (DateTime.Now - dateTime).Days < 3)
                    {

                        img.sprite = deadImage[i];
                        img.name = deadImage[i].name;
                        Debug.Log($"1.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
                    }
                    else
                    {
                        img.sprite = plantsImg[i];
                        img.name = plantsImg[i].name;
                        Debug.Log($"1.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");

                    }
                }
            }
        }
        img.SetNativeSize();
            Debug.Log($"DataSave.Instance.plantsImgName={DataSave.Instance.plantsImgName},seedname = {SeedSeletPage._seedName}");
        potss.sprite = potsSprite[DataSave.potindex];
        if (DataSave.potindex != null)
        { DataSave.Instance._data.plantsData[DataSave.Instance.index].potsIndex = DataSave.potindex; }
        stair = int.Parse(DataSave.Instance.StairTemp);
        Debug.Log($"4.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
        if (CurrentExp == StairExp || CurrentExp > StairExp)
        {
            if (stair < 4)
            {
                Debug.Log($"5.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
                stair += 1;
                Debug.Log($"6.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
                for (int i = 0; i < plantsImg.Count; i++)
                {
                    if (DataSave.Instance.plantPickInGauard != null)
                    {
                        if (plantsImg[i].name == DataSave.Instance.plantPickInGauard.Replace(DataSave.Instance.StairTemp, "") + stair.ToString())
                        {
                            img.sprite = plantsImg[i];
                            img.name = plantsImg[i].name;
                            gogo = Instantiate(go, img.gameObject.transform);
                            audioSource.Play();
                            StartCoroutine(EffectOn(delaytime));
                            DataSave.Instance.plantPickInGauard = img.name;
                            ExpSetting(DataSave.Instance.plantPickInGauard.Replace(stair.ToString(), ""));
                            for (int ji = 0; ji < 9; ji++)
                            {
                                string temp = NameChager(img.name);
                                
                            }

                          

                            LevelUP(); 
                        }
                    }
                    else
                    {
                        if (plantsImg[i].name == DataSave.Instance.plantsImgName)
                        {
                            //img.sprite = plantsImg[i];
                            //img.name = plantsImg[i].name;
                            plantsData.plantsname = img.name;
                            plantsData.plantsStairExp = StairExp;
                            DataSave.Instance.plantsImgName = img.name;
                            for (int j = 0; j < DataSave.Instance._data.plantsData.Count; j++)
                            {
                                if (DataSave.Instance._data.plantsData[j].plantsname == DataSave.Instance.plantsImgName)
                                {
                                    ExpSetting(DataSave.Instance.plantsImgName.Replace(DataSave.Instance._data.plantsData[j].plantsIndex.ToString(), ""));

                                }
                            }
                        }
                    }

                }
                CurrentExp = 0;
                plantsData.isSell = false;
                plantsData.plantsExp = CurrentExp;
                plantsData.plantsStairExp = StairExp;
                plantsData.plantsClass = stair.ToString();
                plantsData.plantsname = img.name;
                plantsData.lastExpDate = DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate;
                plantsData.leapcount = ConsensusUIEvent.leapCount;
                plantsData.isKing = DataSave.Instance._data.plantsData[DataSave.Instance.index].isKing;

                plantsData.plantsIdentification = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsIdentification;
                DataSave.Instance._data.plantsData[DataSave.Instance.index] = plantsData;
                lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");

                DataSave.Instance.StairTemp = stair.ToString();
            }
        }
        Debug.Log($"7.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");

        if (DataSave.Instance.StairTemp == "4")
        {

                Expbar.maxValue = 1;
                Expbar.value = 1;
                expText.text = "MAX";
        }
        else
        {
            Expbar.maxValue = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsStairExp; 
            Expbar.value = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp;
            expText.text = CurrentExp.ToString() + "/" + StairExp.ToString();

        }
        img.SetNativeSize();
        plantsData.isSell = false;
        plantsData.plantsExp = CurrentExp;
        plantsData.plantsStairExp = StairExp;
        plantsData.plantsClass = stair.ToString();
        plantsData.plantsname = img.name;
        plantsData.lastExpDate = DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate;
        plantsData.leapcount = ConsensusUIEvent.leapCount;
        plantsData.plantsIdentification = DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsIdentification;
        DataSave.Instance._data.plantsData[DataSave.Instance.index] = plantsData;

        Debug.Log($"8.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
        if (DataSave.Instance.StairTemp == "2")
        {
            stairsetting.gameObject.SetActive(true);
            stairsetting.sprite = stairs[0];
        }
        else if (DataSave.Instance.StairTemp == "3")
        {
            stairsetting.gameObject.SetActive(true);
            stairsetting.sprite = stairs[1];
        }
        else if (DataSave.Instance.StairTemp == "4")
        {
            stairsetting.gameObject.SetActive(true);
            stairsetting.sprite = stairs[2];
        }
        else if (DataSave.Instance.StairTemp == "0")
        {
            stairsetting.gameObject.SetActive(true);
            stairsetting.sprite = stairs[0];
        }
        else if (DataSave.Instance.StairTemp == "1")
        {
            stairsetting.gameObject.SetActive(true);
            stairsetting.sprite = stairs[0];
        }
        else
        {
            stairsetting.gameObject.SetActive(false);
        }
        //
        for (int i = 0; i < weed.Count; i++)
        {
            if (weed[i] == null)
            {
                weed.RemoveAt(i);
            }
        }
        DataSave.Instance.MaxExp = MaxExp;
        DataSave.Instance.currentExp = CurrentExp;
        DataSave.Instance.plantsImgName = img.name;
        Debug.Log($"9.img = {img.name}, DataSave.Instance.plantPickInGauard = {DataSave.Instance.plantPickInGauard}");
        ButtonSetting();
        for (int i = 0; i < DataSave.Instance._data.plantsData.Count; i++)
        {
            if (DataSave.Instance._data.plantsData[i].plantsname == img.name)
            {
                if (int.Parse(DataSave.Instance._data.plantsData[i].plantsClass) >= 3)
                {
                    if ((DateTime.Now - dateTime).Days >= 2 && (DateTime.Now - dateTime).Days < 3)
                    {

                        giveButton.image.sprite = bts[1];
                        giveButton.enabled = false;

                    }
                    else
                    {
                        DataSave.Instance.plantsImgName = img.name;
                        DataSave.Instance.curentstaticExp = CurrentExp;
                        giveButton.image.sprite = bts[0];
                        giveButton.enabled = true;
                    }
                }
                else
                {
                    giveButton.image.sprite = bts[1];
                    giveButton.enabled = false;
                }
            }
        }
        if(weed.Count >0)
        {
            weedClear.enabled = false;
        }
        else if( weed.Count == 0)
        {
            weedClear.enabled = true;
        }
    }
    public GameObject giveText;
    //������ �� �̹����̸� ��ü
    public void LevelUP()
    {
        string temp = NameChager(img.name);
    }
    //����Ʈ �ѱ�
    IEnumerator EffectOn(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        Destroy(gogo);
    }
    public List<Sprite> stairs = new List<Sprite>();
    public Image stairsetting;
    public GameObject waterFall;
    public GameObject Nutrients;
    public static bool isActiveInHaerarchy = false;
    public ItemManager itemManager;
    public LambdaPublic lambdaPublic;
    public void ButtonSetting()
    {
    }
    public List<GameObject> infos = new List<GameObject>();
    public GameObject popUPs;
    public OnInfoUIOpen uIOpen;
    //�Ĺ� ���� �ൿ ��ư Ŭ�� �� �ش� string���� �޾ƿͼ� ó��
    public void GrowPlants()
    {
        if (UIClickEventSystem.currentSelectedGameObject.name == "Water")
        {
            if (DataSave.Instance.item.water > 0)
            {
                GrowButton[0].enabled = true;
                if (isActiveInHaerarchy == false)
                {
                    DataSave.Instance._data.iswatering = true;
                    isActiveInHaerarchy = true;
                    plantsGrowing.WaterFall(CurrentExp);
                    //uIOpen.OnClickUI(infos[0]);
                    lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
                }
            }
            else
            {
                infos[0].SetActive(false);
                popUPs.SetActive(true);
            }
        }
        else if (UIClickEventSystem.currentSelectedGameObject.name == "Sun")
        {
            if (DataSave.Instance.item.Sun > 0)
            {
                DataSave.Instance._data.isSun = true;
                GrowButton[3].enabled = true;
                plantsGrowing.SunSetAdd(CurrentExp);
                lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
                for (int i = 0; i < GrowButton.Count; i++)
                {
                    GrowButton[i].gameObject.SetActive(false);
                }
            }
            else
            {
                infos[1].SetActive(false);
                popUPs.SetActive(true);
            }
        }
        else if (UIClickEventSystem.currentSelectedGameObject.name == "Nutrients")
        {
            if (DataSave.Instance.item._Gnutrients > 0)
            {
                if (isActiveInHaerarchy == false)
                {
                    DataSave.Instance._data.isnutrients = true;
                    isActiveInHaerarchy = true;
                    plantsGrowing.SetNutrients("Nutrients");
                    lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");

                }
            }
            else
            {
                popUPs.SetActive(true);
            }
        }
        else if (UIClickEventSystem.currentSelectedGameObject.name == "RNutrients")
        {
            if (DataSave.Instance.item._Rnutrients > 0)
            {
                if (isActiveInHaerarchy == false)
                {
                    DataSave.Instance._data.isnutrients = true;
                    isActiveInHaerarchy = true;
                    plantsGrowing.SetNutrients("RNutrients");
                    lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");

                }
            }
            else
            {
                popUPs.SetActive(true);
            }
        }
        else if (UIClickEventSystem.currentSelectedGameObject.name == "YNutrients")
        {
            if (DataSave.Instance.item._Ynutrients > 0)
            {
                if (isActiveInHaerarchy == false)
                {
                    DataSave.Instance._data.isnutrients = true;
                    isActiveInHaerarchy = true;
                    plantsGrowing.SetNutrients("YNutrients");
                    lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");

                }
            }
            else
            {
                popUPs.SetActive(true);
            }
        }
        else if (UIClickEventSystem.currentSelectedGameObject.name == "BNutrients")
        {
            if (DataSave.Instance.item._Bnutrients > 0)
            {
                if (isActiveInHaerarchy == false)
                {
                    DataSave.Instance._data.isnutrients = true;
                    isActiveInHaerarchy = true;
                    plantsGrowing.SetNutrients("BNutrients");
                    lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");

                }
            }
            else
            {
                popUPs.SetActive(true);
            }
        }
        else if (UIClickEventSystem.currentSelectedGameObject.name == "Weed")
        {
            WeedClear();
            for (int i = 0; i < GrowButton.Count; i++)
            {
                GrowButton[i].gameObject.SetActive(false);
            }
            weedClear.gameObject.SetActive(true);
        }
    }
    public GameObject lolol;
    //������ ������ ����Ǵ� �Լ�
    public void ExitSave2()
    {
        StartCoroutine(Delays2());
    }
    IEnumerator Delays2()
    {
        yield return new WaitForSeconds(2.0f);
        lolol.SetActive(true);
    }
    public QuestManager questManager;
    //������ ���� �Լ�
    public void SaveData()
    {
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");

        if (questManager.todayQuest.firstQuest != string.Empty)
        {
            lambdaPublic.Invoke("PatchDailyQuestInfo2", JsonUtility.ToJson(questManager.todayQuest), "todayQuest");
        }
    }
    //����ġ ���� �Լ�
    void ExpSetting(string name)
    {
        if (stair == 0)
        {
            StairExp = plantManager.ReturnSeedEXPName(name);
        }
        else if (stair == 1)
        {
            StairExp = plantManager.ReturnPlantsSecondExp(name);
        }
        else if (stair == 2)
        {
            StairExp = plantManager.ReturnPlantsThirdExp(name);
        }
        else if (stair == 3)
        {
            StairExp = plantManager.ReturnPlantsFourthExp(name);
        }
        else if (stair == 4)
        {
            StairExp = plantManager.ReturnPlantsFifthExp(name);
        }
    }
    #region WeedClear
    private void WeedClear()
    {
        StartCoroutine(RandomRespawn_Coroutine());
    }
    Vector2 Return_RandomPosition()
    {
        Vector2 originalPosition = pots.gameObject.transform.position;

        float range_X = pots.gameObject.GetComponent<EdgeCollider2D>().bounds.size.x;
        float range_Y = pots.gameObject.GetComponent<EdgeCollider2D>().bounds.size.y;

        range_X = UnityEngine.Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = UnityEngine.Random.Range((range_Y / 2) * -1, range_Y / 4);
        Vector2 RandomPosition = new Vector2(range_X, range_Y);

        Vector2 resPosition = originalPosition + RandomPosition;
        return resPosition;
    }
    public List<GameObject> weed = new List<GameObject>();
    IEnumerator RandomRespawn_Coroutine()
    {
        for (int i = 0; i < Max_Weed; i++)
        {
            GameObject instantWeed = Instantiate(_weedPrefabs,
                Return_RandomPosition(), Quaternion.identity, pots.transform);
            weed.Add(instantWeed);
            yield return new WaitForSeconds(0.5f);

        }
    }
    #endregion
    public GameObject TopUI;
    public GameObject wslider;
    //���� �̱� ���� �Լ�
    public void WeedClearClick()
    {
        if (weed.Count == 0)
        {
            source.Play();
            StartCoroutine(popUPOn());
            TopUI.SetActive(true);
            wslider.SetActive(true);
            DataSave.Instance._data.isweeding = true;
            for (int i = 0; i < GrowButton.Count; i++)
            {
                GrowButton[i].gameObject.SetActive(true);
            }
            if (DataSave.Instance._data.isweeding == false)
            {
                DataSave.Instance._data.isweeding = true;
                //DataSave.Instance.Save();
            }
            weedClear.gameObject.SetActive(false);
            lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        }
    }
    public AudioSource source;
    //�˾� ���� �Լ�
    IEnumerator popUPOn()
    {
        popUP.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        popUP.SetActive(false);
        CurrentExp += 3;

    }
    //���ռ� �Ϸ� ��ư Ŭ�� �Լ�
    public void SunSetClearClick()
    {
        if (weed.Count == 0)
        {
            CurrentExp += plantsGrowing.RetrunweedExp();
            for (int i = 0; i < GrowButton.Count; i++)
            {
                GrowButton[i].gameObject.SetActive(true);
            }
            plantsGrowing.SunSetClear();
            if (DataSave.Instance._data.isSun == false)
            {
                DataSave.Instance._data.isSun = true;
            }
        }
    }
    public Button giveButton;
    public List<Sprite> bts = new List<Sprite>();
    //����ϱ� ��ư Ŭ�� �Լ�
    public void OnClickGive()
    {
        for (int i = 0; i < DataSave.Instance._data.plantsData.Count; i++)
        {
            if (DataSave.Instance._data.plantsData[i].plantsname == img.name)
            {
                if (int.Parse(DataSave.Instance._data.plantsData[i].plantsClass) >= 3)
                {
                    DataSave.Instance.plantsImgName = img.name;
                    DataSave.Instance.curentstaticExp = CurrentExp;
                    LoadingSceneController.LoadScean("06_GivePlants");
                    giveButton.image.sprite = bts[0];
                }
                else
                {
                    giveButton.image.sprite = bts[1];
                }
            }
        }
    }

    public void OnCheckDead()
    {
        for (int i = 0; i < DataSave.Instance._data.plantsData.Count; i++)
        {
            DateTime lastExp = Convert.ToDateTime(DataSave.Instance._data.plantsData[i].lastExpDate);
            if ((DateTime.Now - lastExp).Hours <= 12)
            {

            }
        }
    }
    //���� �Ĺ��̸� �ڵ� ��ȯ �Լ�
    private string NameChager(string name)
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
    public void ONAWAKE()
    {
        string name = NameChager(DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsname);

    }
    public void LoadData()
    {

        lambdaPublic.Invoke("GetPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
    }
    //���� ��Ȱ��ȭ�� ���� �Լ�
    private void OnDestroy()
    {
#if UNITY_EDITOR
        Debug.Log("OnApplicationPause");
#endif
        SaveData();
        LevelUP(); 
    }
}
