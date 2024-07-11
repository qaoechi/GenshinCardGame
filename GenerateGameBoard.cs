using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GenerateGameBoard : MonoBehaviour
{
    public GameObject canvasPrefab;
    public GameObject imagePrefab;
    public GameObject card;
    public GameObject button_cal;
    public GameObject button_exit;
    public TMP_FontAsset fontAsset;

    public Sprite[] sprites;
    public Sprite sprite;
    public Sprite hpatk;

    private float card_size = 15f;
    private float correction = 10f;
    private float card_ratio = 2048f / 1329f;
    private float card_height = 346.7269f;
    private float card_width = 225f;
    private float empty_x, empty_y = 10;
    private float r_x;
    private float r_y;

    private ButtonClick script_buttonClick;

    private RectTransform canvas_rectTransform;
   // private CssBoard a

    // Start is called before the first frame update
    void Start()
    {
        script_buttonClick = FindObjectOfType<ButtonClick>();

        Canvas canvas = FindObjectOfType<Canvas>();
        canvas_rectTransform = canvas.GetComponent<RectTransform>();

        // 가로: 9장 세로: 3장
        float r_x1 = canvas_rectTransform.rect.width / 1920f;
        float r_y1 = canvas_rectTransform.rect.height/ 1080f;
        float r_x2 = 1920f / canvas_rectTransform.rect.width;
        float r_y2 = 1080f / canvas_rectTransform.rect.height;
        r_x = Math.Min(r_x1, r_x2);
        r_y = Math.Min(r_y1, r_y2);

        //혹시 rect으로 collide를 할 수 있으니 크기를 맞춰놔야지
        card_size = 15f * r_x;
        correction = 10f;
        card_ratio = r_y * 2048f / 1329f;
        card_width = 225f * r_x;
        card_height = 346.7269f * r_y;
        empty_x = 10f * r_x;
        empty_y = 10f * r_y;

    //a = new CssBoard(15f, 10f, 2048f / 1329f, 2048f / 1329f, 225f, 10, 10);
        set_main_canvas();
    }

    void set_main_canvas()
    {
        GameObject canvas = Instantiate(canvasPrefab, canvas_rectTransform);
        canvas.name = "canvas_main";
        RectTransform rect = canvas.GetComponent<RectTransform>();
        
        set_main_trans(rect);
        set_character_canvas(rect);
        set_story_canvas(rect);
        set_gauge_canvas(rect);
        set_burst_canvas(rect);
        set_creature_canvas(rect);
        set_deck_card(rect);
        set_drawed_canvas(rect);
        set_cemetry_card(rect);
        set_calc_canvas(rect);

        set_exit(rect);

    }
    void set_character_canvas(RectTransform main_canvas)
    {
        GameObject canvas = Instantiate(canvasPrefab, main_canvas);
        canvas.name = "canvas_character";
        RectTransform rect = canvas.GetComponent<RectTransform>();

        set_character_trans(rect, main_canvas);
        set_on_card(rect);
        set_off_card(rect);
    }
    void set_story_canvas(RectTransform main_canvas)
    {
        GameObject canvas = Instantiate(canvasPrefab, main_canvas);
        canvas.name = "canvas_story";
        RectTransform rect = canvas.GetComponent <RectTransform>();

        set_story_trans(rect, main_canvas);
        set_story_card(rect);
    }
    void set_gauge_canvas(RectTransform main_canvas)
    {
        GameObject canvas = Instantiate(canvasPrefab, main_canvas);
        canvas.name = "canvas_gauge";
        RectTransform rect = canvas.GetComponent<RectTransform>();

        set_gauge_trans(rect, main_canvas);
        set_gauge_card(rect);
        
    }
    void set_burst_canvas(RectTransform main_canvas)
    {
        GameObject canvas = Instantiate(canvasPrefab, main_canvas);
        canvas.name = "canvas_burst";
        RectTransform rect = canvas.GetComponent<RectTransform>();

        set_burst_trans(rect, main_canvas);
        set_burst_card(rect);
    }
    void set_creature_canvas(RectTransform main_canvas)
    {
        GameObject canvas = Instantiate(canvasPrefab, main_canvas); 
        canvas.name = "canvas_creature";
        RectTransform rect = canvas.GetComponent<RectTransform>();

        set_creature_trans(rect, main_canvas);
        set_creature_card(rect);
    }
    void set_drawed_canvas(RectTransform main_canvas)
    {
        GameObject canvas = Instantiate(canvasPrefab, main_canvas);
        canvas.name = "canvas_drawed";
        RectTransform rect = canvas.GetComponent<RectTransform>();

        set_drawed_trans(rect, main_canvas);
        set_drawed_card(rect);

    }
    void set_calc_canvas(RectTransform main_canvas)
    {
        GameObject calc = Instantiate(canvasPrefab, main_canvas);
        calc.name = "calc";
        RectTransform rect = calc.GetComponent<RectTransform>();

        set_calc_trans(rect);
        set_calc(rect);
    }




    void set_field_card(RectTransform main_canvas)
    {
        GameObject field = Instantiate(card, main_canvas);
        field.name = "field_card";
        SpriteRenderer image = field.GetComponent<SpriteRenderer>();
        image.sprite = sprites[0];
        RectTransform rect = field.GetComponent<RectTransform>();

        set_field_trans(rect, main_canvas);
    }
    void set_on_card(RectTransform character_canvas)
    {
        GameObject on_field = Instantiate(card, character_canvas);
        on_field.name = "on_field";
        SpriteRenderer image = on_field.GetComponent<SpriteRenderer>();
        image.sprite = sprites[0];
        RectTransform rect = on_field.GetComponent<RectTransform>();

        set_atk_button(on_field.transform, rect);
       
        set_hp_button(on_field.transform, rect);
        
        set_on_trans(rect, character_canvas);
    }
    void set_off_card(RectTransform character_canvas)
    {
        
        for (int i = 0; i < 3; i++)
        {
            GameObject off_field = Instantiate(card, character_canvas);
            off_field.name = "off_field" + i;
            SpriteRenderer image = off_field.GetComponent<SpriteRenderer>();
            image.sprite = sprites[1 + i];
            RectTransform rect = off_field.GetComponent<RectTransform>();

            set_off_trans(rect, character_canvas, i);
        }
    }
    void set_burst_card(RectTransform burst_canvas)
    {
        GameObject burst = Instantiate(card, burst_canvas);
        burst.name = "burst";
        SpriteRenderer image = burst.GetComponent<SpriteRenderer>();
        image.sprite = sprites[2];
        RectTransform rect = burst.GetComponent<RectTransform>();

        set_burst_trans(rect);
    }
    void set_gauge_card(RectTransform gauge_canvas)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject gauge = Instantiate(card, gauge_canvas);
            gauge.name = "gauge" + i;
            SpriteRenderer image = gauge.GetComponent<SpriteRenderer>();
            image.sprite = sprites[4 + i];    //4~8
            image.sortingOrder = i;
            RectTransform rect = gauge.GetComponent<RectTransform>();

            set_gauge_trans(rect, i);
        }
    }
    void set_story_card(RectTransform story_canvas)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject story = Instantiate(card, story_canvas);
            story.name = "story" + i;
            SpriteRenderer image = story.GetComponent<SpriteRenderer>();
            image.sprite = sprites[7 + i];
            RectTransform rect = story.GetComponent<RectTransform>();

            set_story_trans(rect, i);
        }
    }
    void set_creature_card(RectTransform creature_canvas)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject creature = Instantiate(card, creature_canvas);
            creature.name = "creature" + i;
            SpriteRenderer image = creature.GetComponent<SpriteRenderer>();
            image.sprite = sprites[2 + i];
            RectTransform rect = creature.GetComponent<RectTransform>();

            set_creature_trans(rect, i);
        }
    }
    void set_deck_card(RectTransform main_canvas)
    {
        GameObject deck = Instantiate(card, main_canvas);
        deck.name = "deck";
        SpriteRenderer image = deck.GetComponent<SpriteRenderer>();
        image.sprite = sprites[^1];
        RectTransform rect = deck.GetComponent<RectTransform>();

        set_deck_trans(rect);
    }
    void set_drawed_card(RectTransform drawed_canvas)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject drawed = Instantiate(card, drawed_canvas);
            drawed.name = "drawed" + i;
            SpriteRenderer image = drawed.GetComponent<SpriteRenderer>();
            image.sprite = sprites[5 + i];
            RectTransform rect = drawed.GetComponent<RectTransform>();

            set_drawed_trans(rect, i);
        }
    }
    void set_cemetry_card(RectTransform main_canvas)
    {
        GameObject image = Instantiate(imagePrefab, main_canvas);
        image.name = "cemetry";
        Image image1 = image.GetComponent<Image>();
        image1.sprite = sprite;
        RectTransform rect = image1.GetComponent<RectTransform>();

        set_cemetry_trans(rect);
    }
    void set_calc(RectTransform calc_canvas)
    {
        GameObject text = new GameObject("text_express");
        TextMeshProUGUI text_result = text.AddComponent<TextMeshProUGUI>();
        game_object_calc(text_result);
        set_express_trans(text_result, calc_canvas);

        text = new GameObject("text_result");
        TextMeshProUGUI text_express = text.AddComponent<TextMeshProUGUI>();
        set_result_trans(text_express, calc_canvas);
       
        set_calc_btn(calc_canvas);
    }
    public void set_calc_btn(RectTransform calc_canvas)
    {
        string[] button_op = { "+", "-", "×", "=", "<-", "->", "DEL", "C" };
        for (int i = 0; i < 3; i++)
        {
            GameObject button = Instantiate(button_cal, calc_canvas);
            button.name = "button" + button_op[i];
            TextMeshProUGUI textMP = button.GetComponentInChildren<TextMeshProUGUI>();
            textMP.text = button_op[i];
            RectTransform rect = button.GetComponent<RectTransform>();

            Button button_calc = button.GetComponent<Button>();
            button_calc.onClick.AddListener(() => FindObjectOfType<ButtonClick>().CopyOps(textMP));

            set_operation_trans(rect, calc_canvas, button_op[i]);
        }

        for (int i = 3; i < button_op.Length; i++)
        {
            GameObject button = Instantiate(button_cal, calc_canvas);
            button.name = "button" + button_op[i];
            TextMeshProUGUI textMP = button.GetComponentInChildren<TextMeshProUGUI>();
            textMP.text = button_op[i];
            RectTransform rect = button.GetComponent<RectTransform>();

            Button button_calc = button.GetComponent<Button>();
            //button_calc.onClick.AddListener(() => script_buttonClick.CopyOps(textMP));

            switch (button_op[i])
            {
                case "=":
                    button_calc.onClick.AddListener(() => FindObjectOfType<ButtonClick>().EqualFormula());
                    break;
                case "DEL":
                    button_calc.onClick.AddListener(() => FindObjectOfType<ButtonClick>().DeleteFormula());
                    break;
                case "C":
                    button_calc.onClick.AddListener(() => FindObjectOfType<ButtonClick>().ResetFormula());
                    break;
            }

            set_operation_trans(rect, calc_canvas, button_op[i]);
        }

    }


    public void set_atk_button(Transform canvas_on_field, RectTransform on_field)
    {
        GameObject button = Instantiate(button_exit, canvas_on_field);
        button.name = "atk";
        //button.transform.SetParent(on_field.transform);
        Button button_atk = button.GetComponent<Button>();
        //button_atk.onClick.AddListener(() => FindObjectOfType<ButtonClick>().Atk());
        RectTransform rect_btn = button.GetComponent<RectTransform>();
        Image image = rect_btn.GetComponent<Image>();
        image.sprite = hpatk;
        rect_btn.localPosition = new Vector3(-0.25f * (card_width / on_field.localScale.x), -0.25f * (card_height / on_field.localScale.y), 0);
        rect_btn.sizeDelta = new Vector3(0.5f * (card_width / on_field.localScale.x), 0.5f * (card_width / on_field.localScale.x), 0);
        TextMeshProUGUI text_atk = button.GetComponentInChildren<TextMeshProUGUI>();
        text_atk.AddComponent<ButtonClick>();
        text_atk.text = "5";
        text_atk.font = fontAsset;
        text_atk.fontSize = 4;
        text_atk.alignment = TextAlignmentOptions.Center;
        text_atk.color = Color.white;
        RectTransform rect_atk = text_atk.GetComponent<RectTransform>();
        rect_atk.anchorMin = new Vector2(0, 0);
        rect_atk.anchorMax = new Vector2(1, 1);
        rect_atk.offsetMin = new Vector2(0, 0);

        button_atk.onClick.AddListener(() => FindObjectOfType<ButtonClick>().CopyAtk(text_atk));
    }

    public void set_hp_button(Transform canvas_on_field, RectTransform on_field)
    {
        GameObject button = Instantiate(button_exit, canvas_on_field);
        button.name = "hp";
        //button.transform.SetParent(on_field.transform);
        Button button_hp = button.GetComponent<Button>();
        button_hp.onClick.AddListener(() => FindObjectOfType<ButtonClick>().Hp());
        RectTransform rect_btn = button.GetComponent<RectTransform>();
        Image image = rect_btn.GetComponent<Image>();
        image.sprite = hpatk;
        rect_btn.localPosition = new Vector3(0.25f * (card_width / on_field.localScale.x), -0.25f * (card_height / on_field.localScale.y), 0);
        rect_btn.sizeDelta = new Vector3(0.5f * (card_width / on_field.localScale.x), 0.5f * (card_width / on_field.localScale.x), 0);
        TextMeshProUGUI text_hp = button.GetComponentInChildren<TextMeshProUGUI>();
        text_hp.AddComponent<ButtonClick>();
        text_hp.text = "12";
        text_hp.font = fontAsset;
        text_hp.fontSize = 4;
        text_hp.alignment = TextAlignmentOptions.Center;
        text_hp.color = Color.white;
        RectTransform rect_hp = text_hp.GetComponent<RectTransform>();
        rect_hp.anchorMin = new Vector2(0, 0);
        rect_hp.anchorMax = new Vector2(1, 1);
        rect_hp.offsetMin = new Vector2(0, 0);
    }




    public void set_exit(RectTransform main_canvas)
    {
        GameObject button = Instantiate(button_exit, main_canvas);
        button.name = "button_main_menu";
        RectTransform rect = button.GetComponent<RectTransform>();

        Button exit = button.GetComponent<Button>();
        //exit.onClick.AddListener(() => FindObjectOfType<ButtonClick>().MovingScene());
        exit.onClick.AddListener(script_buttonClick.MovingScene);
        set_exit_trans(rect, main_canvas);
    }

    public void game_object_calc(TextMeshProUGUI result)
    {
        GameObject button_click = new GameObject("inputNum");
        ButtonClick script = button_click.AddComponent<ButtonClick>();
        script.tmp_formula = result;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void set_main_trans(RectTransform main_canvas)
    {
        main_canvas.anchorMin = new Vector2(0.5f, 0.5f);
        main_canvas.anchorMax = new Vector2(0.5f, 0.5f);
        main_canvas.pivot = new Vector2(0.5f, 0.5f);
        main_canvas.sizeDelta = new Vector3(canvas_rectTransform.rect.width - 2 * 10, canvas_rectTransform.rect.height - 2 * 10, 0);
        main_canvas.localPosition = new Vector3(0, 0, 0);
        main_canvas.localScale = new Vector3(1, 1, 1);
    }
    public void set_field_trans(RectTransform field, RectTransform main_canvas)
    {
        field.anchorMin = new Vector2(0.5f, 0.5f);
        field.anchorMax = new Vector2(0.5f, 0.5f);
        field.pivot = new Vector2(0.5f, 0.5f);
        field.localPosition = new Vector3(0, main_canvas.rect.height / 2 - card_size * correction, 0);
        field.sizeDelta = new Vector3(card_size, card_size * card_ratio, 1);
        field.localScale = new Vector3(card_size, card_size, 1);
        field.rotation = Quaternion.Euler(0, 0, 90);
    }
    public void set_character_trans(RectTransform character_canvas, RectTransform main_canvas)
    {
        character_canvas.anchorMin = new Vector2(0.5f, 0.5f);
        character_canvas.anchorMax = new Vector2(0.5f, 0.5f);
        character_canvas.pivot = new Vector2(0.5f, 0.5f);
        character_canvas.sizeDelta = new Vector3(card_width * 3 + empty_x * 4, card_height * 2 + empty_y, 0);
        character_canvas.anchoredPosition = new Vector3(0, card_height / 2f + empty_y, 0);
        character_canvas.localScale = new Vector3(1, 1, 1);
    }
    public void set_on_trans(RectTransform on_field, RectTransform character_canvas)
    {
        on_field.anchorMin = new Vector2(0.5f, 1);
        on_field.anchorMax = new Vector2(0.5f, 1);
        on_field.pivot = new Vector2(0.5f, 0.5f);
        on_field.sizeDelta = new Vector3(card_size, card_size * card_ratio, 1);
        on_field.anchoredPosition = new Vector3(0, -card_height / 2f, 0);
        on_field.localScale = new Vector3(card_size, card_size, 1);
    }
    public void set_off_trans(RectTransform off_field, RectTransform character_canvas, int i)
    {
        off_field.anchorMin = new Vector2(0.5f, 0);
        off_field.anchorMax = new Vector2(0.5f, 0);
        off_field.pivot = new Vector2(0.5f, 0.5f);
        off_field.sizeDelta = new Vector3(card_size, card_size * card_ratio, 1);
        off_field.anchoredPosition = new Vector3((i - 1) * (empty_y + card_width), card_height / 2f, 0);
        off_field.localScale = new Vector3(card_size, card_size, 1);
    }
    public void set_story_trans(RectTransform story_canvas, RectTransform main_canvas)
    {
        story_canvas.anchorMin = new Vector2(0.5f, 0.5f);
        story_canvas.anchorMax = new Vector2(0.5f, 0.5f);
        story_canvas.pivot = new Vector2(0.5f, 0.5f);
        story_canvas.sizeDelta = new Vector3(card_width * 3 + empty_x * 4, card_height, 0);
        story_canvas.anchoredPosition = new Vector3(0, -card_height, 0);
        story_canvas.localScale = new Vector3(1, 1, 1);
    }
    public void set_story_trans(RectTransform story, int i)
    {
        story.anchorMin = new Vector2(0.5f, 0);
        story.anchorMax = new Vector2(0.5f, 0);
        story.pivot = new Vector2(0.5f, 0.5f);
        story.sizeDelta = new Vector2(card_size, card_size * card_ratio);
        story.anchoredPosition = new Vector3((i - 1) * (empty_y + card_width), card_height / 2f, 0);
        story.localScale = new Vector3(card_size, card_size, 1);
    }
    public void set_gauge_trans(RectTransform gauge_canvas, RectTransform main_canvas)
    {
        gauge_canvas.anchorMin = new Vector2(0.5f, 0.5f);
        gauge_canvas.anchorMax = new Vector2(0.5f, 0.5f);
        gauge_canvas.pivot = new Vector2(0.5f, 0.5f);
        gauge_canvas.sizeDelta = new Vector3(card_width + 2 * empty_x, 2.5f * card_height + empty_y, 0);
        gauge_canvas.anchoredPosition = new Vector3(-3 * card_width, 0.2f * card_height, 0);
        gauge_canvas.localScale = new Vector3(1, 1, 1);
    }
    public void set_gauge_trans(RectTransform gauge, int i)
    {
        gauge.anchorMin = new Vector2(0.5f, 1);
        gauge.anchorMax = new Vector2(0.5f, 1);
        gauge.pivot = new Vector2(0.5f, 0.5f);
        gauge.sizeDelta = new Vector3(card_size, card_size * card_ratio, 1);
        gauge.anchoredPosition = new Vector3(0, -card_height / 2f - i * card_height / 3f, 0);
        gauge.localScale = new Vector3(card_size, card_size, 1);
    }
    public void set_burst_trans(RectTransform burst_canvas, RectTransform main_canvas)
    {
        burst_canvas.anchorMin = new Vector2(0.5f, 0.5f);
        burst_canvas.anchorMax = new Vector2(0.5f, 0.5f);
        burst_canvas.pivot = new Vector2(0.5f, 0.5f);
        burst_canvas.sizeDelta = new Vector2(card_width + 2 * empty_x, card_height + 2 * empty_y);
        burst_canvas.anchoredPosition = new Vector3(-3 * card_width, -1.5f * card_height);
        burst_canvas.localScale = new Vector3(1, 1, 1);
    }
    public void set_burst_trans(RectTransform burst)
    {
        burst.anchorMin = new Vector2(0.5f, 0.5f);
        burst.anchorMax = new Vector2(0.5f, 0.5f);
        burst.pivot = new Vector2(0.5f, 0.5f);
        burst.sizeDelta = new Vector3(card_size, card_size * card_ratio, 0);
        burst.anchoredPosition = new Vector3(0, 0, 0);
        burst.localScale = new Vector3(card_size, card_size, 0);
    }
    public void set_creature_trans(RectTransform creature_canvas, RectTransform main_canvas)
    {
        creature_canvas.anchorMin = new Vector2(0.5f, 0.5f);
        creature_canvas.anchorMax = new Vector2(0.5f, 0.5f);
        creature_canvas.pivot = new Vector2(0.5f, 0.5f);
        creature_canvas.sizeDelta = new Vector3(card_width + (empty_x / 2f), card_height + (empty_y / 2f), 0);
        creature_canvas.anchoredPosition = new Vector3(card_width + empty_x * 2, card_height, 0);
        creature_canvas.localScale = new Vector3(1, 1, 1);
    }
    public void set_creature_trans(RectTransform creature, int i)
    {
        creature.pivot = new Vector2(0.5f, 0.5f);
        creature.sizeDelta = new Vector2(card_size, card_size * card_ratio);
        creature.localScale = new Vector3(card_size / 2f, card_size / 2f, 1);
        switch (i)
        {
            case 0:
                creature.anchorMin = new Vector2(0, 1);
                creature.anchorMax = new Vector2(0, 1);
                creature.anchoredPosition = new Vector3(card_width / 4f, -card_height / 4f, 0);
                break;
            case 1:
                creature.anchorMin = new Vector2(1, 1);
                creature.anchorMax = new Vector2(1, 1);
                creature.anchoredPosition = new Vector3(-card_width / 4f, -card_height / 4f, 0);
                break;
            case 2:
                creature.anchorMin = new Vector2(0, 0);
                creature.anchorMax = new Vector2(0, 0);
                creature.anchoredPosition = new Vector3(card_width / 4f, card_height / 4f, 0);
                break;
            case 3:
                creature.anchorMin = new Vector2(1, 0);
                creature.anchorMax = new Vector2(1, 0);
                creature.anchoredPosition = new Vector3(-card_width / 4f, card_height / 4f, 0);
                break;
        }
    }
    public void set_deck_trans(RectTransform deck)
    {
        deck.anchorMin = new Vector2(0.5f, 0.5f);
        deck.anchorMax = new Vector2(0.5f, 0.5f);
        deck.pivot = new Vector2(0.5f, 0.5f);
        deck.sizeDelta = new Vector3(card_size, card_size * card_ratio, 0);
        deck.anchoredPosition = new Vector3(2.5f * card_height + 2 + empty_x, -0.75f * card_width, 0);
        deck.localScale = new Vector3(card_size, card_size, 0);
        deck.rotation = Quaternion.Euler(0, 0, 90);
    }
    public void set_drawed_trans(RectTransform drawed_canvas, RectTransform main_canvas)
    {
        drawed_canvas.anchorMin = new Vector2(0.5f, 0.5f);
        drawed_canvas.anchorMax = new Vector2(0.5f, 0.5f);
        drawed_canvas.pivot = new Vector2(0.5f, 0.5f);
        drawed_canvas.sizeDelta = new Vector2(2 * card_width + 1.5f * empty_x, card_height / 2f);
        drawed_canvas.anchoredPosition = new Vector3(3 * card_width + empty_x + 2, -1.5f * card_height);
        drawed_canvas.localScale = new Vector3(1, 1, 1);
    }
    public void set_drawed_trans(RectTransform drawed, int i)
    {
        drawed.anchorMin = new Vector2(0, 0.5f);
        drawed.anchorMax = new Vector2(0, 0.5f);
        drawed.sizeDelta = new Vector3(card_size, card_size * card_ratio, 0);
        drawed.localScale = new Vector3(card_size / 2f, card_size / 2f, 0);
        drawed.pivot = new Vector2(0.5f, 0.5f);
        drawed.anchoredPosition = new Vector3(empty_x + card_width / 4f + i * (card_width / 2f + empty_x), 0, 0);
    }
    public void set_cemetry_trans(RectTransform cemetry)
    {
        cemetry.anchorMin = new Vector2(0.5f, 0.5f);
        cemetry.anchorMax = new Vector2(0.5f, 0.5f);
        cemetry.pivot = new Vector2(0.5f, 0.5f);
        cemetry.sizeDelta = new Vector3(200, 200, 0);
        cemetry.anchoredPosition = new Vector3(3.75f * card_width, 0.25f * card_height, 0);
        cemetry.localScale = new Vector3(1, 1, 0);
    }
    public void set_calc_trans(RectTransform calc_canvas)
    {
        calc_canvas.anchorMin = new Vector2(0.5f, 0.5f);
        calc_canvas.anchorMax = new Vector2(0.5f, 0.5f);
        calc_canvas.pivot = new Vector2(0.5f, 0.5f);
        calc_canvas.sizeDelta = new Vector2(card_width * 2 + empty_x, card_height + empty_y);
        calc_canvas.anchoredPosition = new Vector3(3 * card_width, card_height);
        calc_canvas.localScale = new Vector3(1, 1, 1);
    }
    public void set_express_trans(TextMeshProUGUI text_express, RectTransform calc_canvas)
    {
        text_express.text = "";
        text_express.font = fontAsset;
        text_express.color = Color.black;
        text_express.fontSize = 60;
        text_express.alignment = TextAlignmentOptions.Left;

        RectTransform rect_express = text_express.GetComponent<RectTransform>();    //text.get이든 textG.get이든 똑같음
        text_express.transform.SetParent(calc_canvas, false);
        rect_express.anchorMin = new Vector2(0, 1);
        rect_express.anchorMax = new Vector2(0, 1);
        rect_express.pivot = new Vector2(0.5f, 0.5f);
        rect_express.sizeDelta = new Vector3(calc_canvas.rect.width * 0.75f, calc_canvas.rect.height / 3f, 0);
        rect_express.anchoredPosition = new Vector3(rect_express.rect.width / 2f, -rect_express.rect.height / 2f, 0);
    }
    public void set_result_trans(TextMeshProUGUI text_result, RectTransform calc_canvas)
    {
        text_result.text = "0";
        text_result.font = fontAsset;
        text_result.color = Color.black;
        text_result.fontSize = 60;
        text_result.alignment = TextAlignmentOptions.Right;

        RectTransform rect_result = text_result.GetComponent<RectTransform>();    //text.get이든 textG.get이든 똑같음
        text_result.transform.SetParent(calc_canvas, false);
        rect_result.anchorMin = new Vector2(1, 1);
        rect_result.anchorMax = new Vector2(1, 1);
        rect_result.pivot = new Vector2(0.5f, 0.5f);
        rect_result.sizeDelta = new Vector3(calc_canvas.rect.width * 0.25f, calc_canvas.rect.height / 3f, 0);
        rect_result.anchoredPosition = new Vector3(-rect_result.rect.width / 2f, -rect_result.rect.height / 2f, 0);
    }
    
    public void set_operation_trans(RectTransform button_oper, RectTransform calc_canvas, string oper)
    {
        button_oper.anchorMin = new Vector2(0.5f, 0.5f);
        button_oper.anchorMax = new Vector2(0.5f, 0.5f);
        button_oper.pivot = new Vector2(0.5f, 0.5f);
        button_oper.localScale = new Vector3(1, 1, 1);
        button_oper.sizeDelta = new Vector3(calc_canvas.rect.width / 4f, calc_canvas.rect.height / 6f);

        switch (oper)
        {
            case "+":
                button_oper.anchoredPosition = new Vector3(-1.5f * button_oper.rect.width, 0, 0);
                break;
            case "-":
                button_oper.anchoredPosition = new Vector3(-0.5f * button_oper.rect.width, 0, 0);
                break;
            case "×":
                button_oper.anchoredPosition = new Vector3(0.5f * button_oper.rect.width, 0, 0);
                break;
            case "=":
                button_oper.anchoredPosition = new Vector3(1.5f * button_oper.rect.width, 0, 0);
                break;
            case "<-":
                button_oper.anchoredPosition = new Vector3(-1.5f * button_oper.rect.width, -button_oper.rect.height, 0);
                break;
            case "->":
                button_oper.anchoredPosition = new Vector3(-0.5f * button_oper.rect.width, -button_oper.rect.height, 0);
                break;
            case "DEL":
                button_oper.anchoredPosition = new Vector3(0.5f * button_oper.rect.width, -button_oper.rect.height, 0);
                break;
            case "C":
                button_oper.anchoredPosition = new Vector3(1.5f * button_oper.rect.width, -button_oper.rect.height, 0);
                break;
        }
    }

    public void set_exit_trans(RectTransform exit,  RectTransform main_canvas)
    {
        exit.anchorMin = new Vector2(0.5f, 0.5f);
        exit.anchorMax = new Vector2(0.5f, 0.5f);
        exit.pivot = new Vector2(0.5f, 0.5f);
        exit.sizeDelta = new Vector3(84 * r_x, 84 * r_y, 0);
        exit.anchoredPosition = new Vector3(-3.5f * card_width - exit.rect.width, 1.4f * card_height, 0);
        exit.localScale = new Vector3(1, 1, 1);
    }
}