using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public List<string> seedName = new List<string>();
    public PlantsData plantsData = new PlantsData();
    public LambdaPublic lambdaPublic;
    public void MainSceneChange(GameObject go)
    {
        if (DataSave.Instance._data.uid == 0)
        {
            go.SetActive(true);
        }
        else
        {

            if (DataSave.Instance._data.plantsData.Count != 0)
            {
                LoadingSceneController.LoadScean("04_Garden");
            }
            else
            {
                RandomSeed();
                ChangeScenefirst("07_Tutorial");
            }
        }
    }
    public void OnPopup(GameObject popup)
    {
        popup.SetActive(true );
    }
    public void ExitPopUp(GameObject popup)
    {
        popup?.SetActive(false );
    }
    public void OnAppQuit()
    {
        Application.Quit();
    }
    public void RandomSeed()
    {
        int index = UnityEngine.Random.Range(0, seedName.Count);
        plantsData.plantsname = seedName[index]+"0";
        plantsData.plantsExp = 0;
        plantsData.plantsClass = "0";
        plantsData.plantsStairExp = 10;
        plantsData.pots = null;
        plantsData.plantsIndex = DataSave.Instance._data.plantsData.Count;
        plantsData.plantsIdentification = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");
        plantsData.isSell = false;
        plantsData.lastExpDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        plantsData.isKing = true;
        DataSave.Instance.isFirst = true;
        DataSave.Instance._data.plantsData.Add(plantsData);

        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        Debug.Log("RandomSeed" + DataSave.Instance._data.ToString());
    }
  

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
        //MainSceneChange();
    }
    public void Starts()
    {
        StartCoroutine(Delay());
    }
    public void ChangeScenefirst(string sceneName)
    {
        LoadingSceneController.LoadScean(sceneName);
    }
    public void LoadSceneAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
    public PlnatsDead plnatsDead;

    public void Resets()
    {
    }
}
