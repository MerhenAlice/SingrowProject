using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using static Diary;
using Unity.VisualScripting;
using System.Buffers.Text;
using System.Text;

[Serializable]
public class DiaryDataRoot
{
    public string date; //��¥
    public string diaryText; //����
    public List<string> plantImgName; //�Ĺ� �̹��� �̸�
    public List<string> plantGrade; //�Ĺ� ���
    public List<string> plantName; //�Ĺ� �̸�
}
[Serializable]
public class MonthData
{
    public int uid;
    public string Uuid;
    public int month;
}
[System.Serializable]
public class SaveDiaryData
{
    public string date; //��¥
    public string diaryText; //����
    public List<string> plantImgName; //�Ĺ� �̹��� �̸�
    public List<string> plantGrade; //�Ĺ� ���
    public List<string> plantName; //�Ĺ� �̸�
}
[Serializable]
public class diary 
{
    public int uid;
    public string Uuid;
    public SaveDiaryData saveDiaryData;
}

[Serializable]
public class DiaryDatas
{
    public int uid;

    public string Uuid;
    public List<DiaryDataRoot> DiaryData;
}
public class Diary : MonoBehaviour
{
    private static Diary instance = null;
    public static Diary  Instance
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
    [Header("[-------Diary-------]")]
    public List<Button> monthlyDiaryBtnList;

    public List<Sprite> inactiveImgList;
    public List<Sprite> activeImgList;

    #region Server Data

    public DiaryDataRoot dataRoot;
    public DiaryDatas diaryDatas;
    public MonthData monthData;
    #endregion

    #region Save Data
    public SaveDiaryData saveDiaryData;
    public diary sOnly;
    #endregion

     [Header("[-------Etc-------]")]
    public GameObject diaryPanel;
    public GameObject savePopup;
    public GameObject nullPopup;

    public Text dateField;
    public InputField inputText;

    [Header("[-------Plant-------]")]
    public Sprite defalutImg;
    public List<GameObject> pShowList;
    public List<Sprite> pImgList;
    public List<Sprite> pGradeImgList;

    [Header("[-------Button-------]")]
    public Button saveBtn;
    public Button prevBtn;
    public Button nextBtn;
    public Button backBtn;
    public Button okBtn;
    public Button ok2Btn;
    public Button monthlyBtn;
    public string diaryTextData;
    public string nowDate;
    public string selectDiaryMonth;
    public int nowMonth;
    public int selectMonth;
    public int listIdx;
    public LambdaPublic lambdaPublic;
    bool isOpen;
    void Awake()
    {
        gameObject.SetActive(true);
        diaryPanel.SetActive(false);

        nullPopup.SetActive(false);
        savePopup.SetActive(false);

        InitAlbum();
        monthData.uid = DataSave.Instance.uIDData.uid;
        diaryDatas.uid = DataSave.Instance.uIDData.uid;
        monthData.Uuid = DataSave.Instance.uIDData.Uuid;
        diaryDatas.Uuid = DataSave.Instance.uIDData.Uuid;
    }

    void Start()
    {
        // Add onclick event
        for (int i = 0; i < monthlyDiaryBtnList.Count; i++)
        {
            monthlyDiaryBtnList[i].onClick.AddListener(() => OnMonthlyDiaryBtnClick());
        }

        saveBtn.onClick.AddListener(() => OnSaveBtnClick());
        prevBtn.onClick.AddListener(() => OnPrevBtnClick());
        nextBtn.onClick.AddListener(() => OnNextBtnClick());
        backBtn.onClick.AddListener(() => OnBackBtnClick());

        okBtn.onClick.AddListener(() => OnOkBtnClick());
        ok2Btn.onClick.AddListener(() => OnOk2BtnClick());

        monthlyBtn.onClick.AddListener(() => OnMonthlyBtnclick());
    }

