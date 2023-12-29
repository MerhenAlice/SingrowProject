using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenManager : MonoBehaviour
{
    #region SingleTon
    private static GardenManager instance = null;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static GardenManager Instance
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
    #endregion
    public static int plantsindex=0;

    public void GetIndex(GameObject go)
    {
        DataSave.Instance.plantPickInGauard = go.GetComponent<SpriteRenderer>().sprite.name; 
        DataSave.Instance.plantsImgName = go.GetComponent<SpriteRenderer>().sprite.name;
        plantsindex = int.Parse(go.name);
    }
}
