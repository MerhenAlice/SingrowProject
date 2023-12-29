using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class WaitGetToKnow : MonoBehaviour
{
    public bool isDoingGetToKnow;
    public float sec = 60f;
    public int min=59;

    public static int sendMin;

    public Text timerText;
    public GameObject Wait;
    public GameObject FistScene;
    public GameObject startButton;
    public List<GameObject> clover = new List<GameObject>();


    private void OnEnable()
    {
        if(isDoingGetToKnow == false)
        {
            Wait.SetActive(true); 
            FistScene.SetActive(false);
        }
        else
        {
            Wait.SetActive(false);
            FistScene.SetActive(true);
        }
    }
    private void Update()
    {
        if(Wait.activeInHierarchy ==true)
        {
            if (min >= 0 && sec > 0)
            { Timer(); }
        }
        if(min ==44)
        {
            clover[0].SetActive(true);
        }
        if(min == 29)
        {
            clover[1].SetActive(true);
        }
        if(min == 14)
        {
            clover[2].SetActive(true);
        }
        if(min == -1)
        {
            clover[3].SetActive(true);
            startButton.SetActive(true);
            isDoingGetToKnow = true;
        }
    }
    private void Timer()
    {
        sec -= Time.deltaTime;
        timerText.text = "다음 고민해소까지"+string.Format("{0:D2}:{1:D2}", min, (int)sec)+"분 남았어요.";
        if((int)sec ==0)
        {
            sec = 60;
            min--;
            sendMin = min;
        }
    }
}
