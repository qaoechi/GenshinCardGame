using UnityEngine;
using MainCanvas.Script;
using RTfSetter.Script;
using Assets.Script;
using TMPro;
using UnityEngine.UI;
using OriginalCard.Script;
using Unity.VisualScripting;

public class CreateGameBoard : MonoBehaviour
{
    public GameObject canvasPrefab;
    public GameObject card;
    public GameObject buttonPrefab;
    
    public TMP_FontAsset fontAsset;

    public Sprite[] sprites;

    public Sprite aDHBtn;

    private RectTransform canvasCameraRect;
    private float cardScale = 1;
    public static float cardHeight = 300;
    public static float cardWidth = 196;
    private float emptyX = 16.5f, emptyY = 45;

    public static GameObject selectedCard;

    private bool isShowMore = false;
    private bool isShowCementry = false;

    private ButtonClick buttonClickScript;

    public static TextMeshProUGUI stateADH;
    // Start is called before the first frame update
    void Start()
    {
        GameObject btnClick = new GameObject("buttonClick");
        buttonClickScript = btnClick.AddComponent<ButtonClick>();
        InitializeBoardSettings();
        SetupMainCanvas();
    }

    private void InitializeBoardSettings()
    {
        GameObject canvas = GameObject.Find("CanvasCamera");
        canvasCameraRect = canvas.GetComponent<RectTransform>();
    }   

    private void SetupMainCanvas()
    {
        var ui = new CanvasMain(canvasPrefab, 0, 0);
        GameObject mainCanvas = ui.CreateUI(canvasCameraRect);

        Transform moreCanvasTransform = SetupMoreDrawedCanvas(mainCanvas);
        Transform cementryCanvasTransform = SetupCementryCanvas(mainCanvas);

        DragAndDrop.cementryCanvas = cementryCanvasTransform;
        
        selectedCard = new GameObject("selectedCard");
        selectedCard.transform.SetParent(mainCanvas.transform);
        Image selectedImage = selectedCard.AddComponent<Image>();
        selectedImage.color = new Color32(152, 79, 79, 160);
        RectTransformSetter.SetRectTransform(selectedCard.GetComponent<RectTransform>(),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(cardWidth + 10, cardHeight + 10),
            new Vector2(1920 / 2f + 100, 0),
            Vector3.one);

        SetupCharacterCanvas(mainCanvas);
        SetupGaugeCanvas(mainCanvas);
        SetCreatureCanvas(mainCanvas);

        SetupCardSlot(mainCanvas, moreCanvasTransform, cementryCanvasTransform);
    }

