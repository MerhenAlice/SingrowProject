using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectDataController : MonoBehaviour
{
    private static SelectDataController instance = null;

    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static SelectDataController Instance
    {
        get
        {
            if(instance ==null)
            {
                return null;
            }
            return instance;
        }
    }

    public EventSystem eventSystem;

    [SerializeField]
    public string selectButtonName;

    public Image BackGround;
    public List<Sprite> BGList= new List<Sprite>();
    int index = 0;

    public Button buttonOne;
    public Button ButtonTwo;
    public GameObject _nextButton;
    public List<Sprite> buttonList = new List<Sprite>();
    private void Update()
    {
        if (eventSystem == null)
        {
            eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        }
    }
    public void BackGroundSelect()
    {
        bool isFirstButton = true;
        if (eventSystem != null)
            selectButtonName = eventSystem.currentSelectedGameObject.name.ToString();
        for (int i = 0; i<BGList.Count; i++)
        {
            if (BGList[i].name == selectButtonName)
            {
                BackGround.sprite= BGList[i];
                BackGround.name = selectButtonName;
            }
        }
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (buttonList[i].name != BackGround.name)
            {
                if (isFirstButton)
                {
                    buttonOne.GetComponent<Image>().sprite = buttonList[i];
                    buttonOne.name = buttonList[i].name;
                    isFirstButton = false;
                }
                else
                {
                    ButtonTwo.GetComponent<Image>().sprite = buttonList[i];
                    ButtonTwo.name = buttonList[i].name;
                }
            }
        }
    }
    public void NextButton()
    {
        bool isFirstButton = true;
        int listIndex = BGList.IndexOf(BackGround.sprite);
        for (int i =0; i<BGList.Count; i++)
        {
            if(listIndex == 0 )
            {
                _nextButton.name = BGList[1].name;
                BackGround.name = BGList[1].name;
            }
            else if (listIndex == 1)
            {
                _nextButton.name = BGList[2].name;
                BackGround.name = BGList[2].name;
            }
            else if(listIndex ==2)
            {
                _nextButton.name = BGList[0].name;
                BackGround.name = BGList[0].name;
            }
        }
        BackGroundSelect();
        for(int i = 0; i<buttonList.Count; i++)
        {
            if (buttonList[i].name != BackGround.name)
            {
                if(isFirstButton)
                {
                    buttonOne.GetComponent<Image>().sprite = buttonList[i];
                    buttonOne.name = buttonList[i].name;
                    isFirstButton = false;
                }
                else
                {
                    ButtonTwo.GetComponent<Image>().sprite = buttonList[i];
                    ButtonTwo.name = buttonList[i].name;
                }
            }
        }
    }
}
