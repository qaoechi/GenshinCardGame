using RTfSetter.Script;
using UnityEngine;

public class CanvasInteractive : CanvasCreate
{
    public CanvasInteractive(GameObject canvasPrefab, float emptyX, float emptyY) : base(canvasPrefab, emptyX, emptyY) { }
    protected override string GetCanvasName()
    {
        return "InteractiveCanvas";
    }
    public override void SetRectTransform(RectTransform rectTransform)
    {
        RectTransformSetter.SetRectTransform(
            rectTransform,
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(1, 1),
            Vector3.zero,
            Vector3.one);
    }
}
