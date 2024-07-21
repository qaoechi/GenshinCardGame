using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    public List<int> characters = new List<int>
    {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
        11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
        21, 22, 23, 24, 25, 26, 27, 28, 29, 30
    };
    public Button one_button;
    public Button ten_button;
    public Text resultText;

    // Start is called before the first frame update
    void Start()
    {
                                                                            /*그냥 필드에 pulic Button button으로 선언한거는 아무것도 아님.
                                                                             필드에서 선언한 button에 scene에 미리 만들어 놓은 one_button을 연결, 선언해야함.
                                                                             그게 아래의 transform.Find로 객체를 찾아서 필드에 선언한 변수에 대입
                                                                             transform이랑 GameObject 이거 알아야하는데..... 나도 하면서 익힌거라 모르는거 있으면 물어봐*/
        one_button = transform.Find("one_button").GetComponent<Button>();
        ten_button = transform.Find("ten_button").GetComponent<Button>();

        //버튼 클릭 이벤트 등록이라는데 맞는지 모르겠음
        one_button.onClick.AddListener(SelectOneCharacter);
        ten_button.onClick.AddListener(SelectTenCharacters);
    }
    
    void SelectOneCharacter() //1뽑
    {
        int selectedCharacter = GetRandomCharacter();
        resultText.text = "Selected: " + selectedCharacter;
    }

    void SelectTenCharacters() //10뽑
    {
        List<int> selectedCharacters = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            selectedCharacters.Add(GetRandomCharacter());
        }

        resultText.text = "Selected: " + string.Join(", ", selectedCharacters);
    }
                                                                            /*InGame scene에 CreateMainBoard의 Inspector창을 보면 스크립트칸에 스크립트 필드 부분에서 선언한 변수가 있어
                                                                             거기 Sprites처럼 모든 카드를 저기에 저렇게 넣을거야. GenerateList.cs의 Sprite관련 부분을 참고
                                                                             그래서 밑에 5성, 4성, 나머지 확률 함수에서 추가로 어떤 캐릭터가 나오는지도 반환해줘야해.(배열 index)
                                                                             뭐 예를들어 5성 플블 캐릭터가 거의 80개지. 0~79가 5성캐릭터라면, 5성 확률이 당첨되면 거기서 누가 나오는지(0~79)를 반환해줘야해*/
    int GetRandomCharacter() //뽑기 확률
    {
        float rand = Random.value * 100;
        if (rand <= 0.5f)
        {
            /* 1, 2, 3, 4, 5가 나올 확률 0.1% * 5 = 0.5%
            이상하게 헷갈리는데 0.1f가 아니라 0.5f 맞나.. 밑에 것도*/
            //5성 확률
            int[] highChanceCharacters = { 1, 2, 3, 4, 5 };
            return highChanceCharacters[Random.Range(0, highChanceCharacters.Length)];
        }
        else if (rand <= 2.1f)
        {
            // 6, 7, 8, 9가 나올 확률 0.4% * 4 = 1.6%
            //4성 확률
            int[] mediumChanceCharacters = { 6, 7, 8, 9 };
            return mediumChanceCharacters[Random.Range(0, mediumChanceCharacters.Length)];
        }
        else
        {
            // 나머지 캐릭터들이 나올 확률 97.9%
            int[] lowChanceCharacters =
            {
                10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30
            };
            return lowChanceCharacters[Random.Range(0, lowChanceCharacters.Length)];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
