using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//https://codeposting.tistory.com/entry/Unity-UI-Object-Drag-Tutorial

public class Slot : MonoBehaviour, IDropHandler
{

    GameObject Icon()
    {
        if(transform.childCount > 0)
        {
            return transform.GetChild(0).gameObject;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
