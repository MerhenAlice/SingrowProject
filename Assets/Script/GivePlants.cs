using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GivePlants : MonoBehaviour
{
    public string plantsname;

    [Header("=PlantsData=")]
    public Image plantsImage;
    public List<Sprite> plantsSprites=new List<Sprite>();
    public List<Sprite> seedSprite = new List<Sprite>();
    public int plantsindex;
    public PlantsData plantsData;

    [Header("=SceneData==")]
    public List<GameObject> scenes=new List<GameObject>();
    public Image Box;
    public List<Sprite> boxImage = new List<Sprite>();


    public List<Sprite> seedInfoPages= new List<Sprite>();
    public Image seedInfo;
    public LambdaPublic lambdaPublic;
    public GameObject gogo;
    public GameObject gogo2;
    private void OnEnable()
    {
        SettingImg();
    }
    public void GiveSetting()
    {
        plantsname = DataSave.Instance.plantPickInGauard;

        if (DataSave.Instance._data.plantsData[DataSave.Instance.index].isKing == true)
        {
            DataSave.Instance.isKingBoolean = false;
            DataSave.Instance.kingIndex = 0;
        }
        DataSave.Instance._data.GiveData += 1;
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        DataSave.Instance._data.plantsData.RemoveAt(DataSave.Instance.index);
    }
    public void RandomSeed()
    {
        int index = UnityEngine.Random.Range(0, seedSprite.Count);
        plantsData.plantsname = seedSprite[index].name;

        plantsData.plantsIdentification = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
        plantsData.plantsExp = 0;
        plantsData.plantsClass = "0";
        plantsData.plantsStairExp = 10;
        plantsData.pots = null;
        plantsData.plantsIndex = DataSave.Instance._data.plantsData.Count;
        plantsData.isSell= false;
        plantsData.lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        DataSave.Instance._data.plantsData.Add(plantsData);
        seedInfo.sprite = seedInfoPages[index];
        DataSave.Instance.isFirst = true;
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        Debug.Log("RandomSeed" + DataSave.Instance._data.ToString());
    }

    public void SettingImg()
    {
        for(int i =0; i<plantsSprites.Count; i++)
        {
            if (plantsSprites[i].name == DataSave.Instance.plantsImgName)
            {
                plantsImage.sprite = plantsSprites[i];
                plantsImage.SetNativeSize();
            }
        }
    }
    public void BoxImageAnimation()
    {
        StartCoroutine(RunCorutine());
    }
    IEnumerator RunCorutine()
    {
        yield return StartCoroutine(ImageChange());
    }
    IEnumerator ImageChange()
    {
        for(int i =0; i<5; i++)
        {
            Box.sprite = boxImage[1];
            yield return new WaitForSeconds(0.5f);
            Box.sprite = boxImage[0];
        }
        scenes[2].SetActive(false);
        scenes[3].SetActive(true);
        StartCoroutine(BoxOpen());
    }
    IEnumerator BoxOpen()
    {
        yield return new WaitForSeconds(1.0f);
        scenes[3].SetActive(false);
        scenes[4].SetActive(true);
        gogo.SetActive(false);
        gogo2.SetActive(false);
        yield return new WaitForSeconds(1.0f);
    }
}
