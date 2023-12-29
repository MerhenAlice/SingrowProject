using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image Box;
    public Image BackEffet;
    public Image PlantsInfo; 
    public List<Sprite> boxSprite = new List<Sprite>();
    public void BoxOpen()
    {
        BackEffet.gameObject.SetActive(true);
        Box.sprite = boxSprite[1];
        Box.gameObject.GetComponent<Button>().enabled = false;
        StartCoroutine (OpenBox());
    }
    IEnumerator OpenBox()
    {
        yield return new WaitForSeconds(1.0f);
        PlantsInfo.gameObject.SetActive(true);

    }
}
