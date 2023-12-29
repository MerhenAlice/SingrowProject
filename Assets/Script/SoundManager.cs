using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource bgSound;
    public AudioClip[] bgList;

    public GameObject Effectsound;
    public AudioSource effectsource;
   
    public GameObject[] buttons;
    public GameObject Medi;
    public static bool medion = false;
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(Effectsound);
    }
    private void Update()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }
        Medi = GameObject.FindGameObjectWithTag("Medi");
        if(Medi != null)
        {
            if (medion == false)
            {
                if (Medi.activeInHierarchy == true)
                {
                    BgSoundPlay(bgList[7]);
                    medion = true ;
                }
            }
        }
    }
    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i<bgList.Length; i++)
        {
            if(arg0.name == bgList[i].name)
            {
                BgSoundPlay(bgList[i]);
            }
        }
        for(int i =0; i<buttons.Length; i++) 
        {
            buttons[i].GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }
    }
    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }
    public void OnButtonClick()
    {
        effectsource.loop= false;
        effectsource.volume = 0.5f;
        effectsource.Play();
    }
}
