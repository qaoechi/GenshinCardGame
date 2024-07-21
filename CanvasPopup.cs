using UnityEngine;
using RTfSetter.Script;

//묘지, 드로우한 카드를 보는 캔버스
public class CanvasPopup : CanvasCreate
{
    private float Width, Height;
    public CanvasPopup(GameObject canvasPrefab, float x, float y, float cardWidth, float cardHeight) : base (canvasPrefab, x, y)
    {
        this.Width = cardWidth;
        this.Height = cardHeight;
    }
    protected override string GetCanvasName()
    {
        return "PopupCanvas";
    }
    public override void SetRectTransform(RectTransform rectTransform)
    {
        RectTransformSetter.SetRectTransform(
            rectTransform,
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(Width, Height),
            new Vector3(x, y, 0),
            Vector3.one);
    }
}
