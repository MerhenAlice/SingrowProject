using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
    public GameObject seedSelect;
    public GameObject MainGame;
    public List<GameObject> gameObjects= new List<GameObject>();
    public List<GameObject> gameObjects2 = new List<GameObject>();
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "03_MainGame")
        {
            if(seedSelect != null&&MainGame != null) { 
            if (DataSave.Instance._data.Uuid != string.Empty)
            {
                seedSelect.SetActive(false);
                MainGame.SetActive(true);
            } 
            else
            {
                seedSelect.SetActive(true);
                MainGame.SetActive(false);
            }
            }
        }
    }
    public void UIOff(GameObject ActiveOffUI)
    {
        ActiveOffUI.SetActive(false);
    }
    public void UION(GameObject ActiveOnUI)
    {
        ActiveOnUI.SetActive(true);
    }
    public bool ison = false;
    public void InfoUIOnOFF(GameObject UI)
    {
        if(ison == false)
        {
            UI.SetActive(true);
            ison= true;
        }
        else if(ison == true)
        {
            UI.SetActive(false);
            ison = false;
        }
    }
    public void ResetUI()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].SetActive(false);
        }
        for(int i =0; i<gameObjects2.Count;i++)
        {
            gameObjects2[i].SetActive(true);
        }
    }
    public void CloverReset()
    {
        CatchClover.isCatchClover= false;
    }
    public void OnAppQuit()
    {
        Application.Quit();
    }
    public List<GameObject> popUp = new List<GameObject>();
    public void popupReset()
    {
        if(popUp.Count > 0)
        {
            for(int i =0; i< popUp.Count;i++)
            {
                popUp[i].SetActive(false);  
            }
        }
    }
    public void UnloadsScen()
    {
        SceneManager.UnloadScene(gameObject.scene);
    }
    public void ButtonClickRootine(GameObject go)
    {
        for(int i = 0; i<3; i++)
        {
            go.SetActive(true);
        }
    }
}
