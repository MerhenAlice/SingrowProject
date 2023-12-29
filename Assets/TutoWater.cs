using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoWater : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Distance;
    public GameObject water;
    public GameObject waterFall;
    public float speed; 
    private float fDestroyTime = 3f;
    private float fTickTime;
    public Image panel;
    public Sprite sprite;
    private void Update()
    {
        if (water.transform.position.x < Distance.transform.position.x)
        {
            Vector2 target = new Vector2(water.transform.position.x - 0.1f, water.transform.position.y);
            water.transform.position = Vector2.MoveTowards(water.transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            waterFall.SetActive(true);
            fTickTime += Time.deltaTime;
            panel.sprite = sprite;
            if (fTickTime >= fDestroyTime)
            {
                Panel1.SetActive(false);
                Panel2.SetActive(true);
            }
        }
    }
    


}