    void Update()
    {

        Debug.Log($"diaryDatas.DiaryData[listindex].date = {diaryDatas.DiaryData[listIdx].date}, nowDate = {nowDate}");
        if (diaryDatas.DiaryData.Count != 0)
        {
            if (nowMonth != selectMonth)
            {
                if (diaryDatas.DiaryData.Count >= 2)
                {
                    // Set Button Interactable
                    if (listIdx == 0)
                    {//ù��°�϶�
                        prevBtn.interactable = false;
                        nextBtn.interactable = true;
                    }
                    else if (listIdx == diaryDatas.DiaryData.Count - 1)
                    {//�������϶�

                        prevBtn.interactable = true;
                        nextBtn.interactable = false;
                    }
                    else
                    {
                        prevBtn.interactable = true;
                        nextBtn.interactable = true;
                    }
                }
                else
                {
                    prevBtn.interactable = false;
                    nextBtn.interactable = false;
                }
            }
            else //���� ���̶� ���� ���
            {
                //������ ���� ��
                if (diaryDatas.DiaryData[diaryDatas.DiaryData.Count - 1].date == nowDate)
                {
                    if (diaryDatas.DiaryData.Count >= 2)
                    {
                        if (listIdx == diaryDatas.DiaryData.Count - 1)
                        {
                            inputText.enabled = true;
                            saveBtn.gameObject.SetActive(true);
                            SetTodayDiary();
                            prevBtn.interactable = true;
                            nextBtn.interactable = false;
                        }
                        else
                        {
                            saveBtn.gameObject.SetActive(false);
                        }

                        if (listIdx == 0)
                        {
                            Debug.Log("209");
                            prevBtn.interactable = false;
                            nextBtn.interactable = true;
                        }
                        else if (listIdx == diaryDatas.DiaryData.Count - 1)
                        {
                            prevBtn.interactable = true;
                            nextBtn.interactable = false;
                        }
                        //�ε����� 
                        else
                        {
                            prevBtn.interactable = true;
                            nextBtn.interactable = true;
                        }
                    }
                    else
                    {
                        inputText.enabled = true;
                        saveBtn.gameObject.SetActive(true);

                        prevBtn.interactable = false;
                        nextBtn.interactable = false;
                    }
                }
                else //������ �� ���� ���
                {
                    if (listIdx == 0)
                    {
                        prevBtn.interactable = false;
                        nextBtn.interactable = true;
                    }
                    else if (diaryDatas.DiaryData[diaryDatas.DiaryData.Count - 1].date != nowDate)
                    {
                        prevBtn.interactable = true;
                        nextBtn.interactable = true;
                    }
                }
            }
        }
        else
        {
            prevBtn.interactable = false;
            nextBtn.interactable = false;
        }
        if (lambdaPublic.diaryDatas.DiaryData.Count != 0)
             diaryDatas = lambdaPublic.diaryDatas;

    }

    #region Init Sprite
    private void InitAlbum()
    {
        nowMonth = System.DateTime.Now.Month;

        //Now
        monthlyDiaryBtnList[nowMonth - 1].image.sprite = activeImgList[nowMonth - 1];
    }
    #endregion
    private string ReturnKoreaName(string name)
    {
        #region if
        if (name.Contains("Blueberries"))
        {
            return "��纣��";
        }
        if (name.Contains("Cactus"))
        {
            return "������";
        }
        if (name.Contains("carrot"))
        {
            return "���";
        }
        if (name.Contains("Herb"))
        {
            return "���";
        }
        if (name.Contains("lavender"))
        {
            return "�󺥴�";
        }
        if (name.Contains("Lettuce"))
        {
            return "����";
        }
        if (name.Contains("Rose"))
        {
            return "���";
        }
        if (name.Contains("Sticky"))
        {
            return "����Ű";
        }
        if (name.Contains("Sunflower"))
        {
            return "�عٶ��";
        }
        if (name.Contains("Tomato"))
        {
            return "�丶��";
        }
        else
        {
            return string.Empty;
        }
        #endregion
    }

