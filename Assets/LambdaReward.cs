using UnityEngine;
using UnityEngine.UI;
using Amazon.Lambda;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Amazon;
using System.Text;
using System.IO;
using Amazon.Lambda.Model;
using ThirdParty.Json.LitJson;
using JetBrains.Annotations;
using System.Drawing;

public class LambdaReward : MonoBehaviour
{
    public string IdentityPoolId = "ap-northeast-2:26165b16-ebe3-4555-b9eb-882e5915de31";
    public string CognitoIdentityRegion = RegionEndpoint.APNortheast2.SystemName;
    private RegionEndpoint _CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
    }
    public string LambdaRegion = RegionEndpoint.APNortheast2.SystemName;
    private RegionEndpoint _LambdaRegion
    {
        get { return RegionEndpoint.GetBySystemName(LambdaRegion); }
    }


    public Button InvokeButton = null;
    public Button ListFunctionsButton = null;
    public string FunctionNameText = "RewardPlantsItem2";

    public string EventText;
    public string ResultText = null;
    public bool isOn = false;
    public bool isRecive = false;
    public RewardSetting rewardSetting;
    private void Update()
    {

        //EventText = JsonUtility.ToJson(rewardSetting.reward);
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
    public void Invoke()
    {

        ResultText = "";
        Client.InvokeAsync(new Amazon.Lambda.Model.InvokeRequest()
        {
            FunctionName = FunctionNameText,
            Payload = EventText.ToString()
        },
        (responseObject) =>
        {
            ResultText += "";
            if (responseObject.Exception == null)
            {
                ResultText += Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray());
                string json = JsonUtility.ToJson(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                DataSave.Instance.item = JsonUtility.FromJson<Item>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
            }
            else
            {
                ResultText += responseObject.Exception;
                string json = JsonUtility.ToJson(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                DataSave.Instance.item = JsonUtility.FromJson<Item>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
            }

        }
        );
        Debug.Log($"IdentityPoolId ={IdentityPoolId} ,CognitoIdentityRegion={CognitoIdentityRegion}, EventText = {EventText} FunctionNameText = {FunctionNameText}");
    }

    #endregion

    #region List Functions
    /// <summary>
    /// Example method to demostrate ListFunctions
    /// </summary>
    public void ListFunctions()
    {
        ResultText = "Listing all of your Lambda functions... \n";
        Client.ListFunctionsAsync(new Amazon.Lambda.Model.ListFunctionsRequest(),
        (responseObject) =>
        {
            ResultText += "\n";
            if (responseObject.Exception == null)
            {
                ResultText += "Functions: \n";
                foreach (FunctionConfiguration function in responseObject.Response.Functions)
                {
                    ResultText += "    " + function.FunctionName + "\n";
                }
            }
            else
            {
                ResultText += responseObject.Exception + "\n";
            }
        }
        );
    }

    #endregion
}
