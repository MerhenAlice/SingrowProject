using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class TalkMangers : MonoBehaviour
{
    public List<string> GrowText = new List<string>();
    public List<string> consensusText = new List<string>();
    public List<string> DeadGrowText = new List<string>();
    public List<string> DeadConsensusText = new List<string>();

    public int GrowIndex = 0;
    public int ConsensusIndex = 0;
    public int DeadIndex = 0;
    public int DeadConsensusIndex = 0;

    public int SelectTowPoint = 0;

    public Text TalkText;
    public void Start()
    {
        SetIndex();
        SetTalkText();
    }
    private void OnEnable()
    {
        
    }
    public void SetIndex()
    {
        GrowIndex = UnityEngine.Random.Range(0, GrowText.Count);
        ConsensusIndex = UnityEngine.Random.Range(0, consensusText.Count);
        DeadIndex = UnityEngine.Random.Range(0, DeadGrowText.Count);
        DeadConsensusIndex = UnityEngine.Random.Range(0, DeadConsensusText.Count);
        SelectTowPoint = UnityEngine.Random.Range(0, 2);
    }
    public void SetTalkText()
    {

        if (DataSave.Instance._data.plantsData[DataSave.Instance.index].isDead == false)
        {
            if (SelectTowPoint == 0)
            {
                TalkText.text = GrowText[GrowIndex];
            }
            else
            {
                TalkText.text = consensusText[ConsensusIndex];
            }
        }
        else
        {
            if (SelectTowPoint == 0)
            {
                TalkText.text = DeadGrowText[DeadIndex];
            }
            else
            {
                TalkText.text = DeadConsensusText[DeadConsensusIndex];
            }
        }
    }
    //=================================================================
    [Header("====================================")]
    public GameObject BasicUI;
    public GameObject GrowUI;
    public GameObject DeadUI;
    public void SelectButton()
    {
        if(SelectTowPoint == 0)
        {
            GrowUI.SetActive(true);
            BasicUI.SetActive(false);
        }
        else
        {
            DeadUI.SetActive(true);
            BasicUI.SetActive(false);
        }
    }
}