    #region DiaryBtn Click Event

    IEnumerator Delay()
    {
        LoadData(selectMonth);
        yield return new WaitForSecondsRealtime(3f);

        diaryDatas = lambdaPublic.diaryDatas;
        listIdx = diaryDatas.DiaryData.Count - 1;
        nowDate = System.DateTime.Now.ToString("yyyy-MM-dd");

        /* Check month */
        // When select month == now month
        if (selectMonth == nowMonth)
        {
            // Set panel on
            diaryPanel.SetActive(true);

            Debug.Log(listIdx + "��ü: " + diaryDatas.DiaryData.Count);

            // Set Button Interactable
            if (diaryDatas.DiaryData.Count == 0)
            {
                prevBtn.interactable = false;
                nextBtn.interactable = false;

                // Set now date
                dateField.text = "������ " + nowDate + " �̿���";
                // Turn on save button
                saveBtn.gameObject.SetActive(true);
                // Set text field 
                inputText.text = "";
                inputText.enabled = true;

                /* ���� �Ĺ� ������ �����ֱ�: ���� �ʿ� */
                ClearPlantData(); //�׽�Ʈ �ڵ�

                //������ �ε����� �߰�
                //SetDefaultData();
                SetTodayDiary();
            }
            else
            {
                //prevBtn.interactable = true;
//                nextBtn.interactable = false;

                if (diaryDatas.DiaryData[diaryDatas.DiaryData.Count - 1].date != nowDate)
                {
                    // Set now date
                    dateField.text = "������ " + nowDate + " �̿���";
                    // Turn on save button
                    saveBtn.gameObject.SetActive(true);
                    // Set text field 
                    inputText.text = "";
                    inputText.enabled = true;

                    /* ���� �Ĺ� ������ �����ֱ�: ���� �ʿ� */
                    ClearPlantData(); //�׽�Ʈ �ڵ�

                    //SetDataDetail(diaryDatas.DiaryData.Count - 1);
                    //������ �ε����� �߰�
                    SetDefaultData();
                }
                else
                {  // Set now date
                    dateField.text = "������ " + nowDate + " �̿���";
                    // Turn on save button
                    saveBtn.gameObject.SetActive(true);
                    // Set text field 
                    inputText.text = diaryDatas.DiaryData[listIdx].diaryText;
                    inputText.enabled = true;

                    /* ���� �Ĺ� ������ �����ֱ�: ���� �ʿ� */
                    ClearPlantData(); //�׽�Ʈ �ڵ�

                    SetTodayDiary();
                }
            }

        }
        else // When select month != now month
        {
            // Set Button Interactable
            if (diaryDatas.DiaryData.Count == 0 || diaryDatas.DiaryData.Count == 1)
            {
                prevBtn.interactable = false;
                nextBtn.interactable = false;
            }
            else
            {
                prevBtn.interactable = true;
                nextBtn.interactable = true;
            }

            if (diaryDatas.DiaryData.Count != 0)
            {
                // Set panel on
                diaryPanel.SetActive(true);

                // Turn off save button
                saveBtn.gameObject.SetActive(false);

                SetDataDetail(diaryDatas.DiaryData.Count - 1);
            }
            else //When Diary Data is null
            {
                nullPopup.SetActive(true);
            }
        }
    }
    public Button dictionaryButton;
    public void DictionaryButtonOn()
    {
        dictionaryButton.enabled = true;
    }
    public void OnMonthlyDiaryBtnClick()
    {
        // Initialize list index
        listIdx = 0;
        selectDiaryMonth = EventSystem.current.currentSelectedGameObject.name;

        for (int i = 0; i < monthlyDiaryBtnList.Count; i++)
        {
            if (monthlyDiaryBtnList[i].name == selectDiaryMonth)
                selectMonth = i + 1;
        }
        /*Load Data*/
        dictionaryButton.enabled = false;
        StartCoroutine(Delay());

    }

