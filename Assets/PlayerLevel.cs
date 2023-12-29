using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public Image image;
    void Update()
    {
        if(DataSave.Instance._data.GiveData >=1&& DataSave.Instance._data.GiveData < 10)
        {
            image.sprite = sprites[0];
            image.SetNativeSize();
        }
        else if(DataSave.Instance._data.GiveData>=10&& DataSave.Instance._data.GiveData < 30)
        {
            image.sprite = sprites[1];
            image.SetNativeSize();
        }
        else if (DataSave.Instance._data.GiveData >= 30 && DataSave.Instance._data.GiveData < 100)
        {
            image.sprite = sprites[2];
            image.SetNativeSize();
        }
        else if (DataSave.Instance._data.GiveData >= 100)
        {
            image.sprite = sprites[3];
            image.SetNativeSize();
        }
    }
}
