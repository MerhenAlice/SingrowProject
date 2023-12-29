using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedSeletPage : MonoBehaviour
{
    public GameObject TextGuide;
    public GameObject SeedSelect;
    public GameObject SeedGuide;


    public static string _seedName;

    public List<Sprite> seedInfos= new List<Sprite>();
    public Image seedInfo;

    public List<Image> seedButtons = new List<Image>(); 
    public List<Sprite> seedButtonSprites = new List<Sprite>();

    public int indexofSeedInfos;

    PlantsData plantsData = new PlantsData();
    public void SelectGauradenBackGround()
    {
        TextGuide.SetActive(false);
        SeedSelect.SetActive(true);
    }
    public void SelectSeed(string seedname)
    {
        _seedName = seedname;
        plantsData.plantsname = seedname+"0";
        plantsData.isSell = false;
        plantsData.plantsExp = 0;
        plantsData.plantsClass = "0";
        for (int i = 0; i < seedInfos.Count; i++)
        {
            if (seedInfos[i].name == _seedName)
            {
                indexofSeedInfos = i;
                break;
            }
        }
        SeedInfoImageChange();
    }
    public void SelectSeedSelect()
    {
        SeedSelect.SetActive(false);
        SeedGuide.SetActive(true);
    }
    public void CompeleteSelect()
    {
        SeedNameSelected(_seedName);
        plantsData.plantsname = _seedName + "0";
        DataSave.Instance.DataInput(plantsData);
    }
    public string SeedNameSelected(string seedname)
    {
        int randomindex = UnityEngine.Random.Range(0, 2);
        switch (_seedName)
        {
            case "Succulent":
                {
                    if(randomindex == 0)
                    {
                        _seedName = "Cactus";
                    }
                    else
                    {
                        _seedName = "Sticky";
                    }
                    break;
                }
            case "Herb":
                {
                    if (randomindex == 0)
                    {
                        _seedName = "Herb";
                    }
                    else
                    {
                        _seedName = "lavender";
                    }
                    break;
                }
            case "Angiospermae":
                {
                    if (randomindex == 0)
                    {
                        _seedName = "Rose";
                    }
                    else
                    {
                        _seedName = "Sunflower";
                    }
                    break;
                }
            case "Vegetable":
                {
                    if (randomindex == 0)
                    {
                        _seedName = "carrot";
                    }
                    else
                    {
                        _seedName = "Lettuce";
                    }
                    break;
                }
            case "fruit":
                {
                    if (randomindex == 0)
                    {
                        _seedName = "Blueberries";
                    }
                    else
                    {
                        _seedName = "Tomato";
                    }
                    break;
                }

        }
        return null;
    }
    public void NextButton()
    {
        if(indexofSeedInfos<seedInfos.Count-1)
        {
            indexofSeedInfos++;
        }
        else
        {
            indexofSeedInfos = 0;
        }
        SeedInfoImageChange();
    }
    public void PrevButton()
    {
        if (indexofSeedInfos > 0)
        {
            indexofSeedInfos--;
        }
        else if(indexofSeedInfos == 0)
        {
            indexofSeedInfos = 4;
        }
        SeedInfoImageChange();
    }
    public void SeedInfoImageChange()
    {
        for (int i = 0; i < seedInfos.Count; i++)
        {
            if (indexofSeedInfos == i)
            {
                seedInfo.sprite = seedInfos[indexofSeedInfos];
                seedInfo.name = seedInfos[indexofSeedInfos].name;
                _seedName = seedInfo.name;
            }
        }
        int index = 0;
        for (int i = 0; i<seedButtonSprites.Count;i++)
        {
            if (seedButtonSprites[i].name != seedInfo.name)
            {
                seedButtons[index].sprite = seedButtonSprites[i];
                index++;
            }
        }
    }
    public int GetIndex(string name)
    {
        switch(name)
        {
            case "Angiospermae":
                    return 0;
            case "fruit":
                    return 1;
            case "Herb":
                    return 2;
            case "Succulent":
                return 3;
            case "Vegetable":
                return 4;
        }
        return 0;
    }
    public void OnclickSideButton(GameObject go)
    {
        indexofSeedInfos = GetIndex(go.name);
        SeedInfoImageChange();
    }
}
