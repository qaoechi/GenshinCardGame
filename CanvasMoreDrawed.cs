using UnityEngine;
using IUIElement;
using RTfSetter.Script;

public class CanvasMoreDrawed : CanvasCreate
{
    private float Width, Height;

    public CanvasMoreDrawed(GameObject canvasPrefab, float x, float y, float cardWidth, float cardHeight) : base (canvasPrefab, x, y)
    {
        this.Width = cardWidth;
        this.Height = cardHeight;
    }
    protected override string GetCanvasName()
    {
        return "MoreCanvas";
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
            Vector3.one
                );
    }
}
