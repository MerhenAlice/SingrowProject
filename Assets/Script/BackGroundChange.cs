using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChange : MonoBehaviour
{
    public SpriteRenderer BackGround;
    public List<Sprite> bgList= new List<Sprite>();
    public LambdaPublic lambdaPublic;

    public void ChangeBG(int index)
    {
        BackGround.sprite = bgList[index];
        DataSave.Instance.bgIndex = index;
        DataSave.Instance._data.BGIndex = index; 
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");

    }
}
