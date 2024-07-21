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
                                                                            /*�׳� �ʵ忡 pulic Button button���� �����ѰŴ� �ƹ��͵� �ƴ�.
                                                                             �ʵ忡�� ������ button�� scene�� �̸� ����� ���� one_button�� ����, �����ؾ���.
                                                                             �װ� �Ʒ��� transform.Find�� ��ü�� ã�Ƽ� �ʵ忡 ������ ������ ����
                                                                             transform�̶� GameObject �̰� �˾ƾ��ϴµ�..... ���� �ϸ鼭 �����Ŷ� �𸣴°� ������ �����*/
        one_button = transform.Find("one_button").GetComponent<Button>();
        ten_button = transform.Find("ten_button").GetComponent<Button>();

        //��ư Ŭ�� �̺�Ʈ ����̶�µ� �´��� �𸣰���
        one_button.onClick.AddListener(SelectOneCharacter);
        ten_button.onClick.AddListener(SelectTenCharacters);
    }
    
    void SelectOneCharacter() //1��
    {
        int selectedCharacter = GetRandomCharacter();
        resultText.text = "Selected: " + selectedCharacter;
    }

    void SelectTenCharacters() //10��
    {
        List<int> selectedCharacters = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            selectedCharacters.Add(GetRandomCharacter());
        }

        resultText.text = "Selected: " + string.Join(", ", selectedCharacters);
    }
                                                                            /*InGame scene�� CreateMainBoard�� Inspectorâ�� ���� ��ũ��Ʈĭ�� ��ũ��Ʈ �ʵ� �κп��� ������ ������ �־�
                                                                             �ű� Spritesó�� ��� ī�带 ���⿡ ������ �����ž�. GenerateList.cs�� Sprite���� �κ��� ����
                                                                             �׷��� �ؿ� 5��, 4��, ������ Ȯ�� �Լ����� �߰��� � ĳ���Ͱ� ���������� ��ȯ�������.(�迭 index)
                                                                             �� ������� 5�� �ú� ĳ���Ͱ� ���� 80����. 0~79�� 5��ĳ���Ͷ��, 5�� Ȯ���� ��÷�Ǹ� �ű⼭ ���� ��������(0~79)�� ��ȯ�������*/
    int GetRandomCharacter() //�̱� Ȯ��
    {
        float rand = Random.value * 100;
        if (rand <= 0.5f)
        {
            /* 1, 2, 3, 4, 5�� ���� Ȯ�� 0.1% * 5 = 0.5%
            �̻��ϰ� �򰥸��µ� 0.1f�� �ƴ϶� 0.5f �³�.. �ؿ� �͵�*/
            //5�� Ȯ��
            int[] highChanceCharacters = { 1, 2, 3, 4, 5 };
            return highChanceCharacters[Random.Range(0, highChanceCharacters.Length)];
        }
        else if (rand <= 2.1f)
        {
            // 6, 7, 8, 9�� ���� Ȯ�� 0.4% * 4 = 1.6%
            //4�� Ȯ��
            int[] mediumChanceCharacters = { 6, 7, 8, 9 };
            return mediumChanceCharacters[Random.Range(0, mediumChanceCharacters.Length)];
        }
        else
        {
            // ������ ĳ���͵��� ���� Ȯ�� 97.9%
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
