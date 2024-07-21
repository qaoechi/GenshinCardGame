using UnityEngine;
using UnityEngine.EventSystems;

//https://codeposting.tistory.com/entry/Unity-UI-Object-Drag-Tutorial

public class Slot : MonoBehaviour, IDropHandler
{

    GameObject Icon()
    {
        if (transform.Find("card"))
        {
            return transform.Find("card").gameObject;
        }
        else
        {
            return null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (Icon() == null)
        {
           DragAndDrop.draggedIcon.transform.SetParent(transform);
           DragAndDrop.draggedIcon.transform.position = transform.position;
        }
    }
}
