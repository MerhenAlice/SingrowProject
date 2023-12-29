using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlantsInfo : MonoBehaviour
{
    [SerializeField]
    private PlantsSpawn plantsSpawn;

    public List<Image> leaves = new List<Image>();
    public Sprite leaf;
    public Text leavecount;

    public Text plantsName;
    public List<Sprite> Rank = new List<Sprite>();
    public Image plantsRank;

    public Slider plantsExp;
    public Text plantsll;
    public Image plants;
    private void OnEnable()
    { 
        
    }
    private void Update()
    {
        for (int i = 0; i < ConsensusUIEvent.leapCount; i++)
        {
            leaves[i].sprite = leaf;
        }
        leavecount.text = ConsensusUIEvent.leapCount.ToString() + "/" + leaves.Count.ToString();
        if (plantsSpawn != null)
        {
            plantsExp.maxValue = plantsSpawn.StairExp;
            plantsExp.value = plantsSpawn.CurrentExp;
        }
        else if(plantsSpawn == null)
        {
            plantsExp.maxValue = DataSave.Instance.MaxExp;
            plantsExp.value = DataSave.Instance.currentExp;
        }
        plantsName.text = NameSetting(transform.parent.name); 
        for (int i = 0; i < DataSave.Instance._data.plantsData.Count; i++)
        {
            if (plants.name == DataSave.Instance._data.plantsData[i].plantsname)
            {
                if (DataSave.Instance._data.plantsData[i].plantsClass == "2")
                {
                    plantsRank.gameObject.SetActive(true);
                    plantsRank.sprite = Rank[0];
                    plantsll.text = "현재 덜 자란 식물 단계";
                    break;
                }
                else if (DataSave.Instance._data.plantsData[i].plantsClass == "3")
                {
                    plantsRank.gameObject.SetActive(true);
                    plantsRank.sprite = Rank[1];
                    plantsll.text = "현재 성장 중인 단계";
                    break;
                }
                else if (DataSave.Instance._data.plantsData[i].plantsClass == "4")
                {
                    plantsRank.gameObject.SetActive(true);
                    plantsRank.sprite = Rank[2];
                    plantsll.text = "현재 건강히 자란 단계";
                    break;
                }
                else if (DataSave.Instance._data.plantsData[i].plantsClass == "0")
                {
                    plantsRank.gameObject.SetActive(true);
                    plantsRank.sprite = Rank[0];
                    plantsll.text = "아직 씨앗이에요.";
                    break;
                }
                else if (DataSave.Instance._data.plantsData[i].plantsClass == "1")
                {
                    plantsRank.gameObject.SetActive(true);
                    plantsRank.sprite = Rank[0];
                    plantsll.text = "현재 관심이 필요한 단계";
                    break;
                }
            }
        }
    }
    
    public string NameSetting(string name)
    {
        string nameReplace = name.Replace("0", "");
        nameReplace = nameReplace.Replace("1", "");
        nameReplace = nameReplace.Replace("2", "");
        nameReplace = nameReplace.Replace("3", "");
        nameReplace = nameReplace.Replace("4", "");
        switch (nameReplace)
        {
            case "Cactus":
                return "선인장";
            case "Sticky":
                return "스투키";
            case "Herb":
                return "허브";
            case "lavender":
                return "라벤더";
            case "Rose":
                return "장미";
            case "Sunflower":
                return "해바라기";
            case "Lettuce":
                return "상추";
            case "carrot":
                return "당근";
            case "Tomato":
                return "토마토";
            case "Blueberries":
                return "블루베리";
        }
        return null;
    }
}
