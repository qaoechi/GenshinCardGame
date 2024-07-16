using UnityEngine;
using IUIElement;
using RTfSetter.Script;

public class CanvasCharacter : CanvasCreate
{
    private float cardWidth, canvasHeight;

    public CanvasCharacter(GameObject canvasPrefab, float x, float y, float cardWidth, float cardHeight) : base (canvasPrefab, x, y)
    {
        this.cardWidth = cardWidth;
        this.canvasHeight = cardHeight;
    }
    protected override string GetCanvasName()
    {
        return "CharacterCanvas";
    }
    public override void SetRectTransform(RectTransform rectTransform)
    {
        RectTransformSetter.SetRectTransform(
            rectTransform,
            new Vector2(0.25f, 1),
            new Vector2(0.25f, 1),
            new Vector2(0.25f, 1),
            new Vector2(cardWidth, canvasHeight - 2 * y),
            new Vector3(x + cardWidth / 2, 0, 0),
            Vector3.one
                );
    }
}