    private void SetupCharacterCanvas(GameObject mainCanvas)
    {
        var ui = new CanvasInteractive(canvasPrefab, 0, 0);
        GameObject characterCanvas = ui.CreateUI(mainCanvas.transform);
        characterCanvas.name = "CharacterCanvas";

        GameObject gameObject = CreateSlot(characterCanvas.transform, "OnField", 0, cardHeight + emptyY, 1, 1);
        SetADHNumButton(gameObject.transform);

        for (int i = 0; i < 3; i++)
        {
            GameObject gameObject1 = CreateSlot(characterCanvas.transform, "OffField" + i.ToString(), (i - 1) * (cardWidth + emptyX), 0, 1, 1);
            SetADHNumButton(gameObject1.transform);
        }
    }
    private void SetupGaugeCanvas(GameObject mainCanvas)
    {
        var ui = new CanvasInteractive(canvasPrefab, 0, 0);
        GameObject gaugeCanvas = ui.CreateUI(mainCanvas.transform);
        gaugeCanvas.name = "GaugeCanvas";
        
        for (int i = 0; i < 5; i++)
        {
            CreateSlot(gaugeCanvas.transform, "gauge" + i.ToString(), -3 * (cardWidth + emptyX), cardHeight / 3f + (2 - i) * cardHeight * 0.3f, 1, 1);
        }
    }
    private void SetCreatureCanvas(GameObject mainCanvas)
    {
        var ui = new CanvasInteractive(canvasPrefab, 0, 0);
        GameObject characterCanvas = ui.CreateUI(mainCanvas.transform);
        characterCanvas.name = "CreatureCanvas";

        for (int i = 0; i < 4; i++)
        {
            GameObject creature;
            switch (i / 2)
            {
                case 0:
                    creature = CreateSlot(characterCanvas.transform, "creature" + i.ToString(), (0.75f * cardWidth + emptyX) + i * 0.5f * cardWidth, 1.25f * cardHeight + emptyY, 2f, 2f);
                    SetEachButton(creature.transform, "Atk");
                    SetEachButton(creature.transform, "Period");
                    break;
                case 1:
                    creature = CreateSlot(characterCanvas.transform, "creature" + i.ToString(), (0.75f * cardWidth + emptyX) + (i - 2) * 0.5f * cardWidth, 0.75f * cardHeight + emptyY, 2f, 2f);
                    SetEachButton(creature.transform, "Atk");
                    SetEachButton(creature.transform, "Period");
                    break;
            }
        }
    }
    private void SetupCardSlot(GameObject mainCanvas, Transform moreCanvasTransform, Transform cementryCanvasTransform)
    {
        for (int i = 0; i < 3; i++)
        {
            CreateSlot(mainCanvas.transform, "story" + i.ToString(), (i - 1) * (cardWidth + emptyX), -cardHeight - emptyY, 1, 1);
        }

        for (int i = 0; i < 4; i++)
        {
            var drawedFieldCardSlot = new CardSlot(card, sprites[10], cardWidth / 2f, cardHeight / 2f, cardScale, (2 * cardWidth + 2 * emptyX) + i * (cardWidth / 2f + emptyX / 2f), -1.5f * (emptyY + cardHeight));
            GameObject drawed = drawedFieldCardSlot.CreateUI(mainCanvas.transform);
            drawed.name = "drawed" + i;
            var drawedBtn = drawed.AddComponent<Button>();
            drawedBtn.onClick.AddListener(() =>
            {
                if (!isShowMore)
                {
                    RectTransformSetter.SetRectTransform(moreCanvasTransform.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(3 * cardWidth + 2 * emptyX, 400), new Vector3(400, 0, 0), Vector3.one);
                    isShowMore = true;
                }
                else
                {
                    RectTransformSetter.SetRectTransform(moreCanvasTransform.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(3 * cardWidth + 2 * emptyX, 400), new Vector3(1000, 1000, 0), Vector3.one);
                    isShowMore = false;
                }
            });
        }

        GameObject gameObject = CreateSlot(mainCanvas.transform, "deck", 4 * (cardWidth + emptyX), -cardWidth * 1.5f - emptyY, 1, 1);
        gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90);
        var deckBtn = gameObject.AddComponent<Button>();
        CardCreate cardCreate = new CardCreate(card, sprites[/*Random.Range(0, sprites.Length - 1)*/4], cardWidth, cardHeight, cardScale, 0, 0, mainCanvas.transform, cementryCanvasTransform);
        deckBtn.onClick.AddListener(() => cardCreate.ItsMyTurnDraw(moreCanvasTransform, cementryCanvasTransform));

        CreateSlot(mainCanvas.transform, "burst", -3 * (cardWidth + emptyX), -cardHeight - 2 * emptyY, 1, 1);

        var cementryCardSlot = new CardSlot(card, sprites[10], cardWidth, cardWidth, cardScale, 4 * (cardWidth + emptyX),  emptyY - cardHeight / 2f);
        GameObject cementry = cementryCardSlot.CreateUI(mainCanvas.GetComponent<RectTransform>());
        cementry.name = "cementry";
        var cementryBtn = cementry.AddComponent<Button>();
        cementryBtn.onClick.AddListener(() =>
        {
            if (!isShowCementry)
            {
                RectTransformSetter.SetRectTransform(cementryCanvasTransform.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(3 * cardWidth + 2 * emptyX, 400), new Vector3(400, 0, 0), Vector3.one);
                isShowCementry = true;
            }
            else
            {
                RectTransformSetter.SetRectTransform(cementryCanvasTransform.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(3 * cardWidth + 2 * emptyX, 400), new Vector3(1000, 1000, 0), Vector3.one);
                isShowCementry = false;
            }
        });   

