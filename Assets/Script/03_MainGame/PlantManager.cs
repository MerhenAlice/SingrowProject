using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{
    #region Singletone
    private static PlantManager _instance;
    public static PlantManager Instance
    {
        get
        {
            if(_instance == null)
            {
                return null;
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField]
    private List<string> SeedName = new List<string>() { "Cactus", "Sticky", "Herb", "lavender", "Rose", "Sunflower", "Lettuce", "carrot", "Tomato", "Blueberries" };

    [SerializeField]
    private List<string> Plants = new List<string>() { "Cactus", "Sticky", "Herb", "lavender", "Rose", "Sunflower", "Lettuce", "carrot", "Tomato", "Blueberries" };

    [SerializeField]
    private List<string> Pots = new List<string>();

    [SerializeField]
    private List<int> NeedFistExp = new List<int>() { 10, 12, 10, 15, 10, 12, 10, 12, 10, 15 };

    [SerializeField]
    private List<int> NeedSecondExp = new List<int>() { 10, 15, 8, 15, 14, 14, 10, 14, 14, 15 };

    [SerializeField]
    private List<int> NeedThirdExp = new List<int>() { 10, 15, 12, 10, 24, 14, 10, 24, 12, 20 };

    [SerializeField]
    private List<int> NeedFourthExp = new List<int>() { 10, 13, 20, 10, 12, 20, 10, 10, 14, 20 };

    [SerializeField]
    private List<int> NeedFivethExp = new List<int>() { 10, 15, 20, 30, 20, 40, 10, 25, 20, 30 };
    private void Start()
    {
        SeedName = new List<string>() { "Cactus", "Sticky", "Herb", "lavender", "Rose", "Sunflower", "Lettuce", "carrot", "Tomato", "Blueberries" };
        Plants = new List<string>() { "Cactus", "Sticky", "Herb", "lavender", "Rose", "Sunflower", "Lettuce", "carrot", "Tomato", "Blueberries" };
        NeedFistExp =  new List<int>() { 10, 12, 10, 15, 10, 12, 10, 12, 10, 15 };
        NeedSecondExp = new List<int>() { 10, 15, 8, 15, 14, 14, 10, 14, 14, 15 };
        NeedThirdExp = new List<int>() { 10, 15, 12, 10, 24, 14, 10, 24, 12, 20 };
        NeedFourthExp = new List<int>() { 10, 13, 20, 10, 12, 20, 10, 10, 14, 20 };
        NeedFivethExp = new List<int>() { 10, 15, 20, 30, 20, 40, 10, 25, 20, 30 };
    }
    public string ReturnSeedName(string name)
    {
        int index = SeedName.IndexOf(name);
        return SeedName[index];
    }
    public int ReturnSeedEXPName(string name)
    {
        int index = SeedName.IndexOf(name);
        return NeedFistExp[index];
    }
    public string ReturnPlantsName(string name)
    {
        int index = Plants.IndexOf(name);
        return Plants[index];
    }
    public int ReturnPlantsSecondExp(string name)
    {
        int index = Plants.IndexOf(name);
        return NeedSecondExp[index];
    }
    public int ReturnPlantsThirdExp(string name)
    {
        int index = Plants.IndexOf(name);
        return NeedThirdExp[index];
    }
    public int ReturnPlantsFourthExp(string name)
    {
        int index = SeedName.IndexOf(name);
        return NeedFourthExp[index];
    }
    public int ReturnPlantsFifthExp(string name)
    {
        int index = Plants.IndexOf(name);
        return NeedFivethExp[index];
    }
    public int ReturnPlantsMaxExp(string name)
    {
        int MaxExp = ReturnSeedEXPName(name) + ReturnPlantsSecondExp(name) + ReturnPlantsThirdExp(name) + ReturnPlantsFourthExp(name) + ReturnPlantsFifthExp(name);
        return MaxExp;
    }
}
