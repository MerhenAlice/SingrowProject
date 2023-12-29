using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonDontShow : MonoBehaviour
{
    public GameObject plantsPopUp;
    public GameObject QuestPopUp;
    public GameObject ItemPopUp;
    public GameObject ExitButton;
    void Update()
    {
        if (plantsPopUp.activeInHierarchy == true || QuestPopUp.activeInHierarchy == true || ItemPopUp.activeInHierarchy == true)
        {
            ExitButton.SetActive(false);
        }
        else
        {
            ExitButton.SetActive(true);
        }
    }
}