        GameObject text = new GameObject("text_express");
        text.transform.parent = mainCanvas.transform;
        TextMeshProUGUI text_express = text.AddComponent<TextMeshProUGUI>();
        RectTransformSetter.SetRectTransform(text.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(3 * cardWidth + emptyX, cardHeight / 4f), new Vector3(3 * cardWidth + 3 * emptyX, 1.5f * cardHeight), new Vector3(cardScale, cardScale, 1));
        text_express.text = "";
        text_express.font = fontAsset;
        text_express.color = Color.black;
        text_express.fontSize = 60;
        text_express.alignment = TextAlignmentOptions.Left;

        ButtonClick.tmpFormula = text_express;


        for (int i = 0; i < 10; i++)
        {
            GameObject btn = Instantiate(buttonPrefab, mainCanvas.transform);
            btn.name = "button" + i;
            TextMeshProUGUI tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = i.ToString();
            Button button_num = btn.GetComponent<Button>();
            button_num.onClick.AddListener(() => buttonClickScript.CopyADH(tmp));
            switch (i / 5)
            {
                case 0:
                    RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.6f * cardWidth, 0.4f * cardWidth), new Vector3((1.95f * cardWidth + 2 * emptyX) + i * 0.6f * cardWidth, text.GetComponent<RectTransform>().localPosition.y - 0.8f * cardWidth), new Vector3(cardScale, cardScale, 1));
                    break;
                case 1:
                    RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.6f * cardWidth, 0.4f * cardWidth), new Vector3((1.95f * cardWidth + 2 * emptyX) + (i - 5) * 0.6f * cardWidth, text.GetComponent<RectTransform>().localPosition.y - 0.8f * cardWidth * 1.5f), new Vector3(cardScale, cardScale, 1));
                    break;
            }
        }
        string[] buttonOperation = { "+", "-", "×", "=", "DEL" , "(", ")", "대입", "Atk"};
        for (int i = 0; i < buttonOperation.Length; i++) 
        {
            GameObject btn = Instantiate(buttonPrefab, mainCanvas.transform);
            btn.name = "button" + buttonOperation[i];
            TextMeshProUGUI tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = buttonOperation[i];
            Button buttonCalc = btn.GetComponent<Button>();
            switch (i / 5)
            {
                case 0:
                    RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.6f * cardWidth, 0.4f * cardWidth), new Vector3((1.95f * cardWidth + 2 * emptyX) + i * 0.6f * cardWidth, text.GetComponent<RectTransform>().localPosition.y - 0.8f * cardWidth * 2), new Vector3(cardScale, cardScale, 1));
                    break;
                case 1:
                    RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.6f * cardWidth, 0.4f * cardWidth), new Vector3((1.95f * cardWidth + 2 * emptyX) + (i - 5) * 0.6f * cardWidth, text.GetComponent<RectTransform>().localPosition.y - 0.8f * cardWidth * 2.5f), new Vector3(cardScale, cardScale, 1));
                    break;
            }
            switch (buttonOperation[i])
            {
                case "DEL":
                    buttonCalc.onClick.AddListener(() => buttonClickScript.DeleteFormula());
                    break;
                case "=":
                    buttonCalc.onClick.AddListener(() => buttonClickScript.EqualButton());
                    break;
                case "대입":
                    buttonCalc.onClick.AddListener(() => buttonClickScript.Substitute());
                    break;
                case "Atk":
                    stateADH = buttonCalc.GetComponentInChildren<TextMeshProUGUI>();
                    buttonCalc.onClick.AddListener(() => buttonClickScript.ChangeADH());
                    break;
                default:
                    buttonCalc.onClick.AddListener(() => buttonClickScript.CopyADH(tmp));
                    break;
            }
            btn.GetComponentInChildren<TextMeshProUGUI>().font = fontAsset;
        }
        GameObject tempSlot = CreateSlot(mainCanvas.transform, "TempSlot", 0, cardHeight + emptyY, 1, 1);
        tempSlot.GetComponent<RectTransform>().localPosition = new Vector3(-1.5f * (cardWidth + emptyX), cardHeight + emptyY);

    }

    private Transform SetupMoreDrawedCanvas(GameObject mainCanvas)
    {
        var moreCanvas = new CanvasPopup(canvasPrefab, 400, 0, 3 * cardWidth + 2 * emptyX, 400);
        GameObject moreDrawed = moreCanvas.CreateUI(mainCanvas.transform);
        moreDrawed.name = "MoreCanvas";
        var grid = moreDrawed.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(cardWidth, cardHeight);
        grid.spacing = new Vector2(emptyX, emptyY);
        grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        grid.childAlignment = TextAnchor.MiddleCenter;
        return moreDrawed.transform;
    }

    private Transform SetupCementryCanvas(GameObject mainCanvas)
    {
        var cementryCanvas = new CanvasPopup(canvasPrefab, 400, 0, 3 * cardWidth + 2 * emptyX, 400);
        GameObject cementry = cementryCanvas.CreateUI(mainCanvas.transform);
        cementry.name = "cementryCanvas";
        var horizontalLayout = cementry.AddComponent<HorizontalLayoutGroup>();
        horizontalLayout.spacing = emptyX;
        horizontalLayout.childAlignment = TextAnchor.MiddleLeft;
        horizontalLayout.childScaleHeight = true;
        horizontalLayout.childScaleWidth = true;
        horizontalLayout.childControlHeight = false;
        horizontalLayout.childControlWidth = false;
        
        return cementry.transform;
    }


    private void SetADHNumButton(Transform card)
    {
        SetEachButton(card, "Atk");
        SetEachButton(card, "Def");
        SetEachButton(card, "Hp");
    }
    private void SetEachButton(Transform card, string name)
    {
        GameObject btn = Instantiate(buttonPrefab, card);
        btn.name = name;
        TextMeshProUGUI adh = btn.GetComponentInChildren<TextMeshProUGUI>();
        adh.text = "0";
        Button buttonADH = btn.GetComponent<Button>();
        btn.GetComponent<Image>().sprite = aDHBtn;
        adh.font = fontAsset;
        adh.fontSize = 50;
        adh.alignment = TextAlignmentOptions.Center;
        adh.color = Color.white;
        switch (name)
        {
            case "Atk":
                if (card.parent.name == "CharacterCanvas")
                {
                    RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(cardWidth / 3f, cardWidth / 3f), new Vector3(-cardWidth / 3f, -cardHeight / 3f), new Vector3(cardScale, cardScale, 1));
                }
                else if(card.parent.name == "CreatureCanvas")
                {
                    RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(cardWidth / 4f, cardWidth / 4f), new Vector3(-cardWidth / 8f, -cardHeight / 6f), new Vector3(cardScale, cardScale, 1));
                }
                buttonADH.onClick.AddListener(() => buttonClickScript.CopyADH(adh));
                break;
            case "Def":
                RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(cardWidth / 3f, cardWidth / 3f), new Vector3(0, -cardHeight / 3f), new Vector3(cardScale, cardScale, 1));
                buttonADH.onClick.AddListener(() => buttonClickScript.CopyADH(adh));
                break;
            case "Hp":
                RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(cardWidth / 3f, cardWidth / 3f), new Vector3(cardWidth / 3f, -cardHeight / 3f), new Vector3(cardScale, cardScale, 1));
                buttonADH.onClick.AddListener(() => buttonClickScript.CopyADH(adh));
                break;
            case "Period":
                RectTransformSetter.SetRectTransform(btn.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(cardWidth / 4f, cardWidth / 4f), new Vector3(cardWidth / 8f, -cardHeight / 6f), new Vector3(cardScale, cardScale, 1));
                buttonADH.onClick.AddListener(() => buttonClickScript.CopyADH(adh));
                break;
        }
    }

    private GameObject CreateSlot(Transform parent, string name, float x, float y, float sx, float sy)
    {
        var CardSlot = new CardSlot(card, sprites[10], cardWidth / sx, cardHeight / sy, cardScale, x, y);
        GameObject slot = CardSlot.CreateUI(parent);
        slot.name = name;
        return slot;
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
