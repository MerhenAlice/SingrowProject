using Amazon.Lambda.Model.Internal.MarshallTransformations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Reward
{
    public int uid;
    public string type;
}
public class RewardSetting : MonoBehaviour
{
    public List<Image> Quest = new List<Image>();
    public List<Sprite> QuestUndo = new List<Sprite>();
    public List<Sprite> QuestCom =  new List<Sprite>();
    public List<Button> buttons = new List<Button>();
    public List<Sprite> QuestUndoButton = new List<Sprite>();
    public List<Sprite> QuestComButton = new List<Sprite>();
    public ItemManager itemManager;
    public LambdaReward lambdaReward;
    public Reward reward;
    private void Awake()
    {
        if (itemManager.item.uid != 0)
        { reward.uid = itemManager.item.uid; }
        if (DataSave.Instance._data.istodaystudy == true)
        {
            Quest[2].sprite = QuestCom[2];
            if (DataSave.Instance.istodaystudy == false)
            {
                buttons[2].GetComponent<Image>().sprite = QuestComButton[2];
            }
            else
            {
                buttons[2].GetComponent<Image>().sprite = QuestUndoButton[2];
                buttons[2].enabled = false;
            }
        }
        else
        {
            Quest[2].sprite = QuestUndo[2];
            buttons[2].GetComponent<Image>().sprite = QuestUndoButton[2];
            buttons[2].enabled = false;
        }
        if (DataSave.Instance._data.isStudy == true)
        {
            Quest[0].sprite = QuestCom[0];
            if (DataSave.Instance.isStudy == false)
            {
                buttons[0].GetComponent<Image>().sprite = QuestComButton[0];
            }
            else
            {
                buttons[0].GetComponent<Image>().sprite = QuestUndoButton[0];
                buttons[0].enabled = false;
            }
        }
        else
        {
            Quest[0].sprite = QuestUndo[0];
            buttons[0].GetComponent<Image>().sprite = QuestUndoButton[0];
            buttons[0].enabled = false;
        }
        if (DataSave.Instance._data.isVeryGood == true)
        {
            Quest[1].sprite = QuestCom[1];
            if (DataSave.Instance.isVeryGood == false)
            {
                buttons[1].GetComponent<Image>().sprite = QuestComButton[1];
            }
            else
            {
                buttons[1].GetComponent<Image>().sprite = QuestUndoButton[1];
                buttons[1].enabled = false;
            }
        }
        else
        {
            Quest[1].sprite = QuestUndo[1];
                buttons[1].GetComponent<Image>().sprite = QuestUndoButton[1];
                buttons[1].enabled = false;
        }
    }
    private void Update()
    {
        if (itemManager.item.uid != 0)
        { 
            reward.uid = itemManager.item.uid;
            DataSave.Instance.item = itemManager.item;
        }
        if (DataSave.Instance._data.istodaystudy == true)
        {
            Quest[2].sprite = QuestCom[2];
            if (DataSave.Instance.istodaystudy == false)
            {
                buttons[2].GetComponent<Image>().sprite = QuestComButton[2];
            }
            else
            {
                buttons[2].GetComponent<Image>().sprite = QuestUndoButton[2];
                buttons[2].enabled = false;
            }
        }
        else
        {
            Quest[2].sprite = QuestUndo[2];
            buttons[2].GetComponent<Image>().sprite = QuestUndoButton[2];
            buttons[2].enabled = false;
        }
        if (DataSave.Instance._data.isStudy == true)
        {
            Quest[0].sprite = QuestCom[0];
            if (DataSave.Instance.isStudy == false)
            {
                buttons[0].GetComponent<Image>().sprite = QuestComButton[0];
            }
            else
            {
                buttons[0].GetComponent<Image>().sprite = QuestUndoButton[0];
                buttons[0].enabled = false;
            }
        }
        else
        {
            Quest[0].sprite = QuestUndo[0];
            buttons[0].GetComponent<Image>().sprite = QuestUndoButton[0];
            buttons[0].enabled = false;
        }
        if (DataSave.Instance._data.isVeryGood == true)
        {
            Quest[1].sprite = QuestCom[1];
            if (DataSave.Instance.isVeryGood == false)
            {
                buttons[1].GetComponent<Image>().sprite = QuestComButton[1];
            }
            else
            {
                buttons[1].GetComponent<Image>().sprite = QuestUndoButton[1];
                buttons[1].enabled = false;
            }
        }
        else
        {
            Quest[1].sprite = QuestUndo[1];
            buttons[1].GetComponent<Image>().sprite = QuestUndoButton[1];
            buttons[1].enabled = false;
        }
    }
    public void OnClickStudy1()
    {
        DataSave.Instance.isStudy = true;
        DataSave.Instance.item.isStudy = true;
    }
    public void OnClickStudy()
    {
        DataSave.Instance.isStudy = true;
        reward.type = "_Bnutrients";
        lambdaReward.EventText = JsonUtility.ToJson(reward);
        lambdaReward.Invoke();
        itemManager.item._Bnutrients++;
        DataSave.Instance.item.isStudy = true;
        itemManager.item.isStudy = true;
        buttons[0].GetComponent<Image>().sprite = QuestUndoButton[0];
        buttons[0].enabled = false;
    }
    public void OnClickStudyAll1()
    {
        DataSave.Instance.istodaystudy = true;
        DataSave.Instance.item.istodaystudy = true;
    }
    public void OnClickStudyAll()
    {
        DataSave.Instance.istodaystudy = true;
        reward.type = "_Rnutrients";
        lambdaReward.EventText = JsonUtility.ToJson(reward);
        lambdaReward.Invoke();
        itemManager.item._Rnutrients++;
        DataSave.Instance.item.istodaystudy = true;
        itemManager.item.istodaystudy = true;
        buttons[2].GetComponent<Image>().sprite = QuestUndoButton[2];
        buttons[2].enabled = false;
    }
    public void OnClickgood1()
    {
        DataSave.Instance.isVeryGood = true;
        DataSave.Instance.item.isVeryGood = true;
    }
    public void OnClickgood()
    {
        DataSave.Instance.isVeryGood = true;
        reward.type = "Sun";
        lambdaReward.EventText = JsonUtility.ToJson(reward);
        lambdaReward.Invoke();
        itemManager.item.Sun++;
        DataSave.Instance.item.isVeryGood = true;
        itemManager.item.isVeryGood = true;
        buttons[1].GetComponent<Image>().sprite = QuestUndoButton[1];
        buttons[1].enabled = false;
    }
    public void OnGetItem()
    {
        itemManager.GetItem();
    }
}
