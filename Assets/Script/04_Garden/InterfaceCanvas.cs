using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceCanvas : MonoBehaviour
{
    public Transform parents;
    public Image image;
    public Camera camera;
    public Button btn;
    public Sprite sp1;
    public Sprite sp2;
    public int index = 0;
    private void Start()
    {
        camera= Camera.main;
        image.transform.position = camera.WorldToScreenPoint(parents.position - new Vector3(0f, -2.6f, 0));
    }
    private void Update()
    {
        if (DataSave.Instance._data.plantsData[int.Parse(transform.parent.gameObject.name)].isKing == false)
        {
            btn.GetComponent<Image>().sprite = sp1;
            btn.enabled = true;
        }
        else
        {
            btn.GetComponent<Image>().sprite = sp2;
            btn.enabled = false;
        }
    }
    private string NameChager(string name)
    {
        if (name.Contains("Blueberries"))
        {
            return "P000";
        }
        if (name.Contains("Cactus"))
        {
            return "P001";
        }
        if (name.Contains("carrot"))
        {
            return "P002";
        }
        if (name.Contains("Herb"))
        {
            return "P003";
        }
        if (name.Contains("lavender"))
        {
            return "P004";
        }
        if (name.Contains("Lettuce"))
        {
            return "P005";
        }
        if (name.Contains("Rose"))
        {
            return "P006";
        }
        if (name.Contains("Sticky"))
        {
            return "P007";
        }
        if (name.Contains("Sunflower"))
        {
            return "P008";
        }
        if (name.Contains("Tomato"))
        {
            return "P009";
        }
        return null;
    }
    public void GetIndex(GameObject go)
    {
        DataSave.Instance.plantPickInGauard = go.GetComponent<SpriteRenderer>().sprite.name; 
        DataSave.Instance.plantsImgName = go.GetComponent<SpriteRenderer>().sprite.name;
        DataSave.Instance.plantsImgName = transform.parent.GetComponent<SpriteRenderer>().sprite.name;

        DataSave.Instance.index = int.Parse(transform.parent.gameObject.name);
        DataSave.potindex = DataSave.Instance._data.plantsData[DataSave.Instance.index].potsIndex;
        DataSave.Instance.StairTemp = transform.GetChild(0).name;
        GardenManager.plantsindex = int.Parse(go.name);
    }
    public void SetPopUP(GameObject go)
    {
        DataSave.Instance._data.plantsData[int.Parse(transform.parent.gameObject.name)].isKing = true;
        for (int i = 0; i < DataSave.Instance._data.plantsData.Count; i++)
        {
            if (DataSave.Instance._data.plantsData[i].plantsIndex == int.Parse(transform.parent.gameObject.name))
            {

                btn.GetComponent<Image>().sprite = sp2;
                btn.enabled = false;
                DataSave.Instance._data.plantsData[int.Parse(transform.parent.gameObject.name)].isKing = true;
                DataSave.Instance.kingIndex = i;
                DataSave.Instance.isKingBoolean = true;
            }
            else
            {
                DataSave.Instance._data.plantsData[i].isKing = false;
                btn.GetComponent<Image>().sprite = sp1;
                btn.enabled = true;
            }
        }
        Debug.Log("Function = SetPopUp, Boolean = true");
        StartCoroutine(SetpopUp(go));
    }
    IEnumerator SetpopUp(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        go.SetActive(false);
    }
    public void OnClickGive(SpriteRenderer img)
    {
        DataSave.Instance.plantsImgName = img.sprite.name;
        LoadingSceneController.LoadScean("06_GivePlants");
    }
    public void OnClickREset()
    {
        DataSave.Instance.plants.Clear();
    }
}

