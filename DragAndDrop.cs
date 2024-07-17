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

    [SerializeField] private Transform cementryCanvas;

    /*void Start()
    {
        Invoke("GetCemCan", 1);
        cementryCanvas = GetCemCan();
    }
    private Transform GetCemCan()
    {
        return transform.Find("cementryCanvas").transform;
    }*/

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedIcon = gameObject;

        startPosition = transform.position;
        startParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.SetParent(onDragParent);
        transform.SetAsLastSibling();
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

        if (transform.parent == onDragParent)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        if (transform.parent.name == "cementry")
        {
            transform.SetParent(cementryCanvas);
        }
        transform.SetAsFirstSibling();
    }
}