    private void LoadData(int selectMonth)
    {
        if (diaryDatas.DiaryData != null)
        { 
            diaryDatas.DiaryData.Clear(); 
        }

        Debug.LogFormat("������ ��: {0}", selectMonth);
        monthData.month = selectMonth;
        for(int i =0; i<3; i++)
        {
            lambdaPublic.Invoke("GetPlantsDiary2", JsonUtility.ToJson(monthData), "DiaryLoad");

        }
        /*
         ���� ȣ��: ������ �ε�
         [���� ������]        
         1) uid
         2) selectMonth
        
         [���� ������ ����]: diaryData
         [
          {
            "date": "2023-12-23",
            "plantGrade": ["plantsGrade1","plantsGrade2"],
            "diaryText": "12�� �ϱ�",
            "plantImgName": ["plantsImage1","plantsImage2"],
            "plantName": ["plantsName1","plantsName2"]
          }
         ]

         plantName -> �ѱ� �̸����� ��ȯ �ʿ�
         */

        #region Test Code
        //switch (selectMonth)
        //{
        //    case 6:
        //        {
        //            diaryDatas.diaryData.Add(new DiaryDataRoot()
        //            {
        //                date = "2023-06-23",
        //                diaryText = "�׽�Ʈ",
        //                plantImgName = new List<string>() { "Rose0", "Cactus2" },
        //                plantGrade = new List<string>() { "A", "B" },
        //                plantName = new List<string>() { "���", "������" }
        //            });
        //            diaryDatas.diaryData.Add(new DiaryDataRoot()
        //            {
        //                date = "2023-06-24",
        //                diaryText = "�׽�Ʈ2",
        //                plantImgName = new List<string>() { "Rose0", "Cactus2", "Herb4" },
        //                plantGrade = new List<string>() { "A", "B", "C" },
        //                plantName = new List<string>() { "���", "������", "���" }
        //            });
        //            break;
        //        }
        //    case 9:
        //        {
        //            diaryDatas.diaryData.Add(new DiaryDataRoot()
        //            {
        //                date = "2023-09-01",
        //                diaryText = "�׽�Ʈ3",
        //                plantImgName = new List<string>() { "Rose0", "Cactus2" },
        //                plantGrade = new List<string>() { "A", "B" },
        //                plantName = new List<string>() { "���", "������" }
        //            });
        //            diaryDatas.diaryData.Add(new DiaryDataRoot()
        //            {
        //                date = "2023-09-20",
        //                diaryText = "�׽�Ʈ2",
        //                plantImgName = new List<string>() { "Tomato3", "Cactus2", "Herb4" },
        //                plantGrade = new List<string>() { "A", "B", "C" },
        //                plantName = new List<string>() { "�丶��", "������", "���" }
        //            });
        //            break;
        //        }
        //}
        #endregion
    }
    private void SetTodayDiary()
    {
        sOnly.uid = DataSave.Instance._data.uid;
        sOnly.Uuid = DataSave.Instance._data.Uuid;
        saveDiaryData.plantName.Clear();
        saveDiaryData.plantGrade.Clear();
        saveDiaryData.plantImgName.Clear();
        saveDiaryData.date = DateTime.Now.ToString("yyyy-MM-dd");
        for (int i = 0; i < DataSave.Instance._data.plantsData.Count; i++)
        {
            pShowList[i].name = DataSave.Instance._data.plantsData[i].plantsname;
            saveDiaryData.plantImgName.Add(pShowList[i].name);
            saveDiaryData.plantName.Add(ReturnKoreaName(pShowList[i].name));
        }
        for (int i = 0; i < pShowList.Count; i++)
        {
            for (int j = 0; j < pImgList.Count; j++)
            {
                if (pShowList[i].name == pImgList[j].name)
                {
                    pShowList[i].transform.GetChild(0).GetComponent<Image>().sprite = pImgList[j];
                    pShowList[i].transform.GetChild(2).GetComponent<Text>().text = "     " + ReturnKoreaName(pShowList[i].name);
                }
            }
        }
        for (int j = 0; j < DataSave.Instance._data.plantsData.Count; j++)
        {
            for (int k = 0; k < pGradeImgList.Count; k++)
            {
                if (pGradeImgList[k].name == GradeSetting(DataSave.Instance._data.plantsData[j].plantsClass))
                {
                    pShowList[j].transform.GetChild(1).gameObject.SetActive(true);
                    pShowList[j].transform.GetChild(1).GetComponent<Image>().sprite = pGradeImgList[k];

                    saveDiaryData.plantGrade.Add(GradeSetting(DataSave.Instance._data.plantsData[j].plantsClass));
                }
            }
        }
    }
    private string GradeSetting(string grade)
    {
        switch(grade)
        {
            case "0":
                return "C";
            case "1":
                return "C";
            case "2":
                return "C";
            case "3":
                return "B";
            case "4":
                return "A";
            default:
                return "";
        }
        
    }
    private void SetDataDetail(int index)
    {
        /*Clear plant datas*/
        ClearPlantData();
        //Initialize Index
        listIdx = index;

        Debug.LogFormat("<color=green>�ε���: {0} / ������ �ε���: {1}</color>", listIdx, diaryDatas.DiaryData.Count - 1);

        // Set Latest date
        dateField.text = "������ " + diaryDatas.DiaryData[listIdx].date + " �̿���";

        // Set text field 
        inputText.text = diaryDatas.DiaryData[listIdx].diaryText;
        inputText.enabled = false;

        //Set Plant Data
        int plantDataCnt = diaryDatas.DiaryData[listIdx].plantImgName.Count;
        if (plantDataCnt > 0) // When Data is exist
        {
            SetPlantData();
        }
        else
        {
            ClearPlantData();
        }
    }

