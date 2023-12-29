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
    public string date; //날짜
    public string diaryText; //내용
    public List<string> plantImgName; //식물 이미지 이름
    public List<string> plantGrade; //식물 등급
    public List<string> plantName; //식물 이름
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
    public string date; //날짜
    public string diaryText; //내용
    public List<string> plantImgName; //식물 이미지 이름
    public List<string> plantGrade; //식물 등급
    public List<string> plantName; //식물 이름
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
                    {//첫번째일때
                        prevBtn.interactable = false;
                        nextBtn.interactable = true;
                    }
                    else if (listIdx == diaryDatas.DiaryData.Count - 1)
                    {//마지막일때

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
            else //지금 달이랑 같을 경우
            {
                //저장이 됐을 때
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
                        //인덱스가 
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
                else //저장이 안 됐을 경우
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

            Debug.Log(listIdx + "전체: " + diaryDatas.DiaryData.Count);

            // Set Button Interactable
            if (diaryDatas.DiaryData.Count == 0)
            {
                prevBtn.interactable = false;
                nextBtn.interactable = false;

                // Set now date
                dateField.text = "오늘은 " + nowDate + " 이에요";
                // Turn on save button
                saveBtn.gameObject.SetActive(true);
                // Set text field 
                inputText.text = "";
                inputText.enabled = true;

                /* 현재 식물 데이터 보여주기: 수정 필요 */
                ClearPlantData(); //테스트 코드

                //마지막 인덱스에 추가
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
                    dateField.text = "오늘은 " + nowDate + " 이에요";
                    // Turn on save button
                    saveBtn.gameObject.SetActive(true);
                    // Set text field 
                    inputText.text = "";
                    inputText.enabled = true;

                    /* 현재 식물 데이터 보여주기: 수정 필요 */
                    ClearPlantData(); //테스트 코드

                    //SetDataDetail(diaryDatas.DiaryData.Count - 1);
                    //마지막 인덱스에 추가
                    SetDefaultData();
                }
                else
                {  // Set now date
                    dateField.text = "오늘은 " + nowDate + " 이에요";
                    // Turn on save button
                    saveBtn.gameObject.SetActive(true);
                    // Set text field 
                    inputText.text = diaryDatas.DiaryData[listIdx].diaryText;
                    inputText.enabled = true;

                    /* 현재 식물 데이터 보여주기: 수정 필요 */
                    ClearPlantData(); //테스트 코드

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

        Debug.LogFormat("선택한 달: {0}", selectMonth);
        monthData.month = selectMonth;
        for(int i =0; i<3; i++)
        {
            lambdaPublic.Invoke("GetPlantsDiary2", JsonUtility.ToJson(monthData), "DiaryLoad");

        }
        /*
         람다 호출: 데이터 로드
         [보낼 데이터]        
         1) uid
         2) selectMonth
        
         [받을 데이터 형식]: diaryData
         [
          {
            "date": "2023-12-23",
            "plantGrade": ["plantsGrade1","plantsGrade2"],
            "diaryText": "12월 일기",
            "plantImgName": ["plantsImage1","plantsImage2"],
            "plantName": ["plantsName1","plantsName2"]
          }
         ]

         plantName -> 한글 이름으로 변환 필요
         */

        #region Test Code
        //switch (selectMonth)
        //{
        //    case 6:
        //        {
        //            diaryDatas.diaryData.Add(new DiaryDataRoot()
        //            {
        //                date = "2023-06-23",
        //                diaryText = "테스트",
        //                plantImgName = new List<string>() { "Rose0", "Cactus2" },
        //                plantGrade = new List<string>() { "A", "B" },
        //                plantName = new List<string>() { "장미", "선인장" }
        //            });
        //            diaryDatas.diaryData.Add(new DiaryDataRoot()
        //            {
        //                date = "2023-06-24",
        //                diaryText = "테스트2",
        //                plantImgName = new List<string>() { "Rose0", "Cactus2", "Herb4" },
        //                plantGrade = new List<string>() { "A", "B", "C" },
        //                plantName = new List<string>() { "장미", "선인장", "허브" }
        //            });
        //            break;
        //        }
        //    case 9:
        //        {
        //            diaryDatas.diaryData.Add(new DiaryDataRoot()
        //            {
        //                date = "2023-09-01",
        //                diaryText = "테스트3",
        //                plantImgName = new List<string>() { "Rose0", "Cactus2" },
        //                plantGrade = new List<string>() { "A", "B" },
        //                plantName = new List<string>() { "장미", "선인장" }
        //            });
        //            diaryDatas.diaryData.Add(new DiaryDataRoot()
        //            {
        //                date = "2023-09-20",
        //                diaryText = "테스트2",
        //                plantImgName = new List<string>() { "Tomato3", "Cactus2", "Herb4" },
        //                plantGrade = new List<string>() { "A", "B", "C" },
        //                plantName = new List<string>() { "토마토", "선인장", "허브" }
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

        Debug.LogFormat("<color=green>인덱스: {0} / 마지막 인덱스: {1}</color>", listIdx, diaryDatas.DiaryData.Count - 1);

        // Set Latest date
        dateField.text = "오늘은 " + diaryDatas.DiaryData[listIdx].date + " 이에요";

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
            /*식물 데이터 받아서 리스트에 넣어주기: 수정 필요*/
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
            pShowList[i].transform.GetChild(2).GetComponent<Text>().text = "식물없음";
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
            pShowList[i].transform.GetChild(2).GetComponent<Text>().text = "식물없음";
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
         람다 호출: 데이터 저장
         [보낼 데이터] : SaveDiaryData
         1) uid
         2) nowDate
         3) diaryTextData 
         4) plantImgName
         5) plantGrade
         6) plantName
         */

        /*퀘스트 처리: 수정 필요*/

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