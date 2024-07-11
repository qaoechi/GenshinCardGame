
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public enum SpriteType
    {
        ����, ��û, ����, ���, �����, ������, �뿤, ����Ʈ, �ҷ�, ���̷�ũ, ������, ����, �����, ���̵�, ���̿�����,
        ������, ���϶�, ���ڸ���, ����, ����, ����, ��, ��ī, ����, �ٹٶ�, �����, ����, ����, ��Ƽ, �ϵ�, ���, ����,
        ���̳�, ������Ʈ, ����, ���佺, ��, ���긣��, �ñ���, �ó��, �ſ�, ����, �Ƹ���Ű��, �ƾ�ī, �ƾ���, �˺���,
        ������Ž, �߶�, ���Ϸ���, ����, ������, ����, ���, ���̹̾�, ���, ����, ����, ����, ����, �߿�, ��, ġ����, ġġ,
        ī��, ī����, ĵ��, ���̾�, ���ڹ�, �ݷ���, Ŭ��, Ŭ�θ���, Ű���, Ÿ��Ż����, Ÿ�̳���, �丶, �ķ���, Ǫ����,
        �����̳�, �ǽ�, �ѿ�, ����, �⸪, ������, ȣ��
    }

    public GameObject imagePrefab; // ���������� ����� �� �̹��� ���� ������Ʈ
    private Vector2 newScale = new Vector2(1, 1);

    public List<Sprite> sprites;
    private Dictionary<SpriteType, Sprite> spriteDict;

    private void Awake()
    {
        spriteDict = new Dictionary<SpriteType, Sprite>();
        SpriteType[] spriteTypes = (SpriteType[])System.Enum.GetValues(typeof(SpriteType));

        for (int i = 0; i < spriteTypes.Length; i++)
        {
            if (i < sprites.Count)
            {
                spriteDict[spriteTypes[i]] = sprites[i];
            }
        }
    }

    void Start()
    {
        SpriteType[] spriteTypes = (SpriteType[])System.Enum.GetValues(typeof(SpriteType));

        foreach (SpriteType type in spriteTypes)
        {
            Sprite newSprite = GetSprite(type);
            if (newSprite != null)
            {
                CreateAndSetupImage(newSprite, type);
            }
            else
            {
                Debug.LogError($"Sprite for {type} is not assigned.");
            }
        }
    }

    void CreateAndSetupImage(Sprite newSprite, SpriteType type)
    {
        // �� �̹��� ���� ������Ʈ�� �����ϰ� ����
        GameObject newImageObj = Instantiate(imagePrefab, transform);
        Image img = newImageObj.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("Image component is missing in the prefab.");
            return;
        }

        img.sprite = newSprite;
        img.name = type.ToString(); // ������ �̹��� ������Ʈ�� �̸� ����

        RectTransform rect = img.GetComponent<RectTransform>();
        if (rect != null)
        {
            rect.sizeDelta = new Vector2(100, 100);
            rect.localScale = new Vector3(newScale.x, newScale.y, 1);
        }
    }

    Sprite GetSprite(SpriteType type)
    {
        if (spriteDict.ContainsKey(type))
        {
            return spriteDict[type];
        }
        return null;
    }
}
