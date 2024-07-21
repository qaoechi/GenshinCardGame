using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using System;
using Assets.Script;


public class ButtonClick : MonoBehaviour
{
    public static TextMeshProUGUI tmpFormula; // 연산 식
    public static TextMeshProUGUI characterADHButton;
    public static char adh = 'a';
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
    public void CopyADH(TextMeshProUGUI text)   //공격력을 식에 입력
    {
        tmpFormula.text += text.text;
        Calculator.strFormula = tmpFormula.text;
    }
    public void CopyOps(TextMeshProUGUI text)
    {
        tmpFormula.text += text.text;
        Calculator.strFormula = tmpFormula.text;
    }
    public void EqualButton()
    {
        tmpFormula.text = Calculator.DeriveFormula();
    }
    public void Substitute()
    {
        if(characterADHButton == null)
        {
            Debug.Log("선택되지 않음");
            return;
        }
        try
        {
            int.Parse(tmpFormula.text);
            characterADHButton.text = tmpFormula.text;
            ResetFormula();

        }
        catch (FormatException e)
        {
            Debug.Log(e);
            ResetFormula();
        }
        catch(ArgumentNullException e)
        {
            Debug.Log(e);
        }
    }
    public void DeleteFormula()
    {
        Calculator.DeleteFormula();
    }
    public void ResetFormula()
    {
        Calculator.ResetFormula();
    }
    public void ChangeADH()
    {
        if (adh == 'a')
        {
            CreateGameBoard.stateADH.text = "Def";
            adh = 'd';
        }
        else if (adh == 'd')
        {
            CreateGameBoard.stateADH.text = "Hp";
            adh = 'h';
        }
        else if(adh == 'h')
        {
            CreateGameBoard.stateADH.text = "Atk";
            adh = 'a';
        }
    }
    /*public void Hp()
    {

    }*/
}
