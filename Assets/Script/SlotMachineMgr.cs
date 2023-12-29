using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineMgr : MonoBehaviour
{
    public List<string> slotText1 = new List<string>();
    public List<string> slotText2= new List<string>();

    public Text slot1;
    public Text slot2;
    public bool isClick = false;

    public Button button;

    public List<Sprite> leaves = new List<Sprite>();
    public Image leap;
    public GameObject popUp;

    public GameObject TouchPanel;

    public Button backButton;

    public GameObject EffectHeart;
    public GameObject Parents;

    public List<Sprite> Happy = new List<Sprite>();
    public List<Sprite> Default = new List<Sprite>();
    public Image plants;

    public AudioSource audioSource;
    public Image talkballon;
    public void SlotStart()
    {
        if(isClick == false)
        {
            isClick = true;
        }
        StartCoroutine(SlotStop());
        if (isClick == true)
        {
            StartCoroutine(SlotDelay());
            StartCoroutine(SlotDelay2());
            backButton.gameObject.SetActive(false);
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
    IEnumerator SlotStop()
    {
        button.interactable= false;
        yield return new WaitForSeconds(2.5f);
        isClick = false;
        slot1.text = slotText1[UnityEngine.Random.Range(0, slotText1.Count)];
        slot2.text = slotText2[UnityEngine.Random.Range(0, slotText2.Count)];
        audioSource.Play();
        talkballon.gameObject.SetActive(true);
        GameObject go = Instantiate(EffectHeart, Parents.transform);
        for(int i = 0; i<Happy.Count; i++)
        {
            if(plants.name == Happy[i].name)
            {
                plants.sprite = Happy[i];
            }
        }
        yield return new WaitForSeconds(2.5f);
        Destroy(go);
        talkballon.gameObject.SetActive(false);
        for (int i = 0; i < Default.Count; i++)
        {
            if (plants.name == Default[i].name)
            {
                plants.sprite = Default[i];
            }
        }
        button.interactable = true;
        ConsensusUIEvent.complimentcount += 1;
        if(ConsensusUIEvent.complimentcount == 5)
        {
            button.interactable = false;
            if (DataSave.Instance._data.plantsData[DataSave.Instance.index].isComplimet == false)
            { ConsensusUIEvent.leapCount += 1; }
            yield return new WaitForSeconds(2.0f);
            this.gameObject.SetActive(false);
            leap.sprite = leaves[ConsensusUIEvent.leapCount];
            popUp.SetActive(true);
            ConsensusUIEvent.complimentcount = 0;
            backButton.gameObject.SetActive(false) ;
        }
        
    }
}
