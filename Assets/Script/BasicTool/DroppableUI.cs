using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler,IPointerExitHandler
{
    public Image image;
    public RectTransform rect;
    public Image plants;
    public List<Sprite> Sad = new List<Sprite>();
    public List<Sprite> Happy = new List<Sprite>();
    public List<Sprite> Defalt = new List<Sprite>();
    public GameObject Heart;
    public GameObject Rain;
    public Transform parent;
    public GameObject obj;
    public List<GameObject> heart = new List<GameObject>();

    public GameObject emotion;
    public AudioSource source;
    public List<AudioClip> clip = new List<AudioClip>();
    private void Update()
    {
        if(emotion.activeInHierarchy == false)
        {
            for(int i =0; i<heart.Count; i++)
            {
                Destroy(heart[i]);
                heart.RemoveAt(i);
            }
        }
    }
    private void Awake()
    {
        image= GetComponent<Image>();
        rect= GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }
    public void OnPointerExit(PointerEventData eventData)
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.gameObject.tag == "Positive")
            {
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
                StartCoroutine(HEmotionchange());
                obj.SetActive(true);
                ConsensusUIEvent._e_heart_count++;
                source.clip = clip[0];
                source.Play();
            }
            else if (eventData.pointerDrag.gameObject.tag == "Negative")
            {
                eventData.pointerDrag.gameObject.SetActive(false);
                StartCoroutine(Emotionchange());

                source.clip = clip[1];
                source.Play();
            }
        }
    }
    IEnumerator Emotionchange()
    {
        if (plants.name != string.Empty)
        {
            for (int i = 0; i < Sad.Count; i++)
            {
                if (plants.name == Sad[i].name)
                {
                    plants.sprite = Sad[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < Sad.Count; i++)
            {
                if (plants.name == Sad[i].name)
                {
                    plants.sprite = Sad[i];
                }
            }
        }
        GameObject objs = Instantiate(Rain, parent);
        heart.Add(objs);
        yield return new WaitForSeconds(2.0f);
        if (plants.name != string.Empty)
        {
            for (int i = 0; i < Defalt.Count; i++)
            {
                if (plants.name == Defalt[i].name)
                {
                    plants.sprite = Defalt[i];
                    obj.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < Defalt.Count; i++)
            {
                if (plants.name == Defalt[i].name)
                {
                    plants.sprite = Defalt[i];
                    obj.SetActive(false);
                }
            }
        }
    }
    IEnumerator HEmotionchange()
    {
        if (plants.name != string.Empty)
        {
            for (int i = 0; i < Happy.Count; i++)
            {
                if (plants.name == Happy[i].name)
                {
                    plants.sprite = Happy[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < Happy.Count; i++)
            {
                if (plants.name == Happy[i].name)
                {
                    plants.sprite = Happy[i];
                }
            }
        }
        GameObject objs = Instantiate(Heart, parent);
        heart.Add(objs);
        yield return new WaitForSeconds(2.0f);
        if (plants.name != string.Empty)
        {
            for (int i = 0; i < Defalt.Count; i++)
            {
                if (plants.name == Defalt[i].name)
                {
                    plants.sprite = Defalt[i];
                    obj.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < Defalt.Count; i++)
            {
                if (plants.name == Defalt[i].name)
                {
                    plants.sprite = Defalt[i];
                    obj.SetActive(false);
                }
            }
        }
    }
}
