using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinPopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
    }
}
