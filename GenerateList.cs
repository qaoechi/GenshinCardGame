using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateList : MonoBehaviour
{
    public enum SpriteType
    {
        ����, ��û, ����, ���, �����, ������, �뿤, ����Ʈ, �ҷ�, ���̷�ũ, ������, ����, �����, ���̵�_�, ���̿�����,������, ���϶�,
        ���ڸ���, ����, ����, ����, ��, ������, ��ī, �ٹٶ�, �����, ����, ����, ��Ƽ, �ϵ�, ����, ���̳�, ����̾�_���ڹ�, ������Ʈ, ����, ���佺,
        ��, ���긣��, �ñ���, ��ī����_������, �ſ�, ����, �ƶ�ŸŰ_����, �Ƹ���Ű��, �˺���, ������Ž, �߶�, �߿�_����, ���Ϸ���, ����, ������,
        ����, ���, ���̹̾�, ���, ����, ����, ����, �߿�, ��, ġ����, ġġ, ī�̻���_�ƾ�ī, ī�̻���_�ƾ���, ī��, ī�����϶�_ī����, ĵ��,
        ���̾�, �ݷ���, ����_���, ��Ű_�ó��, Ŭ��, Ŭ�θ���, Ű���, Ÿ��Ż����, Ÿ�̳���, �丶, �ķ���, Ǫ����, �����̳�, �ǽ�, �ѿ�, ����,
        �⸪, ȣ��
    }

    public GameObject canvasPrefab;  //�� �� �������� �ݺ������� ����
    public GameObject imagePrefab;  // �� �� �������� �ݺ������� �����ϰ� �̹����� ����
    public List<Sprite> sprites;    // ��������Ʈ ����Ʈ(���� �ռ� ������)
    private Dictionary<SpriteType, Sprite> spriteDictionary;    //��������Ʈ ��ųʸ�


    private void Awake()    //Start�� �����ϱ� �� ����
    {
        spriteDictionary = new Dictionary<SpriteType, Sprite>();    //��ųʸ� ����
        SpriteType[] spriteTypes = (SpriteType[])System.Enum.GetValues(typeof(SpriteType)); //enum�� ���� �迭�� �ٲ�

        for (int i = 0; i < spriteTypes.Length; i++)
        {
            if (i < sprites.Count)
            {
                spriteDictionary[spriteTypes[i]] = sprites[i];  //��ųʸ� ����   �̹� �����ټ����� enum�� ���� ó���� ���� �ʿ� ����
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SpriteType[] spriteTypes = (SpriteType[])System.Enum.GetValues(typeof(SpriteType)); //��ųʸ� �迭��  ����Ʈ���� �ִ°Ÿ� ���� �� �迭�� ���Ŀ� �����ϵ��� ������� ����
        int columns = (spriteDictionary.Count / 7) + 1;    //������ ĵ������ ��   ���� �����ؼ� ��������Ʈ ����Ʈ�� �ƴ� ��ųʸ� ���̷� ��
        //int index = 0;

        RectTransform parent_rect = GetComponent<RectTransform>();

        for (int i = 0; i < columns; i++)
        {
            GameObject canvas_col = Instantiate(canvasPrefab, transform);   //ĵ���� ����
            canvas_col.name = "column" + i; //ĵ���� �̸�
            RectTransform canvas_rect = canvas_col.GetComponent<RectTransform>();
            set_canvas(canvas_rect, parent_rect);
           
            HorizontalLayoutGroup horizontalLayoutGroup = canvas_col.AddComponent<HorizontalLayoutGroup>(); //ĵ������ҵ� ���� ����
            horizontalLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
            horizontalLayoutGroup.spacing = 5;

            int startIndex = i * 7;
            int endIndex = Mathf.Min(startIndex + 7, sprites.Count);

            for (int j = startIndex; j < endIndex; j++)
            {
                generateImage(canvas_col.transform, sprites[j]);    //�̹��� ���� �޼���

            }
        }
    }

    private void generateImage(Transform transform, Sprite sprite) {
        GameObject image_row = Instantiate(imagePrefab, transform); //�̹��� ������ ����
        image_row.name = sprite.ToString(); //�̹��� ��ü �̸� ����
        RectTransform parent_rect = GetComponent<RectTransform>();
        Image image = image_row.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = sprite; // �̹����� ��������Ʈ ����
        }

        RectTransform img_rect = image.GetComponent<RectTransform>();
        set_img(img_rect, parent_rect);
    }

    private void set_canvas(RectTransform canvas_rect, RectTransform parent_rect)
    {
        //canvas_rect.anchoredPosition = Vector3.zero;
        canvas_rect.anchorMin = new Vector2(0, 0);
        canvas_rect.anchorMax = new Vector2(1, 1);
        canvas_rect.pivot = new Vector2(0.5f, 0.5f);
        canvas_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parent_rect.rect.width);
        canvas_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parent_rect.rect.width / 6);
        canvas_rect.sizeDelta = new Vector2(parent_rect.rect.width, 225);
        canvas_rect.localScale = new Vector3(1, 1, 1);
    }

    private void set_img(RectTransform img_rect, RectTransform parent_rect)
    {
        img_rect.sizeDelta = new Vector2(1, 1);
        /*rectTransform.sizeDelta = new Vector2(100, 100);
        rectTransform.localScale = new Vector3(1, 1, 1);*/
        img_rect.anchorMin = new Vector2(0, 0);
        img_rect.anchorMax = new Vector2(1, 1);
        img_rect.pivot = new Vector2(0.5f, 0.5f);
        //rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parent.rect.width/10);
        //rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parent.rect.width);
        img_rect.sizeDelta = new Vector2(20, parent_rect.rect.height);
        //rectTransform.anchoredPosition = Vector2.zero; // �̹����� ��ġ�� ĵ������ �߾����� ����
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
