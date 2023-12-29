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

namespace AWSSDK.Examples
{

    public class LambdaExample1 : MonoBehaviour
    {
#if UNITY_ANDROID
        public void UsedOnlyForAOTCodeGeneration()
        {
            //Bug reported on github https://github.com/aws/aws-sdk-net/issues/477
            //IL2CPP restrictions: https://docs.unity3d.com/Manual/ScriptingRestrictions.html
            //Inspired workaround: https://docs.unity3d.com/ScriptReference/AndroidJavaObject.Get.html

            AndroidJavaObject jo = new AndroidJavaObject("android.os.Message");
            int valueString = jo.Get<int>("what");
            string stringValue = jo.Get<string>("what");
        }
#endif
        public string IdentityPoolId = "ap-northeast-2:26165b16-ebe3-4555-b9eb-882e5915de31";
        public string CognitoIdentityRegion = RegionEndpoint.APNortheast2.SystemName;
        public InputField input;
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
        public string FunctionNameText;
        public string EventText;
        public string ResultText = null;
        public bool isTest = false;
        public bool isOn = false;
        private void Awake()
        {

            EventText = JsonUtility.ToJson(DataSave.Instance._data);
        }
        void Start()
        {
            EventText = JsonUtility.ToJson(DataSave.Instance._data);
            UnityInitializer.AttachToGameObject(this.gameObject);
            Amazon.AWSConfigs.HttpClient = Amazon.AWSConfigs.HttpClientOption.UnityWebRequest;
            if (isTest == true)
            {
                EventText = JsonUtility.ToJson(DataSave.Instance._data);
            }
            //Invoke();
        }
        private void Update()
        {
            EventText = JsonUtility.ToJson(DataSave.Instance._data);
            if( DataSave.Instance._data.Uuid != string.Empty)
            {
                EventText = JsonUtility.ToJson(DataSave.Instance._data);
                if (isOn ==false)
                {

                    EventText = JsonUtility.ToJson(DataSave.Instance._data);
                    Invoke();
                    isOn = true;
                }
            }
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
            Debug.Log("Invoke");
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
                    DataSave.Instance._data = JsonUtility.FromJson<Data>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
                else
                {
                    ResultText += responseObject.Exception; 
                    DataSave.Instance._data = JsonUtility.FromJson<Data>(Encoding.ASCII.GetString(responseObject.Response.Payload.ToArray()));
                }
            }
            );
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
}
