using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatchClover : MonoBehaviour
{
    public Text TimeSetting;
    public float limiteTime;
    public static bool isCatchClover = false;
    public Scene thisScene;
    public GameObject ThisScene;
    public GameObject NextScene;
    private void Update()
    {
        if (limiteTime > 0)
        { limiteTime -= Time.deltaTime; }
        else
        {
            ConsensusUIEvent.leapCount++;
                isCatchClover = true;
                DataSave.Instance._data.plantsData[DataSave.Instance.index].isCatchClover = true;
            ThisScene.SetActive(false);
            NextScene.SetActive(true);
        }
        TimeSetting.text = Mathf.Round(limiteTime).ToString()+"√  ≥≤¿Ω";
    }
    private void OnEnable()
    {
        limiteTime = 30f;
    }
    public void Boom(GameObject go)
    {
        //go.GetComponent<BezierTransform>().enabled = false;
        go.SetActive(false);
        go.GetComponent<Image>().color = new Color(0,0,0,0);
        StartCoroutine(Delay(go));
    }
    IEnumerator Delay(GameObject go)
    {
        yield return new WaitForSeconds(1.5f);
        go.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        go.GetComponent<Image>().color = new Color(1, 1, 1, 1);

    }
}
