using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDelay : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        button.enabled= false;
        yield return new WaitForSeconds(2.0f);
        button.enabled= true;
    }
}
