using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class tutoslot : MonoBehaviour
{
    public List<string> slotText1 = new List<string>();
    public List<string> slotText2 = new List<string>();
    public Text slot1;
    public Text slot2;
    public bool isClick = false;
    public GameObject TouchPanel;
    public GameObject heartBar;
    public GameObject heartBar2;
    public GameObject EffectHeart;
    public GameObject Parents;

    public GameObject panel1;
    public GameObject panel2;

    public AudioSource AudioSource;
    public void SlotStart()
    {
        if (isClick == false)
        {
            isClick = true;
        }
        StartCoroutine(SlotStop());
        if (isClick == true)
        {
            StartCoroutine(SlotDelay());
            StartCoroutine(SlotDelay2());
        }
        else
        {
            StopCoroutine(SlotDelay());
            StopCoroutine(SlotDelay2());
        }
    }
    IEnumerator SlotDelay()
    {
        TouchPanel.SetActive(false);
        for (int i = 0; i < slotText1.Count; i++)
        {
            if (isClick == true)
            {
                slot1.text = slotText1[i];
                if (i == slotText1.Count - 1)
                {
                    i = 0;
                }
            }
            else
            {
                i = slotText1.Count - 1;
            }
            yield return new WaitForSeconds(0.1f);
        }
        TouchPanel.SetActive(true);
    }
    IEnumerator SlotDelay2()
    {
        for (int i = 0; i < slotText2.Count; i++)
        {
            if (isClick == true)
            {
                slot2.text = slotText2[i];
                if (i == slotText2.Count - 1)
                {
                    i = 0;
                }
            }
            else
            {
                i = slotText2.Count - 1;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public Image plants;
    public Sprite sprite;
    public GameObject gos;
    IEnumerator SlotStop()
    {
        yield return new WaitForSeconds(2.5f);
        isClick = false;
        AudioSource.Play();
        GameObject go = Instantiate(EffectHeart, Parents.transform);
        plants.sprite = sprite;
        yield return new WaitForSeconds(2.5f);
        heartBar.SetActive(false);
        heartBar2.SetActive(true);
        Destroy(go);
        gos.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        panel1.SetActive(false);
        panel2.SetActive(true);

    }
}
