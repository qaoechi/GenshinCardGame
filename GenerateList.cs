
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public enum SpriteType
    {
        가명, 각청, 감우, 고로, 나비아, 나히다, 노엘, 느비예트, 닐루, 다이루크, 데히야, 도리, 디오나, 라이덴, 라이오슬리,
        레이저, 레일라, 로자리아, 리넷, 리니, 리사, 모나, 미카, 미코, 바바라, 방랑자, 백출, 베넷, 벤티, 북두, 사라, 사유,
        사이노, 샤를로트, 설탕, 세토스, 소, 슈브르즈, 시그윈, 시노부, 신염, 신학, 아를레키노, 아야카, 아야토, 알베도,
        알하이탐, 야란, 에일로이, 엠버, 여행자, 연비, 요요, 요이미야, 운근, 유라, 응광, 이토, 종려, 중운, 진, 치오리, 치치,
        카베, 카즈하, 캔디스, 케이야, 코코미, 콜레이, 클레, 클로린드, 키라라, 타르탈리아, 타이나리, 토마, 파루잔, 푸리나,
        프레미네, 피슬, 한운, 행추, 향릉, 헤이조, 호두
    }

    public GameObject imagePrefab; // 프리팹으로 사용할 빈 이미지 게임 오브젝트
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
        // 빈 이미지 게임 오브젝트를 생성하고 설정
        GameObject newImageObj = Instantiate(imagePrefab, transform);
        Image img = newImageObj.GetComponent<Image>();
        if (img == null)
        {
            Debug.LogError("Image component is missing in the prefab.");
            return;
        }

        img.sprite = newSprite;
        img.name = type.ToString(); // 생성된 이미지 오브젝트에 이름 지정

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
