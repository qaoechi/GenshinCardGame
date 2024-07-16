using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using OriginalCanvas.Script;
using RTfSetter.Script;
using Assets.Script;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using UnityEditor.SceneManagement;

public class CreateGameBoard : MonoBehaviour
{
    public GameObject canvasPrefab;
    public GameObject card;
    public GameObject buttonPrefab;
    
    public TMP_FontAsset fontAsset;

    public Sprite[] sprites;

    public Sprite atkBtn;

    private RectTransform canvasRectTransform;
    private float cardScale = 1;
    private float cardHeight = 300;
    private float cardWidth = 196;
    private float emptyX = 16.5f, emptyY = 45;
    //16.5 °¡·Î ºó°ø°£

    private Transform baseCanvasTransform;

    private ButtonClick buttonClickScript;
    private DragAndDrop dragAndDropScript;
    //private Slot slotScript;


    // Start is called before the first frame update
    void Start()
    {
        GameObject btnClick = new GameObject("buttonClick");
        buttonClickScript = btnClick.AddComponent<ButtonClick>();
        GameObject Dadrop = new GameObject("dragAndDrop");
        dragAndDropScript = Dadrop.AddComponent<DragAndDrop>();
        /*GameObject slotCard = new GameObject("slot");
        slotScript = slotCard.AddComponent<Slot>();*/

        InitializeBoardSettings();
        SetupMainCanvas();
    }

    private void InitializeBoardSettings()
    {
        //Canvas canvas = FindObjectOfType<Canvas>();
        GameObject canvas = GameObject.Find("CanvasCamera");
        canvasRectTransform = canvas.GetComponent<RectTransform>();

        /*float rX = Math.Min(canvasRectTransform.rect.width / 1920f, 1920f / canvasRectTransform.rect.width);
        float rY = Math.Min(canvasRectTransform.rect.height / 1080f, 1080f / canvasRectTransform.rect.height);*/
    }

    private void SetupMainCanvas()
    {
        var ui = new CanvasMain(canvasPrefab, emptyX, emptyY);
        GameObject mainCanvas = ui.CreateUI(canvasRectTransform);

        SetupCardSlot(mainCanvas);
        
    }

    private void SetupCardSlot(GameObject mainCanvas)
    {
        var onFieldCardSlot = new CardSlot(card, sprites[10], cardWidth, cardHeight, cardScale, 0, cardHeight + emptyY);
        GameObject on = onFieldCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
        on.name = "onField";

        var imsiCard = new CardSlot(card, sprites[3], cardWidth, cardHeight, cardScale, 0, cardHeight + emptyY);
        GameObject imsi = imsiCard.CreateUI(on.GetComponent<RectTransform>());
        imsi.name = "imsi";
        DragAndDrop dADrop = imsi.AddComponent<DragAndDrop>();
        imsi.AddComponent<CanvasGroup>();
        Destroy(imsi.GetComponent<Slot>());
        dADrop.onDragParent = mainCanvas.transform;
        SetAtkButton(on);

        for (int i = 0; i < 3; i++)
        {
            var offFieldCardSlot = new CardSlot(card, sprites[10], cardWidth, cardHeight, cardScale, (i - 1) * (cardWidth + emptyX), 0);
            GameObject off = offFieldCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
            off.name = "offField" + i;
            SetAtkButton(off);
        }

        for (int i = 0; i < 3; i++)
        {
            var storyFieldCardSlot = new CardSlot(card, sprites[10], cardWidth, cardHeight, cardScale, (i - 1) * (cardWidth + emptyX), -cardHeight - emptyY);
            GameObject story = storyFieldCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
            story.name = "story" + i;
        }

        for (int i = 0; i < 5; i++)
        {
            var GaugeFieldCardSlot = new CardSlot(card, sprites[10], cardWidth, cardHeight, cardScale, -3 * (cardWidth + emptyX), cardHeight / 3f + (2 - i) * cardHeight * 0.3f);
            GameObject gauge = GaugeFieldCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
            gauge.name = "gauge" + i;
        }

        for (int i = 0; i < 4; i++)
        {
            var drawedFieldCardSlot = new CardSlot(card, sprites[10], cardWidth / 2f, cardHeight / 2f, cardScale, (2 * cardWidth + 2 * emptyX) + i * (cardWidth / 2f + emptyX / 2f), -1.5f * (emptyY + cardHeight));
            GameObject drawed = drawedFieldCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
            drawed.name = "drawed" + i;
        }

        var deckCardSlot = new CardSlot(card, sprites[10], cardWidth, cardHeight, cardScale, 4 * (cardWidth + emptyX), -cardWidth * 1.5f - emptyY);
        GameObject deck = deckCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
        deck.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90);
        deck.name = "deck";


        var burstCardSlot = new CardSlot(card, sprites[10], cardWidth, cardHeight, cardScale, -3 * (cardWidth + emptyX), -cardHeight - 2 * emptyY);
        GameObject burst = burstCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
        burst.name = "burst";

