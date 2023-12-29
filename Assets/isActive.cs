using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isActive : MonoBehaviour
{
    public GameObject game;
    public GameObject game2;
    void Update()
    {
        if(game.activeInHierarchy == true)
        {
            game2.SetActive(false);
        }
    }
}
