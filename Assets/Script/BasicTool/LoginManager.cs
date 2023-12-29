using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using System;
using System.Threading.Tasks;

//로그인 데이터 정보
[Serializable]
public class LoginData
{
    public string localUserId;
    public string passward;
    public string localUserName;
    public string loginType;
    public string TypeCode;
}
//회원가입 정보
[Serializable]
public class SignInData
{
    public string localUserId;
    public string localUserName;
    public string passward;
    public string loginType;
    public string TypeCode;
}
public class LoginManager : MonoBehaviour
{
    #region SingleTone 
    //싱글톤 
    private static LoginManager instance = null;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }    
        else
        {
            Destroy(this);
        }

    }
    public static LoginManager Instance
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
    #endregion
    public Text LoginText;
    public Text LoginText2;
    public LoginData loginData;
    public SignInData signInData;
    public bool isSceneLoad = false;

    public GameObject customLoginPanel;
    public InputField customLogin_ID_InputField;
    public InputField customLogin_PW_InputField;
    public InputField customLogin_Name_InputField;
    public GameObject LoginErrors;

    public GameObject customSignInPanel;
    public InputField customSignIn_ID_InputField;
    public InputField customSignIn_PW_InputField;
    public InputField customSignIn_Name_InputField;
    public GameObject SignInErrors;
    public GameObject SignErrorsEmpty;

    public LoginLambda loginLambda;

    private void Update()
    {
        if(customLoginPanel.activeInHierarchy == false)
        {
            customLogin_ID_InputField.text = string.Empty;
            customLogin_PW_InputField.text = string.Empty;

        }
        if(customSignInPanel.activeInHierarchy == false)
        {
            customSignIn_ID_InputField.text = string.Empty;
            customSignIn_PW_InputField.text = string.Empty;
            customSignIn_Name_InputField.text = string.Empty;
        }
    }
    private void Start()
    {

    }
    //public void GooglePlayLogin()
    //{
        
    //    Social.localUser.Authenticate((bool succes) =>
    //        {
    //            if (succes)
    //            {
                    
    //                LoginText.text = $"{Social.localUser.id}";
    //                LoginText2.text = $"{Social.localUser.userName} ";
    //                loginData.loginType = "Google";
    //            }
    //            else
    //            {
    //                Debug.Log("Failed");
    //            }
    //        }
    //        ); 
    //    if (loginData.localUserId == string.Empty)
    //    {

    //        loginData.localUserId = LoginText.text.ToString();
    //        loginData.localUserName = LoginText2.text.ToString();
    //    }
    //    Debug.Log($"{loginData.localUserId}, {loginData.localUserName}");
    //    if (loginData.localUserId != string.Empty)
    //    {
    //        if (isSceneLoad == false)
    //        {

    //            LoadingSceneController.LoadScean("00_Main");
    //            isSceneLoad = true;
    //        }
    //    }
    //}
    //public void GooglePlayLogOut()
    //{
    //    ((PlayGamesPlatform)Social.Active).SignOut();
    //    loginData.localUserName = "";
    //    loginData.localUserId = "";
    //}

    //커스텀로그인 버튼 클릭
    public void OnClickCustomLogin()
    {
        customLoginPanel.SetActive(true);
    }
    //커스텀 로그인 로직
    public  void CustomLogin()
    {
        LoginText.text = customLogin_ID_InputField.text;
        loginData.localUserId = customLogin_ID_InputField.text.ToString();
        loginData.passward = customLogin_PW_InputField.text.ToString();
        loginData.loginType = "Custom";
        if (loginData.localUserId != string.Empty)
        {
            loginLambda.EventText = JsonUtility.ToJson(loginData);
            loginLambda.FunctionNameText = "GetPlantsLogin";
            loginLambda.Invoke("Login");
        }
        loginData.localUserId = customLogin_ID_InputField.text.ToString();
        loginData.passward = customLogin_PW_InputField.text.ToString();
        loginData.loginType = "Custom";
        StartCoroutine(LoginError());
        StartCoroutine(SceneLoad());
    }
    //회원가입 버튼 클릭
    public void OnClickCustomSignIn()
    {
        customSignInPanel.SetActive(true);
    }
    //회원가입 로직
    public void CustomSignIn()
    {
        LoginText.text = customSignIn_ID_InputField.text;
        signInData.localUserId = customSignIn_ID_InputField.text.ToString();
        signInData.passward = customSignIn_PW_InputField.text.ToString();
        signInData.localUserName = customSignIn_Name_InputField.text.ToString();
        signInData.loginType = "Custom";
        if (signInData.localUserId != string.Empty && signInData.localUserName != string.Empty && signInData.passward != string.Empty)
        {
            loginLambda.EventText = JsonUtility.ToJson(signInData);
            loginLambda.FunctionNameText = "PatchPlantsSignUp";
            loginLambda.Invoke("SignIn");
        }
        else
        {
            StartCoroutine(SignErro2r());
        }
        signInData.localUserId = customSignIn_ID_InputField.text.ToString();
        signInData.passward = customSignIn_PW_InputField.text.ToString();
        signInData.localUserName = customSignIn_Name_InputField.text.ToString();
        signInData.loginType = "Custom";
        if(signInData.localUserId != string.Empty&&signInData.localUserName !=string.Empty&&signInData.passward !=string.Empty) 
        {
            StartCoroutine(SignError());
            StartCoroutine(SignIn());
        }
    }
    //씬로드
    IEnumerator SceneLoad()
    {
        yield return new WaitUntil(() => loginData.TypeCode =="200");
        if (loginData.localUserId != string.Empty)
        {
            if (isSceneLoad == false)
            {

                LoadingSceneController.LoadScean("00_Main");
                isSceneLoad = true;
            }
        }
    }
    IEnumerator SignIn()
    {
        yield return new WaitUntil(() => signInData.TypeCode == "200");
        if(signInData.localUserId != string.Empty)
        {
            loginData.localUserId = customSignIn_ID_InputField.text.ToString();
            loginData.passward = customSignIn_PW_InputField.text.ToString();
            loginData.TypeCode = signInData.TypeCode;
        }
        StartCoroutine(SceneLoad());
    }
    IEnumerator SignError()
    {
        yield return new WaitUntil(() => signInData.TypeCode == "401");

        SignInErrors.SetActive(true);

        yield return new WaitForSeconds(0.7f);
        SignInErrors.SetActive(false);

    }
    IEnumerator SignErro2r()
    {

        SignErrorsEmpty.SetActive(true);

        yield return new WaitForSeconds(0.7f);
        SignErrorsEmpty.SetActive(false);

    }
    IEnumerator LoginError()
    {
        yield return new WaitUntil(() => loginData.TypeCode == "400");

        LoginErrors.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        LoginErrors.SetActive(false);
    }
    public void OnClickBackButton(GameObject _object)
    {
        _object.SetActive(false);
    }
}
