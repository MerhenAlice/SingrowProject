using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infosettings : MonoBehaviour
{
    public List<Sprite> infos = new List<Sprite>();
    public Image img;


    public void Update()
    {
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("Blueberries"))
        {
            img.sprite = infos[1];
        }
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("Cactus"))
        {
            img.sprite = infos[3];

        }
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("carrot"))
        {
            img.sprite = infos[4];

        }
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("Herb"))
        {

            img.sprite = infos[2];
        }
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("lavender"))
        {

            img.sprite = infos[2];
        }
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("Lettuce"))
        {
            img.sprite = infos[4];

        }
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("Rose"))
        {

            img.sprite = infos[0];
        }
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("Sticky"))
        {

            img.sprite = infos[3];
        }
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("Sunflower"))
        {

            img.sprite = infos[0];
        }
        if (DataSave.Instance._data.plantsData[0].plantsname.Contains("Tomato"))
        {

            img.sprite = infos[1];
        }
    }
}
