using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestLink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public EventSystem es;
    public GameObject Quest;
    // Update is called once per frame
    void Update()
    {
        if(es.currentSelectedGameObject.name == Quest.name)
        {
            Quest.SetActive(true);
        }
    }
}
