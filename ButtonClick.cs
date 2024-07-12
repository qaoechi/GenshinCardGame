using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;


public class ButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*public enum UiName
    {
        MainMenu, InGame, Storage, PictorialBook, Gacha, Social, Setting
    }*/
    public void MovingScene()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        switch (clickedObject.name)
        {
            case "button_game_start":
                SceneManager.LoadScene("InGame");
                break;
            case "button_pictorial_book":
                SceneManager.LoadScene("Pictorialbook");
                break;
            case "button_storage":
                SceneManager.LoadScene("Storage");
                break;
            case "button_gacha":
                SceneManager.LoadScene("Gacha");
                break;
            case "button_social":
                SceneManager.LoadScene("Social");
                break;
            case "button_main_menu":
                SceneManager.LoadScene("MainMenu");
                break;
            case "button_exit":
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
                break;
            case "button_setting":
                SceneManager.LoadScene("Setting");
                break;
            default:
                break;
        }
        Debug.Log(clickedObject);
    }



    public TextMeshProUGUI tmp_formula; // 연산 식
    private string[] string_formula = new string[10];  //연산 식 배열 두자리수를 한 번에 지우기 위해. 클래스의 멤버변수는 default 값으로 초기화됨
    private int pointer_formula;    //현재 가리키고 있는 자리
    public void Atk()   //무시하셈
    {
        int atk;
        atk = int.Parse(tmp_formula.text) + 1;
        if (atk > 20)
        {
            atk = 0;
        }
        tmp_formula.text = atk.ToString();

    }

    public void CopyAtk(TextMeshProUGUI text)   //공격력을 식에 입력
    {
        string_formula[pointer_formula++] = text.text.ToString();
        tmp_formula.text += text.text + " ";
        
        //무시
        string a = "";
        for (int i = 0; i < string_formula.Length; i++)
        {
            a += string_formula[i];
            
        }
        Debug.Log(a);
    }
    public void CopyOps(TextMeshProUGUI text)
    {
        string_formula[pointer_formula++] = text.text.ToString();
        tmp_formula.text += text.text + " ";

        //무시
        string a = "";
        for (int i = 0; i < string_formula.Length; i++)
        {
            a += string_formula[i];
        }
        Debug.Log(a);
    }
    public void EqualFormula()
    {
        //식을 가지고 계산
        if (IsValidFormula())
        {
            //계산하기
            Debug.Log("valid formula");
        }
        else
        {
            Debug.Log("invalid formula");
        }
    }

    public void DeleteFormula()
    {
        if(pointer_formula == 0)
        {
            return;
        }

        int i = 0;
        tmp_formula.text = string.Empty;
        string_formula[--pointer_formula] = "";
        while (string_formula[i] != "")
        {
            tmp_formula.text += string_formula[i].ToString();
            i++;
        }

        //무시
        string a = "";
        for (int j = 0; j < string_formula.Length; j++)
        {
            a += string_formula[j];
        }
        Debug.Log(a);
    }
    public void ResetFormula()
    {
        tmp_formula.text = string.Empty;
        for(int i = 0; i < string_formula.Length; i++)
        {
            string_formula[i] = string.Empty;
        }
        pointer_formula = 0;
    }
    public void Hp()
    {

    }

    private int[] num_list = new int[5];
    private char[] op_list = new char[5];
    private void ResetLists()
    {
        for (int i = 0;i < 5; i++)
        {
            num_list[i] = 0;
            op_list[i] = '~';
        }
    }
    public bool IsValidFormula()
    {
        ResetLists();
        if (string_formula[0] == string.Empty)
        {
            tmp_formula.text = string.Empty;
            return false;
        }
        for (int i = 0; i < string_formula.Length; i++)
        {
            if(string_formula[i] == string.Empty)
            {
                break;
            }

            switch (i % 2)
            {
                case 0:
                    if (FormulaToNum(string_formula[i]))
                    {
                        num_list[i / 2] = int.Parse(string_formula[i]);
                        break;
                    }
                    return false;
                case 1:
                    if (FormulaToOps(string_formula[i]))
                    {
                        op_list[i / 2 + 1] = char.Parse(string_formula[i]);
                        break;
                    }
                    return false;
            }
        }
        return true;
    }
    public bool FormulaToNum(string a)
    {
        try
        {
            int.Parse(a);
        }
        catch (FormatException)
        {
            return false;
        }
        return true;
    }
    public bool FormulaToOps(string a)
    {
        try
        {
            char.Parse(a);
        }
        catch (FormatException)
        {
            return false;
        }
        return true;
    }
}
