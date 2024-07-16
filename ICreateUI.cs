using UnityEngine;

namespace IUIElement.Script
{
    public interface ICreateUI
    {
        //UI를 parent 밑에 생성
        GameObject CreateUI(Transform parent);
        //UI의 rectTransform을 설정
        void SetRectTransform(RectTransform rectTransform);
    }
}