    private void SetDefaultData()
    {
        // Today's Default Data Setting
        listIdx = diaryDatas.DiaryData.Count;

        diaryDatas.DiaryData.Add(new DiaryDataRoot()
        {
            date = nowDate,
            diaryText = "",
            /*�Ĺ� ������ �޾Ƽ� ����Ʈ�� �־��ֱ�: ���� �ʿ�*/
            plantImgName = new List<string>() { },
            plantGrade = new List<string>() { },
            plantName = new List<string>() { }
        });

        SetTodayDiary();
    }

    private void ClearPlantData()
    {
        for (int i = 0; i < 5; i++)
        {
            pShowList[i].transform.GetChild(0).GetComponent<Image>().sprite = defalutImg;
            pShowList[i].transform.GetChild(1).gameObject.SetActive(false);
            pShowList[i].transform.GetChild(2).GetComponent<Text>().text = "�Ĺ�����";
        }
    }

    private void SetPlantData()
    {   
        int pCnt = diaryDatas.DiaryData[listIdx].plantImgName.Count;

        for (int i = 0; i < pCnt; i++)
        {
            string pImgName = diaryDatas.DiaryData[listIdx].plantImgName[i];
            string pGrade = diaryDatas.DiaryData[listIdx].plantGrade[i];
            string pName = diaryDatas.DiaryData[listIdx].plantName[i];

            for (int k = 0; k < pImgList.Count; k++)
            {
                if (pImgList[k].name == pImgName)
                    pShowList[i].transform.GetChild(0).GetComponent<Image>().sprite = pImgList[k];
            }

            for (int j = 0; j < pGradeImgList.Count; j++)
            {
                if (pGradeImgList[j].name == pGrade)
                {
                    pShowList[i].transform.GetChild(1).gameObject.SetActive(true);
                    pShowList[i].transform.GetChild(1).GetComponent<Image>().sprite = pGradeImgList[j];
                }
            }

            pShowList[i].transform.GetChild(2).GetComponent<Text>().text = "     " + pName;
        }
        for (int i = pCnt; i < 5; i++)
        {
            pShowList[i].transform.GetChild(0).GetComponent<Image>().sprite = defalutImg;
            pShowList[i].transform.GetChild(1).gameObject.SetActive(false);
            pShowList[i].transform.GetChild(2).GetComponent<Text>().text = "�Ĺ�����";
        }
    }
    #endregion


