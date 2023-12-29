using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class DoingPlantsGrowing : MonoBehaviour
{
    #region Singletone
    private static DoingPlantsGrowing _instance;
    public static DoingPlantsGrowing Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }
            return _instance;
        }
    }
    #endregion

    #region Light
    [Header("=Light==========")]
    public GameObject normalBackGround;
    public GameObject SunsetBakcGround;
    public GameObject PopUP;
    public GameObject pots;
    public Light2D Effect;
    public Light2D bgLight;
    #endregion
    #region Water
    [Header("=WaterFall======")]
    public Image waterFall;
    public Image Waterimg;
    public float waterDistance;
    public float waterMoveSpeed;
    public float waterRotationValue;
    public float waterRotationSpeed;
    public Transform potTransform;
    public List<GameObject> walls = new List<GameObject>();
    #endregion
    #region Nutrients
    [Header("=Nutrients======")]
    public GameObject Nutrients;
    public Animator NutrientsAnimator;
    public float NutrientTime;
    #endregion
    #region Public Use
    public GameObject PopUp;
    public List<GameObject> growButton = new List<GameObject>();
    public PlantsSpawn psawn;
    public AudioSource waterSound;
    public ItemManager itemManager;
    public GameObject ExitButton;
    #endregion
    public int ReturnNutrientsExp()
    {
        return 3;
    }
    public int RetrunweedExp()
    {
        
        return 1;
    }
    #region WaterFall
    public GameObject objj;
    public void WaterFall(int currentExp)
    {

        ExitButton.SetActive(true);
        bbt.SetActive(false); objj.SetActive(true);
        StartCoroutine(WaterPotSetting(currentExp));
        for(int i =0; i< walls.Count; i++)
        {
            walls[i].gameObject.SetActive(false);
        }
    }
    IEnumerator WaterPotSetting(int currentExp)
    {
        yield return new WaitForSeconds(0.5f);
        waterFall.gameObject.SetActive(true);
        yield return StartCoroutine(WaterFallForSetting());
        yield return new WaitForSeconds(2.0f);
        Waterimg.gameObject.SetActive(false);
        waterFall.transform.position = new Vector3(330.5f, waterFall.transform.position.y, 0);
        waterFall.transform.rotation = Quaternion.Euler(0, 0, 0);
        waterFall.gameObject.SetActive(false);
        PopUp.SetActive(true);
        waterSound.Play();
        yield return new WaitForSeconds(1.0f);
        Debug.Log("currentExp = " + currentExp);
        psawn.CurrentExp += 3;
        itemManager.item.water -= 1;
        itemManager.UseItems("Water"); 
        DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp = psawn.CurrentExp;
        DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        PopUp.SetActive(false);
        bbt.SetActive(true); objj.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        ExitButton.SetActive(false);
        exitImages.SetActive(false);
        
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].gameObject.SetActive(true);
        }
        PlantsSpawn.isActiveInHaerarchy = false;
        if (DataSave.Instance._data.iswatering == false)
        {
            DataSave.Instance._data.iswatering = true;
        }
        StartCoroutine(Delay());
    }
    IEnumerator WaterFallForSetting()
    {
        waterDistance = Mathf.Abs (waterFall.transform.position.x - potTransform.position.x); 
        while (waterFall.transform.position.x <= waterDistance)
        {
            yield return new WaitForSeconds(0.05f);
            waterFall.transform.position = new Vector3(waterFall.transform.position.x+ waterMoveSpeed, waterFall.transform.position.y, 0);
        }
        while(waterFall.transform.position.x>= waterDistance && waterFall.transform.rotation.z>waterRotationValue)
        {
            yield return new WaitForSeconds(0.05f);
            waterFall.transform.Rotate(new Vector3(0,0,-3) * Time.deltaTime * waterRotationSpeed);
        }
        if(waterFall.transform.rotation.z <= waterRotationValue)
        {
            for(int i =0; i<5; i++)
            {
                yield return new WaitForSeconds(0.1f);
                Waterimg.gameObject.SetActive(true);
            }
        }
    }
    #endregion
    #region SunSet
    public GameObject tree;
    public Light2D globalLight;
    public Light2D spotLight;
    public GameObject Sun;
    public float speed;
    public Transform sunDistance;
    public Transform sunFirstPos;
    public GameObject bbt;
    public void SunSetAdd(int currentExp)
    {
        ExitButton.SetActive(true);
        SunsetBakcGround.SetActive(true);
        sunFirstPos.position = Sun.transform.position;
        globalLight.intensity = 0.3f;
        tree.SetActive(true);
        normalBackGround.SetActive(false);
        pots.GetComponent<Image>().enabled = false;
        DataSave.Instance._data.isSun = true;
        StartCoroutine(SuntSetOn());
    }
    IEnumerator SuntSetOn()
    {
        animator.SetTrigger("SunTrigger");
        StartCoroutine(SunMove());
        yield return new WaitForSeconds(2.0f);

    }
    public Animator animator;
    IEnumerator SunMove()
    {
        while (Sun.transform.position.x <= sunDistance.position.x)
        {
            yield return new WaitForSeconds(0.05f);
            Sun.transform.position = new Vector3(Sun.transform.position.x + speed, Sun.transform.position.y, 0);
            if (spotLight.intensity < 0.7f)
            {
                spotLight.intensity += 0.01f;
            }
        }
        while (Sun.transform.position.x >= sunDistance.position.x)
        {
            yield return new WaitForSeconds(0.05f);
            Sun.transform.position = sunFirstPos.position;
            PopUP.SetActive(true);
            waterSound.Play();
            yield return new WaitForSeconds(2.0f);
            SunSetClear();
            spotLight.intensity = 0f;

        }
    }
    public GameObject img;
    public void SunSetClear()
    {
        for(int i=0; i<growButton.Count; i++)
        {
            growButton[i].SetActive(true);
        }

        globalLight.intensity = 1.0f;
        psawn.CurrentExp += 3;
        itemManager.item.Sun -= 1;
        DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp = psawn.CurrentExp;
        itemManager.UseItems("Sun");
        DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        PopUP.SetActive(false);
        SunsetBakcGround.SetActive(false);
        tree.SetActive(false);
        pots.GetComponent<Image>().enabled = true;
        img.SetActive(true );
        normalBackGround.SetActive(true);
        ExitButton.SetActive(false);
        exitImages.SetActive(false);
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {

        yield return new WaitForSeconds(2f);
        itemManager.GetItem();
    }
    #endregion
    #region Nutrients
    public GameObject Panels;
    public void SetNutrients(string name)
    {
        bbt.SetActive(false);
        Panels.SetActive(true);

        for (int i =0; i<walls.Count; i++)
        {
            walls[i].SetActive(false);
        }
        if (name == "Nutrients")
        {
            nut.sprite = nuts[0];
            itemManager.item._Gnutrients -= 1;
        }
        else if (name == "RNutrients")
        {
            nut.sprite = nuts[1];
            itemManager.item._Rnutrients -= 1;
        }
        else if (name == "YNutrients")
        {
            nut.sprite = nuts[2];
            itemManager.item._Ynutrients -= 1;
        }
        else if (name == "BNutrients")
        {
            nut.sprite = nuts[3];
            itemManager.item._Bnutrients -= 1;
        }
        StartCoroutine(NutrientsSetting(name));
    }
    public GameObject Itme;
    public Image nut;
    public List<Sprite> nuts = new List<Sprite>();
    public LambdaPublic lambdaPublic;
    IEnumerator NutrientsSetting(string name)
    {
        ExitButton.SetActive(true);
        Nutrients.SetActive(true);
        NutrientsAnimator.SetTrigger(name);
        Itme.SetActive(false);
        yield return new WaitForSeconds(NutrientTime);
        Nutrients.SetActive(false);
        PopUp.SetActive(true);
        waterSound.Play();
        yield return new WaitForSeconds(1.0f);
        if (name == "Nutrients")
        {
            itemManager.UseItems("Nutrients");
            DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            psawn.CurrentExp += 3;
            DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp = psawn.CurrentExp;
            lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        }
        else if (name == "RNutrients")
        {
            itemManager.UseItems("RNutrients");
            DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            psawn.CurrentExp += 3;
            DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp = psawn.CurrentExp;
            lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        }
        else if (name == "YNutrients")
        {
            itemManager.UseItems("YNutrients");
            DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            psawn.CurrentExp += 3;
            DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp = psawn.CurrentExp;
            lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        }
        else if (name == "BNutrients")
        {
            itemManager.UseItems("BNutrients");
            DataSave.Instance._data.plantsData[DataSave.Instance.index].lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            psawn.CurrentExp += 3;
            DataSave.Instance._data.plantsData[DataSave.Instance.index].plantsExp = psawn.CurrentExp;
            lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        }
        bbt.SetActive(true);
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].SetActive(true);
        }
        PlantsSpawn.isActiveInHaerarchy = false;
        if (DataSave.Instance._data.isnutrients == false)
        {
            DataSave.Instance._data.isnutrients = true;
        }
        yield return new WaitForSeconds(0.5f);
        PopUp.SetActive(false);
        Panels.SetActive(false);
        ExitButton.SetActive(false);
        exitImages.SetActive(false);
        StartCoroutine(Delay());
    }
    public GameObject exitImages;
    #endregion
}
