using UnityEngine;
using UnityEngine.UI;
using Amazon.Lambda;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon;
using System.Text;
using Amazon.Lambda.Model;
using ThirdParty.Json.LitJson;
using JetBrains.Annotations;
using System.Collections.Generic;
using System;

public class Crypto
{
    public static string DecodingBase64(string base64plainText)
    {
        byte[] strbyte = Convert.FromBase64String(base64plainText);
        return Encoding.UTF8.GetString(strbyte);
    }
}
public class LambdaPublic : MonoBehaviour
{
    private string IdentityPoolId = "ap-northeast-2:26165b16-ebe3-4555-b9eb-882e5915de31";
    private string CognitoIdentityRegion = RegionEndpoint.APNortheast2.SystemName;
    private RegionEndpoint _CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
    }
    public string LambdaRegion = RegionEndpoint.APNortheast2.SystemName;
    private RegionEndpoint _LambdaRegion
    {
        get { return RegionEndpoint.GetBySystemName(LambdaRegion); }
    }

    private static LambdaPublic instance = null;
    public static LambdaPublic Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Update()
    {
        diaryDatas.uid = DataSave.Instance._data.uid;
        diaryDatas.Uuid = DataSave.Instance._data.Uuid;
        if(diaryDatas.DiaryData.Count!=0)
        {
            for(int i =0; i<diaryDatas.DiaryData.Count; i++)
            {
                diaryDatas.DiaryData[i].diaryText = Crypto.DecodingBase64(diaryDatas.DiaryData[i].diaryText);
                for (int j = 0; j < diaryDatas.DiaryData[i].plantName.Count; j++)
                {
                    diaryDatas.DiaryData[i].plantName[j]= Crypto.DecodingBase64(diaryDatas.DiaryData[i].plantName[j]);
                }
            }
        }
    }
    public TodayQuest today;
    public CoolTime coolTime;
    public Data data;
    public List<DiaryDataRoot> diaries;
    public DiaryDatas diaryDatas;
    public CoolTime2 cooltime2;
    void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
    }
    #region private members

    private IAmazonLambda _lambdaClient;
    private AWSCredentials _credentials;

    private AWSCredentials Credentials
    {
        get
        {
            if (_credentials == null)
                _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
            return _credentials;
        }
    }

    private IAmazonLambda Client
    {
        get
        {
            if (_lambdaClient == null)
            {
                _lambdaClient = new AmazonLambdaClient(Credentials, _LambdaRegion);
            }
            return _lambdaClient;
        }
    }

    #endregion

    #region Invoke
    /// <summary>
    /// Example method to demostrate Invoke. Invokes the Lambda function with the specified
    /// function name (e.g. helloWorld) with the parameters specified in the Event JSON.
    /// Because no InvokationType is specified, the default 'RequestResponse' is used, meaning
    /// that we expect the AWS Lambda function to return a value.
    /// </summary> 
    //public string EventText = JsonUtility.ToJson(DataSave.Instance._data).ToString();
    public string ResultText = null;
    public void Invoke(string FunctionName, string EventText, string className)
    {
         Debug.Log($"Func = {FunctionName}, Evt = {EventText}, ClassN = {className}");
        ResultText = "";
        Client.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest()
        {
            FunctionName = FunctionName,
            Payload = EventText.ToString()
        },
        (responseObject) =>
        {
            ResultText += "";
            if (responseObject.Exception == null)
            {
                ResultText += Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray());
                string json = JsonUtility.ToJson(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                if(className == "item")
                {
                    DataSave.Instance.item = JsonUtility.FromJson<Item>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                if(className == "todayQuest")
                {
                    today = JsonUtility.FromJson<TodayQuest>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                    QuestManager.Instance.todayQuest = JsonUtility.FromJson<TodayQuest>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                if(className == "coolTime")
                {
                    coolTime = JsonUtility.FromJson<CoolTime>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                if (className == "coolTime2")
                {
                    cooltime2 = JsonUtility.FromJson<CoolTime2>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                if (className == "DataSave")
                {
                    data = JsonUtility.FromJson<Data>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                    DataSave.Instance._data = data;
                }
                if (className == "DataSend")
                {
                    string jsons = JsonUtility.ToJson(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                    DataSave.Instance._data = data;
                }
                if(className == "DiaryLoad")
                {
                    diaryDatas = JsonUtility.FromJson<DiaryDatas>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                    Diary.Instance.diaryDatas = diaryDatas;
                    Debug.Log("dd" + Diary.Instance.sOnly.ToString());
                }
                if (className == "DiarySend")
                {
                    string jsons = JsonUtility.ToJson(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                if(className == "DestoyAccount")
                {
                    string jsons = JsonUtility.ToJson(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
            }
            else
            {
                ResultText += responseObject.Exception;
                string json = JsonUtility.ToJson(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                if (className == "item")
                {
                    DataSave.Instance.item = JsonUtility.FromJson<Item>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                if (className == "todayQuest")
                {
                    today = JsonUtility.FromJson<TodayQuest>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                    QuestManager.Instance.todayQuest = JsonUtility.FromJson<TodayQuest>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                if (className == "coolTime")
                {
                    coolTime = JsonUtility.FromJson<CoolTime>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                if (className == "coolTime2")
                {
                    cooltime2 = JsonUtility.FromJson<CoolTime2>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                if (className == "DataSave")
                {
                    data = JsonUtility.FromJson<Data>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                    DataSave.Instance._data = data;
                }
                if (className == "DiaryLoad")
                {
                    diaryDatas = JsonUtility.FromJson<DiaryDatas>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                    Diary.Instance.diaryDatas = diaryDatas;
                    Debug.Log("dd" + Diary.Instance.sOnly.ToString());

                }
                if (className == "DiarySend")
                {
                    string jsons = JsonUtility.ToJson(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));

                }
                if (className == "DestoyAccount")
                {
                    string jsons = JsonUtility.ToJson(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
            }
        }
        );
    }

    #endregion
}
