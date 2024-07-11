using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateList : MonoBehaviour
{
    public enum SpriteType
    {
        가명, 각청, 감우, 고로, 나비아, 나히다, 노엘, 느비예트, 닐루, 다이루크, 데히야, 도리, 디오나, 라이덴_쇼군, 라이오슬리,레이저, 레일라,
        로자리아, 리넷, 리니, 리사, 모나, 물츄츄, 미카, 바바라, 방랑자, 백출, 베넷, 벤티, 북두, 사유, 사이노, 산고노미야_코코미, 샤를로트, 설탕, 세토스,
        소, 슈브르즈, 시그윈, 시카노인_헤이조, 신염, 신학, 아라타키_이토, 아를레키노, 알베도, 알하이탐, 야란, 야에_미코, 에일로이, 엠버, 여행자,
        연비, 요요, 요이미야, 운근, 유라, 응광, 종려, 중운, 진, 치오리, 치치, 카미사토_아야카, 카미사토_아야토, 카베, 카에데하라_카즈하, 캔디스,
        케이야, 콜레이, 쿠죠_사라, 쿠키_시노부, 클레, 클로린드, 키라라, 타르탈리아, 타이나리, 토마, 파루잔, 푸리나, 프레미네, 피슬, 한운, 행추,
        향릉, 호두
    }

    public GameObject canvasPrefab;  //이 빈 프리팹을 반복문으로 생성
    public GameObject imagePrefab;  // 이 빈 프리팹을 반복문으로 생성하고 이미지를 넣음
    public List<Sprite> sprites;    // 스프라이트 리스트(내가 손수 넣은거)
    private Dictionary<SpriteType, Sprite> spriteDictionary;    //스프라이트 딕셔너리


    private void Awake()    //Start가 실행하기 전 실행
    {
        spriteDictionary = new Dictionary<SpriteType, Sprite>();    //딕셔너리 생성
        SpriteType[] spriteTypes = (SpriteType[])System.Enum.GetValues(typeof(SpriteType)); //enum의 값을 배열로 바꿈

        for (int i = 0; i < spriteTypes.Length; i++)
        {
            if (i < sprites.Count)
            {
                spriteDictionary[spriteTypes[i]] = sprites[i];  //딕셔너리 정의   이미 가나다순으로 enum을 만들어서 처음은 정렬 필요 없음
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SpriteType[] spriteTypes = (SpriteType[])System.Enum.GetValues(typeof(SpriteType)); //딕셔너리 배열로  리스트에는 있는거만 들어가고 이 배열은 정렬에 용이하도록 만들려고 했음
        int columns = (spriteDictionary.Count / 7) + 1;    //생성할 캔버스의 수   가방 생각해서 스프라이트 리스트가 아닌 딕셔너리 길이로 함
        //int index = 0;

        RectTransform parent_rect = GetComponent<RectTransform>();

        for (int i = 0; i < columns; i++)
        {
            GameObject canvas_col = Instantiate(canvasPrefab, transform);   //캔버스 생성
            canvas_col.name = "column" + i; //캔버스 이름
            RectTransform canvas_rect = canvas_col.GetComponent<RectTransform>();
            set_canvas(canvas_rect, parent_rect);
           
            HorizontalLayoutGroup horizontalLayoutGroup = canvas_col.AddComponent<HorizontalLayoutGroup>(); //캔버스요소들 수평 정렬
            horizontalLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
            horizontalLayoutGroup.spacing = 5;

            int startIndex = i * 7;
            int endIndex = Mathf.Min(startIndex + 7, sprites.Count);

            for (int j = startIndex; j < endIndex; j++)
            {
                generateImage(canvas_col.transform, sprites[j]);    //이미지 생성 메서드

            }
        }
    }

    private void generateImage(Transform transform, Sprite sprite) {
        GameObject image_row = Instantiate(imagePrefab, transform); //이미지 프리팹 생성
        image_row.name = sprite.ToString(); //이미지 객체 이름 설정
        RectTransform parent_rect = GetComponent<RectTransform>();
        Image image = image_row.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = sprite; // 이미지에 스프라이트 설정
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
        //rectTransform.anchoredPosition = Vector2.zero; // 이미지의 위치를 캔버스의 중앙으로 설정
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
