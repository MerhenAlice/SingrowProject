using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform canvas;
    public Transform previousPrents;
    public RectTransform rect;
    public CanvasGroup canvasGroup;

    private Transform _pos_image;
    private float posX;
    private float posY;

    private float width = 1333;
    private float height = 800;

    private Vector3 vInterval;


    private void Awake()
    {
        canvas = FindAnyObjectByType<Canvas>().transform;
        rect = GetComponent<RectTransform>();
        canvasGroup=GetComponent<CanvasGroup>();
        _pos_image = GetComponent<Transform>();
        posX = _pos_image.position.x;
        posY = _pos_image.position.y;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        previousPrents = transform.parent;

        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        Vector3 pos = eventData.position;
        vInterval = pos - transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = eventData.position;
        rect.position = pos - vInterval;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent == canvas)
        {
            transform.SetParent(canvas);
            if (transform.parent.name == "StickDrop")
            {
                rect.position = previousPrents.GetComponent<RectTransform>().position;
                //ConsensusUIEvent._e_heart_count += 1;
            }
        }

        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true; 
        if (transform.parent.name == "StickDrop")
        {
            this.GetComponent<Image>().raycastTarget = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "tagSticker")
        {
            this.transform.position = new Vector3(posX, posY);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "tagSticker")
        {
            this.transform.position = new Vector3(posX, posY);
        }
    }
}
