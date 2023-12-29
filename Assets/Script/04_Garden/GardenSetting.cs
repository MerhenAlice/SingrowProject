using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GardenSetting : MonoBehaviour
{
    public List<GameObject> pots = new List<GameObject>();
    public List<Sprite> potsSprite = new List<Sprite>();
    public List<GameObject> field = new List<GameObject>();
    public GameObject fieldtile;
    public List<Sprite> plants = new List<Sprite>();
    public GameObject plantImage;
    Data _data;
    public GameObject go;
    public LambdaPublic lambdaPublic;
    public GameObject popUP;
    public List<SpriteRenderer> plantss = new List<SpriteRenderer>(); 
    private void Start()
    {
        _data = DataSave.Instance._data;
        go.SetActive(false);
        for(int i =0; i<_data.plantsData.Count; i++)
        {
            for(int j = 0; j<plants.Count; j++)
            {
                if (plants[j].name == _data.plantsData[i].plantsname)
                {
                    GameObject instantPlants = Instantiate(plantImage, pots[i].transform);
                    instantPlants.GetComponent<SpriteRenderer>().sprite = plants[j];
                    instantPlants.name = _data.plantsData[i].plantsIndex.ToString();
                    instantPlants.transform.position = instantPlants.transform.position + new Vector3(0, 0.3f, 0);
                    instantPlants.transform.GetChild(0).name = instantPlants.GetComponent<SpriteRenderer>().sprite.name;
                    instantPlants.transform.GetChild(0).GetChild(0).name = _data.plantsData[i].plantsClass;
                    plantss.Add(instantPlants.GetComponent<SpriteRenderer>());
                    DataSave.Instance.plants = plantss;
                    pots[i].GetComponent<SpriteRenderer>().sprite = potsSprite[_data.plantsData[i].potsIndex];
                }
            }
        }
        go.SetActive(true);
        lambdaPublic.Invoke("PatchPlantsStart2", JsonUtility.ToJson(DataSave.Instance._data), "DataSave");
        int count = 0;
        for(int i =0; i<DataSave.Instance._data.plantsData.Count; i++)
        {
            if (DataSave.Instance._data.plantsData[i].isKing == false)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        if(count == DataSave.Instance._data.plantsData.Count)
        {
            StartCoroutine(popup());
        }
    }
    private void Update()
    {

        if (DataSave.Instance.isStudy == false && DataSave.Instance.istodaystudy == false && DataSave.Instance.isVeryGood == false)
        {
            DataSave.Instance.isStudy = DataSave.Instance.item.isStudy;
            DataSave.Instance.istodaystudy = DataSave.Instance.item.istodaystudy;
            DataSave.Instance.isVeryGood = DataSave.Instance.item.isVeryGood;
        }
    }
    IEnumerator popup()
    {
        popUP.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        popUP.SetActive(false);
    }
}
