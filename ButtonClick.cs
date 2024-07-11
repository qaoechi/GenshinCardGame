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
}
