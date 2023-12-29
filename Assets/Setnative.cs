using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Setnative : MonoBehaviour
{
    private void Update()
    {
        this.gameObject.GetComponent<Image>().SetNativeSize();
    }
}
