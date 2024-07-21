using RTfSetter.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

//https://codeposting.tistory.com/entry/Unity-UI-Object-Drag-Tutorial

public class DragAndDrop : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject draggedIcon;
    Vector3 startPosition;

    [SerializeField] public Transform onDragParent;

    [HideInInspector] public Transform startParent;

    public static Transform cementryCanvas;
    public static TextMeshProUGUI tMPUADH;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.parent.parent.name == "CharacterCanvas")
        {
            CreateGameBoard.selectedCard.transform.SetParent(transform.parent);
            CreateGameBoard.selectedCard.GetComponent<RectTransform>().localPosition = Vector2.zero;
            CreateGameBoard.selectedCard.GetComponent<RectTransform>().sizeDelta = new Vector2(CreateGameBoard.cardWidth + 10, CreateGameBoard.cardHeight + 10);
            CreateGameBoard.selectedCard.transform.SetAsFirstSibling();
            return;
        }
        else if (transform.parent.parent.name == "CreatureCanvas")
        {
            CreateGameBoard.selectedCard.transform.SetParent(transform.parent);
            CreateGameBoard.selectedCard.GetComponent<RectTransform>().localPosition = Vector2.zero;
            CreateGameBoard.selectedCard.GetComponent<RectTransform>().sizeDelta = new Vector2(CreateGameBoard.cardWidth / 2f + 10, CreateGameBoard.cardHeight / 2f + 10);
            CreateGameBoard.selectedCard.transform.SetAsFirstSibling();
            return;
        }
        if (transform.parent.parent.name == "GaugeCanvas")
        {
            if (transform.parent.localRotation.z == 0)
            {
                transform.parent.localRotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
            }
            return;
        }
        Debug.Log(transform);
        if (transform.GetComponent<RectTransform>().sizeDelta.x == CreateGameBoard.cardWidth * 3)
        {
            transform.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(CreateGameBoard.cardWidth, CreateGameBoard.cardHeight);
        }
        else if (transform.GetComponent<RectTransform>().sizeDelta.x == CreateGameBoard.cardWidth)
        {
            transform.position = new Vector3(0, 0, 0);
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(CreateGameBoard.cardWidth * 3, CreateGameBoard.cardHeight * 3);
        }
    }
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
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            transform.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(CreateGameBoard.cardWidth, CreateGameBoard.cardHeight);

        draggedIcon = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Debug.Log(transform);
        if (transform.parent == onDragParent)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);

            try
            {
                if (!CreateGameBoard.selectedCard.transform.parent.Find("card"))
                {
                    CreateGameBoard.selectedCard.transform.SetParent(transform.Find("MainCanvas"));
                    CreateGameBoard.selectedCard.GetComponent<RectTransform>().localPosition = new Vector3(1920 / 2f + 100, 0, 0);
                }
            } catch(NullReferenceException e)
            {
                Debug.Log(e);
            }
        }

        if (transform.parent.parent.name == "CreatureCanvas")
        {
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(CreateGameBoard.cardWidth / 2f, CreateGameBoard.cardHeight / 2f);
        }
            if (transform.parent.name == "cementry")
        {
            transform.SetParent(cementryCanvas);
        }
        if (transform.parent.Find("selectedCard"))
        {
            transform.SetSiblingIndex(1);
        }
        else
        {
            transform.SetAsFirstSibling();
        }
    }
    void Update()
    {
        if (transform.parent.parent.name == "CharacterCanvas")
        {
            /*if(CreateGameBoard.selectedCard.transform.parent.childCount == 5 || CreateGameBoard.selectedCard.transform.parent.childCount == 6)
            {
                return;
            }*/
            switch (ButtonClick.adh)
            {
                case 'a':
                    ButtonClick.characterADHButton = CreateGameBoard.selectedCard.transform.parent.Find("Atk").GetChild(0).GetComponent<TextMeshProUGUI>();
                    break;
                case 'd':
                    ButtonClick.characterADHButton = CreateGameBoard.selectedCard.transform.parent.Find("Def").GetChild(0).GetComponent<TextMeshProUGUI>();
                    break;
                case 'h':
                    ButtonClick.characterADHButton = CreateGameBoard.selectedCard.transform.parent.Find("Hp").GetChild(0).GetComponent<TextMeshProUGUI>();
                    break;
            }
        } else if (transform.parent.parent.name == "CreatureCanvas")
        {
            /*if (CreateGameBoard.selectedCard.transform.parent.childCount == 3 || CreateGameBoard.selectedCard.transform.parent.childCount == 4)
            {
                return;
            }*/
            switch (ButtonClick.adh)
            {
                case 'a':
                    ButtonClick.characterADHButton = CreateGameBoard.selectedCard.transform.parent.Find("Atk").GetChild(0).GetComponent<TextMeshProUGUI>();
                    break;
                default:
                    ButtonClick.characterADHButton = CreateGameBoard.selectedCard.transform.parent.Find("Period").GetChild(0).GetComponent<TextMeshProUGUI>();
                    break;
            }
        }
    }
}
