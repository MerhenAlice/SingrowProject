using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGDataSave : MonoBehaviour
{
    public SpriteRenderer BackGround;
    public List<Sprite> bgList = new List<Sprite>();

    private void Awake()
    {
        BackGround.sprite = bgList[DataSave.Instance._data.BGIndex];
    }
}
