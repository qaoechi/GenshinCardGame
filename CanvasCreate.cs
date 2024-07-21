using IUIElement.Script;
using UnityEngine;

//캔버스 생성 기본 클래스
public abstract class CanvasCreate : ICreateUI
{
    protected GameObject canvasPrefab;
    protected float x, y;

    public CanvasCreate(GameObject canvasPrefab, float x, float y)
    {
        this.canvasPrefab = canvasPrefab;
        this.x = x;
        this.y = y;
    }
    //캔버스 생성 및 설정
   public GameObject CreateUI(Transform parent)
   {
        GameObject canvas = Object.Instantiate(canvasPrefab, parent);
        canvas.name = GetCanvasName();
        SetRectTransform(canvas.GetComponent<RectTransform>());
        return canvas;
   }
    protected abstract string GetCanvasName();
    public abstract void SetRectTransform(RectTransform rectTransform);
}
