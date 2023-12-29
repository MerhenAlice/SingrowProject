using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LoadingDelay : MonoBehaviour
{

    public GameObject tf;
    public float speed;
    public GameObject button;
    public GameObject Text;

    private void Start()
    {
        StartCoroutine(Delay());
    }
    private void Update()
    {
        tf.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime)); 
    }
    public void OnAppQuit()
    {
        Application.Quit();
    }
    IEnumerator Delay()
    {
        button.SetActive(false);
        tf.SetActive(true);
        Text.SetActive(true);
        yield return new WaitUntil(()=>DataSave.Instance._data.uid !=0);
        button.SetActive(true);
        tf.SetActive(false);
        Text.SetActive(false);
    }

}
