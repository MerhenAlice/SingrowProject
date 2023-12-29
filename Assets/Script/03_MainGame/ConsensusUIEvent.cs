using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ConsensusUIEvent : MonoBehaviour
{
    #region Consensus
    [Header("=Consensus======")]
    public List<GameObject> consensusButton = new List<GameObject>();
    public int friendsShipPoint;
    public Image plantsImg;
    public static int leapCount = 0;
    public int MAX_Leap_COunt = 5;
    #endregion

    #region compliment
    [Header("=compliment======")]
    public GameObject complimentUI;
    public GameObject plantsSayUI;
    //초기화 필요
    public List<Image> _heart = new List<Image>();
    public Sprite defaultHeart;
    public Sprite redHeart;
    public static int complimentcount = 0;
    [Range(0, 1)]
    public float t;
    public int Complimentspeed;
    public Button butn;
    #endregion
    #region EmotionSticker
    [Header("=EmotionSticker==")]
    public GameObject _emotionStickerUI;
    public List<GameObject> _emotionSticker = new List<GameObject>();
    public GameObject firstStickerSpwan;
    public GameObject sceondStickerSpwan;
    //초기화 필요
    public List<GameObject> _emotionStickerON = new List<GameObject>();
    public List<string> _emotionStickerName = new List<string>();
    public List<Image> _e_heart = new List<Image>();
    public static int _e_heart_count = 0;
    public GameObject Warring;
    public GameObject eLeapimage;
    public Image eLaeap;
    public List<Sprite> Leapchange = new List<Sprite>(); 
    #endregion
    #region Meditation
    [Header("=Meditation======")]
    public GameObject MeditationUI;
    public bool btn_active;
    public bool isONs = false;
    public Text[] text_time;
    public float time = 108000f;
    public SpriteRenderer normalBg;
    public List<Sprite> meditationBG = new List<Sprite>();
    public Slider TimeSetting;
    public RectTransform Handle;

    //public Image _topUI;
    public List<Sprite> _topUIImag = new List<Sprite>();

    public Button startButton;
    public Sprite _buttonAbel;
    public Sprite _buttonDisable;

    public GameObject _popUpPanel;
    public Image _popUpText;
    public List<Sprite> _popUpTextList = new List<Sprite>();
    public List<Button> _popUpButton = new List<Button>();

    public GameObject FinishPanel;

    public GameObject _popUpPanel2;
    public static bool isMediEnd = false;
    public LambdaPublic lambdaPublic;
    #endregion
    public AudioSource audioSource;

    public bool isMediEnds=false;
    private void Start()
    {
        btn_active = false;
        leapCount = DataSave.Instance._data.plantsData[DataSave.Instance.index].leapcount;
    }
    private void OnEnable()
    {
        leapCount = DataSave.Instance._data.plantsData[DataSave.Instance.index].leapcount;
    }
    public GameObject btnbutton;
    public bool ismedileap = false;
    private void Update()
    {
        if (_e_heart_count > 0 && _e_heart_count <= 5)
        {
            for (int i = 0; i < _e_heart_count; i++)
            {
            }
        }
        if (_e_heart_count == 5)
        {
            leapCount+=1;
                DataSave.Instance._data.plantsData[DataSave.Instance.index].isEmotion = true;
                audioSource.Play();
            _e_heart_count++;
            eLeapimage.SetActive(true);
            if(leapCount>7)
            {
                leapCount =7;
            }
            eLaeap.sprite = Leapchange[leapCount];
            ResetEverything();
            btnbutton.SetActive(false);
            if(leapCount>7)
            {
                leapCount = 7;
                eLaeap.sprite = Leapchange[leapCount];

            }
        }
        if (btn_active)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                text_time[0].text = ((int)time / 60 % 60).ToString("D2");
                text_time[1].text = ((int)time % 60).ToString("D2");
                if(isMediEnds == false)
                {
                    isMediEnds = true;
                }
                    DataSave.Instance._data.plantsData[DataSave.Instance.index].isMeditate = true;
                MeditationBackGroundChange();
                
            }
            else if(time<= 0 && time>=-0.1f)
            {
                FinishPanel.SetActive(true);
                if(ismedileap == false)
                {
                    ismedileap = true;
                    leapCount += 1;
                }
                isMediEnd = true;
            }
        }
        if (time < 301f && time > 241f)
        {
            normalBg.sprite = meditationBG[1];
        }
        else if (time < 241f && time > 181f)
        {
            normalBg.sprite = meditationBG[8];
        }
        else if (time < 181f && time > 121f)
        {
            normalBg.sprite = meditationBG[6];
        }
        else if (time < 121f && time > 61f)
        {
            normalBg.sprite = meditationBG[4];
        }
        Handle.gameObject.GetComponent<Image>().SetNativeSize();
        //_topUI.SetNativeSize();
        for(int i =0; i<complimentcount; i++)
        {
            _heart[i].sprite = redHeart;
        }
        if (complimentcount == 5)
        {
            DataSave.Instance._data.plantsData[DataSave.Instance.index].isTalk= true;
            comBack.SetActive(false);
        }
        else
        {
            comBack.SetActive(true);
        }
        if(leapCount >= 7)
        {
            leapCount = 7;
        }
        DataSave.Instance._data.plantsData[DataSave.Instance.index].leapcount = leapCount;
    }
    public GameObject comBack;
    public void ButtonON()
    {
        for (int i = 0; i < consensusButton.Count; i++)
        {
            consensusButton[i].SetActive(true);
        }
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        _emotionStickerUI.SetActive(false);
        ResetEverything();
    }
    
    public void OnClickButtonOne(string btn_name)
    {
        for (int i = 0; i < consensusButton.Count; i++)
        {
            consensusButton[i].SetActive(false);
        }
        if (btn_name == "compliment")
        {
            complimentUI.SetActive(true);
        }
        else if (btn_name == "emotionsticker")
        {
            _emotionStickerUI.SetActive(true);
        }
        else if (btn_name == "meditation")
        {
            MeditationUI.SetActive(true);
        }
        else if (btn_name == "bakcbutton")
        {
            for (int i = 0; i < consensusButton.Count; i++)
            {
                consensusButton[i].SetActive(true);
            }
        }
    }
    #region complimentEvent
    public void ResetCompliment()
    {
        for (int i = 0; i < _heart.Count; i++)
        {
            _heart[i].sprite = defaultHeart;

        }
        complimentcount = 0;
        butn.interactable = true;
    }
    public void ComplimentsSetting()
    {
    }
    public void ComplimentOnClick(GameObject ClickButton)
    {
        t = 0;
        StartCoroutine(ComplimentSlide(ClickButton));
        friendsShipPoint += 2;
        if (complimentcount == 4)
        {

            leapCount += 1;
        }
    }

    IEnumerator ComplimentSlide(GameObject ClickButton)
    {
        while (ClickButton.transform.position.x >= plantsImg.transform.position.x + 10)
        {
            yield return new WaitForSeconds(0.1f);
            ClickButton.transform.position = Vector3.Slerp(ClickButton.transform.position, plantsImg.transform.position, t * Complimentspeed);
            t = t + 0.05f;
        }
        if (ClickButton.transform.position.x <= plantsImg.transform.position.x + 10)
        {
            plantsSayUI.SetActive(true);
            StartCoroutine(SayUIOff(ClickButton));
        }
    }
    IEnumerator SayUIOff(GameObject ClickButton)
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(i / 2f);
        }
        if (complimentcount <= 5)
        {
            _heart[complimentcount].sprite = redHeart;
            complimentcount++;
            plantsSayUI.SetActive(false);
            ClickButton.SetActive(false);
        }
    }
    #endregion
    #region EmotionSticker
    public void EmotionStickerSpawn()
    {
        int index = -1;
        int count = 0;
        int NegativeCount = 0;
        while (count < 3)
        {
            int randomindex = Random.Range(0, _emotionSticker.Count - 1);
            if (index != randomindex)
            {
                if (_emotionStickerName.Count == 0)
                {
                    GameObject InstantSticker = Instantiate(_emotionSticker[randomindex], Return_RandomPosition(firstStickerSpwan), Quaternion.identity, firstStickerSpwan.transform);
                    _emotionStickerON.Add(InstantSticker);
                    _emotionStickerName.Add(InstantSticker.name);
                    count += 1;
                }
                else if (_emotionStickerName.Count > 0)
                {
                    GameObject InstantSticker = Instantiate(_emotionSticker[randomindex], Return_RandomPosition(firstStickerSpwan), Quaternion.identity, firstStickerSpwan.transform);
                    for (int i = 0; i < _emotionStickerName.Count; i++)
                    {
                        if (InstantSticker.name == _emotionStickerName[i])
                        {
                            Destroy(InstantSticker);
                            InstantSticker = null;
                            break;
                        }
                        if (NegativeCount >= 1)
                        {
                            if (_emotionSticker[randomindex].tag == "Negative")
                            {
                                Destroy(InstantSticker);
                                InstantSticker = null;
                                break;
                            }
                        }
                    }
                    if (InstantSticker != null)
                    {
                        _emotionStickerON.Add(InstantSticker);
                        _emotionStickerName.Add(InstantSticker.name);
                        count += 1;
                    }

                }
                index = randomindex;
                if (_emotionSticker[randomindex].tag == "Negative")
                {
                    NegativeCount++;
                }
            }

        }
        count = 0;
        index = -1;
        while (count < 3)
        {
            int randomindex = Random.Range(0, _emotionSticker.Count - 1);
            if (index != randomindex)
            {
                GameObject InstantSticker = Instantiate(_emotionSticker[randomindex], Return_RandomPosition(sceondStickerSpwan), Quaternion.identity, sceondStickerSpwan.transform);
                for (int i = 0; i < _emotionStickerName.Count; i++)
                {
                    if (InstantSticker.name == _emotionStickerName[i])
                    {
                        Destroy(InstantSticker);
                        InstantSticker = null;
                        break;
                    }
                    if (NegativeCount >= 1)
                    {
                        if (_emotionSticker[randomindex].tag == "Negative")
                        {
                            Destroy(InstantSticker);
                            InstantSticker = null;
                            break;
                        }
                    }
                }
                if (InstantSticker != null)
                {
                    _emotionStickerON.Add(InstantSticker);
                    _emotionStickerName.Add(InstantSticker.name);
                    count += 1;
                }
                index = randomindex;
                if (_emotionSticker[randomindex].tag == "Negative")
                {
                    NegativeCount += 1;
                }
            }

        }
    }
    public void ResetEverything()
    {
        for (int i = 0; i < _emotionStickerON.Count; i++)
        {
            Destroy(_emotionStickerON[i]);
        }
        _emotionStickerON.Clear();
        _emotionStickerName.Clear();
        _e_heart_count = 0;
    }
    public void BackButtonClickMiddle()
    {
        if (_e_heart_count < 5)
        {
            Warring.SetActive(true);
        }
    }
    Vector2 Return_RandomPosition(GameObject emotionStickerSpqwnPosition)
    {
        Vector2 originalPosition = emotionStickerSpqwnPosition.gameObject.transform.position;

        float range_X = emotionStickerSpqwnPosition.gameObject.GetComponent<BoxCollider2D>().bounds.size.x;
        float range_Y = emotionStickerSpqwnPosition.gameObject.GetComponent<BoxCollider2D>().bounds.size.y;
        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector2 RandomPosition = new Vector2(range_X, range_Y);

        Vector2 resPosition = originalPosition + RandomPosition;
        return resPosition;
    }

    #endregion
    #region Meditation
    public GameObject gono;
    public void LeapPlus()
    {
        ismedileap = false;
    }
    public void Btn_Click()
    {
        if (!btn_active)
        {
            if (time > 30f)
            {
                SetTimerOn();
                normalBg.sprite = meditationBG[0];
                startButton.gameObject.SetActive(false);
                TimeSetting.gameObject.SetActive(false);
                isONs = true;
                gono.SetActive(true);
            }
            else
            {

                _popUpPanel2.SetActive(true);
                /*startButton.GetComponent<Image>().sprite = _buttonDisable;
                _popUpButton[0].gameObject.SetActive(true);
                _popUpText.sprite = _popUpTextList[0];
                _popUpPanel.SetActive(true);*/
            }
        }
        else
        {
            SetTimerOff();
        }
    }
    public void SetTimerOn()
    {
        btn_active = true;
    }
    public void SetTimerOff()
    {
        btn_active = false;
        if (time != 0)
        {
            _popUpPanel.SetActive(true);
            _popUpButton[0].gameObject.SetActive(true);
            _popUpText.sprite = _popUpTextList[0];
        }
    }
    public void SetExits()
    {
        if(isONs == true)
        {
            NotMEditationExit();
        }
        else
        {
            NotMEditationExit2();
        }
    }
    public void OnTimeSet()
    {
        if (TimeSetting.value > 0.33f && TimeSetting.value < 0.66f)
        {
            time = 51f;
            startButton.GetComponent<Image>().sprite = _buttonAbel;
            text_time[0].text = ((int)time / 60 % 60).ToString("D2");
            text_time[1].text = (((int)time % 60) - 1).ToString("D2");
        }
        else if (TimeSetting.value > 0.66f && TimeSetting.value < 0.99f)
        {
            time = 181f;
            startButton.GetComponent<Image>().sprite = _buttonAbel;
            text_time[0].text = ((int)time / 60 % 60).ToString("D2");
            text_time[1].text = (((int)time % 60) - 1).ToString("D2");
        }
        else if (TimeSetting.value > 0.99f && TimeSetting.value <= 1.0f)
        {
            time = 301f;
            startButton.GetComponent<Image>().sprite = _buttonAbel;
            text_time[0].text = ((int)time / 60 % 60).ToString("D2");
            text_time[1].text = (((int)time % 60)-1).ToString("D2");
        }
        else
        {
            time = 0f;
            startButton.GetComponent<Image>().sprite = _buttonDisable;
            text_time[0].text = ((int)time / 60 % 60).ToString("D2");
            text_time[1].text = (((int)time % 60)).ToString("D2");
        }
    }
    public void MeditationUIOFF()
    {
        _popUpPanel.SetActive(true);
        _popUpButton[1].gameObject.SetActive(true);
        _popUpButton[2].gameObject.SetActive(true);
        _popUpText.sprite = _popUpTextList[1];
        _popUpText.SetNativeSize();
        TimeSetting.gameObject.SetActive(false);
        btn_active = false;
    }
    public AudioClip main;

    public void MeditationExit()
    {
        for (int i = 0; i < _popUpButton.Count; i++)
        {
            _popUpButton[i].gameObject.SetActive(false);
        }
        normalBg.sprite = meditationBG[2];
        _popUpPanel.SetActive(false);
        startButton.gameObject.SetActive(true);
        normalBg.gameObject.SetActive(false);
        TimeSetting.value = 0f;
        time = 0; btn_active = false;
        isMediEnds = false;
        FinishPanel.SetActive(false);
        TimeSetting.gameObject.SetActive(true);
        gono.SetActive(false);
        isONs = false;
        SoundManager.medion = false;
        SoundManager.instance.BgSoundPlay(main);
        lambdaPublic.Invoke("GetPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
    }
    public void NotMEditationExit()
    {
        for (int i = 0; i < _popUpButton.Count; i++)
        {
            _popUpButton[i].gameObject.SetActive(false);
        }
        btn_active = true;
        MeditationBackGroundChange();
    }
    public void NotMEditationExit2()
    {
        for (int i = 0; i < _popUpButton.Count; i++)
        {
            _popUpButton[i].gameObject.SetActive(false);
        }
        TimeSetting.gameObject.SetActive(true);
        normalBg.sprite = meditationBG[2];
    }
    public void MeditationBackGroundChange()
    {
        if (time < 301f && time > 221f)
        {
            normalBg.sprite = meditationBG[1];
        }
        else if (time < 221f && time > 161f)
        {
            normalBg.sprite = meditationBG[8];
        }
        else if (time < 161f && time > 101f)
        {
            normalBg.sprite = meditationBG[6];
        }
        else if (time < 101f && time > 31f)
        {
            normalBg.sprite = meditationBG[4];
        }
    }
    #endregion
}
