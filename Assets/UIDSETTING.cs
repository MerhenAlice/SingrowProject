using AWSSDK.Examples;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
public class UIDSettingClass
{
    public List<string> uidOn = new List<string>();
    public List<int> uidIntOn = new List<int>();
    public string uidStr;
    public int uidInt;

}
public class UIDSETTING : MonoBehaviour
{
    
    public bool isUidIsNull;
    public LambdaExample2 example2;
    UIDSettingClass uIDSettingClass;
    public void Start()
    {
        if (uIDSettingClass.uidOn.Contains(uIDSettingClass.uidStr))
        {
            for(int i =0; i< uIDSettingClass.uidOn.Count; i++)
            {
                if (uIDSettingClass.uidOn[i] == uIDSettingClass.uidStr)
                {
                    uIDSettingClass.uidInt = uIDSettingClass.uidIntOn[i];
                }
            }
        }
        else
        {
            uIDSettingClass.uidInt = uIDSettingClass.uidIntOn.Last() + 1;
            uIDSettingClass.uidIntOn.Add(uIDSettingClass.uidInt);
            uIDSettingClass.uidOn.Add(uIDSettingClass.uidStr);
        }
    }
    public async void startInvoke()
    {
        var task = Task.Run(() =>  example2.Invoke() );
        uIDSettingClass = await task;
    }
}