        var cementryCardSlot = new CardSlot(card, sprites[10], cardWidth, cardWidth, cardScale, 4 * (cardWidth + emptyX),  emptyY - cardHeight / 2f);
        GameObject cementry = cementryCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
        cementry.name = "cementry";

        for(int i = 0; i < 4; i++)
        {
            CardSlot creatureFieldCardSlot;
            GameObject creature;
            switch (i / 2)
            {
                case 0:
                    creatureFieldCardSlot = new CardSlot(card, sprites[10], cardWidth / 2f, cardHeight / 2f, cardScale, (0.75f * cardWidth + emptyX) + i * 0.5f * cardWidth, 1.25f * cardHeight + emptyY);
                    creature = creatureFieldCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
                    creature.name = "creature" + i;
                    break;
                case 1:
                    creatureFieldCardSlot = new CardSlot(card, sprites[10], cardWidth / 2f, cardHeight / 2f, cardScale, (0.75f * cardWidth + emptyX) + (i - 2) * 0.5f * cardWidth, 0.75f * cardHeight + emptyY);
                    creature = creatureFieldCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
                    creature.name = "creature" + i;
                    break;
            }
        }

        GameObject text = new GameObject("text_express");
        text.transform.parent = mainCanvas.transform;
        TextMeshProUGUI text_express = text.AddComponent<TextMeshProUGUI>();
        RectTransformSetter.SetRectTransform(text.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(2 * cardWidth + emptyX, cardHeight / 4f), new Vector3(2.5f * cardWidth + 2 * emptyX, 1.5f * cardHeight), new Vector3(cardScale, cardScale, 1));
        text_express.text = "10";
        text_express.font = fontAsset;
        text_express.color = Color.black;
        text_express.fontSize = 60;
        text_express.alignment = TextAlignmentOptions.Left;

        buttonClickScript.tmp_formula = text_express;


        for (int i = 0; i < 10; i++)
        {
            GameObject btn = UnityEngine.Object.Instantiate(buttonPrefab, mainCanvas.transform);
            btn.name = "button" + i;
            TextMeshProUGUI tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = i.ToString();
            Button button_num = btn.GetComponent<Button>();
            button_num.onClick.AddListener(() => buttonClickScript.CopyAtk(tmp));
            switch (i / 5)
            {
                case 0:
                    RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.6f * cardWidth, 0.4f * cardWidth), new Vector3((1.75f * cardWidth + 2 * emptyX) + i * 0.6f * cardWidth, text.GetComponent<RectTransform>().localPosition.y - 0.8f * cardWidth), new Vector3(cardScale, cardScale, 1));
                    break;
                case 1:
                    RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.6f * cardWidth, 0.4f * cardWidth), new Vector3((1.75f * cardWidth + 2 * emptyX) + (i - 5) * 0.6f * cardWidth, text.GetComponent<RectTransform>().localPosition.y - 0.8f * cardWidth * 1.5f), new Vector3(cardScale, cardScale, 1));
                    break;
            }
        }
        string[] buttonOperation = { "+", "-", "¡¿", "=", "DEL"};
        for (int i = 0; i < buttonOperation.Length; i++) 
        {
            GameObject btn = UnityEngine.Object.Instantiate(buttonPrefab, mainCanvas.transform);
            btn.name = "button" + buttonOperation[i];
            TextMeshProUGUI tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = buttonOperation[i];
            Button button_num = btn.GetComponent<Button>();
            RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.6f * cardWidth, 0.4f * cardWidth), new Vector3((1.75f * cardWidth + 2 * emptyX) + i * 0.6f * cardWidth, text.GetComponent<RectTransform>().localPosition.y - 0.8f * cardWidth * 2), new Vector3(cardScale, cardScale, 1));
            switch (buttonOperation[i])
            {
                case "DEL":
                    button_num.onClick.AddListener(() => buttonClickScript.DeleteFormula());
                    break;
                case "=":
                    button_num.onClick.AddListener(() => buttonClickScript.EqualFormula());
                    break;
                default:
                    button_num.onClick.AddListener(() => buttonClickScript.CopyAtk(tmp));
                    break;
            }
        }
    }

    private void SetAtkButton(GameObject card)
    {
        GameObject btn = UnityEngine.Object.Instantiate(buttonPrefab, card.transform);
        btn.name = "Atk";
        TextMeshProUGUI atk = btn.GetComponentInChildren<TextMeshProUGUI>();
        atk.text = "3";
        Button buttonAtk = btn.GetComponent<Button>();
        RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(cardWidth / 2f, cardWidth / 2f), new Vector3(0, -cardHeight / 2f), new Vector3(cardScale, cardScale, 1));
        btn.GetComponent<UnityEngine.UI.Image>().sprite = atkBtn;
        atk.font = fontAsset;
        atk.fontSize = 50;
        atk.alignment = TextAlignmentOptions.Center;
        atk.color = Color.white;

        buttonAtk.onClick.AddListener(() => buttonClickScript.CopyAtk(atk));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
