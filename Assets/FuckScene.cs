using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuckScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.09f);
        SceneManager.LoadScene("00_Main");
    }
}
