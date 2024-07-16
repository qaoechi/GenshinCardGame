using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//https://codeposting.tistory.com/entry/Unity-UI-Object-Drag-Tutorial

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject draggedIcon;
    Vector3 startPosition;

    [SerializeField] public Transform onDragParent;

    [HideInInspector] public Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedIcon = gameObject;

        startPosition = transform.position;
        startParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.SetParent(onDragParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = Input.mousePosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            transform.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedIcon = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if(transform.parent == onDragParent)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }
    //public RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        //rectTransform = FindObjectOfType(typeof(RectTransform)) as RectTransform;
        //rectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            rectTransform.anchoredPosition = Input.mousePosition;
        }*/
    }
}
