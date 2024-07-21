using IUIElement.Script;
using UnityEngine;

//ĵ���� ���� �⺻ Ŭ����
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
    //ĵ���� ���� �� ����
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
