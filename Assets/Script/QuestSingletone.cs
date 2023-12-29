using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int uid;
    //±âº» Äù½ºÆ®
    public bool isWatering;
    public bool isWeeding;
    public bool isSun;
    public bool isNutrients;
    public bool isWriting;
    public bool isGiving;
    //ºñ»ó Äù½ºÆ®

}
public class FriendshipQuest
{
    public bool isCompliment;
    public bool isEmotion;
    public bool isMeditation;
    public bool isConsensus;
    public bool isClover;
}
public class QuestSingletone : MonoBehaviour
{
    #region Singletone
    private static QuestSingletone instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static QuestSingletone Instance
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
    public Quest quest;
    public FriendshipQuest friendshipQuest;

    public void FriendshipQuestSet(string key)
    {
        switch(key)
        {
            case "compliment":
                {
                    friendshipQuest.isCompliment = true;
                    break;
                }
            case "Emotion":
                {
                    friendshipQuest.isEmotion = true;
                    break;
                }
            case "Meditation":
                {
                    friendshipQuest.isMeditation = true;
                    break;
                }
            case "Consensus":
                {
                    friendshipQuest.isConsensus = true;
                    break;
                }
            case "Clover":
                {
                    friendshipQuest.isClover = true;
                    break;
                }
        }
    }
    public void NormalQuestSet(string key)
    {
        switch(key)
        {
            case "Water":
                {
                    quest.isWatering= true;
                    break;
                }
            case "Weed":
                {
                    quest.isWeeding= true;
                    break;
                }
            case "Sun":
                {
                    quest.isSun = true;
                    break;
                }
            case "Nutrients":
                {
                    quest.isNutrients= true;
                    break;
                }
            case "Writing":
                {
                    quest.isWriting= true;
                    break;
                }
            case "Giving":
                {
                    break;
                }
        }
    }
    IEnumerator GivingSetting()
    {
        yield return null;
    }
}