    #region Button Click Event Functions
    public GameObject popUp;

    public void OnSaveBtnClick()
    {
        saveDiaryData.diaryText = inputText.text;
        if (inputText.text.Length >= 5)
        {
            savePopup.SetActive(true);
            sOnly.saveDiaryData = saveDiaryData;
            if (diaryDatas.DiaryData.Count != 0)
            {
                if (diaryDatas.DiaryData[diaryDatas.DiaryData.Count - 1].date == nowDate)
                {
                    diaryDatas.DiaryData[diaryDatas.DiaryData.Count - 1].diaryText = diaryTextData;
                }
            }
            DiaryDataRoot temp = new DiaryDataRoot();
            if(diaryDatas.DiaryData.Count ==0)
            {
                    temp.date = saveDiaryData.date;
                    temp.diaryText = saveDiaryData.diaryText;
                    temp.plantGrade = saveDiaryData.plantGrade;
                    temp.plantImgName = saveDiaryData.plantImgName;
                    temp.plantName = saveDiaryData.plantName;
                diaryDatas.DiaryData.Add(temp);
            }
            else
            {

                DataSave.Instance._data.iswriting = true;
                if (diaryDatas.DiaryData[diaryDatas.DiaryData.Count - 1].date != saveDiaryData.date)
                {
                    temp.date = saveDiaryData.date;
                    temp.diaryText = saveDiaryData.diaryText;
                    temp.plantGrade = saveDiaryData.plantGrade;
                    temp.plantImgName = saveDiaryData.plantImgName;
                    temp.plantName = saveDiaryData.plantName;
                    diaryDatas.DiaryData.Add(temp);
                }
                else
                {
                    temp.date = saveDiaryData.date;
                    temp.diaryText = saveDiaryData.diaryText;
                    temp.plantGrade = saveDiaryData.plantGrade;
                    temp.plantImgName = saveDiaryData.plantImgName;
                    temp.plantName = saveDiaryData.plantName;
                    diaryDatas.DiaryData[diaryDatas.DiaryData.Count - 1] = temp;
                }
            }
            lambdaPublic.Invoke("PatchPlantsDiary2", JsonUtility.ToJson(sOnly), "DiarySend");
            StartCoroutine(Delays());
            DataSave.Instance._data.iswriting = true;
        }
        else if(inputText.text.Length<5)
        {
            popUp.SetActive(true);
        }
        
        /*
         ���� ȣ��: ������ ����
         [���� ������] : SaveDiaryData
         1) uid
         2) nowDate
         3) diaryTextData 
         4) plantImgName
         5) plantGrade
         6) plantName
         */

        /*����Ʈ ó��: ���� �ʿ�*/

    }
    IEnumerator Delays()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        yield return new WaitForSecondsRealtime(1.5f);

        lambdaPublic.Invoke("GetPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSend");
    }
    public void OnPrevBtnClick()
    {
        SetDataDetail(listIdx - 1);
    }

    public void OnNextBtnClick()
    {
        SetDataDetail(listIdx + 1);
    }
    public void OnBackBtnClick()
    {
        diaryPanel.SetActive(false);
    }

    public void OnOkBtnClick()
    {
        savePopup.SetActive(false);
    }

    public void OnOk2BtnClick()
    {
        nullPopup.SetActive(false);
    }
    public void OnMonthlyBtnclick()
    {
        diaryPanel.SetActive(false);
    }

    #endregion
}