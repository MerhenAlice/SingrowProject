using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//잡초뽑기 쿨타임 정보
[Serializable]
public class CoolTime
{
    public int uid;
    public string Uuid;
    public string type;
    public string endTime;
    public string coolTime;
}
//명상 쿨타임 정보
[Serializable]
public class CoolTime2
{
    public int uid;
    public string Uuid;
    public string type;
    public string endTime;
    public string coolTime;
}
public class CoolTimeSetting : MonoBehaviour
{
    public CoolTime coolTime;
    public CoolTime2 coolTime2;
    public Text weedCoolTime;
    public Text meditationCoolTime;

    public LambdaPublic lambdaPublic;

    public int min;
    public float sec;
    public float sectmp;
    public int times;
    public Button SunButton;

    public int mediMin;
    public float mediSec;
    public float mediSectmp;
    public Button MediButton;
    private void Awake()
    {
        
        if (lambdaPublic == null)
        { lambdaPublic = GameObject.FindGameObjectWithTag("LambdaPublic").GetComponent<LambdaPublic>(); }
        if (weedCoolTime == null)
        {
            weedCoolTime = GameObject.FindGameObjectWithTag("WeedCool").GetComponent<Text>();
        }
        if (meditationCoolTime == null)
        {
            meditationCoolTime = GameObject.FindGameObjectWithTag("MediCool").GetComponent<Text>();
        }
        coolTime.uid = DataSave.Instance._data.uid;
        coolTime.Uuid = DataSave.Instance._data.Uuid;
        coolTime2.uid = DataSave.Instance._data.uid;
        coolTime2.Uuid = DataSave.Instance._data.Uuid;
        OnGetInvokeAws("grass");
        OnGetInvokeAws2("meditation");
    }
    float time;
    private void FixedUpdate()
    {
            //OnGetInvokeAws("grass");
            //sectmp = int.Parse(lambdaPublic.coolTime.coolTime) / 1000;
            //OnGetInvokeAws("meditation");
            //mediSectmp = int.Parse(lambdaPublic.coolTime.coolTime) / 1000;
    }
    // Update is called once per frame
    public bool isGet = false;
    void Update()
    {
        coolTime.uid = DataSave.Instance._data.uid;
        coolTime.Uuid = DataSave.Instance._data.Uuid; 
        coolTime2.uid = DataSave.Instance._data.uid;
        coolTime2.Uuid = DataSave.Instance._data.Uuid;
        //if(isGet == false)
        //{
        //    if(DataSave.Instance._data.isweeding == true || DataSave.Instance._data.plantsData[DataSave.Instance.index].isMeditate == true)
        //    {
        //        OnGetInvokeAws("grass");
        //        OnGetInvokeAws2("meditation");
        //        isGet = true;
        //    }
        //}
        if (sectmp >0)
        {
            SunButton.enabled = false;
        }
        else if(sectmp <=0)
        {
            SunButton.enabled = true;
        }
        if (mediSectmp > 0)
        {
            MediButton.enabled = false;
        }
        else if (mediSectmp <= 0)
        {
            MediButton.enabled = true;
        }
        if (coolTime.coolTime != lambdaPublic.coolTime.coolTime)
        {
            if (coolTime.type == "grass")
            {
                coolTime = lambdaPublic.coolTime;
                sectmp = int.Parse(coolTime.coolTime) / 1000;
            }
        }
        if(coolTime2.coolTime!=lambdaPublic.cooltime2.coolTime)
        {
            if (coolTime2.type == "meditation")
            {
                coolTime2 = lambdaPublic.cooltime2;
                mediSectmp = int.Parse(coolTime2.coolTime) / 1000;
            }
        }
        if (sectmp > 0)
        {
            sectmp -= Time.deltaTime;

            sec = sectmp % 60;
            min = (int)sectmp/60%60;
            times = (int)sectmp / 3600;
            weedCoolTime.text =times.ToString()+":"+ min.ToString() + ":" + ((int)sec).ToString();
        }
        else
        {
            weedCoolTime.text = min.ToString() + " : " + ((int)sec).ToString();
        }
        if(mediSectmp>0)
        {
            mediSectmp -= Time.deltaTime;
            mediSec = mediSectmp % 60;
            mediMin = (int)mediSectmp / 60;
            meditationCoolTime.text = mediMin.ToString() + " : " + ((int)mediSec).ToString();
        }
        else
        {
            meditationCoolTime.text = mediMin.ToString() + " : " + ((int)mediSec).ToString();
        }
    }
    public void SettingEndTime()
    {
        coolTime.endTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ"); 
        coolTime2.endTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
    }
    public void OnGetInvokeAws(string type)
    {
        coolTime.type = "grass";
        lambdaPublic.Invoke("GetPlantsCoolTime2", JsonUtility.ToJson(coolTime), "coolTime");
    }
    public void OnPatchInvokeAws(string type)
    {
        ConsensusUIEvent.isMediEnd = false;
        coolTime.type = "grass";
        coolTime.endTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        lambdaPublic.Invoke("PatchPlantsEndTime2", JsonUtility.ToJson(coolTime), "coolTime");
        sectmp = 21600;
        isGet = false;
        //StartCoroutine(delay(type)); 
    }
    public void OnGetInvokeAws2(string type)
    {
        coolTime2.type = "meditation";
        lambdaPublic.Invoke("GetPlantsCoolTime2", JsonUtility.ToJson(coolTime2), "coolTime2");
    }
    public void OnPatchInvokeAws2(string type)
    {
        ConsensusUIEvent.isMediEnd = false;
        coolTime2.type = "meditation";
        coolTime2.endTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        lambdaPublic.Invoke("PatchPlantsEndTime2", JsonUtility.ToJson(coolTime2), "coolTime2");
        mediSectmp = 3600;
        isGet = false;
        //StartCoroutine(delay(type));
    }
    //IEnumerator delay(string type)
    //{
    //    yield return new WaitForSecondsRealtime(1.5f);
    //    OnGetInvokeAws(type);
    //} 
    public void SettingType(string type)
    {
        coolTime.type = "grass"; 
        coolTime2.type = "meditation";
        OnPatchInvokeAws(type);
    }
}
