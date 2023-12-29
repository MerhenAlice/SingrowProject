using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedButton : MonoBehaviour
{
    private void Update()
    {
        this.gameObject.name = gameObject.GetComponent<Image>().sprite.name;
    }

}
